using Plugin.Maui.Audio;

namespace TerminaClient.Devices.AudioService
{
    public class AudioPlayer
    {
        private Action? _action;

        IAudioPlayer? player;
        private MemoryStream? _stream;

        public void Play(byte[] vorbisData, Action? action)
        {
            Thread.Sleep(1100);
            _stream = new MemoryStream(vorbisData);
            player = AudioManager.Current.CreatePlayer(_stream);
            player.PlaybackEnded += Player_PlaybackEnded;
            _action = action;
            player.Play();
        }



        private void Player_PlaybackEnded(object? sender, EventArgs e)
        {
            _action?.Invoke();
            _action = null;
            if (player != null) player.PlaybackEnded -= Player_PlaybackEnded;
            player?.Dispose();
            _stream?.Close();
            _stream?.Dispose();
            _stream = null;
        }

        public void Stop()
        {
            player?.Stop();
        }
    }
}
