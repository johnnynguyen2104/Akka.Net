using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Sagas.ForexAccountSaga;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.Events
{
    public class ForeignCurrencyTransactionCreationCompletedEvent : AggregateEvent<ForeignCurrencyTransactionCreationSaga, ForeignCurrencyTransactionCreationSagaId>
    {
    }
}
