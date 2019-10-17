using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage
{
    public class ActivateWalletRequest
    {
        public string WalletId { get; set; }

        public ActivateWalletRequest(string walletId)
        {
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }
    }
}
