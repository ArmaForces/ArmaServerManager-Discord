using System;
using System.Collections.Generic;
using ArmaForces.ArmaServerManager.Discord.Features.Server.DTOs;
using Discord;

namespace ArmaForces.ArmaServerManager.Discord.Features.Server.Helpers
{
    internal static class ServerStatusEmbedHelpers
    {
        public static Embed CreateServerStatusEmbed(ServerStatus serverStatus)
        {
            return serverStatus.Status switch
            {
                ServerStatusEnum.Stopped => BuildEmbedForStopped(serverStatus),
                ServerStatusEnum.Starting => BuildEmbedForStarting(serverStatus),
                ServerStatusEnum.Started => BuildEmbedForStarted(serverStatus),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static Embed BuildEmbedForStopped(ServerStatus serverStatus) => new EmbedBuilder
        {
            Title = "Server Status",
            Fields = new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = $"server:{serverStatus.Port}",
                    Value = ":x: Server unavailable"
                }
            }
        }.Build();

        private static Embed BuildEmbedForStarting(ServerStatus serverStatus) => new EmbedBuilder
        {
            Title = "Server Status",
            Fields = new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = $"server:{serverStatus.Port}",
                    Value = ":arrows_counterclockwise: Server starting"
                },
                new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = "Modset name",
                    Value = string.IsNullOrWhiteSpace(serverStatus.ModsetName)
                        ? "No modset"
                        : serverStatus.ModsetName
                }
            }
        }.Build();

        private static Embed BuildEmbedForStarted(ServerStatus serverStatus) => new EmbedBuilder
        {
            Title = "Server Status",
            Fields = new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = $"server:{serverStatus.Port}",
                    Value = $@":white_check_mark: Server ""{serverStatus.Name}"" online"
                },
                new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = "Players online",
                    Value = $"{serverStatus.Players}/{serverStatus.PlayersMax}"
                },
                new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = "Modset name",
                    Value = string.IsNullOrWhiteSpace(serverStatus.ModsetName)
                        ? "No modset"
                        : serverStatus.ModsetName
                },
                new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = "Current map",
                    Value = string.IsNullOrWhiteSpace(serverStatus.Map) ? "No map" : serverStatus.Map
                },
                new EmbedFieldBuilder
                {
                    IsInline = false,
                    Name = "Connected Headless Clients",
                    Value = serverStatus.HeadlessClientsConnected ?? 0
                }
            }
        }.Build();
    }
}
