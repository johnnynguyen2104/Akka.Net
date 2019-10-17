using Akkatecture.Aggregates;
using Akkatecture.Sagas;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Events;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Sagas.ForexAccountSaga
{
    public class ForexEscrowFundsReleaseSagaLocator : ISagaLocator<ForexEscrowFundsReleaseSagaId>
    {
        public const string LocatorPrefixId = "escrow-funds-release";

        public ForexEscrowFundsReleaseSagaId LocateSaga(IDomainEvent domainEvent)
        {
            switch (domainEvent.GetAggregateEvent())
            {
                case ForexEscrowFundsReleasedEvent evt: return new ForexEscrowFundsReleaseSagaId($"{LocatorPrefixId}-{evt.ForexTransactionId}");

                default: throw new ArgumentNullException(nameof(domainEvent));
            }
        }
    }
}
