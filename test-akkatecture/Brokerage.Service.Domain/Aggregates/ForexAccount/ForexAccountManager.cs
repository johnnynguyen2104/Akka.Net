using Akkatecture.Aggregates;
using Akkatecture.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount
{
    public class ForexAccountManager : AggregateManager<ForexAccount, ForexAccountId, Command<ForexAccount, ForexAccountId>> { }
}
