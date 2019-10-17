using Akka.Actor;
using System;

namespace Brokerage.Service.Application.Tests.Actors
{
    public class MyQuartzReceiverActor : ReceiveActor
    {
        public MyQuartzReceiverActor()
        {
            Receive<string>(i =>
            {
                Console.WriteLine($"I've received a message {i} at {DateTime.UtcNow}");
            });
        }
    }
}
