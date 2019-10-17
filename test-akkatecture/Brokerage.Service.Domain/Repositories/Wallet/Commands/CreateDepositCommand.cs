namespace Brokerage.Service.Domain.Repositories.Wallet.Commands
{
    public class CreateDepositCommand
    {
        public string TransactionId { get; }

        public string WalletId { get; }

        public string SenderWalletId { get; }

        public decimal Amount { get; }

        public CreateDepositCommand(string transactionId, string walletId, string senderWalletId, decimal amount)
        {
            TransactionId = transactionId;
            WalletId = walletId;
            SenderWalletId = senderWalletId;
            Amount = amount;
        }
    }
}
