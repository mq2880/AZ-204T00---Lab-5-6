using System;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

namespace GraphClient
{
    public class Program
    {
        private const string _clientId = "c1cc3002-2cf5-4664-b82b-b0e71b5a60fc";
        private const string _tenantId = "931a21ef-b8f6-4580-a57a-5a0fedb7f4e9";

        public static async Task Main(string[] args)
        {
            IPublicClientApplication app;
            app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build();

            List<string> scopes = new List<string>
            {
                "user.read"
            };

            /* AuthenticationResult result;
             result = await app
                     .AcquireTokenInteractive(scopes)
                     .ExecuteAsync();

             Console.WriteLine($"Token:\t{result.AccessToken}");*/

            DeviceCodeProvider provider = new DeviceCodeProvider(app, scopes);
            GraphServiceClient client = new GraphServiceClient(provider);
            User myProfile = await client.Me
                            .Request()
                            .GetAsync();
            Console.WriteLine($"Name:\t{myProfile.DisplayName}");
            Console.WriteLine($"AAD Id:\t{myProfile.Id}");
        }
    }
}
