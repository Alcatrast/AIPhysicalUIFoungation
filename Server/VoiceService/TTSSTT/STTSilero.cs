using System.Net;

namespace Server.VoiceService.TTSSTT;

internal class STTSilero:ISTT
{
    public string GetText(byte[] audioWavData)
    {
        //  File.WriteAllBytes(@"C:\Temp\blabla.wav", audioWavData);


        string apiToken = General.Configuration.Tokens.SileroTTS;
        string apiUrl = "https://api.silero.ai/transcribe";


        //var bytes = File.ReadAllBytes(@"C:\TEMP\sound.wav");
        //var payloade = Convert.ToBase64String(bytes);
        var payloade = Convert.ToBase64String(audioWavData);


        var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            var json = System.Text.Json.JsonSerializer.Serialize(new
            {
                api_token = apiToken,
                payload = payloade,
                remote_id = "my_tracking_id",
                sample_rate = 16000,
                encrypted = false,
                channels = 1,

            });

            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        var responseStream = httpResponse.GetResponseStream();

        string result = "ERROR";
        using (var streamReader = new StreamReader(responseStream))
        {
            result = streamReader.ReadToEnd();
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);
            result = json.transcriptions[0].transcript;
        }
        Console.WriteLine("S Recognized: " + result);
        return result;
    }
}
