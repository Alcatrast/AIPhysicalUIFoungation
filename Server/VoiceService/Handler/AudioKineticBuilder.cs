using ATNetAPI.CommandModels;
using NAudio.Vorbis;
using Server.VoiceService.Model;
using Server.VoiceService.TTSSTT;

namespace Server.VoiceService.Handler
{
    public class AudioKineticBuilder
    {
        public AudioKineticBuilder() { }
        public async Task<CommandAudioTextBundle> Run(string text, ITTS ttsSystem)
        {
            if (string.IsNullOrEmpty(text)) { return new(new PhysCommand(PhysCommand.DeviceType.Animation, 0, 0), "Command undefined.", new byte[0]); }
            string path = ttsSystem.GetOggPath(text);

            int ms = 0;
            using (var vr = new VorbisWaveReader(path))
            {
                TimeSpan duration = vr.TotalTime;
                ms = (int)duration.TotalMilliseconds;
            }

            int ae = ms / General.Configuration.AnimationMsD;
            if (ae > 9999) { ae = 9999; }
            return new(new PhysCommand(PhysCommand.DeviceType.Animation, ae / 1000, ae % 1000), text, File.ReadAllBytes(path));
            //return new(new PhysCommand(PhysCommand.DeviceType.Animation,1, 0),Speaker.GetVorbisData(Speaker.Audio.Beatles));
        }
    }
}
