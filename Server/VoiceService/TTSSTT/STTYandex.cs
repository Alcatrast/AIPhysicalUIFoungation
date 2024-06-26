﻿using System.Net;
using System.Text;

namespace Server.VoiceService.TTSSTT
{
    public class STTYandex : ISTT
    {
        private readonly string _apiKey;
        private readonly string _folderId;
        public STTYandex(string apiKey, string folderId) { _apiKey = apiKey; _folderId = folderId; }
        public string GetText(byte[] audioWavData)
        {
            string API_KEY = _apiKey;
            string FOLDER_ID = _folderId;

            byte[] data = audioWavData;

            string paramsStr = string.Join("&", new[]
            {
                "topic=general",
                $"folderId={FOLDER_ID}",
                "lang=ru-RU",
                "rawResults=true",
                "format=lpcm",
                "sampleRateHertz=16000"
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
                    Console.WriteLine("Y Recognized text: " + decodedData.result);
                    return decodedData.result;
                }
                else
                {
                    Console.WriteLine("Y Recognized ERROR: " + decodedData.error_code);
                    return "Ошибка спичкит";
                }
            }
        }
    }
}
