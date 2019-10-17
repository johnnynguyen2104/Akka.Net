using Brokerage.Service.Domain.Aggregates.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage
{
    public class CreateWalletRequest
    {
        public WalletId WalletId { get; set; }

        public CreateWalletRequest(WalletId walletId)
        {
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }
    }
}
