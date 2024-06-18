using Microsoft.Maui.Controls.PlatformConfiguration;
using Plugin.Maui.AudioRecorder;

namespace TerminalClient.Devices.AudioService.MicroRecorders.Android
{
    public interface IRecordAudio
    {
        void StartRecord();
        string StopRecord();
    }

    public class AndroidMicrophoneRecorder : IRecordAudio
    {
        private AudioRecorderAndroidImpl recorder;
        static string storagePath = General.StorageMicroPenis;

        public AndroidMicrophoneRecorder()
        {
            recorder = new();
        }

        public void StartRecord()
        {
            recorder.FilePath = storagePath;
            recorder.StartRecordAsync();
        }
        public string StopRecord()
        {
            recorder.StopRecordAsync();
            return recorder.FilePath;
        }
    }
}