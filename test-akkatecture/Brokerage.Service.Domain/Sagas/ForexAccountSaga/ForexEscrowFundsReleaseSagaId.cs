using Akkatecture.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForexEscrowFundsReleaseSagaId : SagaId<ForexEscrowFundsReleaseSagaId>
    {
        public ForexEscrowFundsReleaseSagaId(string value) : base(value)
        {
        }
    }
}
