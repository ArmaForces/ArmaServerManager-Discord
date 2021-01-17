using System;

namespace ArmaForces.ArmaServerManager.Discord.Features.Mods.DTOs
{
    internal class ModsUpdateRequest
    {
        public string ModsetName { get; set; }

        public DateTime? ScheduleAt { get; set; }
    }
}
