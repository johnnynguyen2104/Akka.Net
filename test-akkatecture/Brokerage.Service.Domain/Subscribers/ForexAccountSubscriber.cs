using Akka.Actor;
using Akkatecture.Aggregates;
using Akkatecture.Subscribers;
using Brokerage.Service.Domain.Aggregates.ForexAccount;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Events;
using Brokerage.Service.Domain.Subscribers.Commands;
using System;

namespace Brokerage.Service.Domain.Subscribers
{
    public class ForexAccountSubscriber : DomainEventSubscriber,
        ISubscribeTo<ForexAccount, ForexAccountId, ForeignCurrencyTransactionCreatedEvent>,
        ISubscribeTo<ForexAccount, ForexAccountId, ForeignCurrencyTransactionCancelledEvent>,
        ISubscribeTo<ForexAccount, ForexAccountId, ForeignCurrencyTransactionSettledEvent>
    {
        public IActorRef ForexRepository { get; }

        public ForexAccountSubscriber(IActorRef forexRepository)
        {
            ForexRepository = forexRepository ?? throw new ArgumentNullException(nameof(forexRepository));
        }

        public bool Handle(IDomainEvent<ForexAccount, ForexAccountId, ForeignCurrencyTransactionCreatedEvent> domainEvent)
        {
            var addNewTransaction = new AddForeignCurrencyTransactionCommand(domainEvent.AggregateEvent.Transaction);

            ForexRepository.Tell(addNewTransaction);

            return true;
        }

        public bool Handle(IDomainEvent<ForexAccount, ForexAccountId, ForeignCurrencyTransactionCancelledEvent> domainEvent)
        {
            var addNewTransaction = new CancelTransactionCommand(domainEvent.AggregateEvent.TransactionId, domainEvent.AggregateEvent.Reason);

            ForexRepository.Tell(addNewTransaction);

            return true;
        }

        public bool Handle(IDomainEvent<ForexAccount, ForexAccountId, ForeignCurrencyTransactionSettledEvent> domainEvent)
        {
            var settlementCommand = new SettleTransactionCommand(domainEvent.AggregateEvent.TransactionId,
                                                                 domainEvent.AggregateEvent.SettlementRate,
                                                                 domainEvent.AggregateEvent.SettlementAmount,
                                                                 domainEvent.AggregateEvent.SettlementFees,
                                                                 DateTime.UtcNow);

            ForexRepository.Tell(settlementCommand);

            return true;
        }
    }
}
