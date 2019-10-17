using Akkatecture.Aggregates;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Entities;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Events;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using System.Collections.Generic;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount
{
    public class ForexAccountState : AggregateState<ForexAccount, ForexAccountId>,
        IApply<ForeignCurrencyTransactionStartedEvent>,
        IApply<ForeignCurrencyTransactionCreatedEvent>,
        IApply<EscrowMoneyAddedEvent>,
        IApply<ForeignCurrencyTransactionCancelledEvent>,
        IApply<ForeignCurrencyTransactionSettledEvent>,
        IApply<ForexEscrowFundsReleasedEvent>,
        IApply<ForexAccountOpenedEvent>,
        IApply<ForexAccountActivatedEvent>
    {
        public Money WithdrawalEscrowBalance { get; set; }

        public Dictionary<string, ForeignCurrencyTransaction> UnsettledTransactions { get; set; }

        public string WalletId { get; set; }

        public bool IsActivated { get; set; }

        public void Apply(ForeignCurrencyTransactionCreatedEvent aggregateEvent)
        {
            if (UnsettledTransactions == null)
            {
                UnsettledTransactions = new Dictionary<string, ForeignCurrencyTransaction>(1000);
            }

            if (!UnsettledTransactions.ContainsKey(aggregateEvent.Transaction.Id.Value))
            {
                UnsettledTransactions[aggregateEvent.Transaction.Id.Value] = aggregateEvent.Transaction;
            }
        }

        public void Apply(EscrowMoneyAddedEvent aggregateEvent)
        {
            if (WithdrawalEscrowBalance == null)
            {
                WithdrawalEscrowBalance = aggregateEvent.Amount;
                return;
            }

            WithdrawalEscrowBalance += aggregateEvent.Amount;
        }

        public void Apply(ForeignCurrencyTransactionCancelledEvent aggregateEvent)
        {
            if (UnsettledTransactions != null && UnsettledTransactions.ContainsKey(aggregateEvent.TransactionId.Value))
            {
                UnsettledTransactions.Remove(aggregateEvent.TransactionId.Value);
            }
        }

        public void Apply(ForexEscrowFundsReleasedEvent aggregateEvent)
        {
            WithdrawalEscrowBalance -= aggregateEvent.Amount;
        }

        public void Apply(ForeignCurrencyTransactionSettledEvent aggregateEvent)
        {
            if (UnsettledTransactions != null && UnsettledTransactions.ContainsKey(aggregateEvent.TransactionId.Value))
            {
                if (aggregateEvent.IsWithdrawalTransaction)
                {
                    WithdrawalEscrowBalance -= (aggregateEvent.SettlementAmount + aggregateEvent.SettlementFees);
                }

                UnsettledTransactions.Remove(aggregateEvent.TransactionId.Value);
            }
        }

        public void Apply(ForexAccountOpenedEvent aggregateEvent)
        {
            WalletId = aggregateEvent.WalletId;
        }

        public void Apply(ForexAccountActivatedEvent aggregateEvent)
        {
            IsActivated = true;
        }

        public void Apply(ForeignCurrencyTransactionStartedEvent aggregateEvent)
        {
            if (UnsettledTransactions == null)
            {
                UnsettledTransactions = new Dictionary<string, ForeignCurrencyTransaction>(1000);
            }
        }
    }
}
