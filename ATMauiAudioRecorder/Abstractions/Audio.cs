namespace ATMauiAudioRecorder.Abstractions;

public class Audio : IDisposable
{
    private byte[] _filePath;

    public Audio(byte[] filePath)
    {
        _filePath = filePath;
    }

    public byte[] GetFilePath()
    {
        return _filePath;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}