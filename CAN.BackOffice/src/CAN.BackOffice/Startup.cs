using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CAN.BackOffice.Services;
using Serilog;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.BackOffice.Agents.BestellingsAgent.Agents;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CAN.Webwinkel.Infrastructure.EventListener;
using InfoSupport.WSA.Infrastructure;
using CAN.BackOffice.Swagger;
using Swashbuckle.Swagger.Model;
using CAN.BackOffice.Security;
using CAN.BackOffice.Agents.AuthenticatieAgents.Agents;

namespace CAN.BackOffice
{
    public class Startup
    {
        private BackofficeEventListener listener;

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


            services.AddMvc();

            services.AddScoped<IBestellingBeheerService, BestellingBeheerService>(b => new BestellingBeheerService() { BaseUri = new Uri("http://can-bestellingbeheer") });
            services.AddScoped<IAuthenticatieService, AuthenticatieService>(b => new AuthenticatieService() { BaseUri = new Uri("http://can-backofficeauthenticatie") });

            services.AddScoped<IRepository<Bestelling, long>, BestellingRepository>();
            services.AddScoped<IRepository<Klant, long>, KlantRepository>();

            services.AddScoped<IMagazijnService, MagazijnService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IFactuurService, FactuurService>();


            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Backoffice service",
                    Description = "Backoffice service",
                    TermsOfService = "None"
                });

                //      options.OperationFilter<SwaggerAuthorization>();

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Serilog"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            var secretKey = Configuration.GetValue<string>("SecretKey");
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
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = "/Login/LoginAction",
                AccessDeniedPath = "/Login/AccesDenied",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token",
                TicketDataFormat = new JwtCookie(SecurityAlgorithms.HmacSha256, tokenValidationParameters)
            });
            

            var applicationLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUi();
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
                    template: "{controller=Login}/{action=LoginAction}");
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
            var dbconnectionString = Environment.GetEnvironmentVariable("dbconnectionstring");
            var locker = new EventListenerLock();
            var replayQueue = Environment.GetEnvironmentVariable("ReplayServiceQueue");
           listener = new BackofficeEventListener(
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
            Log.Logger.Information("Continue startup");
        }

    }
}
