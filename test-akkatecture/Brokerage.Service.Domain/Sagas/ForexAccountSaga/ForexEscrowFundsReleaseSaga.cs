using Akka.Actor;
using Akkatecture.Aggregates;
using Akkatecture.Extensions;
using Akkatecture.Sagas;
using Akkatecture.Sagas.AggregateSaga;
using Akkatecture.Specifications.Provided;
using Brokerage.Service.Domain.Aggregates.ForexAccount;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Events;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Aggregates.Wallet;
using Brokerage.Service.Domain.Aggregates.Wallet.Commands;
using Brokerage.Service.Domain.Aggregates.Wallet.Entities;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using Brokerage.Service.Domain.Sagas.Events;
using System;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForexEscrowFundsReleaseSaga : AggregateSaga<ForexEscrowFundsReleaseSaga, ForexEscrowFundsReleaseSagaId, ForexEscrowFundsReleaseSagaState>,
        ISagaIsStartedBy<ForexAccount, ForexAccountId, ForexEscrowFundsReleasedEvent>
    {
        public IActorRef WalletAggregateManager { get; }

        public ForexEscrowFundsReleaseSaga(IActorRef walletAggregateManager)
        {
            WalletAggregateManager = walletAggregateManager ?? throw new ArgumentNullException(nameof(walletAggregateManager));
        }

        public bool Handle(IDomainEvent<ForexAccount, ForexAccountId, ForexEscrowFundsReleasedEvent> domainEvent)
        {
            var spec = new AggregateIsNewSpecification();

            if (spec.IsSatisfiedBy(this))
            {
                var releaseEscrow = new ReleaseEscrowCommand(new WalletId(domainEvent.AggregateEvent.WalletId), domainEvent.AggregateEvent.Amount, TransactionId.New.Value, domainEvent.AggregateEvent.ForexTransactionId);
                WalletAggregateManager.Tell(releaseEscrow);
            }

            return true;
        }
    }
}
