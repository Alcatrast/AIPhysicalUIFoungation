using Configuration.List;
using Microsoft.Maui.Controls.PlatformConfiguration;
using TerminalClient.Devices.Internet;
//using Android.OS;
//using Android.App;
//using Android.Content;
namespace TerminalClient
{
    public static class General
    {
        private static string fileName = "termrecordedaudio.wav";
        public static string StorageMicroPenis= Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath, fileName);
        public static ATWebSocketClient NetClient { get; private set; } = new(ForNetTextAPIUrls.Client);
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
