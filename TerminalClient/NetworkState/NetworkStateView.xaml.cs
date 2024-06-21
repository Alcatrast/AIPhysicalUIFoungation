namespace TerminalClient;

public partial class NetworkStateView : ContentView
{
    public NetworkStateView()
    {
        InitializeComponent();
    }
    private Thread thread;
    private bool _isConnected = true;
    private void ContentView_Loaded(object sender, EventArgs e)
    {
        thread = new Thread(WatchingNetState);
        thread.IsBackground = true;
        thread.Start();
    }
    private void WatchingNetState()
    {
        bool receiveState;
        while (true)
        {
            receiveState = General.AutoReconnectSystem.IsConnected;
            if (receiveState != _isConnected)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    NetStateLb.IsVisible = !receiveState;
                });
                _isConnected = receiveState;
            }
            Thread.Sleep(1000);
        }
    }
}