using Akkatecture.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Revenue
{
    public class RevenueId : Identity<RevenueId>
    {
        public RevenueId(string value) : base(value)
        {
        }
    }
}
