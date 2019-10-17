using Akkatecture.Aggregates;
using Akkatecture.Commands;

namespace Brokerage.Service.Domain.Aggregates.Wallet
{
    public class WalletManager : AggregateManager<Wallet, WalletId, Command<Wallet, WalletId>> { }
}
