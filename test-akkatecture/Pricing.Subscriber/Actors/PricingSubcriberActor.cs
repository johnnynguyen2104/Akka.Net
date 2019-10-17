using Akka.Actor;
using Pricing.Shared;
using Pricing.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pricing.Subscriber.Actors
{
    public class PricingSubscriberActor: ReceiveActor
    {
        public PricingSubscriberActor()
        {
            Receive<NewPriceUpdateCommand>(command =>
            {
                Console.WriteLine($"Received new price from Pricing Service.");
                Console.WriteLine($"Id : {command.CommandId}, New Price : {command.NewPrice}");

                Console.WriteLine("-----------------------------");
            });

            Receive<SubscribeToBrokerCommand>(command =>
            {
                Context.ActorSelection($"{SeedNodeAddress.PricingSeedNodeAddress}/user/broker").Tell(command);
            });

            Receive<UnsubscribeFromBrokerCommand>(command =>
            {
                Context.ActorSelection($"{SeedNodeAddress.PricingSeedNodeAddress}/user/broker").Tell(command);
            });
        }
    }
}
