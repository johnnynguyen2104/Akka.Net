using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.Revenue.Entities;
using Brokerage.Service.Domain.Aggregates.Revenue.Events;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Revenue
{
    public class RevenueState : AggregateState<Revenue, RevenueId>,
        IApply<RevenueAddedEvent>
    {
        public Transaction Transaction { get; set; }

        public void Apply(RevenueAddedEvent aggregateEvent)
        {
            Transaction = aggregateEvent.Transaction;
        }
    }
}
