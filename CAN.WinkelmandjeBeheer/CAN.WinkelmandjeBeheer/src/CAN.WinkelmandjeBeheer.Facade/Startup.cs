using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CAN.WinkelmandjeBeheer.Infrastructure.DAL;
using System;
using CAN.WinkelmandjeBeheer.Domain.Domain.Interfaces;
using CAN.WinkelmandjeBeheer.Infrastructure.Infrastructure.Repositories;
using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;
using CAN.WinkelmandjeBeheer.Domain.Domain.Services;
using CAN.WinkelmandjeBeheer.Domain.Interfaces;
using InfoSupport.WSA.Infrastructure;

namespace CAN.WinkelmandjeBeheer.Facade.Facade
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
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("dbconnectionstring")));
            services.AddScoped<IRepository<Winkelmandje, string>, WinkelmandjeRepository>();
            services.AddScoped<IEventPublisher, EventPublisher>(e => new EventPublisher(BusOptions.CreateFromEnvironment()));
            services.AddScoped<IWinkelmandjeService, WinkelmandjeService>();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Winkelmandje Service",
                    Description = "Winkelmandje Service voor het bijhouden van de winkelmandjes",
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
