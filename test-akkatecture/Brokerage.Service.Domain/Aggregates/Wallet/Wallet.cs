using Akkatecture.Aggregates;
using Akkatecture.Extensions;
using Akkatecture.Specifications.Provided;
using Brokerage.Service.Domain.Aggregates.Wallet.Commands;
using Brokerage.Service.Domain.Aggregates.Wallet.Events;
using Brokerage.Service.Domain.Aggregates.Wallet.Specifications;
using System;
using System.Linq;

namespace Brokerage.Service.Domain.Aggregates.Wallet
{
    public class Wallet : AggregateRoot<Wallet, WalletId, WalletState>
    {
        public Wallet(WalletId aggregateId) : base(aggregateId)
        {
            Command<CreateWalletCommand>(Execute);
            Command<ActivateWalletCommand>(Execute);
            Command<PlaceEscrowCommand>(Execute);
            Command<ReleaseEscrowCommand>(Execute);
        }

        public bool Execute(PlaceEscrowCommand command)
        {
            // Make sure it's not a new aggregate
            var spec = new AggregateIsNewSpecification().Not();

            // Check other business rules here
            // Is activated
            // Is min balance enough
            // Is amount enough etc
            var specs = new WalletActiveSpecification().And(new WalletAvailableSpecification()).And(new EnoughFundsSpecification(command.Amount.Value)).WhyIsNotSatisfiedBy(State).ToList();
            var escrowMiniumSpec = new MinimumEscrowAmountSpecification().WhyIsNotSatisfiedBy(command);
            specs.AddRange(escrowMiniumSpec);

            Console.WriteLine(string.Join(", ", specs));

            if (spec.IsSatisfiedBy(this) && specs.Count() == 0)
            {
                var eve = new EscrowPlacedEvent(command.ForeignTransactionId, command.Amount);

                Emit(eve);
            }

            return true;
        }


        public bool Execute(ReleaseEscrowCommand command)
        {
            // Make sure it's not a new aggregate
            var spec = new AggregateIsNewSpecification().Not();

            // Check other business rules here

            if (spec.IsSatisfiedBy(this))
            {
                var eve = new EscrowReleasedEvent(command.ForeignTransactionId, command.Amount);

                Emit(eve);
            }

            return true;
        }

        public bool Execute(CreateWalletCommand command)
        {
            var spec = new AggregateIsNewSpecification();

            if (spec.IsSatisfiedBy(this))
            {
                Emit(new WalletCreatedEvent(command.OpeningBalance));
            }

            return true;
        }

        public bool Execute(ActivateWalletCommand command)
        {
            var isWalletAvailable = new WalletAvailableSpecification().WhyIsNotSatisfiedBy(this.State);

            if (isWalletAvailable.Count() > 0)
            {
                Console.WriteLine($"Errors: {string.Join(", ", isWalletAvailable)}");

                return true;
            }

            var isWalletAlreadyActivated = new WalletActiveSpecification().Not().WhyIsNotSatisfiedBy(this.State);

            if (isWalletAlreadyActivated.Count() > 0)
            {
                Console.WriteLine($"Errors: {string.Join(", ", isWalletAlreadyActivated)}");

                return true;
            }

            Emit(new WalletActivatedEvent());

            return true;
        }

        //public bool Execute(WithdrawFundsCommand command)
        //{
        //    var isWalletAvailable = new WalletAvailableSpecification().WhyIsNotSatisfiedBy(this.State);
        //    if (isWalletAvailable.Count() > 0)
        //    {
        //        Console.WriteLine($"Errors: {string.Join(", ", isWalletAvailable)}");

        //        return true;
        //    }

        //    var isActivated = new WalletActiveSpecification().WhyIsNotSatisfiedBy(this.State);
        //    if (isActivated.Count() > 0)
        //    {
        //        Console.WriteLine($"Errors: {string.Join(", ", isActivated)}");

        //        return true;
        //    }

        //    var miniumSpec = new MiniumWithdrawSpecification();
        //    var stateSpecs = new EnoughFundsSpecification(command.Transaction.Amount.Value);

        //    if (stateSpecs.IsSatisfiedBy(this.State) && miniumSpec.IsSatisfiedBy(command))
        //    {

        //        var withdrawEvent = new FundsWithdrawnEvent(command.Transaction);

        //        Emit(withdrawEvent);
        //    }
        //    else
        //    {
        //        List<string> errors = new List<string>();
        //        errors.AddRange(miniumSpec.WhyIsNotSatisfiedBy(command));
        //        errors.AddRange(stateSpecs.WhyIsNotSatisfiedBy(this.State));

        //        Console.WriteLine($"Errors: {string.Join(", ", errors)}");
        //    }

        //    return true;
        //}

        //public bool Execute(DepositFundsCommand command)
        //{
        //    var isWalletAvailable = new WalletAvailableSpecification().WhyIsNotSatisfiedBy(this.State);
        //    if (isWalletAvailable.Count() > 0)
        //    {
        //        Console.WriteLine($"Errors: {string.Join(", ", isWalletAvailable)}");

        //        return true;
        //    }

        //    var isActivated = new WalletActiveSpecification().WhyIsNotSatisfiedBy(this.State);
        //    if (isActivated.Count() > 0)
        //    {
        //        Console.WriteLine($"Errors: {string.Join(", ", isActivated)}");

        //        return true;
        //    }

        //    var miniumSpec = new MiniumDepositSpecification();

        //    if (miniumSpec.IsSatisfiedBy(command))
        //    {

        //        var depositEvent = new FundsDepositedEvent(command.Transaction);

        //        Emit(depositEvent);
        //    }
        //    else
        //    {
        //        List<string> errors = new List<string>();
        //        errors.AddRange(miniumSpec.WhyIsNotSatisfiedBy(command));

        //        Console.WriteLine($"Errors: {string.Join(", ", errors)}");
        //    }

        //    return true;
        //}
    }
}
