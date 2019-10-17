namespace Brokerage.Service.Domain.Repositories.Wallet.Commands
{
    public class CreateWalletCommand
    {
        public string WalletId { get; }

        public decimal OpeningBalance { get; }

        public CreateWalletCommand(string walletId, decimal openingBalance)
        {
            WalletId = walletId;
            OpeningBalance = openingBalance;
        }
    }
}
