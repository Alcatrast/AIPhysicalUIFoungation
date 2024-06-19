using ATMauiAudioRecorder.Abstractions;

namespace TerminalClient;
public partial class VoiceModePage : ContentPage
{
    public static string outAudioPath;
    private readonly IAudioRecorder recorder;
    public void StartRecord()
    {
        outAudioPath = recorder.FilePath;
        recorder.StartRecordAsync();
    }

    public void StopRecord()
    {
        recorder.StopRecordAsync();

        if (_isCancelled == false)
        {
            HandleVoiceCommand();
        }
    }
}