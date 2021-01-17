using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ArmaForces.ArmaServerManager.Discord.Features
{
    public abstract class ManagerModuleBase : ModuleBase<SocketCommandContext>
    {
        protected async Task<IUserMessage> ReplyAsyncTruncate(string message)
        {
            return message.Length > 2000
                ? await ReplyAsync(message.Remove(2000))
                : await ReplyAsync(message);
        }
    }
}
