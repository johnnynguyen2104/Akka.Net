using Akka.Actor;
using Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage;
using Brokerage.Service.Domain.Aggregates.ForexAccount;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Aggregates.Wallet;
using Brokerage.Service.Domain.Aggregates.Wallet.Commands;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace Brokerage.Service.Application.Actors.Brokerage
{
    public class GatewayServiceActor : ReceiveActor
    {
        private readonly IActorRef _brokerageServiceActor;

        public GatewayServiceActor(IActorRef brokerageServiceActor)
        {
            _brokerageServiceActor = brokerageServiceActor;

            Receive<CreateWalletRequest>(m => {

                var createWalletCommand = new CreateWalletCommand(m.WalletId, new Money(100.0m));
                _brokerageServiceActor.Tell(createWalletCommand);
            });

            Receive<ActivateWalletRequest>(m => {

                var activateWalletCommand = new ActivateWalletCommand(new WalletId(m.WalletId));
                _brokerageServiceActor.Tell(activateWalletCommand);
            });

            Receive<OpenForexAccount>(m => {

                _brokerageServiceActor.Tell(new OpenNewForexAccountCommand(m.ForexAccountId, m.WalletId));
            });

            Receive<ActivateForexAccount>(m => {

                _brokerageServiceActor.Tell(new ActivateForexAccountCommand(new ForexAccountId(m.ForexAccountId)));
            });

            Receive<AddForeignCurrencyTransaction>(m => {

                var transactionId = ForeignCurrencyTransactionId.New;
                var forexAccountId = new ForexAccountId(m.ForexAccountId);
                var forexTransactionCreationCommand = new BeginForeignCurrencyTransactionCommand(forexAccountId, new ForeignCurrencyTransaction(transactionId, m.WalletId, forexAccountId, CurrencyPairType.FRXTHB, new Money(15), DateTime.UtcNow, PaymentProviderType.KBank, "ProviderUserId", "KBANK12345678", null, null, null, null, SettlementStatusType.Pending, null));

                _brokerageServiceActor.Tell(forexTransactionCreationCommand);
            });

            Receive<GetFCTransactionsRequest>(m =>
            {

                //using forward instead of Tell because we have a nested actors inside 
                //and the deepest actor's Sender will this Actor.
                _brokerageServiceActor.Forward(m);
            });
        }
    }
}
