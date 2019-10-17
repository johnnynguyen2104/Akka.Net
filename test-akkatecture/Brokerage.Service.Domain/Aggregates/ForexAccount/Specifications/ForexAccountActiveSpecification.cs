using Akkatecture.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Specifications
{
    public class ForexAccountActiveSpecification : Specification<ForexAccountState>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(ForexAccountState aggregate)
        {
            if (!aggregate.IsActivated)
            {
                yield return $"The account has not been activated.";
            }
        }
    }
}
