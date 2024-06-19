using ATNetAPI.Configuration.List;
using TerminalClient.Devices.Internet;

namespace TerminalClient
{
    public static class General
    {
        public static ATWebSocketClient NetClient { get; private set; } = new(ForNetTextAPIUrls.Client);
    }
}

