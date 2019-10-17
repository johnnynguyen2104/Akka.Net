using Akkatecture.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Commands
{
    public class ActivateForexAccountCommand : Command<ForexAccount, ForexAccountId>
    {
        public ActivateForexAccountCommand(ForexAccountId aggregateId) : base(aggregateId)
        {
        }
    }
}
