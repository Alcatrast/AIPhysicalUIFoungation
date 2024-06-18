
namespace TerminalClient;

public partial class DirectModePage : ContentPage
{
    public void SlidersServoThreadsInit()
    {
        sliderControl_1 = new(ControlSliderServo_1);
        sliderControl_1.Start();

        sliderControl_2 = new(ControlSliderServo_2);
        sliderControl_2.Start();

        sliderControl_3 = new(ControlSliderServo_3);
        sliderControl_3.Start();

        sliderControl_4 = new(ControlSliderServo_4);
        sliderControl_4.Start();
    }


    bool servoSliderEnable_1 = false;
    private Thread sliderControl_1;
    private void serv1_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        servoSliderEnable_1 = true;
    }
    private void ControlSliderServo_1()
    {
        while (true)
        {
            if (servoSliderEnable_1)
            {
                RunServo(1, Convert.ToInt32(Math.Round(serv1.Value)));
                servoSliderEnable_1 = false;
                Thread.Sleep(SLIDER_DELAY);
            }
        }
    }



    bool servoSliderEnable_2 = false;
    private Thread sliderControl_2;
    private void serv2_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        servoSliderEnable_2 = true;
    }
    private void ControlSliderServo_2()
    {
        while (true)
        {
            if (servoSliderEnable_2)
            {
                RunServo(2, Convert.ToInt32(Math.Round(serv2.Value)));
                servoSliderEnable_2 = false;
                Thread.Sleep(SLIDER_DELAY);
            }
        }
    }


    bool servoSliderEnable_3 = false;
    private Thread sliderControl_3;
    private void serv3_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        servoSliderEnable_3 = true;
    }
    private void ControlSliderServo_3()
    {
        while (true)
        {
            if (servoSliderEnable_3)
            {
                RunServo(3, Convert.ToInt32(Math.Round(serv3.Value)));
                servoSliderEnable_3 = false;
                Thread.Sleep(SLIDER_DELAY);
            }
        }
    }


    bool servoSliderEnable_4 = false;
    private Thread sliderControl_4;
    private void serv4_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        servoSliderEnable_4 = true;
    }
    private void ControlSliderServo_4()
    {
        while (true)
        {
            if (servoSliderEnable_4)
            {
                RunServo(4, Convert.ToInt32(Math.Round(serv4.Value)));
                servoSliderEnable_4 = false;
                Thread.Sleep(SLIDER_DELAY);
            }
        }
    }
}