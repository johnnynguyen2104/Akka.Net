using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Entities
{
    /// <summary>
    /// An escrow that has been returned to the wallet. Usually because the Forex or Trade transaction was cancelled or failed.
    /// </summary>
    public class ReleaseEscrowTransaction : Transaction
    {
        public ReleaseEscrowTransaction(TransactionId entityId, WalletId walletId, Money amount, WalletTransactionSourceType transactionSourceType, string foreignTransactionId)
            : base(entityId, walletId, amount, WalletTransactionType.Credit, transactionSourceType, foreignTransactionId)
        {
        }
    }
}
