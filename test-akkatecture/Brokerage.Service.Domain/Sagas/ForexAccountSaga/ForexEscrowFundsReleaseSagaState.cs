using Akkatecture.Aggregates;
using Akkatecture.Sagas;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Sagas.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForexEscrowFundsReleaseSagaState :
        SagaState<ForexEscrowFundsReleaseSaga, ForexEscrowFundsReleaseSagaId, IMessageApplier<ForexEscrowFundsReleaseSaga, ForexEscrowFundsReleaseSagaId>>
    {
    }
}
