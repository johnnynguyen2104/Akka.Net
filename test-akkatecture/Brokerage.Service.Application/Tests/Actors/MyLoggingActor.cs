using Akka.Actor;
using Akka.Event;

namespace Brokerage.Service.Application.Tests.Actors
{
    public class MyLoggingActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        private readonly string _symbol;

        public enum MessageType // Using an enum means we can't send a payload with our message
        {
            DoSomething,
            DoSomethingElse,
        }

        public MyLoggingActor(string symbol)
        {
            _log.Info($"Created new MyUntypedActor with Symbol:{symbol}!");
            _symbol = symbol;

            Receive<MessageType>(i =>
            {
                switch (i)
                {
                    case MessageType.DoSomething: DoSomething(); break;
                    case MessageType.DoSomethingElse: DoSomethingElse(); break;
                }
            });
        }

        private void DoSomething()
        {
            _log.Info($"MyUntypedActor: DoSomething with Symbol:{_symbol}!");
        }

        private void DoSomethingElse()
        {
            _log.Info($"MyUntypedActor: DoSomethingElse with Symbol:{_symbol}!");
        }

        protected override void Unhandled(object message)
        {
            _log.Error($"MyUntypedActor: Received an unhandled message:{message}");
        }
    }
}
