using Akkatecture.Aggregates;
using Akkatecture.Extensions;
using Akkatecture.Specifications.Provided;
using Brokerage.Service.Domain.Aggregates.Revenue.Commands;
using Brokerage.Service.Domain.Aggregates.Revenue.Entities;
using Brokerage.Service.Domain.Aggregates.Revenue.Events;
using Brokerage.Service.Domain.Aggregates.Revenue.Specifications;
using System;
using System.Linq;

namespace Brokerage.Service.Domain.Aggregates.Revenue
{
    public class Revenue : AggregateRoot<Revenue, RevenueId, RevenueState>
    {
        public Revenue(RevenueId aggregateId) : base(aggregateId)
        {
            Command<AddRevenueCommand>(Execute);
        }

        public bool Execute(AddRevenueCommand command)
        {
            var specIsNew = new AggregateIsNewSpecification().WhyIsNotSatisfiedBy(this);

            if (specIsNew.Count() > 0)
            {
                Console.WriteLine(string.Join(", ", specIsNew));

                return true;
            }

            var spec = new MiniumAmountSpecification().WhyIsNotSatisfiedBy(command);

            Console.WriteLine(string.Join(", ", spec));

            if (spec.Count() == 0)
            {
                var revenueAddedEvt = new RevenueAddedEvent(new Transaction(TransactionId.New, command.Source, command.Type, command.Amount, DateTime.UtcNow));

                Emit(revenueAddedEvt);
            }

            return true;
        }
    }
}
