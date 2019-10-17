using Akkatecture.Aggregates;
using Akkatecture.Specifications;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Specifications
{
    public class TransactionExistSpecification : Specification<ForexAccountState>
    {
        public ForeignCurrencyTransactionId TransactionId { get; }

        public TransactionExistSpecification(ForeignCurrencyTransactionId transactionId)
        {
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
        }

        protected override IEnumerable<string> IsNotSatisfiedBecause(ForexAccountState aggregate)
        {
            if (aggregate.UnsettledTransactions == null
                || !aggregate.UnsettledTransactions.ContainsKey(TransactionId.Value))
            {
                yield return $"Transaction {TransactionId.Value} doesn't exist.";
            }
        }
    }
}
