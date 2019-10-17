using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Events
{
    public class ForexEscrowFundsReleasedEvent : AggregateEvent<ForexAccount, ForexAccountId>
    {
        public string ForexTransactionId { get; }

        public Money Amount { get; }

        public string WalletId { get; }

        public ForexEscrowFundsReleasedEvent(string transactionId, Money amount, string walletId)
        {
            ForexTransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }
    }
}
