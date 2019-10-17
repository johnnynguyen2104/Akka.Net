using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Subscribers.Commands
{
    public class SettleTransactionCommand
    {
        public ForeignCurrencyTransactionId TransactionId { get; }

        public Money SettlementRate { get; }

        public Money SettlementAmount { get; }

        public Money SettlementFees { get; }

        public DateTime? SettlementDate { get; }

        public SettleTransactionCommand(ForeignCurrencyTransactionId transactionId, Money settlementRate, Money settlementAmount, Money settlementFees, DateTime? settlementDate)
        {
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            SettlementRate = settlementRate ?? throw new ArgumentNullException(nameof(settlementRate));
            SettlementAmount = settlementAmount ?? throw new ArgumentNullException(nameof(settlementAmount));
            SettlementFees = settlementFees ?? throw new ArgumentNullException(nameof(settlementFees));
            SettlementDate = settlementDate ?? throw new ArgumentNullException(nameof(settlementDate));
        }
    }
}
