using TerminalClient.Devices;
[assembly: Dependency(typeof(PermissionsService))]
namespace TerminalClient.Devices
{
    public static class PermissionsService
    {
        public static async void RequestStoragePermission()
        {

            var status = await Permissions.RequestAsync<Permissions.Media>();
            status = await Permissions.RequestAsync<Permissions.StorageRead>();
            status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            status = await Permissions.RequestAsync<Permissions.Microphone>();

        }
    }
}