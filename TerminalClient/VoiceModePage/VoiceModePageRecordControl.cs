using TerminaClient.Devices.AudioService.MicroRecorders.Android;

namespace TerminaClient;
public partial class VoiceModePage : ContentPage
{
    public static string outAudioPath = General.StorageMicroPenis;
    AndroidMicrophoneRecorder recorder;
    public void StartRecord()
    {
        recorder = new AndroidMicrophoneRecorder();
        recorder.StartRecord();
    }

    public void StopRecord()
    {
        recorder.StopRecord();

        if (_isCancelled == false)
        {
            HandleVoiceCommand();
        }
    }
}