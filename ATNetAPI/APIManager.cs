
using Newtonsoft.Json;

namespace ATNetAPI
{
    public static class APIManager
    {
        public static string Boxing<T>(T message) where T : BaseMessage
        {
            if (message.ToString() == null) return "NoType";
            else {
                ReceivedMessage receivedMessage = new(message.ToString(), JsonConvert.SerializeObject(message));
                return JsonConvert.SerializeObject(receivedMessage);
            } 
        }
        public static bool TryUnboxing(string ms,out BaseMessage message)
        {
            message = new();
            ReceivedMessage? rm = null;
            BaseMessage? messageNull=null;
            try
            {
                rm = JsonConvert.DeserializeObject<ReceivedMessage>(ms) ?? null;
                if (rm == null) return false;
                string tp = rm.Type;
                string bd = rm.Object;


                if (tp == new ResendMessage().ToString())
                    messageNull = JsonConvert.DeserializeObject<ResendMessage>(bd);
                else if (tp == new ConfigurationMessage().ToString())
                    messageNull = JsonConvert.DeserializeObject<ConfigurationMessage>(bd);
                else if (tp == new AudioMessage().ToString()) 
                    messageNull = JsonConvert.DeserializeObject<AudioMessage>(bd);

                if (messageNull == null)
                    return false;
                message = messageNull;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
