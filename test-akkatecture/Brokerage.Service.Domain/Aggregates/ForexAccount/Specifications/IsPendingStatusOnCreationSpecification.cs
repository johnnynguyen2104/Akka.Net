using Akkatecture.Specifications;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Commands;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Specifications
{
    public class IsPendingStatusOnCreationSpecification : Specification<BeginForeignCurrencyTransactionCommand>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(BeginForeignCurrencyTransactionCommand aggregate)
        {
            if (aggregate.Transaction.SettlementStatusType != SettlementStatusType.Pending)
            {
                yield return $"The Settlement Status Type of the transaction should be Pending.";
            }
        }
    }
}
