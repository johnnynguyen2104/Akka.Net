using Akkatecture.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForeignCurrencyTransactionCreationSagaId : SagaId<ForeignCurrencyTransactionCreationSagaId>
    {
        public ForeignCurrencyTransactionCreationSagaId(string value) : base(value)
        {
        }
    }
}
