using Server.ConfigurationTypes;
using Server.Services;
using Server.UserDataBase;

namespace Server
{
    public static class General
    {
        public static readonly User User = new();
        public static ConnectedDevices Devices { get; private set; }= new();

        
        public static class Configuration
        {
            private static readonly string dir = @"C:\TEMP\glados\configuration";
            private static readonly string tokensFile = "tokens.cfg";
            public static Tokens Tokens { get; private set; }
            public static readonly int AnimationMsD = 50;

            static Configuration()
            {
                Tokens = (Serializer.Deserialize<Tokens>(File.ReadAllText(Path.Combine(dir, tokensFile))));
            }
        }
    }
}