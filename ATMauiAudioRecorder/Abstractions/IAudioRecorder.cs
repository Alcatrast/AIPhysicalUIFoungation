
namespace ATMauiAudioRecorder.Abstractions;
public interface IAudioRecorder
{
    bool CanRecordAudio { get; }
    bool IsRecording { get; }
    byte[] AudioDataWav { get; }
    Task StartRecordAsync();
    Task<Audio> StopRecordAsync();
    void Dispose();
}