using Akka.Actor;
using Akkatecture.Aggregates;
using Akkatecture.Extensions;
using Akkatecture.Sagas;
using Akkatecture.Sagas.AggregateSaga;
using Akkatecture.Specifications.Provided;
using Brokerage.Service.Domain.Aggregates.ForexAccount;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Events;
using Brokerage.Service.Domain.Aggregates.Revenue;
using Brokerage.Service.Domain.Aggregates.Revenue.Commands;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Aggregates.Wallet;
using Brokerage.Service.Domain.Aggregates.Wallet.Commands;
using Brokerage.Service.Domain.Aggregates.Wallet.Entities;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using Brokerage.Service.Domain.Enumerations;
using Brokerage.Service.Domain.Sagas.Events;
using System;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForeignCurrencyTransactionSettlementSaga : AggregateSaga<ForeignCurrencyTransactionSettlementSaga, ForeignCurrencyTransactionSettlementSagaId, ForeignCurrencyTransactionSettlementSagaState>,
        ISagaIsStartedBy<ForexAccount, ForexAccountId, ForeignCurrencyTransactionSettledEvent>
    {
        public IActorRef WalletAggregateManager { get; }

        private IActorRef RevenueAggregateManager { get; }

        public ForeignCurrencyTransactionSettlementSaga(IActorRef walletAggregateManager, IActorRef revenueAggregateManager)
        {
            WalletAggregateManager = walletAggregateManager ?? throw new ArgumentNullException(nameof(walletAggregateManager));
            RevenueAggregateManager = revenueAggregateManager ?? throw new ArgumentNullException(nameof(revenueAggregateManager));
        }

        public bool Handle(IDomainEvent<ForexAccount, ForexAccountId, ForeignCurrencyTransactionSettledEvent> domainEvent)
        {
            //Need to deposit money from ForexAccount => Wallet.
            var spec = new AggregateIsNewSpecification();

            if (spec.IsSatisfiedBy(this))
            {
                var type = domainEvent.AggregateEvent.IsWithdrawalTransaction ? RevenueTransactionType.Withdrawal : RevenueTransactionType.Deposit;
                var evt = new AddRevenueCommand(RevenueId.New, SourceType.Forex, type, domainEvent.AggregateEvent.SettlementFees);

                RevenueAggregateManager.Tell(evt);
            }

            return true;
        }
    }
}
