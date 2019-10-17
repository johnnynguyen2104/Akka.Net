using Akkatecture.Sagas.AggregateSaga;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForeignCurrencyTransactionSettlementSagaManager : AggregateSagaManager<ForeignCurrencyTransactionSettlementSaga, ForeignCurrencyTransactionSettlementSagaId, ForeignCurrencyTransactionSettlementSagaLocator>
    {
        public ForeignCurrencyTransactionSettlementSagaManager(Expression<Func<ForeignCurrencyTransactionSettlementSaga>> factory) : base(factory) { }
    }
}
