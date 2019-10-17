using Akka.Actor;
using Akka.Event;
using System;

namespace Brokerage.Service.Application.Tests.Actors
{
    public class DeadletterMonitorActor : ReceiveActor
    {
        public DeadletterMonitorActor()
        {
            Receive<DeadLetter>(dl => HandleDeadletter(dl));
        }

        private void HandleDeadletter(DeadLetter dl)
        {
            Console.WriteLine($"DeadLetter captured!\nMessage:{dl.Message}\nSender:{dl.Sender}\nRecipient:{dl.Recipient}");
        }
    }
}
