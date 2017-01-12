using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CAN.Klantbeheer.Infrastructure.DAL;
using CAN.Klantbeheer.Domain.Interfaces;
using CAN.Klantbeheer.Infrastructure.Repositories;
using CAN.Klantbeheer.Domain.Entities;
using InfoSupport.WSA.Infrastructure;
using CAN.Klantbeheer.Domain.Services;

namespace CAN.Klantbeheer.Facade
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(Configuration)
              .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSwaggerGen();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(@"Server=can_klantbeheer_db;Database=can_klantbeheerDb;UserID=sa,Password=admin"));
            services.AddScoped<IRepository<Klant, long>, KlantRepository>();
            services.AddScoped<IEventPublisher, EventPublisher>(config => new EventPublisher(null));
            services.AddScoped<IKlantService, KlantService>();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Klanten domain service",
                    Description = "Domain service",
                    TermsOfService = "None"
                });
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
