using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Extensions
{
    public static class ServiceCollectionUtils
    {
        public static IServiceCollection AddFractionAuthentication(this IServiceCollection services)
        {
            var builder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            // Stop the JWT bearer middleware converting the JWT claim types into the antiquated SOAP claim types
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            builder.AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LocalOnlySuperSecretFractionSecurityKey2019")),

                    ValidateIssuer = true,
                    ValidIssuer = "Fraction",

                    ValidateAudience = true,
                    ValidAudience = "Fraction",

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            return services;
        }

        public static IServiceCollection AddBackgroundService<TService>(this IServiceCollection services) where TService : class
        {
            services.AddSingleton<TService>();
            services.AddHostedService<BackgroundServiceStarter<TService>>();

            return services;
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
