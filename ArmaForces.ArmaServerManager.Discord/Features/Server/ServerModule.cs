using System;
using System.Threading.Tasks;
using ArmaForces.ArmaServerManager.Discord.Features.Server.Extensions;
using ArmaForces.ArmaServerManager.Discord.Features.Server.Helpers;
using CSharpFunctionalExtensions;
using Discord.Commands;

namespace ArmaForces.ArmaServerManager.Discord.Features.Server
{
    public class ServerModule : ManagerModuleBase
    {
        private readonly IServerManagerClient _serverManagerClient;

        public ServerModule(IServerManagerClient serverManagerClient)
        {
            _serverManagerClient = serverManagerClient;
        }

        [Command("startServer")]
        [Summary("Allows to start the server with given modset. Eg. '!startServer default'.")]
        public virtual async Task StartServer(string modsetName)
            => await StartServer(modsetName, null);

        [Command("startServer")]
        [Summary("Allows to start the server with given modset at given dateTime. Eg. '!startServer default 2020-07-17T19:00'.")]
        public virtual async Task StartServer(string modsetName, DateTime? dateTime)
        {
            var result = _serverManagerClient.RequestStartServer(modsetName, dateTime);

            await result.Match(
                onSuccess: () => ReplyAsync($"Server startup scheduled {(dateTime.HasValue ? $"at {dateTime.Value}" : "now")}."),
                onFailure: ReplyAsyncTruncate);
        }

        [Command("serverStatus")]
        [Summary("Allows to check server status. Returns one of 3 embeds for stopped, starting or running server.")]
        public virtual async Task ServerStatus()
        {
            var result = _serverManagerClient.GetServerStatus();

            await result.Match(
                onSuccess: serverStatus => ReplyAsync(embed: ServerStatusEmbedHelpers.CreateServerStatusEmbed(serverStatus)),
                onFailure: ReplyAsyncTruncate);
        }
    }
}
