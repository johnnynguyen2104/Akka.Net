using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Events
{
    public class EscrowMoneyAddedEvent : AggregateEvent<ForexAccount, ForexAccountId>
    {
        public Money Amount { get; }

        public EscrowMoneyAddedEvent(Money amount)
        {

            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
  
        }
    }
}
