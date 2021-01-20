using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Discord.Commands;

namespace ArmaForces.ArmaServerManager.Discord.Features.Mods
{
    public class ModsModule : ManagerModuleBase
    {
        private readonly IModsManagerClient _modsManagerClient;

        public ModsModule(IModsManagerClient modsManagerClient)
        {
            _modsManagerClient = modsManagerClient;
        }

        [Command("updateMods")]
        [Summary("Allows to schedule all mods update at given dateTime. Eg. '!updateMods 2020-07-17T19:00'.")]
        public virtual async Task UpdateMods(DateTime? scheduleAt = null)
            => await UpdateMods(null, scheduleAt);

        [Command("updateMods")]
        [Summary("Allows to schedule given modset mods update at given dateTime. Eg. '!updateMods default 2020-07-17T19:00'.")]
        public virtual async Task UpdateMods(string modsetName = null, DateTime? scheduleAt = null)
        {
            var result = await _modsManagerClient.UpdateMods(modsetName, scheduleAt);

            await result.Match(
                onSuccess: () => ReplyAsync($"Mods update of {modsetName} modset is scheduled at {scheduleAt ?? DateTime.Now}"),
                onFailure: ReplyAsyncTruncate);
        }
    }
}
