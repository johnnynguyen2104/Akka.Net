using Brokerage.Service.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage
{
    public class ForeignCurrencyTransactionMessage
    {
        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string WalletId { get; set; }

        public string ForexAccountId { get; set; }

        public SettlementStatusType SettlementStatusType { get; set; }

        public CurrencyPairType CurrencyPairType { get; set; }
    }
}
