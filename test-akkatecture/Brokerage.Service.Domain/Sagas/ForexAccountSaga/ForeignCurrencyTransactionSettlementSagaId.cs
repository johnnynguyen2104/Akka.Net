using Akkatecture.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForeignCurrencyTransactionSettlementSagaId : SagaId<ForeignCurrencyTransactionSettlementSagaId>
    {
        public ForeignCurrencyTransactionSettlementSagaId(string value) : base(value)
        {
        }
    }
}
