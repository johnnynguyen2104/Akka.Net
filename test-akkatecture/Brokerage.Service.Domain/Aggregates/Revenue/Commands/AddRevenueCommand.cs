using Akkatecture.Commands;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Revenue.Commands
{
    public class AddRevenueCommand : Command<Revenue, RevenueId>
    {
        public SourceType Source { get; }

        public RevenueTransactionType Type { get; }

        public Money Amount { get; }

        public AddRevenueCommand(RevenueId aggregateId, SourceType source, RevenueTransactionType type, Money amount)
            : base(aggregateId)
        {
            Source = source;
            Type = type;
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }
    }
}
