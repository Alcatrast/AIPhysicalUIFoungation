using TerminaClient.Devices;

namespace TerminaClient
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
