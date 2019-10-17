using Akkatecture.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Commands
{
    public class OpenNewForexAccountCommand : Command<ForexAccount, ForexAccountId>
    {
        public string WalletId { get; }

        public OpenNewForexAccountCommand(ForexAccountId aggregateId, string walletId)
            : base(aggregateId)
        {
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }
    }
}
