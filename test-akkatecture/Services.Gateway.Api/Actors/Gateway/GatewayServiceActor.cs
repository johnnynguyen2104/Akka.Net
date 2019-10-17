using Akka.Actor;
using Akka.Event;

namespace Services.Gateway.Api.Actors.Gateway
{
    public class GatewayServiceActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        private readonly IActorRef _productServiceActor;

        public GatewayServiceActor(IActorRef productServiceActor)
        {
            _productServiceActor = productServiceActor;
        }
    }
}
