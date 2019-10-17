using Akkatecture.Aggregates;
using Akkatecture.Sagas;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Sagas.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForeignCurrencyTransactionCreationSagaState :
        SagaState<ForeignCurrencyTransactionCreationSaga, ForeignCurrencyTransactionCreationSagaId, IMessageApplier<ForeignCurrencyTransactionCreationSaga, ForeignCurrencyTransactionCreationSagaId>>,
        IApply<ForeignCurrencyTransactionCreationStartedEvent>,
        IApply<ForeignCurrencyTransactionCreationCompletedEvent>
    {
        public ForeignCurrencyTransaction Transaction { get; set; }

        public void Apply(ForeignCurrencyTransactionCreationStartedEvent aggregateEvent)
        {
            Transaction = aggregateEvent.Transaction;
        }

        public void Apply(ForeignCurrencyTransactionCreationCompletedEvent aggregateEvent) { }
    }
}
