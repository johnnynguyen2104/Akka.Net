using Akkatecture.Commands;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Aggregates.Wallet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Commands
{
    public class ActivateWalletCommand : Command<Wallet, WalletId>
    {
        public ActivateWalletCommand(WalletId aggregateId) : base(aggregateId) { }
    }
}
