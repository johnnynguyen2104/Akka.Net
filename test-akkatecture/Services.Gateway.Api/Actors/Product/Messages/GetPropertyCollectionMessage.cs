using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Gateway.Api.Actors.Product.Messages
{
    public class GetPropertyCollectionMessage
    {
        public string Id { get; }

        public GetPropertyCollectionMessage(string id)
        {
            Id = id;
        }
    }
}
