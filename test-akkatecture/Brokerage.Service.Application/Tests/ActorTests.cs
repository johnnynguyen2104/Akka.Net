using Akka.Actor;
using Akka.Event;
using Akka.Quartz.Actor;
using Akka.Quartz.Actor.Commands;
using Brokerage.Service.Application.Tests.Actors;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Aggregates.Wallet;
using Brokerage.Service.Domain.Aggregates.Wallet.Commands;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brokerage.Service.Application
{
    public static class ActorTests
    {
        private static async Task TestWalletActivation(ActorSystem actorSystem)
        {
            var walletManager = actorSystem.ActorOf<WalletManager>("wallet-manager");

            var walletId = WalletId.New;
            var createWalletCommand = new CreateWalletCommand(walletId, new Money(100.0m));
            walletManager.Tell(createWalletCommand);

            //var activateCommand = new ActivateWalletCommand(walletId);
            //walletManager.Tell(activateCommand);

            //var deposit = new Deposit(Domain.Aggregates.Wallet.Entities.TransactionId.New, walletId, new Money(10.0m));
            //var depositCommand = new DepositFundsCommand(walletId, deposit);
            //walletManager.Tell(depositCommand);
        }

        private static void CreateLoggingActor(ActorSystem actorSystem)
        {
            var myUntypedActor = actorSystem.ActorOf(Props.Create<MyLoggingActor>("HUBA-1000"));

            myUntypedActor.Tell(MyLoggingActor.MessageType.DoSomething);
            myUntypedActor.Tell(MyLoggingActor.MessageType.DoSomethingElse);
            myUntypedActor.Tell("Have a nice day!");
        }

        private static async Task CreateQuartzActor(ActorSystem actorSystem)
        {
            var myQuartzActor = actorSystem.ActorOf(Props.Create(() => new QuartzActor()), "QuartzActor");
            var myQuartzReceiverActor = actorSystem.ActorOf<MyQuartzReceiverActor>("QuartzReceiverActor");

            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(i =>
                {
                    i.WithIntervalInSeconds(1).RepeatForever();
                })
                .Build();

            var quartzJob = new CreateJob(myQuartzReceiverActor, "Hey dude!", trigger);

            await Task.Delay(TimeSpan.FromSeconds(2));

            myQuartzActor.Tell(quartzJob);

            await Task.Delay(TimeSpan.FromSeconds(30));
        }

        private static void CreateDeadLetterMonitor(ActorSystem actorSystem)
        {
            // var deadletterWatchMonitorProps = Props.Create(() => new DeadletterMonitor());
            var deadletterWatchActorRef = actorSystem.ActorOf<DeadletterMonitorActor>("DeadLetterMonitoringActor");

            actorSystem.EventStream.Subscribe(deadletterWatchActorRef, typeof(DeadLetter));
        }

        private static async Task TestPersistentActor(ActorSystem actorSystem)
        {
            var myActor = actorSystem.ActorOf<MyPersistentActor>("persistence-actor");

            await Task.Delay(TimeSpan.FromSeconds(1));

            for (int i = 0; i < 12; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                myActor.Tell($"hello-world at {DateTime.UtcNow}");

                Console.WriteLine($"Message {i} Sent");
            }

            var reply = await myActor.Ask<IReadOnlyList<string>>(new MyPersistentActor.GetMessages());

            Console.WriteLine($"Message Received: {string.Join(", ", reply)}");

            await Task.Delay(TimeSpan.FromSeconds(1));

            Console.WriteLine("Program Finished");
        }

        private static void TestWallet(ActorSystem actorSystem)
        {
            var walletManager = actorSystem.ActorOf<WalletManager>("wallet-manager");

            var walletId = WalletId.New;
            var createWalletCommand = new CreateWalletCommand(walletId, new Money(100.0m));
            walletManager.Tell(createWalletCommand);

            //var deposit = new Deposit(WalletTransactionId.New, walletId, new Money(10.0m));
            //var depositCommand = new DepositFundsCommand(walletId, deposit);
            //walletManager.Tell(depositCommand);

            //var withdrawal = new Withdrawal(WalletTransactionId.New, walletId, new Money(10.0m));
            //var withDrawCommand = new WithdrawFundsCommand(walletId, withdrawal);
            //walletManager.Tell(withDrawCommand); 
        }

        public static async Task TestForex(ActorSystem actorSystem)
        {
            //var walletManager = actorSystem.ActorOf<WalletManager>("wallet-manager");
            //var forexAccountManager = actorSystem.ActorOf<ForexAccountManager>("forex-account-manager");
            //var revenueManager = actorSystem.ActorOf<RevenueManager>("revenue-manager");
            //actorSystem.ActorOf(Props.Create(() => new ForeignCurrencyCreationSagaManager(() => new ForeignCurrencyCreationSaga(walletManager, forexAccountManager))), "forex-account-saga");
            ////var forexAccountHanlder = actorSystem.ActorOf(Props.Create<ForexAccountHandlers>(forexAccountRepository));
            //actorSystem.ActorOf(Props.Create<ForexAccountSubscriber>(forexAccountHanlder), "forex-account-subscriber");

            //actorSystem.ActorOf(Props.Create(() => new ForexEscrowFundsReleaseSagaManager(() => new ForexEscrowFundsReleaseSaga(walletManager))), "forex-escrow-saga");
            //actorSystem.ActorOf(Props.Create(() => new FeesDeductionSagaManager(() => new FeesDeductionSaga(revenueManager))), "fees-deduction-saga");

            //var revenueRepository = actorSystem.ActorOf<RevenueRepository>("revenue-repository");
            //var revenueSubscriber = actorSystem.ActorOf(Props.Create<RevenueSubscriber>(revenueRepository), "revenue-subscriber");

            //#region Wallet creation and activation
            //var walletId = WalletId.New;
            //var createWalletCommand = new CreateWalletCommand(walletId, new Money(100.0m));
            //walletManager.Tell(createWalletCommand);

            //var activateWalletCommand = new ActivateWalletCommand(walletId);
            //walletManager.Tell(activateWalletCommand);
            //#endregion

            //#region ForexAccount creation and activation
            //var forexAccountId = ForexAccountId.New;
            //var forexAccountCreationCommand = new OpenNewForexAccountCommand(forexAccountId, walletId.Value);
            //forexAccountManager.Tell(forexAccountCreationCommand);

            //var forexAccountActivationCommand = new ActivateForexAccountCommand(forexAccountId);
            //forexAccountManager.Tell(forexAccountActivationCommand);

            //#endregion


            //var transactionId = ForeignCurrencyTransactionId.New;
            //var forexTransactionCreationCommand = new BeginForeignCurrencyTransactionCommand(forexAccountId, new ForeignCurrencyTransaction(transactionId, walletId.Value, forexAccountId, CurrencyPairType.FRXTHB, new Money(15), DateTime.UtcNow, PaymentProviderType.KBank, "ProviderUserId", "KBANK12345678", null, null, null, null, SettlementStatusType.Pending, null));

            //forexAccountManager.Tell(forexTransactionCreationCommand);

            //await Task.Delay(TimeSpan.FromSeconds(10));

            ////var cancellationCommand = new CancelForeignCurrencyTransactionCommand(forexAccountId, transactionId, CancellationReasonType.Expired);
            ////forexAccountManager.Tell(cancellationCommand);

            //////Assuming, After recieved the approve from third-party
            //////We query the forex transaction details by the returned ForexTransactionId from third-party.
            //////Then we fill the details into the command below.
            //var txnsettled = new SettleForeignCurrencyTransactionCommand(forexAccountId, transactionId, "KBANK12345678");
            //forexAccountManager.Tell(txnsettled);

            //await Task.Delay(TimeSpan.FromSeconds(3));
            ////Get Transactions Detail
            //var transactionDetails = await forexAccountHanlder.Ask<List<TransactionDetailsProjection>>(new GetForeignCurrencyTransactionsQuery(walletId.Value));

            //foreach (var txn in transactionDetails)
            //{
            //    Console.WriteLine(txn.ToString());
            //}
        }
    }
}
