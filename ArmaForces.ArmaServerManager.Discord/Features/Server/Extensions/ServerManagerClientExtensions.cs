using System;
using ArmaForces.ArmaServerManager.Discord.Features.Server.DTOs;
using CSharpFunctionalExtensions;

namespace ArmaForces.ArmaServerManager.Discord.Features.Server.Extensions
{
    public static class ServerManagerClientExtensions
    {
        public static Result RequestStartServer(
            this IServerManagerClient serverManagerClient,
            string modsetName,
            DateTime? dateTime)
        {
            var request = new ServerStartRequest
            {
                ModsetName = modsetName,
                ScheduleAt = dateTime
            };

            return serverManagerClient.RequestStartServer(request);
        }
    }
}
