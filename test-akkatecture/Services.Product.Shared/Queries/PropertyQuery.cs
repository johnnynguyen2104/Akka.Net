using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Product.Shared.Queries
{
    public class PropertyQuery
    {
        public string Symbol { get; }

        public PropertyQuery(string symbol)
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        }
    }
}
