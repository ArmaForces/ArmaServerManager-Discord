using System.IO;
using System.Text;

namespace ArmaForces.ArmaServerManager.Discord.Features.ServerConfig.Helpers
{
    internal class StreamHelpers
    {
        public static Stream CreateStreamFromJsonString(string jsonString)
        {
            var bytes = Encoding.UTF8.GetBytes(jsonString);

            return new MemoryStream(bytes);
        }
    }
}
