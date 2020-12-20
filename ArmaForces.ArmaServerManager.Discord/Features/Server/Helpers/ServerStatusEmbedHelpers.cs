using System.Collections.Generic;
using ArmaForces.ArmaServerManager.Discord.Features.Server.DTOs;
using Discord;

namespace ArmaForces.ArmaServerManager.Discord.Features.Server.Helpers
{
    internal class ServerStatusEmbedHelpers
    {
        public static Embed CreateServerStatusEmbed(ServerStatus serverStatus)
        {
            if (!serverStatus.IsServerRunning && !serverStatus.IsServerStarting)
            {
                return new EmbedBuilder
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
            }

            if (serverStatus.IsServerStarting)
            {
                return new EmbedBuilder
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
                            Value = string.IsNullOrWhiteSpace(serverStatus.ModsetName) ? "No modset" : serverStatus.ModsetName
                        }
                    }
                }.Build();
            }

            var embedBuilder = new EmbedBuilder
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
                        Value = string.IsNullOrWhiteSpace(serverStatus.ModsetName) ? "No modset" : serverStatus.ModsetName
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
            };

            return embedBuilder.Build();
        }
    }
}
