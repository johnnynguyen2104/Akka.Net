using Akka.Actor;
using Brokerage.Service.Application.Actors;
using Brokerage.Service.Application.Actors.Brokerage.Messages.Brokerage;
using Brokerage.Service.Application.Services;
using Brokerage.Service.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Brokerage.Service.Application.Hubs
{
    public interface IUserHubClient
    {
        Task UpdateUserProfile(UserProfileViewModel updatedData);
    }

    public interface IUserHubServer
    {
        Task<UserProfileViewModel> SubscribeUserProfile(string query);

        Task<string> Test(string query);
    }

    public class UserHub : Hub<IUserHubClient>, IUserHubServer
    {
        private readonly IActorRef _userCoordinator;

        private readonly IBrokerageService _brokerageService;

        public UserHub(ActorSystemService actorSystemService, IBrokerageService brokerageService)
        {
            _userCoordinator = actorSystemService.UserCoordintorActorRef;
            _brokerageService = brokerageService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId(Context);
            var success = await _userCoordinator.Ask<bool>(new UserCoordinatorActor.UserConnected(userId));
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = GetUserId(Context);
            var success = await _userCoordinator.Ask<bool>(new UserCoordinatorActor.UserDisconnected(userId));
        }

        [Authorize]
        public async Task<string> Test(string query)
        {

            return $"Dude, you sent me \"{query}\"!";
        }

        public async Task<string> CreateWalletAndOpenForexAccount()
        {
            var walletId = _brokerageService.CreateWallet();
            _brokerageService.ActivateWallet(walletId);
            var forexAccountId = _brokerageService.OpenForexAccount(walletId);
            _brokerageService.ActivateForexAccount(forexAccountId);

            return "Wallet and ForexAccount Created";
        }

        [Authorize]
        public async Task<string> CreateFCTransaction(string forexAccountId, string walletId)
        {
            _brokerageService.CreateForeignCurrencyTransaction(forexAccountId, walletId);
            return $"FC Transaction Created";
        }

        [Authorize]
        public async Task<GetFCTransactionsResponse> GetAllFCTransactions(string walletId)
        {
            var result = await _brokerageService.GetFCTransactions(walletId);
            return result;
        }

        [Authorize]
        public async Task<UserProfileViewModel> SubscribeUserProfile(string userId)
        {
            // Ask the UserSessionCoordinatorActor to get the UserSessionActor
            var userActorRef = await _userCoordinator.Ask<IActorRef>(new UserCoordinatorActor.GetUserActorRef(userId));
            var result = new UserProfileViewModel
            {
                Email = $"{userId}:{Context.UserIdentifier}",
                Name = userActorRef.Path.ToStringWithAddress(),
            };

            return result;
        }

        private string GetUserId(HubCallerContext hubCallerContext)
        {
            var userId = hubCallerContext.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            return userId;
        }
    }
}
