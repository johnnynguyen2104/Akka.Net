using Akkatecture.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Specifications
{
    public class ValidatePaymentProviderSpecification : Specification<ForexAccountState>
    {
        public string PaymentProviderTransactionId { get; }

        public string TransactionId { get; }

        public ValidatePaymentProviderSpecification(string paymentProviderTransactionId, string transactionId)
        {
            PaymentProviderTransactionId = paymentProviderTransactionId ?? throw new ArgumentNullException(nameof(paymentProviderTransactionId));
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
        }

        protected override IEnumerable<string> IsNotSatisfiedBecause(ForexAccountState aggregate)
        {
            if (aggregate.UnsettledTransactions != null && aggregate.UnsettledTransactions[TransactionId].PaymentProviderTransactionId != PaymentProviderTransactionId)
            {
                yield return $"The Settled PaymentProviderTransactionId from gateway is not matched with the transaction.";
            }
        }
    }
}
