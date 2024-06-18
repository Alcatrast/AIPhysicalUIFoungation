using ATMauiAudioRecorder.Abstractions;
using ATMauiAudioRecorder;


[assembly: XmlnsDefinition("https://schemas.math3ussdl.com/dotnet/2023/maui", "ATMauiAudioRecorder")]
namespace ATMauiAudioRecorder.Hosting;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseAudioRecorder(this MauiAppBuilder builder)
    {
#if ANDROID
        builder.Services.AddSingleton<IAudioRecorder, AudioRecorderAndroidImpl>();
#endif
#if WINDOWS
        builder.Services.AddSingleton<IAudioRecorder, AudioRecorderWindowsImpl>();
#endif
        return builder;
    }
}