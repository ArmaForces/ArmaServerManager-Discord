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
        [Summary("Pozwala zaplanować aktualizację wszystkich modyfikacji lub wybranego modsetu.")]
        public async Task UpdateMods(DateTime? scheduleAt = null)
            => await UpdateMods(null, scheduleAt);

        [Command("updateMods")]
        [Summary("Pozwala zaplanować aktualizację wszystkich modyfikacji lub wybranego modsetu.")]
        public async Task UpdateMods(string modsetName = null, DateTime? scheduleAt = null)
        {
            var result = await _modsManagerClient.UpdateMods(modsetName, scheduleAt);

            await result.Match(
                onSuccess: () => ReplyAsync($"Aktualizacja modyfikacji {modsetName} zaplanowana na {scheduleAt ?? DateTime.Now}"),
                onFailure: ReplyAsyncTruncate);
        }
    }
}
