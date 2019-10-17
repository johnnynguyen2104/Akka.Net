using Akka.Actor;
using Akka.Event;
using Brokerage.Service.Application.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace Brokerage.Service.Application.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        public class TestPing { }

        public class GetUserActorRef
        {
            public string UserId { get; }

            public GetUserActorRef(string userId)
            {
                UserId = userId;
            }
        }

        public class UserConnected
        {
            public string UserId { get; }
            public UserConnected(string userId)
            {
                UserId = userId;
            }
        }

        public class UserDisconnected
        {
            public string UserId { get; }
            public UserDisconnected(string userId)
            {
                UserId = userId;
            }
        }

        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly IHubContext<UserHub, IUserHubClient> _userHubContext;
        private readonly Dictionary<string, IActorRef> _userSessions;

        public UserCoordinatorActor(IHubContext<UserHub, IUserHubClient> userHubContext)
        {
            _userHubContext = userHubContext;
            _userSessions = new Dictionary<string, IActorRef>();

            Receive<UserConnected>(i =>
            {
                if (!_userSessions.TryGetValue(i.UserId, out var userActorRef))
                {
                    try
                    {
                        userActorRef = Context.ActorOf(Props.Create<UserActor>(i.UserId, userHubContext));
                        _userSessions.Add(i.UserId, userActorRef);

                        Sender.Tell(true);
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"Failed to create the UserActor!\nMessage:{ex}");
                    }
                    finally
                    {
                        Sender.Tell(false);
                    }
                }
            });

            ReceiveAsync<UserDisconnected>(async i =>
            {
                if (_userSessions.TryGetValue(i.UserId, out var userActorRef))
                {
                    try
                    {
                        await userActorRef.GracefulStop(TimeSpan.FromSeconds(30));
                        _userSessions.Remove(i.UserId);

                        Sender.Tell(true);
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"Failed to dispose the UserActor!\nMessage:{ex}");
                    }
                    finally
                    {
                        Sender.Tell(false);
                    }
                }
            });

            Receive<GetUserActorRef>(i =>
            {
                if (!_userSessions.TryGetValue(i.UserId, out var userActorRef))
                {
                    try
                    {
                        userActorRef = Context.ActorOf(Props.Create<UserActor>(i.UserId, userHubContext));
                        _userSessions.Add(i.UserId, userActorRef);
                    }
                    catch (Exception ex)
                    {
                        _log.Error("Failed to create the UserActor!");
                    }
                }

                Sender.Tell(userActorRef);
            });

            Receive<TestPing>(i =>
            {
                foreach (var user in _userSessions)
                {
                    user.Value.Tell(new UserActor.UpdateProfile());
                }
            });
        }
    }
}
