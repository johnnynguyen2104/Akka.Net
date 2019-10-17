using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.ObjectSync
{
    public class WalletSyncModel : ISyncObject<WalletViewModel>
    {
        public int Version { get; }

        public bool IsSnapshot { get; }

        public string WalletId { get; }

        public decimal? Balance { get; set; }

        public WalletTransactionSyncModel TransactionSyncModel { get; set; }

        public WalletSyncModel(string walletId, int version)
        {
            WalletId = walletId;
            Version = version;
            IsSnapshot = false;
        }

        public WalletSyncModel(string walletId)
        {
            WalletId = walletId;
            Version = 0;
            IsSnapshot = true;
        }

        public bool TrySync(ref WalletViewModel walletViewModel)
        {
            if (IsSnapshot)
            {
                walletViewModel = new WalletViewModel
                {
                    WalletId = WalletId,
                    Version = Version,
                    IsSnapshot = IsSnapshot,
                    Balance = Balance.GetValueOrDefault(),
                    Transactions = TransactionSyncModel.AsDictionary(),
                };

                return true;
            }

            if ((Version - 1) != walletViewModel.Version)
            {
                return false;
            }

            walletViewModel.Balance = Balance ?? walletViewModel.Balance;
            if (TransactionSyncModel != null)
            {
                TransactionSyncModel.Sync(walletViewModel.Transactions);
            }

            return true;
        }
    }
}
