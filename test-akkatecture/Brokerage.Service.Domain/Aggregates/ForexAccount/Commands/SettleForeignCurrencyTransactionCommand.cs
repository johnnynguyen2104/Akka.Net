using Akkatecture.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Commands
{
    public class SettleForeignCurrencyTransactionCommand : Command<ForexAccount, ForexAccountId>
    {
        public ForeignCurrencyTransactionId ForeignCurrencyTransactionId { get; }

        public string PaymentProviderTransactionId { get; }

        public SettleForeignCurrencyTransactionCommand(ForexAccountId aggregateId, ForeignCurrencyTransactionId foreignCurrencyTransactionId, string paymentProviderTransactionId)
            : base(aggregateId)
        {
            ForeignCurrencyTransactionId = foreignCurrencyTransactionId ?? throw new ArgumentNullException(nameof(foreignCurrencyTransactionId));
            PaymentProviderTransactionId = paymentProviderTransactionId ?? throw new ArgumentNullException(nameof(paymentProviderTransactionId));
        }
    }
}
