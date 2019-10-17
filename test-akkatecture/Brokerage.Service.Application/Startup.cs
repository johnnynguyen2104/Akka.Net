using Akka.Actor;
using Brokerage.Service.Application.Actors;
using Brokerage.Service.Application.Extensions;
using Brokerage.Service.Application.Hubs;
using Brokerage.Service.Application.Services;
using Brokerage.Service.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Data;

namespace Brokerage.Service
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment environment, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _environment = environment;
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFractionAuthentication();

            string dbConnection = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BrokerageContext>(options => 
            {
                options.UseNpgsql(dbConnection);
            });

            services.AddScoped<IDbConnection>((serviceProvider) =>
            {
                return new NpgsqlConnection(dbConnection);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.KeepAliveInterval = TimeSpan.FromSeconds(5);
            });

            services.AddBackgroundService<ActorSystemService>();

            services.AddTransient<IBrokerageService, BrokerageService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BrokerageContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                context.Database.EnsureCreated();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSignalR(routes =>
            {
                routes.MapHub<UserHub>("/UserHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
