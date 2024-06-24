using Server.UserDataBase;

namespace Server.VoiceService.GPTService.Yandex
{
    public class YandexGPTRequestBody
    {
        public string modelUri { get; set; }
        public YandexGPTCompletionOptions completionOptions { get; set; } = new();
        public List<YandexGPTMessage> messages { get; set; } = new();
        public YandexGPTRequestBody(string folderId) { modelUri = $"gpt://{folderId}/yandexgpt"; }
        public static YandexGPTRequestBody BuildFrom(string folderId, string userCurrentRequest, GPTSettings settings)
        {
            YandexGPTRequestBody result = new (folderId);
            result.messages = settings.Dialog.Messages;
            result.messages.Add(new() { role = "user", text = userCurrentRequest });
            return result;
        }

    }
    public class YandexGPTCompletionOptions
    {
        public bool stream {  get; set; }=false;
        public double temperature { get; set; } = 0.6;
        public int maxTokens { get; set; } = 2000;

    }
    public class YandexGPTMessage
    {
        public string role { get; set; } = string.Empty;
        public string text { get; set; } = string.Empty;
    
    }
}
