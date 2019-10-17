using System;
using System.Collections.Generic;

namespace Services.Product.Shared.Queries.DataTransferObjects
{
    public sealed class PropertyCollectionsDto
    {
        public DateTime UpdatedTimeStamp { get; set; }

        public Dictionary<string, string> PropertyCollections { get; set; }
    }
}
