using Akka.Actor;
using Brokerage.Service.Application.Hubs;
using Brokerage.Service.Application.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Brokerage.Service.Application.Actors
{
    public class UserActor : ReceiveActor
    {
        public class UpdateProfile { }

        private readonly string _userId;
        private readonly IHubContext<UserHub, IUserHubClient> _queryHub;

        private UserProfileViewModel _userProfile;
        private int _count;

        public UserActor(string userId, IHubContext<UserHub, IUserHubClient> queryHub)
        {
            _userId = userId;
            _queryHub = queryHub;

            _count = 0;
            _userProfile = new UserProfileViewModel
            {
                Email = "",
                Name = "",
            };

            ReceiveAsync<UpdateProfile>(async i =>
            {
                _count++; // Dummy data to show changes
                _userProfile.Email += _count;
                _userProfile.Name += _count;

                await _queryHub.Clients.User(_userId).UpdateUserProfile(_userProfile);
            });
        }
    }
}
