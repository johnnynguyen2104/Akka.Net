using Brokerage.Service.Domain.Subscribers.Commands;
using Brokerage.Service.Domain.Aggregates.ForexAccount;

namespace Brokerage.Service.Domain.Aggregates.RepositoryAbstractions
{
    public interface IForexAccountRepository : IRepository<Brokerage.Service.Domain.Aggregates.ForexAccount.ForexAccount>
    {

        void Handle(SettleTransactionCommand command);

        void Handle(CancelTransactionCommand obj);

        void Handle(AddForeignCurrencyTransactionCommand command);
    }
}
