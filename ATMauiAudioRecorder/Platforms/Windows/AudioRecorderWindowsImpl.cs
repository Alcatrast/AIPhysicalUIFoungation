using NAudio.Wave;
using System;
using System.IO;

using ATMauiAudioRecorder.Abstractions;

namespace ATMauiAudioRecorder;

public class AudioRecorderWindowsImpl : IAudioRecorder, IDisposable
{
    private WaveInEvent waveSource;
    private WaveFileWriter waveFile;

    public bool CanRecordAudio { get; private set; } = true;
    public bool IsRecording { get; private set; }
    public byte[] AudioDataWav { get; private set; } = new byte[0];
    private MemoryStream _audioStream;

    public Task StartRecordAsync()
    {
        waveSource = new();
        waveSource.WaveFormat = new WaveFormat(16000, 1); // Задаем формат аудио (44100 Гц, 16 бит, моно)
        _audioStream = new MemoryStream();
        waveFile = new WaveFileWriter(_audioStream, waveSource.WaveFormat);

        waveSource.DataAvailable += (s, e) =>
        {
            waveFile.Write(e.Buffer, 0, e.BytesRecorded);
        };

        waveSource.StartRecording();
        return Task.CompletedTask;
    }

    public async Task<Audio> StopRecordAsync()
    {
        waveSource.StopRecording();
        waveSource.Dispose();
        waveFile.Dispose();
        AudioDataWav = _audioStream.ToArray();
        _audioStream.Close();
        _audioStream.Dispose();
        return new Audio(AudioDataWav);
    }

    public void Dispose()
    {
    }
}