using Akkatecture.Aggregates;
using Akkatecture.Sagas;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Events;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForeignCurrencyTransactionCreationSagaLocator : ISagaLocator<ForeignCurrencyTransactionCreationSagaId>
    {
        public const string LocatorPrefixId = "fc-transaction-creation";

        public ForeignCurrencyTransactionCreationSagaId LocateSaga(IDomainEvent domainEvent)
        {
            switch (domainEvent.GetAggregateEvent())
            {
                case ForeignCurrencyTransactionStartedEvent evt: return new ForeignCurrencyTransactionCreationSagaId($"{LocatorPrefixId}-{evt.Transaction.Id}");
                case EscrowPlacedEvent evt: return new ForeignCurrencyTransactionCreationSagaId($"{LocatorPrefixId}-{evt.ForeignTransactionId}");

                default: throw new ArgumentNullException(nameof(domainEvent));
            }
        }
    }
}
