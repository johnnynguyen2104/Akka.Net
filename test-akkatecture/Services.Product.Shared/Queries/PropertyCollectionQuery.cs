using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Product.Shared.Queries
{
    public class PropertyCollectionQuery
    {
        public string Id { get; }

        public PropertyCollectionQuery(string id)
        {
            Id = id;
        }
    }
}
