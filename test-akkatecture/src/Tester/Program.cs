using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tester
{
    public class Program
    {
        public class UserProfileViewModel
        {
            public string Name { get; set; }

            public string Email { get; set; }
        }

        public class SignInResultViewModel
        {
            public string UserId { get; set; }

            public string AuthToken { get; set; }
        }

        private static string _authToken = "";
        private static string _hubUrl = "http://localhost:5000/UserHub";
        private static string _authUrl = "http://localhost:5000";

        private static HubConnection _hubConnection;

        private static UserProfileViewModel _userProfile;

        public static async Task Main(string[] args)
        {
            //var walletSyncTester = new WalletSyncTester();

            //walletSyncTester.UpdateWallet();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();

            _authToken = await Authenticate(_authUrl);

            Console.WriteLine("Authenticate Done! Press any key to continue...");
            Console.ReadLine();

            //_userProfile = new UserProfileViewModel
            //{
            //    Email = "",
            //    Name = "",
            //};

            Console.WriteLine($"Creating HubConnection with JWT:{_authToken}");
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(_authToken);
                })
                .Build();

            _hubConnection.Closed += async error =>
            {
                Console.WriteLine($"{_hubUrl} is disconnected");

                //await Task.Delay(1000);

                //await Connect();
            };

            await Connect();

            var result = await _hubConnection.InvokeAsync<string>("CreateWalletAndOpenForexAccount");

            Console.WriteLine($"Test Result:{result}");

            await Task.Delay(3000);
            result = await _hubConnection.InvokeAsync<string>("CreateFCTransaction", "forexaccount-fb702292-a46a-489e-aa14-c7c3ff5a4c3b", "wallet-344ccae9-a97c-4eed-8862-86f88d21650c");

            Console.WriteLine($"Test Result:{result}");

            await Task.Delay(3000);
            result = await _hubConnection.InvokeAsync<string>("CreateFCTransaction", "forexaccount-fb702292-a46a-489e-aa14-c7c3ff5a4c3b", "wallet-344ccae9-a97c-4eed-8862-86f88d21650c");

            Console.WriteLine($"Test Result:{result}");

            await Task.Delay(3000);
            result = JsonConvert.SerializeObject(await _hubConnection.InvokeAsync<object>("GetAllFCTransactions", "wallet-344ccae9-a97c-4eed-8862-86f88d21650c"));

            Console.WriteLine($"Query Result:{result}");

            //GetAllFCTransactions

            //_hubConnection.On<UserProfileViewModel>("UpdateUserProfile", (i) =>
            //{
            //    _userProfile = i;

            //    Console.WriteLine($"UserProfile Updated: Name:{_userProfile.Name}, Email:{_userProfile.Email}");
            //});

            //_userProfile = await _hubConnection.InvokeAsync<UserProfileViewModel>("SubscribeUserProfile", "12345678");

            //Console.WriteLine($"UserProfile Initialized:{_userProfile.Email}, {_userProfile.Name}");

            //Console.WriteLine("Press any key to disconnect...");
            //Console.ReadLine();

            //await _hubConnection.StopAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static async Task Connect()
        {
            Console.WriteLine($"Connecting to {_hubUrl}...");

            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine($"Connected to {_hubUrl}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task<string> Authenticate(string authUrl)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(authUrl),
            };

            // POST content that will be sent with our Create token request
            var post = new Dictionary<string, string>
            {
                { "email", "admin@fraction.co" },
                { "password", "password" }
            };

            // Create and POST request, and call our Create token route to generate a new JWT Bearer Token
            var content = new FormUrlEncodedContent(post);
            var response = await client.PostAsync("/token/signin", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var signInResult = JsonConvert.DeserializeObject<SignInResultViewModel>(responseString);

            Console.WriteLine(response);
            Console.WriteLine($"AUTHENTICATED: UserId:{signInResult.UserId}\nJWT:{signInResult.AuthToken}");

            #region ONLY NEEDED FOR HTTP REQUESTS, NOTHING TO DO WITH SIGNALR

            //// Let's call an Authorized APIController route, if our JWT Bearer Token is valid, we should get a response
            //Console.WriteLine("Trying authorized route...");

            //// Ensure we update the HttpClient with our newly minted JWT
            //client.SetBearerToken(signInResult.AuthToken);

            //// Call the secured route, and write the response content into a string
            //content = new FormUrlEncodedContent(new Dictionary<string, string>());
            //response = await client.PostAsync("token/validatetoken", content);
            //responseString = await response.Content.ReadAsStringAsync();
            //signInResult = JsonConvert.DeserializeObject<SignInResultViewModel>(responseString);

            //Console.WriteLine(response);
            //Console.WriteLine($"VALIDATED: UserId:{signInResult.UserId}\nJWT:{signInResult.AuthToken}");

            #endregion

            return signInResult.AuthToken;
        }
    }
}
