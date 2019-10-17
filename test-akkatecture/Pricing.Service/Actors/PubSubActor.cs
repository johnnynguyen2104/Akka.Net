using Akka.Actor;
using Pricing.Service.Commands;
using Pricing.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pricing.Service.Actors
{
    public class PubSubActor : ReceiveActor
    {
        protected HashSet<IActorRef> Subscribers = new HashSet<IActorRef>();

        public PubSubActor()
        {
            Receive<UpdateNewPriceCommand>(Handle);
            Receive<SubscribeToBrokerCommand>(Handle);
            Receive<UnsubscribeFromBrokerCommand>(Handle);
        }

        public bool Handle(UpdateNewPriceCommand command)
        {
            //Received new price, then tell all the subscribers.
            foreach (IActorRef subscriber in Subscribers)
            {
                subscriber.Tell(new NewPriceUpdateCommand(Guid.NewGuid().ToString(), command.NewPrice));
            }

            return true;
        }

        public bool Handle(SubscribeToBrokerCommand command)
        {
            //Recieved new subscriber.
            if (command.Subscriber != null)
            {
                Subscribers.Add(command.Subscriber);
            }

            return true;
        }

        public bool Handle(UnsubscribeFromBrokerCommand command)
        {
            //unsubcribe
            if (command.Subscriber != null)
            {
                Subscribers.Remove(command.Subscriber);
            }

            return true;
        }
    }
}
