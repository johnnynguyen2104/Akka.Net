using Akka.Actor;
using Akka.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Services.Gateway.Api.Actors.Gateway;
using Services.Gateway.Api.Actors.Product;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Gateway.Api.Actors
{
    public sealed class ActorSystemService : BackgroundService
    {
        private readonly ActorSystem _actorSystem;

        private string actorSystemName = string.Empty;

        public IActorRef GatewayServiceActor { get; }

        public IActorRef ProductQueryActor { get; }

        public ActorSystemService(IServiceProvider services, IConfiguration configuration)
        {
            actorSystemName = configuration["ActorSystem"];

            var config = ConfigurationFactory.ParseString(File.ReadAllText("service.gateway.hocon"));
            _actorSystem = ActorSystem.Create(actorSystemName, config);

            ProductQueryActor = _actorSystem.ActorOf(Props.Create(() => new ProductQueryActor(configuration["ProductServiceNodeAddress"], services, _actorSystem)));
            
            // Create the Gateway last, since it depends on all the other Services
            GatewayServiceActor = _actorSystem.ActorOf(Props.Create(() => new GatewayServiceActor(ProductQueryActor)));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information($"Actor System \"{actorSystemName}\" is running...");

            await _actorSystem.WhenTerminated;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information($"Terminating Actor System \"{actorSystemName}\"...");

            await base.StopAsync(cancellationToken);

            await _actorSystem.Terminate();

            Log.Information($"Actor System \"{actorSystemName}\" has terminated successfully. Uptime was:{_actorSystem.Uptime}");
        }
    }
}
