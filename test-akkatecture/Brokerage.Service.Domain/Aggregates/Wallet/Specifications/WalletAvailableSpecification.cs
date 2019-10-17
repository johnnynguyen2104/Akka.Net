using Akkatecture.Specifications;
using System.Collections.Generic;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Specifications
{
    public class WalletAvailableSpecification : Specification<WalletState>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(WalletState aggregate)
        {
            if (aggregate.Balance == null)
            {
                yield return $"The wallet does not exist.";
            }
        }
    }
}
