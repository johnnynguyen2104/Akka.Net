using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Subscribers.Commands
{
    public class CancelTransactionCommand
    {
        public ForeignCurrencyTransactionId TransactionId { get; }

        public CancellationReasonType Reason { get; }

        public CancelTransactionCommand(ForeignCurrencyTransactionId transactionId, CancellationReasonType reason)
        {
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            Reason = reason;
        }
    }
}
