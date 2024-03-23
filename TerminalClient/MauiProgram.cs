using Plugin.Maui.Audio;
using Microsoft.Extensions.Logging;
using Plugin.Maui.AudioRecorder.Hosting;
using MauiAudio;

namespace TerminaClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseAudioRecorder()
              //  .UseMauiAudio()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(AudioManager.Current);
            return builder.Build();
        }
    }
}
