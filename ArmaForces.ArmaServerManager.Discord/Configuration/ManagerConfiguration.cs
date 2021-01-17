using ArmaForces.ArmaServerManager.Discord.Configuration.Constants;
using Microsoft.Extensions.Configuration;

namespace ArmaForces.ArmaServerManager.Discord.Configuration
{
    internal class ManagerConfiguration : IManagerConfiguration
    {
        public string ServerManagerUrl { get; }

        public string ServerManagerApiKey { get; }

        public ManagerConfiguration(IConfiguration configuration)
        {
            ServerManagerUrl = configuration[ConfigurationKeyConstants.ServerManagerUrl];

            ServerManagerApiKey = configuration[ConfigurationKeyConstants.ServerManagerApiKey];
        }
    }
}
