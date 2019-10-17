using Akkatecture.Aggregates;
using Akkatecture.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Revenue
{
    public class RevenueManager: AggregateManager<Revenue, RevenueId, Command<Revenue, RevenueId>>
    {
    }
}
