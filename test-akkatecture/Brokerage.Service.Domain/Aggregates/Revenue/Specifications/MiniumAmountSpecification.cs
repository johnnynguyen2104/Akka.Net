using Akkatecture.Specifications;
using Brokerage.Service.Domain.Aggregates.Revenue.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Revenue.Specifications
{
    public class MiniumAmountSpecification : Specification<AddRevenueCommand>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(AddRevenueCommand aggregate)
        {
            if (aggregate.Amount.Value <= 0)
            {
                yield return $"Amount should be more than 1 unit.";
            }
        }
    }
}
