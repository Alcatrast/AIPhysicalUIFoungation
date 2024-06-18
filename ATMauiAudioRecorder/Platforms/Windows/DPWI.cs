namespace ATMauiAudioRecorder;
internal static class DPWI
{
    public static string GetDownloadsFolderPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    }
}