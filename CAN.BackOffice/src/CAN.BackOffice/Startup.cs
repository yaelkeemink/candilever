using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CAN.BackOffice.Models;
using CAN.BackOffice.Services;
using Serilog;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.BackOffice.Domain.Interfaces;
using InfoSupport.WSA.Infrastructure;
using Microsoft.Extensions.Logging;
using CAN.Webwinkel.Infrastructure.EventListener;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Agents.BestellingsAgent.Agents;

namespace CAN.BackOffice
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.ConfigurationSection(Configuration.GetSection("Serilog"))
                .CreateLogger();

            StartEventListeners();
        }


        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            var connectionstring = Environment.GetEnvironmentVariable("dbconnectionstring");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionstring));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();
            
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IBestellingBeheerService, BestellingBeheerService>(b => new BestellingBeheerService() { BaseUri = new Uri("http://can-bestellingbeheer:80") });

            services.AddScoped<IRepository<Bestelling, long>, BestellingRepository>();
            services.AddScoped<IRepository<Klant, long>, KlantRepository>();

            services.AddScoped<IMagazijnService, MagazijnService>();
            services.AddScoped<IFactuurService, FactuurService>();
            services.AddScoped<ISalesService, SalesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Serilog"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Magazijn}/{action=BestellingOphalen}/{id?}");
            });
        }


        /// <summary>
        /// 
        /// </summary>
        private void StartEventListeners()
        {
            var dbconnectionString = Environment.GetEnvironmentVariable("dbconnectionstring");
            var locker = new EventListenerLock();
            var replayQueue = Environment.GetEnvironmentVariable("ReplayServiceQueue");
            var listener = new BackofficeEventListener(
                busOptions: BusOptions.CreateFromEnvironment(),
                dbConnectionString: dbconnectionString,
                logger: Log.Logger,
                replayEndPoint: replayQueue,
                locker: locker,
                replayAuditService: true
                );
            listener.Start();
            // wachten
            Log.Logger.Information("Waiting for release startup lock");
            locker.StartUpLock.WaitOne();
            Log.Logger.Information("Continuing startup");
        }

    }
}
