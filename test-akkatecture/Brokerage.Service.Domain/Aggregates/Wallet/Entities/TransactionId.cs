using Akkatecture.Core;

namespace Brokerage.Service.Domain.Aggregates.Wallet.Entities
{
    public class TransactionId : Identity<TransactionId>
    {
        public TransactionId(string value) : base(value) { }
    }
}
