using System;
using System.Collections.Generic;

namespace Services.Product.Shared.DataTransferObjects
{
    public sealed class PropertyCollectionItemDto
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string IconImageUrl { get; set; }
    }
}
