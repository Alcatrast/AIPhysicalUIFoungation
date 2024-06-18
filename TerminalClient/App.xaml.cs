
namespace TerminalClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            General.NetClient.Connect();
            MainPage = new AppShell();
        }
       
    } 
}
