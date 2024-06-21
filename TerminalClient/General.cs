using ATNetAPI.Configuration.List;
using TerminalClient.Devices.Internet;

namespace TerminalClient
{
    public static class General
    {
        public static ATWebSocketClient NetClient { get; private set; } = new(ForNetTextAPIUrls.Client);
        public static MessageSorter MessageSorter { get; private set; } = new(NetClient);
        public static class AutoReconnectSystem
        {
            private static ATWebSocketClient net;
            private static Thread thread;
            public static bool IsConnected { get; private set; } = true;
            static AutoReconnectSystem()
            {
                net = General.NetClient;
                thread = new Thread(WatchingNetState);
                thread.IsBackground = true;
                thread.Start();
            }
            private static void WatchingNetState()
            {
                while (true)
                {
                    IsConnected = net.IsConnected;
                    if (IsConnected == false) net.Connect();
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

