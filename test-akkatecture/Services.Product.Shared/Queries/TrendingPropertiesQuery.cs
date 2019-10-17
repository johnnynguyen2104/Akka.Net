using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Product.Shared.Queries
{
    public class TrendingPropertiesQuery
    {
        public int Count { get; }

        public TrendingPropertiesQuery(int count)
        {
            Count = count;
        }
    }
}
