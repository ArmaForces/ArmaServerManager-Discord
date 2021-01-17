using ArmaForces.ArmaServerManager.Discord.Configuration;
using RestSharp;
using RestSharp.Authenticators;

namespace ArmaForces.ArmaServerManager.Discord.Features
{
    internal class ApiKeyAuthenticator : IAuthenticator
    {
        private readonly IManagerConfiguration _config;
        private const string HeaderField = "ApiKey";

        public ApiKeyAuthenticator(IManagerConfiguration config)
        {
            _config = config;
        }

        public void Authenticate(IRestClient client, IRestRequest request) => 
            request.AddHeader(HeaderField, _config.ServerManagerApiKey);
    }
}
