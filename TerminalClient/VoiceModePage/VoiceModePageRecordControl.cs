using ATMauiAudioRecorder.Abstractions;

namespace TerminalClient;
public partial class VoiceModePage : ContentPage
{
    public static byte[] outAudioPath;
    private readonly IAudioRecorder recorder;
    public void StartRecord()
    {
      //  outAudioPath = recorder.FilePath;
        recorder.StartRecordAsync();
    }

    public async void StopRecord()
    {
        outAudioPath=( await recorder.StopRecordAsync()).GetFilePath();

        if (_isCancelled == false)
        {
            HandleVoiceCommand();
        }
    }
}