using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Gateway.Api.Actors;
using Services.Gateway.Api.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Gateway.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<ActorSystemService>();
            services.AddHostedService<BackgroundServiceStarter<ActorSystemService>>();
            services.AddScoped<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();
        }

        private class BackgroundServiceStarter<TService> : IHostedService
        {
            private readonly IHostedService _hostedService;

            public BackgroundServiceStarter(TService hostedService)
            {
                _hostedService = (IHostedService)hostedService;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                return _hostedService.StartAsync(cancellationToken);
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                return _hostedService.StopAsync(cancellationToken);
            }
        }
    }
}
