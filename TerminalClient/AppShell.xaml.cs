using TerminalClient.Devices;

namespace TerminalClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        private void Shell_Loaded(object sender, EventArgs e)
        {
            PermissionsService.RequestStoragePermission();
        }
    }
}
