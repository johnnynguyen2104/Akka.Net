using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Entities
{
    /// <summary>
    /// Place funds from the Wallet into escrow in the users Forex or Trade account. Usually for a withdrawal.
    /// </summary>
    public class PlaceEscrowTransaction :Transaction
    {
        public PlaceEscrowTransaction(TransactionId entityId, WalletId walletId, Money amount, WalletTransactionSourceType transactionSourceType, string foreignTransactionId) 
            : base(entityId, walletId, amount, WalletTransactionType.Debit, transactionSourceType, foreignTransactionId)
        {
        }
    }
}
