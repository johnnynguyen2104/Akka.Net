using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage
{
    public class GetFCTransactionsRequest
    {
        public string WalletId { get; set; }

        public GetFCTransactionsRequest(string walletId)
        {
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }
    }
}
