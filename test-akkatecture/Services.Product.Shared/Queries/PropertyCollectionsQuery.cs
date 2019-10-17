using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Product.Shared.Queries
{
    public class PropertyCollectionsQuery
    {
        public int Count { get; }

        public PropertyCollectionsQuery(int count)
        {
            Count = count;
        }
    }
}
