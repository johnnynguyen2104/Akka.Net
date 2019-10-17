using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Events
{
    public class ForeignCurrencyTransactionCancelledEvent : AggregateEvent<ForexAccount, ForexAccountId>
    {
        public ForeignCurrencyTransactionId TransactionId { get; }

        public CancellationReasonType Reason { get; }

        public ForeignCurrencyTransactionCancelledEvent(ForeignCurrencyTransactionId transactionId, CancellationReasonType reason)
        {
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            Reason = reason;
        }
    }
}
