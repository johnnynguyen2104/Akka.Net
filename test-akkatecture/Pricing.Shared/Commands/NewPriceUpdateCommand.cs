using System;
using System.Collections.Generic;
using System.Text;

namespace Pricing.Shared.Commands
{
    public class NewPriceUpdateCommand
    {
        public string CommandId { get; }

        public decimal NewPrice { get; }

        public NewPriceUpdateCommand(string commandId, decimal newPrice)
        {
            CommandId = commandId ?? throw new ArgumentNullException(nameof(commandId));
            NewPrice = newPrice;
        }
    }
}
