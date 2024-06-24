using ATNetAPI;
using ATNetAPI.Configuration.List;
using Fleck;
using Server.UserDataBase;
using Server.VoiceService.GPTService.Yandex;
using Server.VoiceService.Handler;
using Server.VoiceService.Model;
using Server.VoiceService.TTSSTT;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebSocketServer(ForNetTextAPIUrls.SLocalhost);
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine($"Client connected: {socket.ConnectionInfo.ClientIpAddress}");
                };

                socket.OnMessage = message =>
                {
                    if (General.Devices.IsTerminal(socket))
                    {
                        Console.WriteLine("From terminal: " + message.Length + " bytes.");
                        if (APIManager.TryUnboxing(message, out var msg) == false) return;
                        if (msg is ResendMessage resend)
                        {
                            Console.WriteLine($"ResendMessage: {resend}");
                            if (General.Devices.Robot != null)
                                General.Devices.Robot.Send(resend.Message);
                        }
                        else if (msg is AudioTextMessage audio)
                        {
                            Console.WriteLine("\nIts AudioMessage");
                            RunVoiceComand(audio);
                        }
                    }
                    else
                    {
                        if (AuthorizeDevice(ref socket, message) == false) socket.Close();
                    }
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine($"Client disconnected: {socket.ConnectionInfo.ClientIpAddress}");
                    General.Devices.Remove(socket);
                };

            });

            Console.WriteLine("Server started. Press Esc to exit.");
            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                key = Console.ReadKey();
            }
            server.Dispose();
        }

        static bool AuthorizeDevice(ref IWebSocketConnection socket, string message)
        {
            if (message == ConnectedDevices.AuthorizationTokens.TERMINAL)
            {
                General.Devices.Terminal = socket;
                Console.WriteLine($"Client {ConnectedDevices.AuthorizationTokens.TERMINAL} connected.");
            }
            else if (message == ConnectedDevices.AuthorizationTokens.ROBOT)
            {
                General.Devices.Robot = socket;
                Console.WriteLine($"Client {ConnectedDevices.AuthorizationTokens.ROBOT} connected.");
            }
            else
            {
                Console.WriteLine("Invalid authentication message.");
                socket.Close();
                return false;
            }
            return true;
        }

        private static ISTT stt = new STTYandex(General.Configuration.Tokens.YandexAPIKey, General.Configuration.Tokens.YandexFolderId);
        private static YandexGPTHandler GPTHandler = new YandexGPTHandler(General.Configuration.Tokens.YandexAPIKey, General.Configuration.Tokens.YandexFolderId);
        private static ITTS tts = new TTSSilero(General.Configuration.Tokens.SileroTTS);
        static async void RunVoiceComand(AudioTextMessage message)
        {
            //_ = Task.Run(async () =>
            //{
            string text = stt.GetText(message.AudioData);
            CommandAudioTextBundle result;

            if (new CommandDefiner().Define(text, out var command))
                result = command;
            else
            {
                string gptResponse = await GPTHandler.GetResponse(text, General.User.Settings.GhatGPT);
                result = await new AudioKineticBuilder().Run(gptResponse, tts);
            }

            if (General.Devices.Terminal != null)
            {
                AudioTextMessage audioMessage = new() { Text=result.TextData, AudioData = result.AudioData };
                string request = APIManager.Boxing(audioMessage);
                _ = General.Devices.Terminal.Send(request);

                Console.WriteLine("...Sended To Client");

                if (General.Devices.Robot != null) { General.Devices.Robot.Send(result.Command.Render()); Console.WriteLine("Sended To Roobot"); }
            }
            // });
            Console.WriteLine("...Main: Voice command temporared.");
        }
    }
}