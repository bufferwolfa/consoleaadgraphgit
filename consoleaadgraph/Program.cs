using System;
using System.Collections.Generic;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using System.Threading.Tasks;
using System.Security;

namespace consoleaadgraph
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string clientId = "<clientId>";
            string tenantId = "<tenantId>";
            string clientSecret = "<clientSecret>";

            // Build a client application.​
            var clientApplication = PublicClientApplicationBuilder.Create(clientId);
            clientApplication = clientApplication.WithTenantId(tenantId);

            // Create an authentication provider by username password.​
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();
            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            
            // Create a new instance of GraphServiceClient with the authentication provider.​
            GraphServiceClient graphClient = new GraphServiceClient(authProvider​);

            // Makes a request to https://graph.microsoft.com/v1.0/me​
            var users = await graphClient.Users.Request().GetAsync();
            foreach (var user in users)
            {
                Console.WriteLine($"{user.DisplayName}");
            }
        }
    }
}
