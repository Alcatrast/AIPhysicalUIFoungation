using Android.OS;
namespace ATMauiAudioRecorder;
internal static class DPAI
{
    public static string GetDownloadsFolderPath()
    {
        return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).Path;
    }
}