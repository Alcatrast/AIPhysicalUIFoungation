using System.Net;

namespace Server.VoiceService.TTSSTT;
internal class TTSSilero : ITTS
{
    private readonly string _apiToken;
    public TTSSilero(string apiToken) => _apiToken = apiToken;
    public string GetOggPath(string texte)
    {
        string audioFilePath = $@"C:\TEMP\glados\speech\temp\sound-{DateTime.Now.Ticks}.ogg";
        string apiToken = _apiToken;
        string apiUrl = "https://api.silero.ai/voice";

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            var json = System.Text.Json.JsonSerializer.Serialize(new
            {
                api_token = apiToken,
                text = texte,
                sample_rate = 48000,
                speaker = "glados",
                remote_id = "test",
                lang = "ru",
                format = "ogg",
                word_ts = false
            });

            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        var responseStream = httpResponse.GetResponseStream();

        string result = "DEFAULT";
        byte[] oggAudioData = [];
        using (var streamReader = new StreamReader(responseStream))
        {
            result = streamReader.ReadToEnd();
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);
            oggAudioData = json.results[0].audio;
        }


        File.WriteAllBytes(audioFilePath, oggAudioData);
        Console.WriteLine("...S Answer syntesired.");
        return audioFilePath;
    }

}
