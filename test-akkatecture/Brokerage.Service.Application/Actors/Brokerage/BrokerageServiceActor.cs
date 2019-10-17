using Akka.Actor;
using Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Commands;
using Brokerage.Service.Domain.Aggregates.Wallet.Commands;

namespace Brokerage.Service.Application.Actors.Brokerage
{
    public class BrokerageServiceActor : ReceiveActor
    {
        private readonly IActorRef forexManagementActor;

        private readonly IActorRef walletManagementActor;

        public BrokerageServiceActor()
        {
            walletManagementActor = Context.ActorOf<WalletManagementActor>();
            forexManagementActor = Context.ActorOf<ForexManagementActor>();

            Receive<CreateWalletCommand>(command =>
            {
                walletManagementActor.Tell(command);
            });

            Receive<ActivateWalletCommand>(command =>
            {
                walletManagementActor.Tell(command);
            });

            Receive<OpenNewForexAccountCommand>(command =>
            {
                forexManagementActor.Tell(command);
            });

            Receive<ActivateForexAccountCommand>(command =>
            {
                forexManagementActor.Tell(command);
            });

            Receive<BeginForeignCurrencyTransactionCommand>(command =>
            {
                forexManagementActor.Tell(command);
            });

            Receive<GetFCTransactionsRequest>(query =>
            {
                forexManagementActor.Forward(query);
            });
        }
    }
}
