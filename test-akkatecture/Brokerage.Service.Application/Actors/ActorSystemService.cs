using Akka.Actor;
using Brokerage.Service.Akka.Extensions;
using Brokerage.Service.Application.Actors.Brokerage;
using Brokerage.Service.Application.Actors.Brokerage;
using Brokerage.Service.Application.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors
{
    public class ActorSystemService : BackgroundService
    {
        private const string Name = "actor-system";

        private ActorSystem _actorSystem;
        public ActorSystem ActorSystem => _actorSystem;

        private IHubContext<UserHub, IUserHubClient> _userHub;

        public IActorRef UserCoordintorActorRef { get; }

        private IActorRef _brokerageServiceActor;
        public IActorRef GatewayServiceActor { get; }

        public ActorSystemService(IHubContext<UserHub, IUserHubClient> userHub, IServiceScopeFactory serviceScopeFactory)
        {
            _userHub = userHub;

            Log.Information($"Starting Actor System \"{Name}\"...");

            // Create ActorSystem Serilog Logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Information()
                .CreateLogger();

            // var configuration = ConfigurationFactory.ParseString(File.ReadAllText("akka.conf"));
            // _actorSystem = ActorSystem.Create("brokerage-system", configuration);

            _actorSystem = ActorSystem.Create(Name);
            _actorSystem.AddServiceScopeFactory(serviceScopeFactory);

            UserCoordintorActorRef = _actorSystem.ActorOf(Props.Create<UserCoordinatorActor>(userHub));

            _brokerageServiceActor = _actorSystem.ActorOf<BrokerageServiceActor>();
            GatewayServiceActor = _actorSystem.ActorOf(Props.Create(() => new GatewayServiceActor(_brokerageServiceActor)));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information($"Actor System \"{Name}\" is running...");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(3000, stoppingToken);

                Log.Information($"Actor System \"{Name}\" is PINGING UserCoordinatorActor...");

                UserCoordintorActorRef.Tell(new UserCoordinatorActor.TestPing());
            }

            await _actorSystem.WhenTerminated;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information($"Terminating Actor System \"{Name}\"...");

            await base.StopAsync(cancellationToken);

            await _actorSystem.Terminate();

            Log.Information($"Actor System \"{Name}\" has terminated successfully. Uptime was:{_actorSystem.Uptime}");
        }
    }
}
