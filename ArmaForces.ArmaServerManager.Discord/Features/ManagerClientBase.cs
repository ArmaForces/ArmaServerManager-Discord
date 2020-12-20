using System;
using ArmaForces.ArmaServerManager.Discord.Configuration;
using CSharpFunctionalExtensions;
using RestSharp;

namespace ArmaForces.ArmaServerManager.Discord.Features
{
    public abstract class ManagerClientBase
    {
        private readonly IManagerConfiguration _configuration;
        protected IRestClient ManagerClient { get; set; }

        protected ManagerClientBase(IManagerConfiguration configuration)
        {
            _configuration = configuration;

            ManagerClient = CreateRestClient(_configuration.ServerManagerUrl);
        }

        protected static Result ReturnFailureFromResponse(IRestResponse restResponse)
        {
            // It is not always false.
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return restResponse.ErrorMessage is null && restResponse.ErrorException is null
                ? Result.Failure($"{restResponse.StatusCode}: {restResponse.Content}")
                : Result.Failure($"{restResponse.StatusCode}: {restResponse.Content}: {restResponse.ErrorException.Message} | Exception: {restResponse.ErrorException.Source} \n {restResponse.ErrorException.StackTrace}");
        }

        protected static Result<T> ReturnFailureFromResponse<T>(IRestResponse restResponse)
        {
            // It is not always false.
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return restResponse.ErrorMessage is null && restResponse.ErrorException is null
                ? Result.Failure<T>($"{restResponse.StatusCode}: {restResponse.Content}")
                : Result.Failure<T>($"{restResponse.StatusCode}: {restResponse.Content}: {restResponse.ErrorMessage} | Exception: {restResponse.ErrorException}");
        }

        private IRestClient CreateRestClient(string url)
        {
            return new RestClient
            {
                BaseUrl = new Uri(url),
                Authenticator = new ApiKeyAuthenticator(_configuration)
            };
        }
    }
}
