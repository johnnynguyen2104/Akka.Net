using System;
using System.Collections.Generic;

namespace Services.Gateway.Shared.ViewModels
{
    public sealed class PropertyCollectionsViewModel
    {
        public DateTime UpdatedTimeStamp { get; set; }

        public Dictionary<string, string> PropertyCollections { get; set; }
    }
}
