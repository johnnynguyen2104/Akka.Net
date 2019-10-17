using Akkatecture.Core;

namespace Brokerage.Service.Domain.Aggregates.Wallet
{
    public class WalletId : Identity<WalletId>
    {
        public WalletId(string value) : base(value) { }
    }
}
