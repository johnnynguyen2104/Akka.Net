using System;
using System.Linq;
using Akkatecture.Aggregates;
using Akkatecture.Extensions;
using Akkatecture.Specifications.Provided;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Events;
using Brokerage.Service.Domain.Aggregates.ForexAccount.Specifications;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;

namespace Brokerage.Service.Domain.Aggregates.ForexAccount
{
    public class ForexAccount : AggregateRoot<ForexAccount, ForexAccountId, ForexAccountState>
    {
        /// <summary>
        /// Default rate for FRX.
        /// </summary>
        private const decimal FRXRate = 1;

        /// <summary>
        /// Default real-world rate.
        /// </summary>
        private const decimal RealWorldCurrencyRate = 1;

        public ForexAccount(ForexAccountId aggreagateId) : base(aggreagateId)
        {
            Command<CreateForeignCurrencyTransactionCommand>(Execute);
            Command<CancelForeignCurrencyTransactionCommand>(Execute);
            Command<SettleForeignCurrencyTransactionCommand>(Execute);
            Command<OpenNewForexAccountCommand>(Execute);
            Command<ActivateForexAccountCommand>(Execute);
            Command<BeginForeignCurrencyTransactionCommand>(Execute);
        }

        public bool Execute(BeginForeignCurrencyTransactionCommand command)
        {
            var specErrors = new AggregateIsNewSpecification().Not().WhyIsNotSatisfiedBy(this);

            if (specErrors.Count() == 0)
            {
                var forexAccountSpecs = new ForexAccountActiveSpecification().And(new TransactionExistSpecification(command.Transaction.Id).Not()).WhyIsNotSatisfiedBy(State);

                if (forexAccountSpecs.Count() > 0)
                {
                    Console.WriteLine(string.Join(", ", forexAccountSpecs));

                    return true;
                }

                var commandSpec = new MiniumAmountSpecification().And(new IsPendingStatusOnCreationSpecification()).WhyIsNotSatisfiedBy(command);

                //TODO:Need to think about how to handle the fail transaction. 
                //I think we need to record it, event failed.
                if (commandSpec.Count() > 0)
                {
                    Console.WriteLine(string.Join(", ", commandSpec));

                    return true;
                }

                if (IsWithdrawalForeignCurrencyTransaction(command.Transaction.CurrencyPairType))
                {
                    var createdEvent = new ForeignCurrencyTransactionStartedEvent(command.Transaction);

                    Emit(createdEvent);
                }
                else
                {
                    //Need to think about the deposit logic.
                }
                
            }

            return true;
        }

        public bool Execute(CreateForeignCurrencyTransactionCommand command)
        {
            var spec = new AggregateIsNewSpecification().Not();

            if (spec.IsSatisfiedBy(this))
            {
                var eve = new ForeignCurrencyTransactionCreatedEvent(command.Transaction);

                if (IsWithdrawalForeignCurrencyTransaction(command.Transaction.CurrencyPairType))
                {
                    var escrowEvent = new EscrowMoneyAddedEvent(command.Transaction.Amount);

                    EmitAll(eve, escrowEvent);
                    return true;
                }

                Emit(eve);
            }

            return true;
        }

        public bool Execute(ActivateForexAccountCommand command)
        {
            var specErrors = new AggregateIsNewSpecification().Not().WhyIsNotSatisfiedBy(this);

            if (specErrors.Count() == 0)
            {
                var activatedCommand = new ForexAccountActivatedEvent();

                Emit(activatedCommand);
            }

            return true;
        }

        public bool Execute(OpenNewForexAccountCommand command)
        {
            var spec = new AggregateIsNewSpecification();

            if (spec.IsSatisfiedBy(this))
            {
                var forexAccountOpenedEvent = new ForexAccountOpenedEvent(command.WalletId);

                Emit(forexAccountOpenedEvent);
            }
 
            return true;
        }

        public bool Execute(SettleForeignCurrencyTransactionCommand command)
        {
            var specErrors = new AggregateIsNewSpecification().Not().WhyIsNotSatisfiedBy(this);

            if (specErrors.Count() == 0)
            {
                var forexAccountSpecs = new ForexAccountActiveSpecification().And(new TransactionExistSpecification(command.ForeignCurrencyTransactionId)).And(new ValidatePaymentProviderSpecification(command.PaymentProviderTransactionId, command.ForeignCurrencyTransactionId.Value)).WhyIsNotSatisfiedBy(State);

                if (forexAccountSpecs.Count() > 0)
                {
                    Console.WriteLine(string.Join(", ", forexAccountSpecs));

                    return true;
                }

                var unsettleTransaction = State.UnsettledTransactions[command.ForeignCurrencyTransactionId.Value];
                bool isWithdrawal = IsWithdrawalForeignCurrencyTransaction(unsettleTransaction.CurrencyPairType);
                Money settlementRate = new Money(isWithdrawal ? RealWorldCurrencyRate : FRXRate);
                //Need to think about the fees for revenue
                //Iam assuming the FRX currency also apply for Revenue.
                Money settlementFees = new Money(unsettleTransaction.Amount.Value * 1 / 100);
                Money settlementAmount = (unsettleTransaction.Amount - settlementFees) * settlementRate;

                var settlementEvent = new ForeignCurrencyTransactionSettledEvent(command.ForeignCurrencyTransactionId, settlementRate, settlementAmount, settlementFees, unsettleTransaction.WalletId, isWithdrawal);

                Emit(settlementEvent);

            }

            return true;
        }

        public bool Execute(CancelForeignCurrencyTransactionCommand command)
        {
            var specErrors = new AggregateIsNewSpecification().Not().WhyIsNotSatisfiedBy(this);

            if (specErrors.Count() == 0)
            {
                var forexAccountSpecs = new ForexAccountActiveSpecification().And(new TransactionExistSpecification(command.ForeignCurrencyTransactionId)).WhyIsNotSatisfiedBy(State);

                if (forexAccountSpecs.Count() > 0)
                {
                    Console.WriteLine(string.Join(", ", forexAccountSpecs));

                    return true;
                }

                var cancelledEvent = new ForeignCurrencyTransactionCancelledEvent(command.ForeignCurrencyTransactionId, command.Reason);
                var transaction = State.UnsettledTransactions[command.ForeignCurrencyTransactionId.Value];

                if (IsWithdrawalForeignCurrencyTransaction(transaction.CurrencyPairType) && command.ReturnEscrowBalance)
                {
                    var returnedFundsEvent = new ForexEscrowFundsReleasedEvent(transaction.Id.Value, transaction.Amount, transaction.WalletId);
                    EmitAll(cancelledEvent, returnedFundsEvent);
                }
                else
                {
                    Emit(cancelledEvent);
                }
            }

            return true;
        }

        // Need to find a better way to identify the CurrencyPair
        private bool IsWithdrawalForeignCurrencyTransaction(CurrencyPairType currencyPairType)
        {
            if (currencyPairType.ToString().Substring(0, 3) == "FRX")
            {
                return true;
            }

            return false;
        }
    }
}
