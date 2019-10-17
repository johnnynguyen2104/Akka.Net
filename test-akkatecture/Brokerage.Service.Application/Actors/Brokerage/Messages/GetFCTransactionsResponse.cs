

using System.Collections.Generic;

namespace Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage
{
    public class GetFCTransactionsResponse
    {
        public IEnumerable<ForeignCurrencyTransactionMessage> Transactions { get; set; }
    }
}
