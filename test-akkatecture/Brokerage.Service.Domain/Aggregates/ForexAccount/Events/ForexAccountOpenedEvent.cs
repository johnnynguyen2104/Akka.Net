using Akkatecture.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Events
{
    public class ForexAccountOpenedEvent : AggregateEvent<ForexAccount, ForexAccountId>
    {
        public string WalletId { get; }

        public ForexAccountOpenedEvent(string walletId)
        {
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }
    }
}
