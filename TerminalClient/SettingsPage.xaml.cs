namespace TerminalClient;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        NetStateVSL.Children.Add(new NetworkStateView());
    }
}