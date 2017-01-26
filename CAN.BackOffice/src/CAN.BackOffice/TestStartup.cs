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
using Microsoft.IdentityModel.Tokens;
using CAN.BackOffice.Security;
using System.Text;

namespace CAN.BackOffice
{
    public class TestStartup
    {
        private BackofficeEventListener listener;

        public TestStartup(IHostingEnvironment env)
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

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer("Server =.\\SQLEXPRESS;Database=BackOfficeIntegration;Trusted_Connection=True;"));

            services.AddMvc();

            // Add application services.
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

            var applicationLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            var secretKey = "secretkeyisverysecureyoucannotguessthis!";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "http://cancandeliverbackofficeauthenticatie_can.candeliver.backofficeauthenticatie_1",
                ValidateAudience = true,
                ValidAudience = "http://cancandeliverbackofficeauthenticatie_can.candeliver.backofficeauthenticatie_1",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });
          

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

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Magazijn}/{action=BestellingOphalen}/{id?}");
            });
        }

        private void OnShutdown()
        {
            listener.Stop();
        }


        /// <summary>
        /// 
        /// </summary>
        private void StartEventListeners()
        {
            var dbconnectionString = "Server=.\\SQLEXPRESS;Database=BackOfficeIntegration;Trusted_Connection=True;";
            var locker = new EventListenerLock();
            var replayQueue = "ReplayServiceQueue";
            listener = new BackofficeEventListener(
                busOptions: new BusOptions()
                {
                    ExchangeName = "TestExchange",
                    QueueName = null,
                    HostName = "localhost",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest",
                },
                dbConnectionString: dbconnectionString,
                logger: Log.Logger,
                replayEndPoint: replayQueue,
                locker: locker,
                replayAuditService: false
            );
            listener.Start();
            // wachten
            Log.Logger.Information("Waiting for release startup lock");
            locker.StartUpLock.WaitOne();
            Log.Logger.Information("Continuing startup");
        }

    }
}