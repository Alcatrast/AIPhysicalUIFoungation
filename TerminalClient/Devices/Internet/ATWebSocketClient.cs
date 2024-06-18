using System.Net.WebSockets;
using System.Text;

namespace TerminalClient.Devices.Internet
{
    public class ATWebSocketClient
    {
        ClientWebSocket webSocket = new ClientWebSocket();
        Uri serverUri;
        public ATWebSocketClient(string url)
        {
            serverUri = new Uri(url);

        }
        private async void _Connect()
        {
            try
            {
                await webSocket.ConnectAsync(serverUri, CancellationToken.None);
                if (IsConnected)
                {
                    await Send("IAMGLADOSTERMINAL");
                }
            }
            catch { }
        }
        public bool IsConnected { get { return webSocket.State == WebSocketState.Open; } }
        public bool Connect()
        {
            _Connect();
            return IsConnected;
        }
        public async Task<bool> Send(string message)
        {
            if (IsConnected)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                try
                {
                    await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    return true;
                }
                catch { return false; }
            }
            return false;
        }
        public async Task<string> Receive()
        {
            byte[] receiveBuffer = new byte[268435456 * 2 * 2];
            try
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);

                string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                return receivedMessage;
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error", "No connection for receive.", "Ok");
                return string.Empty;
            }
        }
    }
}
