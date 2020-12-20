using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArmaForces.ArmaServerManager.Discord.Features.Mods;
using ArmaForces.ArmaServerManager.Discord.Features.Server;
using ArmaForces.ArmaServerManager.Discord.Features.ServerConfig;
using Discord.Commands;

namespace ArmaForces.ArmaServerManager.Discord.Extensions
{
    public static class CommandServiceExtensions
    {
        public static async Task<IEnumerable<ModuleInfo>> AddServerManagerModules(
            this CommandService commandService,
            IServiceProvider serviceProvider)
        {
            var modulesInfo = new List<ModuleInfo>
            {
                await commandService.AddModuleAsync<ModsModule>(serviceProvider),
                await commandService.AddModuleAsync<ServerModule>(serviceProvider),
                await commandService.AddModuleAsync<ServerConfigModule>(serviceProvider)
            };

            return modulesInfo;
        }
    }
}
