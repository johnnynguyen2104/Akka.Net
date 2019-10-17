using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.Revenue.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Revenue.Events
{
    public class RevenueAddedEvent : AggregateEvent<Revenue, RevenueId>
    {
        public Transaction Transaction { get; }

        public RevenueAddedEvent(Transaction transaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}
