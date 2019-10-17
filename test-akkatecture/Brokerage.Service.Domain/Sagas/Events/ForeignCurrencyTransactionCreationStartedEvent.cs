using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Sagas.ForexAccountSaga;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.Events
{
    public class ForeignCurrencyTransactionCreationStartedEvent : AggregateEvent<ForeignCurrencyTransactionCreationSaga, ForeignCurrencyTransactionCreationSagaId>
    {
        public ForeignCurrencyTransaction Transaction { get; }

        public ForeignCurrencyTransactionCreationStartedEvent(ForeignCurrencyTransaction transaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}
