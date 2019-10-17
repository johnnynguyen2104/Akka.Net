using Brokerage.Service.Domain.Aggregates.Revenue.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using System;

namespace Brokerage.Service.Domain.Repositories.Revenue.Commands
{
    public class AddRevenueCommand
    {
        public Transaction Transaction { get; }

        public AddRevenueCommand(Transaction transaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}
