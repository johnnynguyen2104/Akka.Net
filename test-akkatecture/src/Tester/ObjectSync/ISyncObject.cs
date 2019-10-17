using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.ObjectSync
{
    public interface ISyncObject<TObject> : ISynchronizable
    {
        bool TrySync(ref TObject @object);
    }
}
