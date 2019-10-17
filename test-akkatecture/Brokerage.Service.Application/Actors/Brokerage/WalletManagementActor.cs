using Akka.Actor;
using Brokerage.Service.Domain.Aggregates.Wallet;
using Brokerage.Service.Domain.Aggregates.Wallet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors.Brokerage
{
    public class WalletManagementActor : ReceiveActor
    {
        private IActorRef walletManager;
        public IActorRef WalletManager => walletManager;

        public WalletManagementActor()
        {
            walletManager = Context.ActorOf<WalletManager>();

            Receive<CreateWalletCommand>(command => {

                WalletManager.Tell(command);
            });

            Receive<ActivateWalletCommand>(command => {

                WalletManager.Tell(command);
            });
        }
    }
}
