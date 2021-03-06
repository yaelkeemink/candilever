﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CAN.Webwinkel.Data;
using CAN.Webwinkel.Models;
using Serilog;
using CAN.Webwinkel.Infrastructure.EventListener;
using InfoSupport.WSA.Infrastructure;
using CAN.Webwinkel.Domain.Interfaces;
using CAN.Webwinkel.Domain.Entities;
using CAN.Webwinkel.Infrastructure.DAL.Repositories;
using CAN.Webwinkel.Infrastructure.DAL;
using Swashbuckle.Swagger.Model;
using CAN.Webwinkel.Agents.KlantAgent;
using CAN.Webwinkel.Agents.WinkelwagenAgent;
using CAN.Webwinkel.Infrastructure.Services;

namespace CAN.Webwinkel
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


            StartEventListener();
        }


        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("dbconnectionstring")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Webwinkel",
                    Description = "API voor artikelen en categorieen",
                    TermsOfService = "None"
                });
            });
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //o => new OnderhoudsServiceAgent() { BaseUri = new Uri("http://lapiwe-onderhoudservice:80") }
            services.AddDbContext<WinkelDatabaseContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("dbconnectionstring")));
            services.AddScoped<IRepository<Categorie, int>, CategorieRepository>();
            services.AddScoped<IRepository<Artikel, int>, ArtikelRepository>();
            services.AddScoped<ICategorieService, CategorieService>();
            services.AddScoped<IArtikelService, ArtikelService>();
            services.AddScoped<IKlantAgent, KlantAgent>(s => new KlantAgent() { BaseUri = new Uri("http://can-klantbeheer:80") });
            services.AddScoped<IWinkelwagenAgentClient, WinkelwagenAgentClient>(s => new WinkelwagenAgentClient() { BaseUri = new Uri("http://can-winkelmandjebeheer:80") });
            services.AddScoped<IRepository<Winkelmandje, long>, WinkelmandjeRepository>();
            services.AddScoped<IWinkelwagenService, WinkelmandjeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Serilog"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseSwagger();
                app.UseSwaggerUi();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
        }


        /// <summary>
        /// 
        /// </summary>
        private void StartEventListener()
        {
            var log = new LoggerConfiguration().ReadFrom.Configuration(Configuration).MinimumLevel.Debug().CreateLogger();
            var dbconnectionString = Environment.GetEnvironmentVariable("dbconnectionstring");
            var locker = new EventListenerLock();
            var replayService = Environment.GetEnvironmentVariable("ReplayServiceQueue");
            var listener = new WinkelEventListener(BusOptions.CreateFromEnvironment(), dbconnectionString, log, replayService, locker);
            listener.Start();
            /// wachten
            log.Information("Waiting for release startup lock");
            locker.StartUpLock.WaitOne();
            log.Information("Continuing startup");
        }
    }
}
