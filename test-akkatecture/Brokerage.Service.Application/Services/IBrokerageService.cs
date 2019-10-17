using Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Services
{
    public interface IBrokerageService
    {
        string CreateWallet();
        void ActivateWallet(string walletId);
        string OpenForexAccount(string walletId);
        void ActivateForexAccount(string forexAccountId);
        void CreateForeignCurrencyTransaction(string forexAccountId, string walletId);

        Task<GetFCTransactionsResponse> GetFCTransactions(string walletId);
    }
}
