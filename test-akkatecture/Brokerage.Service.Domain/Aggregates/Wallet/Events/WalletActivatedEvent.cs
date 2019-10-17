using Akkatecture.Aggregates;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Events
{
    public class WalletActivatedEvent : AggregateEvent<Wallet, WalletId>
    {
        public WalletActivatedEvent() { }
    }
}
