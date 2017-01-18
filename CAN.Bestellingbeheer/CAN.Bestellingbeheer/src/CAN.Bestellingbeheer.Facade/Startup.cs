using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CAN.Bestellingbeheer.Infrastructure.DAL;
using CAN.Bestellingbeheer.Domain.Interfaces;
using CAN.Bestellingbeheer.Domain.Entities;
using CAN.Bestellingbeheer.Infrastructure.Repositories;
using InfoSupport.WSA.Infrastructure;
using CAN.Bestellingbeheer.Domain.Services;
using System;

namespace CAN.Bestellingbeheer.Facade.Facade
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
                .ReadFrom.ConfigurationSection(Configuration.GetSection("Serilog"))
                .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSwaggerGen();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("dbconnectionstring")));

            services.AddScoped<IRepository<Bestelling, long>, BestellingRepository>();
            services.AddScoped<IEventPublisher, EventPublisher>(e => new EventPublisher(BusOptions.CreateFromEnvironment()));
            services.AddScoped<IBestellingService, BestellingService>();
            services.AddScoped<BestellingService, BestellingService>();
            
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Bestellingbeheer Service",
                    Description = "Bestellingbeheer Service voor het bestellen van artikelen",
                    TermsOfService = "None"
                });
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Serilog"));
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
