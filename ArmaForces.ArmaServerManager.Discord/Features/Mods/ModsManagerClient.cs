using System;
using System.Threading.Tasks;
using ArmaForces.ArmaServerManager.Discord.Configuration;
using ArmaForces.ArmaServerManager.Discord.Features.Mods.DTOs;
using CSharpFunctionalExtensions;
using RestSharp;

namespace ArmaForces.ArmaServerManager.Discord.Features.Mods
{
    internal class ModsManagerClient : ManagerClientBase, IModsManagerClient
    {
        private string ModsApiPath { get; } = "api/mods";

        public ModsManagerClient(IManagerConfiguration config) : base(config)
        {
        }

        public async Task<Result> UpdateMods(string modsetName, DateTime? scheduleAt)
        {
            var resource = string.Join(
                '/',
                ModsApiPath,
                "update");
            var restRequest = new RestRequest(resource, Method.POST);
            var modsUpdateRequest = new ModsUpdateRequest
            {
                ModsetName = modsetName,
                ScheduleAt = scheduleAt
            };
            restRequest.AddJsonBody(modsUpdateRequest);

            var response = await ManagerClient.ExecuteAsync(restRequest);
            return response.IsSuccessful
                ? Result.Success(response)
                : ReturnFailureFromResponse(response);
        }
    }
}
