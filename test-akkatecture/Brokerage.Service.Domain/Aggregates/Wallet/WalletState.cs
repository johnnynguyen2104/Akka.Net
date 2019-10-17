using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using Brokerage.Service.Domain.Enumerations;

namespace Brokerage.Service.Domain.Aggregates.Wallet
{
    public class WalletState : AggregateState<Wallet, WalletId>, 
        IApply<WalletCreatedEvent>,
        IApply<WalletActivatedEvent>,
        IApply<EscrowPlacedEvent>,
        IApply<EscrowReleasedEvent>
    {
        public Money Balance { get; private set; }

        public WalletStatusType Status { get; private set; }

        public void Apply(WalletCreatedEvent aggregateEvent)
        {
            Status = WalletStatusType.PendingActivation;
            Balance = aggregateEvent.OpeningBalance;
        }

        public void Apply(WalletActivatedEvent aggregateEvent)
        {
            Status = WalletStatusType.Active;
        }

        public void Apply(EscrowPlacedEvent aggregateEvent)
        {
            Balance -= aggregateEvent.Amount;
        }

        public void Apply(EscrowReleasedEvent aggregateEvent)
        {
            Balance += aggregateEvent.Amount;
        }
    }
}
