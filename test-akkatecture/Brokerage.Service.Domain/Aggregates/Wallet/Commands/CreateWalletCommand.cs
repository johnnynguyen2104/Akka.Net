using Akkatecture.Commands;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Commands
{
    public class CreateWalletCommand : Command<Wallet, WalletId>
    {
        public Money OpeningBalance { get; }

        public CreateWalletCommand(WalletId aggregateId, Money openingBalance) : base(aggregateId)
        {
            OpeningBalance = openingBalance ?? throw new ArgumentNullException(nameof(openingBalance));
        }
    }
}
