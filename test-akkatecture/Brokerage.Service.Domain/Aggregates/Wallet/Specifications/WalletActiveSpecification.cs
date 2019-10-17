using Akkatecture.Specifications;
using Brokerage.Service.Domain.Enumerations;
using System.Collections.Generic;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Specifications
{
    public class WalletActiveSpecification : Specification<WalletState>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(WalletState aggregate)
        {
            if (aggregate.Status != WalletStatusType.Active)
            {
                yield return $"The wallet has not been activated.";
            }
        }
    }
}
