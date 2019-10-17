using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage
{
    public class AddForeignCurrencyTransaction
    {
        public string ForexAccountId { get; set; }

        public string WalletId { get; set; }

        public AddForeignCurrencyTransaction(string forexAccountId, string walletId)
        {
            ForexAccountId = forexAccountId ?? throw new ArgumentNullException(nameof(forexAccountId));
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }
    }
}
