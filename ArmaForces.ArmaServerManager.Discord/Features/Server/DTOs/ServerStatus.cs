﻿namespace ArmaForces.ArmaServerManager.Discord.Features.Server.DTOs
{
    public class ServerStatus
    {
        public int? HeadlessClientsConnected { get; set; }

        public bool IsServerStarting { get; set; }
        
        public bool IsServerRunning { get; set; }

        public string ModsetName { get; set; }

        public string Name { get; set; }

        public string Map { get; set; }

        public int Players { get; set; }

        public int PlayersMax { get; set; }

        public int Port { get; set; }
    }
}
