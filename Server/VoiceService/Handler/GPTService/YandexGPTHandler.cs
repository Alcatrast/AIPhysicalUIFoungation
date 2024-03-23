using Server.UserDataBase;
using System.Text;

namespace Server.VoiceService.Handler.GPTService.Yandex
{
    internal class YandexGPTHandler
    {
        public async Task<string> GetResponse(string text, GPTSettings settings)
        {
            string url = "https://llm.api.cloud.yandex.net/foundationModels/v1/completion";
                
            YandexGPTRequestBody body = YandexGPTRequestBody.BuildFrom(text, settings);
            string data = System.Text.Json.JsonSerializer.Serialize(body);
            string result = "Запрос не исполнен.";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Api-Key {General.Configuration.Tokens.YandexAPIKey}");
                client.DefaultRequestHeaders.Add("x-data-logging-enabled", "false");

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    dynamic responseJson = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseString);
                    result = responseJson.result.alternatives[0].message.text;
                    settings.Dialog.AddUserMessage(text);
                    settings.Dialog.AddAssistantMessage(result);
                }
                else result = "Ошибка: " + response.StatusCode;
            }
            return result;
        }
    }
}
