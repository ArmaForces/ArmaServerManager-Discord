using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ArmaForces.ArmaServerManager.Discord.Features.Mods
{
    public interface IModsManagerClient
    {
        Task<Result> UpdateMods(string modsetName, DateTime? scheduleAt);
    }
}
