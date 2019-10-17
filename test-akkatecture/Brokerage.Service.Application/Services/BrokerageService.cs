using Akka.Actor;
using Brokerage.Service.Application.Actors;
using Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage;
using Brokerage.Service.Domain.Aggregates.ForexAccount;
using Brokerage.Service.Domain.Aggregates.Wallet;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Services
{
    public class BrokerageService : IBrokerageService
    {
        private readonly IActorRef _gatewayServiceActor;

        public BrokerageService(ActorSystemService actorSystemService)
        {
            _gatewayServiceActor = actorSystemService.GatewayServiceActor;
        }

        public string CreateWallet()
        {
            //Test WalletId
            var walletId = new WalletId("wallet-344ccae9-a97c-4eed-8862-86f88d21650c");

            _gatewayServiceActor.Tell(new CreateWalletRequest(walletId));

            return walletId.Value;
        }

        public void ActivateWallet(string walletId)
        {
            _gatewayServiceActor.Tell(new ActivateWalletRequest(walletId));
        }

        public string OpenForexAccount(string walletId)
        {
            //Test ForexAccountId
            var forexAccountId = new ForexAccountId("forexaccount-fb702292-a46a-489e-aa14-c7c3ff5a4c3b");

            _gatewayServiceActor.Tell(new OpenForexAccount(forexAccountId, walletId));

            return forexAccountId.Value;
        }

        public void ActivateForexAccount(string forexAccountId)
        {
            _gatewayServiceActor.Tell(new ActivateForexAccount(forexAccountId));
        }

        public void CreateForeignCurrencyTransaction(string forexAccountId, string walletId)
        {
            _gatewayServiceActor.Tell(new AddForeignCurrencyTransaction(forexAccountId, walletId));
        }

        public async Task<GetFCTransactionsResponse> GetFCTransactions(string walletId)
        {
            var result = await _gatewayServiceActor.Ask<GetFCTransactionsResponse>(new GetFCTransactionsRequest(walletId));

            return result;
        }
    }
}
