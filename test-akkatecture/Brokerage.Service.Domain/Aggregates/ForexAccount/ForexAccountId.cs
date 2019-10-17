using Akkatecture.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount
{
    public class ForexAccountId : Identity<ForexAccountId>
    {
        public ForexAccountId(string value) : base(value)
        {
        }
    }
}
