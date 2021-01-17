using System.Text;
using ArmaForces.ArmaServerManager.Discord.Configuration;
using CSharpFunctionalExtensions;
using RestSharp;

namespace ArmaForces.ArmaServerManager.Discord.Features.ServerConfig
{
    internal class ConfigurationManagerClient : ManagerClientBase, IConfigurationManagerClient
    {
        private string ConfigurationApiPath { get; } = "api/configuration";

        public ConfigurationManagerClient(IManagerConfiguration config) : base(config)
        {
        }

        public Result<string> GetServerConfiguration()
        {
            var resource = string.Join(
                '/',
                ConfigurationApiPath,
                "server");
            var restRequest = new RestRequest(resource, Method.GET);

            var response = ManagerClient.Execute(restRequest);
            return response.IsSuccessful
                ? Result.Success(response.Content)
                : ReturnFailureFromResponse<string>(response);
        }

        public Result<string> GetModsetConfiguration(string modsetName)
        {
            var resource = string.Join(
                '/',
                ConfigurationApiPath,
                "modset",
                modsetName);
            var restRequest = new RestRequest(resource, Method.GET);

            var response = ManagerClient.Execute(restRequest);
            return response.IsSuccessful
                ? Result.Success(response.Content)
                : ReturnFailureFromResponse<string>(response);
        }

        public Result<string> PutServerConfiguration(string configContent)
        {
            var resource = string.Join(
                '/',
                ConfigurationApiPath,
                "server");

            var restRequest = new RestRequest(resource, Method.PUT);

            restRequest.AddFileBytes(
                "file",
                Encoding.UTF8.GetBytes(configContent),
                "config.json",
                "application/json");

            var response = ManagerClient.Execute<string>(restRequest);

            return response.IsSuccessful
                ? Result.Success(response.Content)
                : ReturnFailureFromResponse<string>(response);
        }

        public Result<string> PutModsetConfiguration(string modsetName, string configContent)
        {
            var resource = string.Join(
                '/',
                ConfigurationApiPath,
                "modset",
                modsetName);

            var restRequest = new RestRequest(resource, Method.PUT);

            restRequest.AddFileBytes(
                "file",
                Encoding.UTF8.GetBytes(configContent),
                "config.json",
                "application/json");

            var response = ManagerClient.Execute<string>(restRequest);

            return response.IsSuccessful
                ? Result.Success(response.Content)
                : ReturnFailureFromResponse<string>(response);
        }
    }
}
