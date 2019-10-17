using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;

namespace Brokerage.Service.Domain.Repositories.Revenue.Projections
{
    public class RevenueProjection
    {
        public Money Revenue { get; }

        public int Transactions { get; }

        public RevenueProjection(Money revenue, int transactions)
        {
            Revenue = revenue ?? throw new ArgumentNullException(nameof(revenue));
            Transactions = transactions;
        }
    }
}
