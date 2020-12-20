using System;

namespace ArmaForces.ArmaServerManager.Discord.Features.Mods.DTOs
{
    public class ModsUpdateRequest
    {
        public string ModsetName { get; set; }

        public DateTime? ScheduleAt { get; set; }
    }
}
