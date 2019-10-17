using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.ObjectSync
{
    public interface ISynchronizable
    {
        int Version { get; }

        bool IsSnapshot { get; }
    }
}
