using Akka.Actor;
using Akka.Bootstrap.Docker;
using Akka.Configuration;
using Pricing.Service.Actors;
using Pricing.Service.Commands;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pricing.Service
{
    public class PricingService
    {
        public IActorRef PubSubActor;
        public IActorRef PriceUpdaterActor;
        public ActorSystem PricingSystem;

        public Task WhenTerminated => PricingSystem.WhenTerminated;

        public bool Start()
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("pricing.service.hocon"));
            PricingSystem = ActorSystem.Create("pricingsystem", config.BootstrapFromDocker());

            PubSubActor = PricingSystem.ActorOf(Props.Create(() => new PubSubActor()), "broker");
            PriceUpdaterActor = PricingSystem.ActorOf(Props.Create(() => new PriceUpdaterActor()), "pricingupdater");

            return true;
        }

        public Task Stop()
        {
            return CoordinatedShutdown.Get(PricingSystem).Run();
        }
    }
}
