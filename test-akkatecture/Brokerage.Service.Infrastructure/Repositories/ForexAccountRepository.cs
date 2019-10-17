using Akka.Actor;
using Brokerage.Service.Domain.Aggregates.ValueObjects;
using Brokerage.Service.Domain.Enumerations;
using Brokerage.Service.Domain.Subscribers.Commands;
using Brokerage.Service.Infrastructure.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Brokerage.Service.Akka.Extensions;
using Brokerage.Service.Domain.Aggregates.RepositoryAbstractions;

namespace Brokerage.Service.Infrastructure.Repositories
{
    public class ForexAccountRepository : ReceiveActor, IForexAccountRepository
    {
        private BrokerageContext _context;

        public ForexAccountRepository()
        {
            Receive<AddForeignCurrencyTransactionCommand>(Handle);
            Receive<CancelTransactionCommand>(Handle);
            Receive<SettleTransactionCommand>(Handle);
        }

        protected override bool AroundReceive(Receive receive, object message)
        {
            using (IServiceScope serviceScope = Context.CreateScope())
            {
                _context = serviceScope.ServiceProvider.GetService<BrokerageContext>();
                return base.AroundReceive(receive, message);
            }
        }

        public void Handle(SettleTransactionCommand command)
        {
            if (command != null && command.TransactionId.IsValid())
            {
                var transaction = _context.ForeignCurrencyTransactions.SingleOrDefault(a => a.TransactionId == command.TransactionId.Value);

                if (transaction != null)
                {
                    transaction.SettlementRate = command.SettlementRate.Value;
                    transaction.SettlementAmount = command.SettlementAmount.Value;
                    transaction.SettlementFees = command.SettlementFees.Value;
                    transaction.SettlementDate = command.SettlementDate;
                    transaction.SettlementStatusType = SettlementStatusType.Settled;

                    _context.SaveChangesAsync();
                }
            }
        }

        public void Handle(CancelTransactionCommand command)
        {
            if (command != null && command.TransactionId.IsValid())
            {
                var transaction = _context.ForeignCurrencyTransactions.SingleOrDefault(a => a.TransactionId == command.TransactionId.Value);

                if (transaction != null)
                {
                    transaction.Reason = command.Reason;
                    transaction.SettlementStatusType = SettlementStatusType.Cancelled;

                    _context.SaveChanges();
                }
            }
        }

        public void Handle(AddForeignCurrencyTransactionCommand command)
        {
            if (command != null && command.Transaction != null)
            {
                var transaction = new ForeignCurrencyTransactions()
                {
                    TransactionId = command.Transaction.Id.Value,
                    Amount = command.Transaction.Amount.Value,
                    CurrencyPairType = command.Transaction.CurrencyPairType,
                    DateCreated = command.Transaction.DateCreated,
                    ForexAccountId = command.Transaction.ForexAccountId.Value,
                    PaymentProviderTransactionId = command.Transaction.PaymentProviderTransactionId,
                    PaymentProviderType = command.Transaction.PaymentProviderType,
                    PaymentProviderUserId = command.Transaction.PaymentProviderUserId,
                    Reason = command.Transaction.Reason,
                    SettlementAmount = command.Transaction.SettlementAmount.Value,
                    SettlementDate = command.Transaction.SettlementDate,
                    SettlementFees = command.Transaction.SettlementFees.Value,
                    SettlementRate = command.Transaction.SettlementRate.Value,
                    SettlementStatusType = command.Transaction.SettlementStatusType,
                    WalletId = command.Transaction.WalletId
                };

                _context.ForeignCurrencyTransactions.Add(transaction);
                _context.SaveChanges();
            }
        }
    }
}
