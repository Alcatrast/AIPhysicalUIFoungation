using Android.Content;
using Android.Media;
using ATMauiAudioRecorder.Abstractions;
using Java.IO;
using System.Diagnostics;

namespace ATMauiAudioRecorder;

public class AudioRecorderAndroidImpl : IAudioRecorder, IDisposable
{
    public bool CanRecordAudio { get; private set; } = true;
    public bool IsRecording { get; private set; }
    public string FilePath { get; set; } = Path.Combine(DPAI.GetDownloadsFolderPath(), "sound.wav");

    private AudioRecord _audioRecord;

    private string _rawFilePath;
    private int _bufferSize;
    private int _sampleRate;

    public AudioRecorderAndroidImpl()
    {
        Debug.WriteLine("[Plugin.Maui.AudioRecorder] STARTING...");

        var packageManager = Android.App.Application.Context.PackageManager;
        CanRecordAudio =
            packageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureMicrophone);
    }

    private AudioRecord GetAudioRecord(int sampleRate)
    {
        _sampleRate = sampleRate;
        var channelConfig = ChannelIn.Mono;
        var encoding = Encoding.Pcm16bit;

        _bufferSize = AudioRecord.GetMinBufferSize(sampleRate, channelConfig, encoding) * 8;

        return new AudioRecord(AudioSource.Mic, sampleRate, ChannelIn.Mono, encoding, _bufferSize);
    }

    private Audio GetAudio()
    {
        if (_audioRecord is null ||
                _audioRecord.RecordingState is RecordState.Recording ||
                System.IO.File.Exists(FilePath) is false)
        {
            return null;
        }

        return new Audio(FilePath);
    }

    private void WriteAudioDataToFile()
    {
        var data = new byte[_bufferSize];

        var fileName = FilePath.Split("/").Last();
        _rawFilePath = Path.Combine(FileSystem.CacheDirectory, fileName);

        FileOutputStream outputStream;
        try
        {
            outputStream = new FileOutputStream(_rawFilePath);
        }
        catch (Exception ex)
        {
            throw new FileLoadException($"unable to create a new file: {ex.Message}");
        }

        if (outputStream is not null)
        {
            while (_audioRecord.RecordingState == RecordState.Recording)
            {
                _audioRecord.Read(data, 0, _bufferSize);
                outputStream.Write(data);
            }

            outputStream.Close();
        }
    }

    private void CopyWaveFile(string sourcePath, string destinationPath)
    {
        FileInputStream inputStream;
        FileOutputStream outputStream;

        int channels = 1;
        long byteRate = 16 * _sampleRate * channels / 8;

        var data = new byte[_bufferSize];

        try
        {
            inputStream = new FileInputStream(sourcePath);
            outputStream = new FileOutputStream(destinationPath);
            var totalAudioLength = inputStream.Channel.Size();
            var totalDataLength = totalAudioLength + 36;

            WriteWaveFileHeader(
                outputStream,
                totalAudioLength,
                totalDataLength,
                _sampleRate,
                channels,
                byteRate);

            while (inputStream.Read(data) != -1)
            {
                outputStream.Write(data);
            }

            inputStream.Close();
            outputStream.Close();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private static void WriteWaveFileHeader(
        FileOutputStream outputStream,
        long audioLength,
        long dataLength,
        long sampleRate,
        int channels,
        long byteRate)
    {
        byte[] header = new byte[44];

        header[0] = Convert.ToByte('R'); // RIFF/WAVE header
        header[1] = Convert.ToByte('I'); // (byte)'I'
        header[2] = Convert.ToByte('F');
        header[3] = Convert.ToByte('F');
        header[4] = (byte)(dataLength & 0xff);
        header[5] = (byte)((dataLength >> 8) & 0xff);
        header[6] = (byte)((dataLength >> 16) & 0xff);
        header[7] = (byte)((dataLength >> 24) & 0xff);
        header[8] = Convert.ToByte('W');
        header[9] = Convert.ToByte('A');
        header[10] = Convert.ToByte('V');
        header[11] = Convert.ToByte('E');
        header[12] = Convert.ToByte('f'); // fmt chunk
        header[13] = Convert.ToByte('m');
        header[14] = Convert.ToByte('t');
        header[15] = (byte)' ';
        header[16] = 16; // 4 bytes - size of fmt chunk
        header[17] = 0;
        header[18] = 0;
        header[19] = 0;
        header[20] = 1; // format = 1
        header[21] = 0;
        header[22] = Convert.ToByte(channels);
        header[23] = 0;
        header[24] = (byte)(sampleRate & 0xff);
        header[25] = (byte)((sampleRate >> 8) & 0xff);
        header[26] = (byte)((sampleRate >> 16) & 0xff);
        header[27] = (byte)((sampleRate >> 24) & 0xff);
        header[28] = (byte)(byteRate & 0xff);
        header[29] = (byte)((byteRate >> 8) & 0xff);
        header[30] = (byte)((byteRate >> 16) & 0xff);
        header[31] = (byte)((byteRate >> 24) & 0xff);
        header[32] = 2 * 16 / 8; // block align
        header[33] = 0;
        header[34] = Convert.ToByte(16); // bits per sample
        header[35] = 0;
        header[36] = Convert.ToByte('d');
        header[37] = Convert.ToByte('a');
        header[38] = Convert.ToByte('t');
        header[39] = Convert.ToByte('a');
        header[40] = (byte)(audioLength & 0xff);
        header[41] = (byte)((audioLength >> 8) & 0xff);
        header[42] = (byte)((audioLength >> 16) & 0xff);
        header[43] = (byte)((audioLength >> 24) & 0xff);

        outputStream.Write(header, 0, 44);
    }

    public Task StartRecordAsync()
    {
        if (!CanRecordAudio || _audioRecord?.RecordingState is RecordState.Recording)
            return Task.CompletedTask;

        var audioManager = (AudioManager)Android.App.Application.Context.GetSystemService(Context.AudioService);
        //        var micSampleRate = int.Parse(audioManager.GetProperty(AudioManager.PropertyOutputSampleRate));
        var micSampleRate = 16000;


        _audioRecord = GetAudioRecord(micSampleRate);
        _audioRecord.StartRecording();

        return Task.Run(() => WriteAudioDataToFile());
    }

    public Task<Audio> StopRecordAsync()
    {
        if (_audioRecord?.RecordingState is RecordState.Recording)
        {
            _audioRecord?.Stop();
        }

        CopyWaveFile(_rawFilePath, FilePath);

        return Task.FromResult(GetAudio());
    }

    public void Dispose()
    {
        Debug.WriteLine("[Plugin.Maui.AudioRecorder] DISPOSING...");
        GC.SuppressFinalize(this);
    }
}