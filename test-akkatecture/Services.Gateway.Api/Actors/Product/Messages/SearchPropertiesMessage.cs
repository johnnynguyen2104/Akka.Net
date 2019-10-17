using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Gateway.Api.Actors.Product.Messages
{
    public class SearchPropertiesMessage
    {
        public string SearchText { get; }

        public SearchPropertiesMessage(string searchText)
        {
            SearchText = searchText;
        }
    }
}
