using Server.VoiceService.GPTService.Yandex;

namespace Server.UserDataBase
{
    public class User
    {
        public Settings Settings { get; private set; } = new();
    }
    public class Settings
    {
        public GPTSettings GhatGPT { get; private set; } = new();
    }
    public class GPTSettings
    {
        public GPTDialog Dialog { get; private set; } = new();

    }
    public class GPTDialog
    {
        public List<YandexGPTMessage> Messages { get; private set; }

        public GPTDialog()
        {
            Restore();
        }
        public void AddUserMessage(string message)
        {
            Messages.Add(new() { role = "user", text = message });
        }
        public void AddAssistantMessage(string message)
        {
            Messages.Add(new() { role = "assistant", text = message });
        }
        public void Restore()
        {
            Messages = [new() { role = "system", text = "Ты умный ассистент, отвечай ОЧЕНЬ кратко. В твоём ответе МАКСИМУМ 3 предложения!" }];
        }
    }
}
