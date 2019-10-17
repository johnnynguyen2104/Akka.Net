using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Gateway.Api.Actors.Product.Messages
{
    public class GetPropertyCollectionsMessage
    {
        public int Count { get; }

        public GetPropertyCollectionsMessage(int count)
        {
            Count = count;
        }
    }
}
