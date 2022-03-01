using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace IdentityServer4ClientForClientCredentialsFlow
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = CreateConfiguration();

            var clientCredentialConfig =
                ReadClientCredentialConfigs(configuration);

            var client = new RestClient();
            
            var request = new RestRequest(
                clientCredentialConfig.TokenEndPointUrl,
                Method.Post);
            request.AddParameter("grant_type",
                "client_credentials");
            request.AddParameter("client_id", 
                clientCredentialConfig.ClientId);
            request.AddParameter("client_secret",
                clientCredentialConfig.ClientSecret);
           
            
            foreach (var scope in 
                     clientCredentialConfig.ApiScopes)
            {
              request.AddParameter("scope", scope);
            }

            var response = await client.ExecutePostAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert
                    .DeserializeObject<TokenResult>(response.Content);
                
                Console.WriteLine(result.ToString());
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
            }

            Console.ReadKey();
        }

        private static ClientCredentialConfig ReadClientCredentialConfigs(IConfigurationRoot configuration)
        {
            return configuration.GetSection("ClientCredentialsConfig")
                .Get<ClientCredentialConfig>();
        }

        private static IConfigurationRoot CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
        }
    }
}
