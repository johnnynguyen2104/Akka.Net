using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Events
{
    public class ForeignCurrencyTransactionSettledEvent : AggregateEvent<ForexAccount, ForexAccountId>
    {
        public bool IsWithdrawalTransaction { get; }

        public ForeignCurrencyTransactionId TransactionId { get; }

        public Money SettlementRate { get; }

        public Money SettlementAmount { get; }

        public Money SettlementFees { get; }

        public string WalletId { get; }

        public ForeignCurrencyTransactionSettledEvent(ForeignCurrencyTransactionId transactionId, Money settlementRate, Money settlementAmount, Money settlementFees, string walletId, bool isWithdrawal)
        {
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            SettlementRate = settlementRate ?? throw new ArgumentNullException(nameof(settlementRate));
            SettlementAmount = settlementAmount ?? throw new ArgumentNullException(nameof(settlementAmount));
            SettlementFees = settlementFees ?? throw new ArgumentNullException(nameof(settlementFees));
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
            IsWithdrawalTransaction = isWithdrawal;
        }
    }
}
