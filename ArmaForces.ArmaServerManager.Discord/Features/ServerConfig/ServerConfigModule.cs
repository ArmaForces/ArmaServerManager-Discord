using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ArmaForces.ArmaServerManager.Discord.Features.ServerConfig.Helpers;
using CSharpFunctionalExtensions;
using Discord.Commands;

namespace ArmaForces.ArmaServerManager.Discord.Features.ServerConfig
{
    public class ServerConfigModule : ManagerModuleBase
    {
        private readonly IConfigurationManagerClient _configurationManagerClient;

        public ServerConfigModule(IConfigurationManagerClient configurationManagerClient)
        {
            _configurationManagerClient = configurationManagerClient;
        }

        [Command("modsetConfig")]
        [Summary("Downloads modset server config. Eg. !modsetConfig default")]
        public virtual async Task GetModsetConfig(string modsetName)
        {
            var result = _configurationManagerClient.GetModsetConfiguration(modsetName);

            await result.Match(
                onSuccess: config => ReplyWithConfigContent(config, modsetName),
                onFailure: ReplyAsyncTruncate);
        }

        [Command("putModsetConfig")]
        [Summary("Uploads modset server config. Config can be provided as attached *.json file or as a JSON string after modsetName.")]
        public virtual async Task PutModsetConfig(string modsetName, [Remainder] string configContent = null)
        {
            if (Context.Message.Attachments.Any(x => x.Filename.EndsWith(".json")))
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(
                    Context.Message.Attachments
                        .First(x => x.Filename.EndsWith(".json"))
                        .Url);
                configContent = await response.Content.ReadAsStringAsync();
            }

            if (configContent is null)
            {
                await ReplyAsync("No configuration specified or attached file has incorrect extension. Only *.json files are allowed.");
                return;
            }

            var result = _configurationManagerClient.PutModsetConfiguration(modsetName, configContent);

            await result.Match(
                onSuccess: modset => ReplyAsync($"Configuration for {modset} modset updated."),
                onFailure: ReplyAsyncTruncate);
        }

        [Command("serverConfig")]
        [Summary("Downloads main server config.")]
        public virtual async Task GetServerConfig()
        {
            var result = _configurationManagerClient.GetServerConfiguration();

            await result.Match(
                onSuccess: config => ReplyWithConfigContent(config),
                onFailure: ReplyAsyncTruncate);
        }

        [Command("putServerConfig")]
        [Summary("Uploads main server config.")]
        public virtual async Task PutServerConfig([Remainder] string configContent = null)
        {
            if (Context.Message.Attachments.Any(x => x.Filename.EndsWith(".json")))
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(
                    Context.Message.Attachments
                        .First(x => x.Filename.EndsWith(".json"))
                        .Url);
                configContent = await response.Content.ReadAsStringAsync();
            }

            if (configContent is null)
            {
                await ReplyAsync("No configuration specified or attached file has incorrect extension. Only *.json files are allowed.");
                return;
            }

            var result = _configurationManagerClient.PutServerConfiguration(configContent);

            await result.Match(
                onSuccess: modset => ReplyAsync($"Configuration for {modset} modset updated."),
                onFailure: ReplyAsyncTruncate);
        }

        private async Task ReplyWithConfigContent(string configString, string modsetName = "server")
        {
            if (configString.Length > 1900)
            {
                await Context.Channel.SendFileAsync(
                    StreamHelpers.CreateStreamFromJsonString(configString),
                    $"{modsetName}-config.json",
                    $"Config for {modsetName} is too long to fit in message. Please see attached file.");
                return;
            }

            await ReplyAsync($"Config for {modsetName}:\n```json\n{configString}\n```");
        }
    }
}
