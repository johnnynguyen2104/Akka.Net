using Akkatecture.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Aggregates.Revenue.Entities
{
    public class TransactionId : Identity<TransactionId>
    {
        public TransactionId(string value) : base(value)
        {
        }
    }
}
