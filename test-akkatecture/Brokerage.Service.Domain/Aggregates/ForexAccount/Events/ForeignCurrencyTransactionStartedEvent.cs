using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Events
{
    public class ForeignCurrencyTransactionStartedEvent: AggregateEvent<ForexAccount, ForexAccountId>
    {
        public ForeignCurrencyTransaction Transaction { get; }

        public ForeignCurrencyTransactionStartedEvent(ForeignCurrencyTransaction transaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}
