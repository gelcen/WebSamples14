﻿using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using GrapeCity.ActiveReports.Aspnetcore.Viewer;

namespace JSViewer_MVCCore
{
    public class Startup
    {
        public static string EmbeddedReportsPrefix = "JSViewer_MVC(Core).Reports";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(config =>
                {
                    // Disable the default logging configuration
                    config.ClearProviders();

                    // Enable logging for debug mode only
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                    {
                        config.AddConsole();
                    }
                })
                .AddReporting()
                .AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseReporting(settings =>
            {
                settings.UseEmbeddedTemplates(EmbeddedReportsPrefix, Assembly.GetAssembly(GetType()));
                settings.UseCompression = true;
            });

            app.UseMvc();
        }
    }
}