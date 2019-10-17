using Akka.Actor;
using Brokerage.Service.Akka.Extensions;
using Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage;
using Brokerage.Service.Domain.Aggregates.ForexAccount;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Commands;
using Brokerage.Service.Domain.Aggregates.Wallet;
using Brokerage.Service.Domain.Sagas.ForexAccountSaga;
using Brokerage.Service.Domain.Subscribers;
using Brokerage.Service.Infrastructure;
using Brokerage.Service.Infrastructure.Repositories;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Brokerage.Service.Application.Actors.Brokerage
{
    public class ForexManagementActor: ReceiveActor
    {
        private IActorRef forexAccountManager;

        public ForexManagementActor()
        {
            var walletManager = Context.ActorOf<WalletManager>("wallet-manager");
            forexAccountManager = Context.ActorOf<ForexAccountManager>("forex-account-manager");
            Context.ActorOf(Props.Create(() => new ForeignCurrencyTransactionCreationSagaManager(() => new ForeignCurrencyTransactionCreationSaga(walletManager, forexAccountManager))), "forex-account-saga");
            var forexAccountRepository = Context.ActorOf<ForexAccountRepository>("forex-account-repository");
            Context.ActorOf(Props.Create<ForexAccountSubscriber>(forexAccountRepository), "forex-account-subscriber");

            Context.ActorOf(Props.Create(() => new ForexEscrowFundsReleaseSagaManager(() => new ForexEscrowFundsReleaseSaga(walletManager))), "forex-escrow-saga");
            //Context.ActorOf(Props.Create(() => new FeesDeductionSagaManager(() => new FeesDeductionSaga(revenueManager))), "fees-deduction-saga");
            //var forexAccountHandler = Context.ActorOf(Props.Create<ForexAccountQueryHandlers>(forexAccountRepository), "forex-account-handler");

            Receive<OpenNewForexAccountCommand>(command =>
            {
                forexAccountManager.Tell(command);
            });

            Receive<ActivateForexAccountCommand>(command =>
            {
                forexAccountManager.Tell(command);
            });

            Receive<BeginForeignCurrencyTransactionCommand>(command =>
            {
                forexAccountManager.Tell(command);
            });

            Receive<GetFCTransactionsRequest>(query =>
            {

                using (IServiceScope serviceScope = Context.CreateScope())
                {
                    var dbConnection = serviceScope.ServiceProvider.GetService<IDbConnection>();

                    var sqlQueryString = @"SELECT ""TransactionId"", ""WalletId"", ""ForexAccountId"", ""CurrencyPairType"", 
                                            ""Amount"", ""SettlementStatusType""
                                    FROM public.""ForeignCurrencyTransactions""
                                    WHERE ""WalletId""= @WalletId;";

                    var queryResult = dbConnection.Query<ForeignCurrencyTransactionMessage>(sqlQueryString, new { query.WalletId });

                    var result = new GetFCTransactionsResponse()
                    {
                        Transactions = queryResult
                    };

                    Sender.Tell(result);
                }
            });
        }
    }
}
