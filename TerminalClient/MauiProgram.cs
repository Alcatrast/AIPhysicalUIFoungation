using Microsoft.Extensions.Logging;
using ATMauiAudioRecorder.Hosting;

namespace TerminalClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseAudioRecorder()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddTransient<VoiceModePage>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
