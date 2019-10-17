using Akkatecture.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Entities
{
    /// <summary>
    /// Base class for all Wallet transactions.
    /// NOTE: Still need to decide on how to perform Wallet to Wallet transactions, but likely this won't ever be allowed.
    /// </summary>
    public class Transaction : Entity<TransactionId>
    {
        public TransactionId TransactionId { get; }
        public WalletId WalletId { get; }
        public Money Amount { get; }
        public WalletTransactionType TransactionType { get; }
        public WalletTransactionSourceType TransactionSourceType { get; }
        public string ForeignTransactionId { get; } // This is the Forex TransactionId or Trade TransactionId

        public Transaction(TransactionId entityId, WalletId walletId, Money amount, WalletTransactionType transactionType, WalletTransactionSourceType transactionSourceType, string foreignTransactionId) : base(entityId)
        {
            TransactionId = entityId ?? throw new ArgumentNullException(nameof(entityId));
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            TransactionType = transactionType;
            TransactionSourceType = transactionSourceType;
            ForeignTransactionId = foreignTransactionId ?? throw new ArgumentNullException(nameof(foreignTransactionId));
        }
    }
}
