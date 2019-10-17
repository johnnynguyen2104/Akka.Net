using Akka.Actor;
using Akkatecture.Aggregates;
using Akkatecture.Subscribers;
using Brokerage.Service.Domain.Aggregates.Wallet;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using Brokerage.Service.Domain.Repositories.Wallet.Commands;
using System;

namespace Brokerage.Service.Domain.Subscribers
{
    public class WalletSubscriber : DomainEventSubscriber,
        ISubscribeTo<Wallet, WalletId, WalletCreatedEvent>
        //ISubscribeTo<Wallet, WalletId, FundsDepositedEvent>,
        //ISubscribeTo<Wallet, WalletId, FundsWithdrawnEvent>
    {
        public IActorRef WalletRepository { get; }

        public WalletSubscriber(IActorRef walletRepository)
        {
               WalletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public bool Handle(IDomainEvent<Wallet, WalletId, WalletCreatedEvent> domainEvent)
        {
            var walletId = domainEvent.AggregateIdentity.Value;
            var openingBalance = domainEvent.AggregateEvent.OpeningBalance.Value;

            var command = new CreateWalletCommand(walletId, openingBalance);
            WalletRepository.Tell(command);

            return true;
        }

        //public bool Handle(IDomainEvent<Wallet, WalletId, FundsWithdrawnEvent> domainEvent)
        //{
        //    var transactionId = domainEvent.AggregateEvent.Transaction.Id.Value;
        //    var walletId = domainEvent.AggregateIdentity.Value;
        //    var senderWalletId = domainEvent.AggregateEvent.Transaction.SenderWalletId.Value;
        //    var amount = domainEvent.AggregateEvent.Transaction.Amount.Value;
        //    var command = new CreateDepositCommand(transactionId, walletId, senderWalletId, amount);

        //    WalletRepository.Tell(command);

        //    return true;
        //}

        //public bool Handle(IDomainEvent<Wallet, WalletId, FundsDepositedEvent> domainEvent)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
