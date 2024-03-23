using System.Net;
using System.Text;

namespace Server.VoiceService.TTSSTT
{
    internal class STTYandex : ISTT
    {
        public string GetText(byte[] audioWavData)
        {
            string FOLDER_ID = General.Configuration.Tokens.YandexFolderId;
            string API_KEY = General.Configuration.Tokens.YandexAPIKey;

            byte[] data = audioWavData;

            string paramsStr = string.Join("&", new[]
            {
                "topic=general",
                $"folderId={FOLDER_ID}",
                "lang=ru-RU",
                "rawResults=true",
                "format=lpcm",
                "sampleRateHertz=48000"
            });

            var url = new Uri($"https://stt.api.cloud.yandex.net/speech/v1/stt:recognize?{paramsStr}");
            var request = WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", $"Api-Key {API_KEY}");

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string responseData = reader.ReadToEnd();
                dynamic decodedData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);

                if (decodedData.error_code == null)
                {
                    Console.WriteLine(decodedData.result);
                    return decodedData.result;
                }
                else { return "Ошибка спичкит"; }
            }
        }
    }
}

