
namespace ATNetAPI.CommandModels
{
    public interface IGladCommand
    {
        public string Render();
    }
    public interface IFirstCommandModel : IGladCommand
    {
        public static string Formatting(string command)
        {
            return ("{" + command + "}");
        }
    }
    public class PhysCommand : IFirstCommandModel
    {
        public DeviceType Device { get; set; }
        public enum DeviceType
        {
            Light, Servo, Animation,PowerMode
        }
        private static Dictionary<DeviceType, char> _deviceCodes = new() { { DeviceType.Light, 'L' }, { DeviceType.Servo, 'S' }, { DeviceType.Animation, 'A' } , { DeviceType.PowerMode, 'P' } };

        public int DeviceNumber { get; set; }
        public int DeviceParam { get; set; }
        public PhysCommand(DeviceType device, int deviceNumber, int deviceParam)
        {
            Device = device;
            DeviceNumber = deviceNumber;
            DeviceParam = deviceParam;
        }
        public string Render()
        {
            if(DeviceNumber>9 || DeviceNumber<0) throw new ArgumentOutOfRangeException(nameof(DeviceNumber));
            if(DeviceParam>999 || DeviceParam<0) throw new ArgumentOutOfRangeException(nameof(DeviceParam));  
            string res=string.Empty;
            res+=_deviceCodes[Device];
            res += $"{DeviceNumber}";
            res += $"{DeviceParam:000}";
            return IFirstCommandModel.Formatting(res);
        }
    }
}
    
