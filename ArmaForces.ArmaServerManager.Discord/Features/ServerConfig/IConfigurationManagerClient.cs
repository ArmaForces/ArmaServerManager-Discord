using CSharpFunctionalExtensions;

namespace ArmaForces.ArmaServerManager.Discord.Features.ServerConfig
{
    public interface IConfigurationManagerClient
    {
        Result<string> GetServerConfiguration();

        Result<string> GetModsetConfiguration(string modsetName);

        Result<string> PutServerConfiguration(string configContent);

        Result<string> PutModsetConfiguration(string modsetName, string configContent);
    }
}
