﻿using System;

namespace ArmaForces.ArmaServerManager.Discord.Features.Server.DTOs
{
    public class ServerStartRequest
    {
        public string ModsetName { get; set; }

        public DateTime? ScheduleAt { get; set; }

        public int Port { get; set; }
    }
}
