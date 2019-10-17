using Akkatecture.Aggregates;
using Akkatecture.Sagas;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Events;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForeignCurrencyTransactionSettlementSagaLocator : ISagaLocator<ForeignCurrencyTransactionSettlementSagaId>
    {
        public const string LocatorPrefixId = "fc-transaction-settlement";

        public ForeignCurrencyTransactionSettlementSagaId LocateSaga(IDomainEvent domainEvent)
        {
            switch (domainEvent.GetAggregateEvent())
            {
                case ForeignCurrencyTransactionSettledEvent evt: return new ForeignCurrencyTransactionSettlementSagaId($"{LocatorPrefixId}-{evt.TransactionId}");

                default: throw new ArgumentNullException(nameof(domainEvent));
            }
        }
    }
}
