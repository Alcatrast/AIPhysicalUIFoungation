using ATNetAPI.CommandModels;
using NAudio.Vorbis;
using Server.UserDataBase;
using Server.VoiceService.Handler.GPTService.Yandex;
using Server.VoiceService.Model;
using Server.VoiceService.TTSSTT;

namespace Server.VoiceService.Handler
{
    public class GPTCommandHandler
    {
        public GPTCommandHandler() { }
        public async Task<CommandAudioDataPair> Run(string text, Settings settings)
        {
            var gptHandler= new YandexGPTHandler();
            string response = await gptHandler.GetResponse(text, settings.GhatGPT);

            if (string.IsNullOrEmpty(response)) { return new(new PhysCommand(PhysCommand.DeviceType.Animation, 0, 0), new byte[0]); }
            string path = new TTSSilero().GetOggPath(response);

            int ms = 0;
            using (var vr = new VorbisWaveReader(path))
            {
                TimeSpan duration = vr.TotalTime;
                ms = (int)duration.TotalMilliseconds;
            }

            int ae = ms / General.Configuration.AnimationMsD; 
            if(ae >9999) { ae = 9999; }
            return new(new PhysCommand(PhysCommand.DeviceType.Animation, ae/1000, ae%1000),File.ReadAllBytes(path));
            //return new(new PhysCommand(PhysCommand.DeviceType.Animation,1, 0),Speaker.GetVorbisData(Speaker.Audio.Beatles));

        }
    }
}
