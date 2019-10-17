using Akkatecture.Specifications;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Specifications
{
    public class MiniumAmountSpecification : Specification<BeginForeignCurrencyTransactionCommand>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(BeginForeignCurrencyTransactionCommand aggregate)
        {
            if (aggregate.Transaction.Amount.Value < 1)
            {
                yield return $"The transaction amount should be greater than 1 unit.";
            }
        }
    }
}
