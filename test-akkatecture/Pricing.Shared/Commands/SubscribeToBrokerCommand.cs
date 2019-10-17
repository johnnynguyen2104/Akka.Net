using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pricing.Shared.Commands
{
    public class SubscribeToBrokerCommand
    {
        public SubscribeToBrokerCommand(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }

        public IActorRef Subscriber { get; private set; }
    }
}
