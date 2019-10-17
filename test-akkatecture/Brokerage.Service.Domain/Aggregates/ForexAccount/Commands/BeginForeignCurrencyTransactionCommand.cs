using Akkatecture.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Commands
{
    public class BeginForeignCurrencyTransactionCommand : Command<ForexAccount, ForexAccountId>
    {
        public ForeignCurrencyTransaction Transaction { get; }

        public BeginForeignCurrencyTransactionCommand(ForexAccountId aggregateId, ForeignCurrencyTransaction transaction)
            : base(aggregateId)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}
