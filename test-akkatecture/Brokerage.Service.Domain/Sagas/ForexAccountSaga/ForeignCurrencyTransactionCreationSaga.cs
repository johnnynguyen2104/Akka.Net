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
    public class ForeignCurrencyTransactionCreationSaga : AggregateSaga<ForeignCurrencyTransactionCreationSaga, ForeignCurrencyTransactionCreationSagaId, ForeignCurrencyTransactionCreationSagaState>,
        ISagaIsStartedBy<ForexAccount, ForexAccountId, ForeignCurrencyTransactionStartedEvent>,
        ISagaHandles<Wallet, WalletId, EscrowPlacedEvent>
    {
        public IActorRef WalletAggregateManager { get; }

        public IActorRef ForexAccountAggregateManager { get; }

        public ForeignCurrencyTransactionCreationSaga(IActorRef walletAggregateManager, IActorRef forexAggregateManager)
        {
            WalletAggregateManager = walletAggregateManager ?? throw new ArgumentNullException(nameof(walletAggregateManager));
            ForexAccountAggregateManager = forexAggregateManager ?? throw new ArgumentNullException(nameof(forexAggregateManager));
        }

        public bool Handle(IDomainEvent<ForexAccount, ForexAccountId, ForeignCurrencyTransactionStartedEvent> domainEvent)
        {
            var isNewSpec = new AggregateIsNewSpecification();
           
            if (isNewSpec.IsSatisfiedBy(this))
            {
                var walletStatusCommand = new PlaceEscrowCommand(new WalletId(domainEvent.AggregateEvent.Transaction.WalletId), domainEvent.AggregateEvent.Transaction.Amount, TransactionId.New.Value, domainEvent.AggregateEvent.Transaction.Id.Value);

                WalletAggregateManager.Tell(walletStatusCommand);

                Emit(new ForeignCurrencyTransactionCreationStartedEvent(domainEvent.AggregateEvent.Transaction));
            }

            return true;
        }

        public bool Handle(IDomainEvent<Wallet, WalletId, EscrowPlacedEvent> domainEvent)
        {
            var spec = new AggregateIsNewSpecification().Not();

            if (spec.IsSatisfiedBy(this))
            {
                var creatFCTransactionCommand = new CreateForeignCurrencyTransactionCommand(State.Transaction.ForexAccountId, State.Transaction);

                ForexAccountAggregateManager.Tell(creatFCTransactionCommand);

                Emit(new ForeignCurrencyTransactionCreationCompletedEvent());
            }

            return true;
        }
    }
}
