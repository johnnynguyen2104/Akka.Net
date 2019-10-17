using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;

namespace Brokerage.Service.Domain.Repositories.Revenue.ReadModels
{
    public class RevenueReadModel
    {
        public Money Revenue { get; }

        public int Transactions { get; }

        public RevenueReadModel(Money revenue, int transactions)
        {
            Revenue = revenue ?? throw new ArgumentNullException(nameof(revenue));
            Transactions = transactions;
        }
    }
}
