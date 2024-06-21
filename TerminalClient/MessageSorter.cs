using ATNetAPI;
using TerminalClient.Devices.Internet;

namespace TerminalClient;

public class MessageSorter
{
    private ATWebSocketClient net;
    Thread thread;
    public MessageSorter(ATWebSocketClient net)
    {
        this.net = net;
        thread = new(ReceiveMessage);
        thread.IsBackground = true;
        thread.Start();
    }
    private Queue<AudioMessage> audioMessages = new();
    public AudioMessage ForPlayer() { 
        while(audioMessages.Count == 0) { }
        return audioMessages.Dequeue();
    }
    private async void ReceiveMessage()
    {
        while (true)
        {
            string received = await net.Receive();
            if (APIManager.TryUnboxing(received, out var message))
            {
                if (message is AudioMessage am) { audioMessages.Enqueue(am); }
            }
        }
    }

}
