using System.Threading;
using System.Threading.Tasks;
using ArmaForces.ArmaServerManager.Discord.Bot.Constants;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ArmaForces.ArmaServerManager.Discord.Bot.Services
{
    internal class StartupService : IHostedService
    {
        private readonly DiscordSocketClient _client;
        private readonly string _botToken;

        private bool _botStarted = false;

        public StartupService(DiscordSocketClient client, IConfiguration configuration)
        {
            _client = client;
            _botToken = configuration[ConfigurationKeyConstants.BotToken];
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Ready += WelcomeAsync;

            await _client.LoginAsync(TokenType.Bot, _botToken);
            await _client.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            return;
        }

        private async Task WelcomeAsync()
        {
            if (_botStarted) return;
            
            _botStarted = true;
        }
    }
}
