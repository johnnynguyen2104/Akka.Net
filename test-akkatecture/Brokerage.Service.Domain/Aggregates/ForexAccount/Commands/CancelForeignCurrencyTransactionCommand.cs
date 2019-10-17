using Akkatecture.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Commands
{
    public class CancelForeignCurrencyTransactionCommand : Command<ForexAccount, ForexAccountId>
    {
        public ForeignCurrencyTransactionId ForeignCurrencyTransactionId { get; }

        public CancellationReasonType Reason { get; }

        public bool ReturnEscrowBalance { get; }

        public CancelForeignCurrencyTransactionCommand(ForexAccountId aggregateId, ForeignCurrencyTransactionId foreignCurrencyTransactionId, CancellationReasonType reason, bool returnEscrowBalance = true)
            : base(aggregateId)
        {
            ForeignCurrencyTransactionId = foreignCurrencyTransactionId ?? throw new ArgumentNullException(nameof(foreignCurrencyTransactionId));
            Reason = reason;
            ReturnEscrowBalance = returnEscrowBalance;
        }
    }
}
