using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Subscribers.Commands
{
    public class AddForeignCurrencyTransactionCommand
    {
        public ForeignCurrencyTransaction Transaction { get; }

        public AddForeignCurrencyTransactionCommand(ForeignCurrencyTransaction transaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}
