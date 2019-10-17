using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Infrastructure.Entities
{
    public class ForeignCurrencyTransactions
    {
        public string TransactionId { get; set; }

        public string WalletId { get; set; }

        public string ForexAccountId { get; set; }

        public CurrencyPairType CurrencyPairType { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateCreated { get; set; }

        public PaymentProviderType PaymentProviderType { get; set; }

        public string PaymentProviderUserId { get; set; }

        public string PaymentProviderTransactionId { get; set; }

        public decimal SettlementRate { get; set; }

        public decimal SettlementAmount { get; set; }

        public decimal SettlementFees { get; set;}

        public DateTime? SettlementDate { get; set; }

        public SettlementStatusType SettlementStatusType { get; set; }

        public CancellationReasonType? Reason { get; set; }
    }
}
