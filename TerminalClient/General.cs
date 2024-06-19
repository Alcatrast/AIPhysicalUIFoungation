using Configuration.List;
using TerminalClient.Devices.Internet;

namespace TerminalClient
{
    public static class General
    {
        public static ATWebSocketClient NetClient { get; private set; } = new(ForNetTextAPIUrls.CSLocalhost);
    }
}
namespace Configuration.List
{
    internal class ForNetTextAPIUrls
    {
        public static string Server { get; } = @"ws://[::]:80/";
        public static string Client { get; } = @"ws://***.***.***.***:****/";
        public static string CSLocalhost { get; } = @"ws://localhost:4676/";
    }
}
