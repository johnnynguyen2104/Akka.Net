using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage
{
    public class ActivateForexAccount
    {
        public string ForexAccountId { get; set; }

        public ActivateForexAccount(string forexAccountId)
        {
            ForexAccountId = forexAccountId ?? throw new ArgumentNullException(nameof(forexAccountId));
        }
    }
}
