using Akkatecture.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Revenue.Entities
{
    public class Transaction: Entity<TransactionId>
    {
        public SourceType Source { get; }

        public RevenueTransactionType Type { get; }

        public Money Amount { get; }

        public DateTime TimeStamp { get; }

        public Transaction(TransactionId id, SourceType source, RevenueTransactionType type, Money amount, DateTime timeStamp)
            : base(id)
        {
            Source = source;
            Type = type;
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            TimeStamp = timeStamp;
        }
    }
}
