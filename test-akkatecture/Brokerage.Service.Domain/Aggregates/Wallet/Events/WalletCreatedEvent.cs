using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Events
{
    public class WalletCreatedEvent : AggregateEvent<Wallet, WalletId>
    {
        public Money OpeningBalance { get; }

        public WalletCreatedEvent(Money openingBalance)
        {
            OpeningBalance = openingBalance ?? throw new ArgumentNullException(nameof(openingBalance));
        }
    }
}
