using System.Collections.Generic;

namespace Tester.ObjectSync
{
    public class WalletTransactionSyncModel : ISyncDictionary<string, WalletTransactionViewModel>
    {
        public List<WalletTransactionViewModel> Added { get; }

        public List<string> Removed { get; }

        public WalletTransactionSyncModel(List<WalletTransactionViewModel> added, List<string> removed)
        {
            Added = added;
            Removed = removed;
        }

        public void Sync(Dictionary<string, WalletTransactionViewModel> dictionary)
        {
            foreach (var item in Added)
            {
                dictionary[item.TransactionId] = item;
            }

            foreach (var itemId in Removed)
            {
                dictionary.Remove(itemId);
            }
        }

        public Dictionary<string, WalletTransactionViewModel> AsDictionary()
        {
            var dictionary = new Dictionary<string, WalletTransactionViewModel>(Added.Count);

            foreach (var item in Added)
            {
                dictionary[item.TransactionId] = item;
            }

            return dictionary;
        }
    }
}
