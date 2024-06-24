using ATNetAPI.CommandModels;

namespace Server.VoiceService.Model
{
    public class CommandAudioTextBundle
    {
        public CommandAudioTextBundle(IGladCommand command,string textData, byte[] audioData)
        {
            Command = command;
            TextData = textData;
            AudioData = audioData;
        }

        public IGladCommand Command { get; set; }
        public string TextData {  get; set; }
        public byte[] AudioData { get; set; }
    }
}

