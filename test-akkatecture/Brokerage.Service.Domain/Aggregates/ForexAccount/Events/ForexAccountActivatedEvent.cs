using Akkatecture.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Events
{
    public class ForexAccountActivatedEvent: AggregateEvent<ForexAccount, ForexAccountId>
    {
    }
}
