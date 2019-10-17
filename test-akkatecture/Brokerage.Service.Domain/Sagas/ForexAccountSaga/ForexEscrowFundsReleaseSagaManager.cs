using Akkatecture.Sagas.AggregateSaga;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForexEscrowFundsReleaseSagaManager : AggregateSagaManager<ForexEscrowFundsReleaseSaga, ForexEscrowFundsReleaseSagaId, ForexEscrowFundsReleaseSagaLocator>
    {
        public ForexEscrowFundsReleaseSagaManager(Expression<Func<ForexEscrowFundsReleaseSaga>> factory) : base(factory) { }
    }
}
