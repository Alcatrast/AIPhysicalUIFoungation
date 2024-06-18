
namespace ATMauiAudioRecorder.Abstractions;
public interface IAudioRecorder
{
    bool CanRecordAudio { get; }
    bool IsRecording { get; }
    string FilePath { get; set; }
    Task StartRecordAsync();
    Task<Audio> StopRecordAsync();
    void Dispose();
}