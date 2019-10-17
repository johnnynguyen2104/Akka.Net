using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tester.ObjectSync;

namespace Tester
{
    public class WalletSyncTester
    {
        private const string WALLET_ID = "ABCDEF";
        private WalletViewModel _walletViewModel;

        public WalletSyncTester()
        {
            _walletViewModel = new WalletViewModel();
        }

        public void UpdateWallet()
        {
            // Load a snapshot
            var snapshot = CreateWalletSnapshot(WALLET_ID);
            if (!snapshot.TrySync(ref _walletViewModel))
            {
                Console.WriteLine("ERROR: Snapshot sync failed!");
            }

            var json = JsonConvert.SerializeObject(snapshot, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Console.WriteLine(json);

            Console.WriteLine($"Wallet Id:{_walletViewModel.WalletId}, Balance:{_walletViewModel.Balance}, Transactions:{_walletViewModel.Transactions.Count}, Version:{_walletViewModel.Version}");

            // Update with a delta
            var delta = CreateWalletDelta(WALLET_ID, 1);
            if (!delta.TrySync(ref _walletViewModel))
            {
                Console.WriteLine("ERROR: Delta sync failed!");
            }

            json = JsonConvert.SerializeObject(delta, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Console.WriteLine(json);

            Console.WriteLine($"Wallet Id:{_walletViewModel.WalletId}, Balance:{_walletViewModel.Balance}, Transactions:{_walletViewModel.Transactions.Count}, Version:{_walletViewModel.Version}");
        }

        private WalletSyncModel CreateWalletSnapshot(string walletId)
        {
            var added = new List<WalletTransactionViewModel>();
            var removed = new List<string>();

            var walletSyncModel = new WalletSyncModel(walletId)
            {
                Balance = 100,
                TransactionSyncModel = new WalletTransactionSyncModel(added, removed),
            };

            return walletSyncModel;
        }

        private WalletSyncModel CreateWalletDelta(string walletId, int version)
        {
            var added = new List<WalletTransactionViewModel>();
            var removed = new List<string>();

            var walletSyncModel = new WalletSyncModel(walletId, version)
            {
                Balance = 110,
                // Transactions = new WalletTransactionSyncModel(added, removed),
            };

            return walletSyncModel;
        }
    }
}
