using Akkatecture.Specifications;
using System.Collections.Generic;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Specifications
{
    public class EnoughFundsSpecification : Specification<WalletState>
    {
        private readonly decimal Amount;

        public EnoughFundsSpecification(decimal amount)
        {
            Amount = amount;
        }
        protected override IEnumerable<string> IsNotSatisfiedBecause(WalletState aggregate)
        {
            if (aggregate.Balance.Value < Amount)
            {
                yield return $"Balance is not enough.";
            }
        }
    }
}
