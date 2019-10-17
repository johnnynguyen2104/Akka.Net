using Akka.Actor;
using Pricing.Service.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pricing.Service.Actors
{
    public class PriceUpdaterActor : ReceiveActor
    {
        public PriceUpdaterActor()
        {
            Receive<UpdateNewPriceCommand>(command =>
            {
                //Pass new price to broker.
                Console.WriteLine($"#PriceUpdaterActor - Received new price from a job at {DateTime.UtcNow}.");
                Context.ActorSelection("/user/broker").Tell(command);
            });
        }
    }
}
