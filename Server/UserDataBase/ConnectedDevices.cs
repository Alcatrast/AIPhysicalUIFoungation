using Fleck;

namespace Server.UserDataBase
{
    public class ConnectedDevices
    {
        public IWebSocketConnection? Robot;
        public IWebSocketConnection? Terminal;
        public bool Remove(IWebSocketConnection connection)
        {
            return RemoveIn(connection, ref Terminal) || RemoveIn(connection, ref Robot);
        }
        public bool IsRobot(IWebSocketConnection connection) { return connection == Robot; }
        public bool IsTerminal(IWebSocketConnection connection) { return connection == Terminal; }

        private bool RemoveIn(IWebSocketConnection comparion, ref IWebSocketConnection? val)
        {
            if (comparion == val) { val.Close(); val = null; return true; }
            return false;
        }
        public static class AuthorizationTokens
        {
            public static readonly string TERMINAL = "IAMGLADOSTERMINAL";
            public static readonly string ROBOT = "IAMGLADOSBODY";
        }
    }
}
