using Akka.Actor;
using Brokerage.Service.Domain.Repositories.Wallet.Commands;
using System.Collections.Generic;

namespace Brokerage.Service.Domain.Repositories.Wallet
{
    public class WalletRepository : ReceiveActor
    {
        private class WalletModel
        {
            public string WalletId { get; set; }
            public decimal Balance { get; set; }
            public List<WalletTransactionModel> Transactions { get; set; }
        }

        private class WalletTransactionModel
        {
            public string TransactionId { get; set; }
            public string WalletId { get; set; }
            public string SenderWalletId { get; set; }
            public decimal Amount { get; set; }
        }

        private Dictionary<string, WalletModel> _repository; // FAKE DATABASE

        public WalletRepository(/* We would inject the IDbConnection or DbContext here */)
        {
            _repository = new Dictionary<string, WalletModel>();

            Receive<CreateWalletCommand>(Handle);
            Receive<CreateDepositCommand>(Handle);
        }

        private bool Handle(CreateWalletCommand command)
        {
            var walletModel = new WalletModel
            {
                WalletId = command.WalletId,
                Balance = command.OpeningBalance,
                Transactions = new List<WalletTransactionModel>(),
            };

            _repository.Add(walletModel.WalletId, walletModel);

            return true;
        }

        private bool Handle(CreateDepositCommand command)
        {
            if (_repository.TryGetValue(command.WalletId, out var walletModel))
            {
                var walletTransactionModel = new WalletTransactionModel
                {
                    TransactionId = command.TransactionId,
                    WalletId = command.WalletId,
                    SenderWalletId = command.SenderWalletId,
                    Amount = command.Amount,
                };

                walletModel.Transactions.Add(walletTransactionModel);
            }

            return true;
        }
    }
}
