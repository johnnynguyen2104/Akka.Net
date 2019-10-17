using Akkatecture.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount.Entities
{
    public class ForeignCurrencyTransactionId : Identity<ForeignCurrencyTransactionId>
    {
        public ForeignCurrencyTransactionId(string value) : base(value)
        {
        }

        public bool IsValid()
        {
            return (!string.IsNullOrEmpty(Value) && Value.Trim().Length > 0);
        }
    }
}
