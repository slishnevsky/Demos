using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var clientToken = await GetClientTokenAsync();
            CallApi(clientToken);

            var userToken = await GetUserToken();
            CallApi(userToken);

            Console.ReadKey();
        }

        //static TokenResponse GetClientTokenAsync()
        //{
        //    var client = new TokenClient(
        //        "http://localhost:5000/connect/token",
        //        "silicon",
        //        "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
        //    return client.RequestClientCredentialsAsync("api1").Result;
        //}

        //static TokenResponse GetClientTokenAsync()
        //{
        //    var client = new TokenClient(
        //        "http://localhost:5000/connect/token",
        //        "silicon",
        //        "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
        //    return client.RequestClientCredentialsAsync("api1").Result;
        //}

        static async Task<TokenResponse> GetClientTokenAsync()
        {
            var client = new HttpClient();
            var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "silicon",
                ClientSecret = "F621F470-9731-4A25-80EF-67A6F7C5F4B8",
                Scope = "api1"
            };
            var response = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);
            return response;
        }

        static async Task<TokenResponse> GetUserToken()
        {
            var client = new HttpClient();
            var passwordTokenRequest = new PasswordTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "carbon",
                ClientSecret = "21B5F798-BE55-42BC-8AA8-0025B903DC3B",
                UserName = "bob",
                Password = "secret",
                Scope = "api1"
            };
            var response = await client.RequestPasswordTokenAsync(passwordTokenRequest);
            return response;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);
            var result = client.GetStringAsync("http://localhost:50946/test").Result;
            Console.WriteLine(result);
        }
    }
}
