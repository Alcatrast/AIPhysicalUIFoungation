
namespace ATNetAPI
{
    public class BaseMessage { }
    public class ResendMessage : BaseMessage
    {
        public string Message { get; set; } = "null";
    }
    public class ConfigurationMessage :  BaseMessage
    {

    }
    public class AudioTextMessage : BaseMessage
    {
        public byte[] AudioData { get; set; }= new byte[0];
        public string Text {  get; set; }=string.Empty;
    }

    public class ReceivedMessage 
    {
        public string Type { get; set; } = "null";
        public string Object { get; set; } = "null";

        public ReceivedMessage(string type, string _object)
        {
            Type = type;
            Object = _object;
        }

        public ReceivedMessage() { }
    }
}
