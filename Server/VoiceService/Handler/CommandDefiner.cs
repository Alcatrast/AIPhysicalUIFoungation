using Server.VoiceService.Model;
using Server.VoiceService.SoundSpeaker;

namespace Server.VoiceService.Handler
{
    public class CommandDefiner
    {
        private IEnumerable<KeyWordsCommandPair> _commandList = new List<KeyWordsCommandPair>()
       {
        new LightOn(),
        new LightOff(),
        new EyeOn(),
        new EyeOff(),
        new ShowItSelf(),
        new TellJoke1(),
        new TellFact1(),
        new Dance1(),
       };
        public CommandDefiner() { }

        public bool Define(string text, out CommandAudioTextBundle cadp)
        {
            cadp = default;
            text = text.ToLower();
            if (new ActivateWordNoCommand().IsCommandDefinedIn(text) == false) { return false; }


            CommandCommentPair resultPair = new UndefinedCommandComment();
            foreach (var pair in _commandList)
            {
                if (pair.IsCommandDefinedIn(text))
                {
                    resultPair = pair.Pair;
                    break;
                }
            }
            cadp = new(resultPair.Command, "Done.", SideActionFor(resultPair.Audio));
            return true;
        }

        private byte[] SideActionFor(Speaker.Audio audio)
        {
            return Speaker.GetVorbisData(audio);
        }
    }
}
