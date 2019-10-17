using Brokerage.Service.Domain.Aggregates.ForexAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage
{
    public class OpenForexAccount
    {
        public ForexAccountId ForexAccountId { get; set; }

        public string WalletId { get; set; }

        public OpenForexAccount(ForexAccountId forexAccountId, string walletId)
        {
            ForexAccountId = forexAccountId ?? throw new ArgumentNullException(nameof(forexAccountId));
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }
    }
}
