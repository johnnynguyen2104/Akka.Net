using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Gateway.Api.Actors.Product.Messages
{
    public class GetTrendingPropertiesMessage
    {
        public int Count { get; }

        public GetTrendingPropertiesMessage(int count)
        {
            Count = count;
        }
    }
}
