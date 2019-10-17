using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.ObjectSync
{
    public class WalletViewModel : ISynchronizable
    {
        public int Version { get; set; }

        public bool IsSnapshot { get; set; }

        public string WalletId { get; set; }

        public decimal Balance { get; set; }

        public Dictionary<string, WalletTransactionViewModel> Transactions { get; set; }
    }
}
