using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Product.Shared.Queries
{
    public class SearchPropertyQuery
    {
        public string SearchText { get; }

        public SearchPropertyQuery(string searchText)
        {
            SearchText = searchText;
        }
    }
}
