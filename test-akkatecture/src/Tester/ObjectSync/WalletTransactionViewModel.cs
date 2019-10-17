using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.ObjectSync
{
    public class WalletTransactionViewModel
    {
        public string TransactionId { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
