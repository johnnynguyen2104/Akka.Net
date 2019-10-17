using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Events
{
    public class EscrowReleasedEvent : AggregateEvent<Wallet, WalletId>
    {
        public string ForeignTransactionId { get; }

        public Money Amount { get; }

        public EscrowReleasedEvent(string foreignTransactionId, Money amount)
        {
            ForeignTransactionId = foreignTransactionId ?? throw new ArgumentNullException(nameof(foreignTransactionId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }
    }
}
