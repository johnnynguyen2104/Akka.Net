using Akkatecture.Sagas.AggregateSaga;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForeignCurrencyTransactionCreationSagaManager : AggregateSagaManager<ForeignCurrencyTransactionCreationSaga, ForeignCurrencyTransactionCreationSagaId, ForeignCurrencyTransactionCreationSagaLocator>
    {
        public ForeignCurrencyTransactionCreationSagaManager(Expression<Func<ForeignCurrencyTransactionCreationSaga>> factory) : base(factory) { }
    }
}
