using ATNetAPI.CommandModels;

namespace Server.VoiceService.Model
{
    public class CommandAudioDataPair
    {
        public CommandAudioDataPair(IGladCommand command, byte[] audioData)
        {
            Command = command;
            AudioData = audioData;
        }

        public IGladCommand Command { get; set; }
        public byte[] AudioData { get; set; }
    }
}

