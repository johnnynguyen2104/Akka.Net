using Akkatecture.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Commands
{
    public class CreateForeignCurrencyTransactionCommand : Command<ForexAccount, ForexAccountId>
    {
        public ForeignCurrencyTransaction Transaction { get; }

        public CreateForeignCurrencyTransactionCommand(ForexAccountId aggregateId, ForeignCurrencyTransaction transaction)
            : base(aggregateId)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}
