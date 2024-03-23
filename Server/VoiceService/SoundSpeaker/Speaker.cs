namespace Server.VoiceService.SoundSpeaker
{
    public static class Speaker
    {
        private static readonly string _dir = @"C:\TEMP\glados\speech\";
        private static readonly string _ext = @".ogg";
        public enum Audio {WakeUp, ShowItSelf, Joke1, Fact1, LightOn, LightOff, UndefinedCommand, EyeOn, EyeOff, Beatles }

        private static IReadOnlyDictionary<Audio, string> _audio = new Dictionary<Audio, string>()
        {
            { Audio.WakeUp,"wakeup" },
            { Audio.UndefinedCommand,"undefined_command" },
            { Audio.LightOn,"light_on" },
            { Audio.LightOff,"light_off" },
            { Audio.EyeOn,"activate" },
            { Audio.EyeOff,"deactivate" },
            { Audio.ShowItSelf,"show_itself" },
            { Audio.Joke1,"joke" },
            { Audio.Fact1,"fact" },
            { Audio.Beatles,"beatles" },

        };
        public static byte[] GetVorbisData(Audio audio)
        {
            if ((_audio.TryGetValue(audio, out var file)) == false) { throw new Exception($"{audio} not included."); }
            string path = _dir + file + _ext; if (File.Exists(path)==false) { throw new Exception($"File {path} undefined."); }
            return File.ReadAllBytes(path);
        }
    }
}
