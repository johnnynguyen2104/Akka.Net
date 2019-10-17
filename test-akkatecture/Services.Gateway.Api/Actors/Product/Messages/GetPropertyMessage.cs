using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Gateway.Api.Actors.Product.Messages
{
    public class GetPropertyMessage
    {
        public string Symbol { get; }

        public GetPropertyMessage(string symbol)
        {
            Symbol = symbol;
        }
    }
}
