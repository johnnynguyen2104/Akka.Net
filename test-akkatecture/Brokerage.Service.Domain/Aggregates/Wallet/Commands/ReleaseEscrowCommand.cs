using Akkatecture.Commands;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Commands
{
    public class ReleaseEscrowCommand : Command<Wallet, WalletId>
    {
        public string TransactionId { get; }

        public string ForeignTransactionId { get; }

        public Money Amount { get; }

        public ReleaseEscrowCommand(WalletId walletId, Money amount, string transactionId, string foreignTransactionId) :base(walletId)
        {
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            ForeignTransactionId = foreignTransactionId ?? throw new ArgumentNullException(nameof(foreignTransactionId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }
    }
}
