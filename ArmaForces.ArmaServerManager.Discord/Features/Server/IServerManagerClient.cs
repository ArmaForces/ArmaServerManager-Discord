using ArmaForces.ArmaServerManager.Discord.Features.Server.DTOs;
using CSharpFunctionalExtensions;

namespace ArmaForces.ArmaServerManager.Discord.Features.Server
{
    public interface IServerManagerClient
    {
        Result<ServerStatus> GetServerStatus();

        Result RequestStartServer(ServerStartRequest serverStartRequest);
    }
}
