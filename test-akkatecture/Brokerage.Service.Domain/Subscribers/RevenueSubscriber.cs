using Akka.Actor;
using Akkatecture.Aggregates;
using Akkatecture.Subscribers;
using Brokerage.Service.Domain.Aggregates.Revenue;
using Brokerage.Service.Domain.Aggregates.Revenue.Events;
using Brokerage.Service.Domain.Repositories.Revenue.Commands;
using System;

namespace Brokerage.Service.Domain.Subscribers
{
    public class RevenueSubscriber : DomainEventSubscriber, 
        ISubscribeTo<Revenue, RevenueId, RevenueAddedEvent>
    {
        public IActorRef RevenueRepository { get; }

        public RevenueSubscriber(IActorRef revenueRepository)
        {
            RevenueRepository = revenueRepository ?? throw new ArgumentNullException(nameof(revenueRepository));
        }

        public bool Handle(IDomainEvent<Revenue, RevenueId, RevenueAddedEvent> domainEvent)
        {
            var command = new AddRevenueCommand(domainEvent.AggregateEvent.Transaction);
            RevenueRepository.Tell(command);

            return true;
        }
    }
}
