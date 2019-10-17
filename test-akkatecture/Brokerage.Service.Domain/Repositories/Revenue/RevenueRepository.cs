using Akka.Actor;
using Brokerage.Service.Domain.Aggregates.Revenue.Entities;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Repositories.Revenue.Commands;
using Brokerage.Service.Domain.Repositories.Revenue.Projections;
using Brokerage.Service.Domain.Repositories.Revenue.Queries;
using System.Collections.Generic;

namespace Brokerage.Service.Domain.Repositories.Revenue
{
    public class RevenueRepository : ReceiveActor
    {
        private Money Revenue;

        private Dictionary<string, Transaction> Transactions;
       
        public RevenueRepository()
        {
            Revenue = new Money(0m);
            Transactions = new Dictionary<string, Transaction>();

            Receive<AddRevenueCommand>(Handle);
            Receive<GetRevenueQuery>(Handle);
        }

        private bool Handle(AddRevenueCommand command)
        {
            Revenue += command.Transaction.Amount;
            Transactions.Add(command.Transaction.Id.Value, command.Transaction);

            return true;
        }

        private bool Handle(GetRevenueQuery query)
        {
            var projection = new RevenueProjection(Revenue, Transactions.Count);
            Sender.Tell(projection);

            return true;
        }
    }
}
