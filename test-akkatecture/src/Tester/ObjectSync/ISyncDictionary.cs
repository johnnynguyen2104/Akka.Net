using System.Collections.Generic;

namespace Tester.ObjectSync
{
    public interface ISyncDictionary<TKey, TValue>
    {
        List<TValue> Added { get; }

        List<TKey> Removed { get; }

       void Sync(Dictionary<TKey, TValue> dictionary);

        Dictionary<TKey, TValue> AsDictionary();
    }
}
