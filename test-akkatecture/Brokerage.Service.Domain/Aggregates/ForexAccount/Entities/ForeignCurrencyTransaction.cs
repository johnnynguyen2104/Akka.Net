using Akkatecture.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Entities
{
    public class ForeignCurrencyTransaction : Entity<ForeignCurrencyTransactionId>
    {
        public string WalletId { get; }

        public ForexAccountId ForexAccountId { get; }

        public CurrencyPairType CurrencyPairType { get; }

        public Money Amount { get; }

        public DateTime DateCreated { get; }

        public PaymentProviderType PaymentProviderType { get; }

        public string PaymentProviderUserId { get; }

        public string PaymentProviderTransactionId { get; }

        public Money SettlementRate { get; }

        public Money SettlementAmount { get; }

        public Money SettlementFees { get; }

        public DateTime? SettlementDate { get; }

        public SettlementStatusType SettlementStatusType { get; }

        public CancellationReasonType? Reason { get; }

        public ForeignCurrencyTransaction(ForeignCurrencyTransactionId aggregateId, string walletId, ForexAccountId forexAccountId, CurrencyPairType currencyPairType, Money amount, DateTime dateCreated, PaymentProviderType paymentProviderType, string paymentProviderUserId, string paymentProviderTransactionId, Money settlementRate, Money settlementAmount, Money settlementFees, DateTime? settlementDate, SettlementStatusType settlementStatusType, CancellationReasonType? reason)
            :base(aggregateId)
        {
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
            ForexAccountId = forexAccountId ?? throw new ArgumentNullException(nameof(forexAccountId));
            CurrencyPairType = currencyPairType;
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            DateCreated = dateCreated;
            PaymentProviderType = paymentProviderType;
            PaymentProviderUserId = paymentProviderUserId ?? throw new ArgumentNullException(nameof(paymentProviderUserId));
            PaymentProviderTransactionId = paymentProviderTransactionId ?? throw new ArgumentNullException(nameof(paymentProviderTransactionId));
            SettlementRate = settlementRate ?? new Money(0);
            SettlementAmount = settlementAmount ?? new Money(0); ;
            SettlementFees = settlementFees ?? new Money(0); ;
            SettlementDate = settlementDate;
            SettlementStatusType = settlementStatusType;
            Reason = reason;
        }
    }
}
