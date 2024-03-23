using Server.UserDataBase;

namespace Server.VoiceService.Handler.GPTService.Yandex
{
    public class YandexGPTRequestBody

    {
        public string modelUri { get; set; } = $"gpt://{General.Configuration.Tokens.YandexFolderId}/yandexgpt";
        public YandexGPTCompletionOptions completionOptions { get; set; } = new();
        public List<YandexGPTMessage> messages { get; set; } = new();
        public static YandexGPTRequestBody BuildFrom(string userCurrentRequest, GPTSettings settings)
        {
            YandexGPTRequestBody result= new();
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
