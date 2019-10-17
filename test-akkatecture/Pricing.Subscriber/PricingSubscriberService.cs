using Akka.Actor;
using Akka.Bootstrap.Docker;
using Akka.Configuration;
using Pricing.Shared.Commands;
using Pricing.Subscriber.Actors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pricing.Subscriber
{
    public class PricingSubcriberService
    {
        protected IActorRef PricingSubcriberActor;
        protected ActorSystem PricingSystem;

        public Task WhenTerminated => PricingSystem.WhenTerminated;


        public bool Start()
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("pricing.subscriber.hocon"));
            PricingSystem = ActorSystem.Create("pricingsystem", config.BootstrapFromDocker());

            PricingSubcriberActor = PricingSystem.ActorOf(Props.Create(() => new PricingSubscriberActor()));

            //Subscribe to Pricing Service.
            PricingSubcriberActor.Tell(new SubscribeToBrokerCommand(PricingSubcriberActor));

            return true;
        }

        public Task Stop()
        {
            //Unsubcribe from Pricing Service.
            PricingSubcriberActor.Tell(new UnsubscribeFromBrokerCommand(PricingSubcriberActor));
            return CoordinatedShutdown.Get(PricingSystem).Run();
        }
    }
}
