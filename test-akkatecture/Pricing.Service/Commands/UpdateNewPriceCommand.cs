using System;
using System.Collections.Generic;
using System.Text;

namespace Pricing.Service.Commands
{
    public class UpdateNewPriceCommand
    {
        public decimal NewPrice { get; }

        public UpdateNewPriceCommand(decimal newPrice)
        {
            NewPrice = newPrice;
        }
    }
}
