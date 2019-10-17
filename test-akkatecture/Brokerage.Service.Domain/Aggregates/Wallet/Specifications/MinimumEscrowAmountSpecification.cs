using Akkatecture.Specifications;
using Brokerage.Service.Domain.Aggregates.Wallet.Commands;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Specifications
{
    public class MinimumEscrowAmountSpecification : Specification<PlaceEscrowCommand>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(PlaceEscrowCommand aggregate)
        {
            if (aggregate.Amount.Value < 10)
            {
                yield return $"The minium of escrow amount is 10.";
            }
        }
    }
}
