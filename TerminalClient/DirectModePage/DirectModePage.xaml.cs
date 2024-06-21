using ATNetAPI;
using ATNetAPI.CommandModels;

namespace TerminalClient;

public partial class DirectModePage : ContentPage
{
    private const int SLIDER_DELAY = 800;

    public DirectModePage()
    {
        InitializeComponent();
        NetStateVSL.Children.Add(new NetworkStateView());
        SlidersServoThreadsInit();
        SlidersLumenThreadsInit();
    }
    private void RunServo(int number, int value)
    {
        PhysCommand command = new(PhysCommand.DeviceType.Servo, number, value);
        Send(command);
    }
    private void RunLumen(int number, int value)
    {
        PhysCommand command = new(PhysCommand.DeviceType.Light, number, value);
        Send(command);
    }

    void Send(PhysCommand command)
    {
        ResendMessage resendMessage = new() { Message = command.Render() };
        string ms = APIManager.Boxing(resendMessage);
        _ = General.NetClient.Send(ms);
    }
}