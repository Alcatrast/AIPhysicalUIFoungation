using ATNetAPI;
using System.Timers;
using TerminalClient.Devices.AudioService;
using ATMauiAudioRecorder.Abstractions;

namespace TerminalClient;
public partial class VoiceModePage : ContentPage
{
    private bool _voiceActive = false;
    private bool _isCancelled = false;
    public VoiceModePage(IAudioRecorder audioRecorder)
    {
        recorder = audioRecorder;
        InitializeComponent();
        NetStateVSL.Children.Add(new NetworkStateView());
    }

    private void OnStartStopRecordButtonClicked(object sender, EventArgs e)
    {
        if (_voiceActive) { _isCancelled = false; StopRecord(); }
        else { StartRecord(); }
        ChangeIntState(!_voiceActive);
    }

    private void ChangeIntState(bool toActiveState)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            _voiceActive = toActiveState;
            SpeakLb.IsVisible = toActiveState;
            UnspeakBtn.IsVisible = toActiveState;
            SpeakBtn.IsVisible = !toActiveState;
            PressLb.IsVisible = !toActiveState;
            cancelBtn.IsVisible = toActiveState;
        });
    }
    private void Cancel_Clicked(object sender, EventArgs e)
    {
        ChangeIntState(!_voiceActive);
        _isCancelled = true;
        StopRecord();
    }

    System.Timers.Timer _responseAwaiter = new();

    async void HandleVoiceCommand()
    {
        Controlable(false);
        RecreateTimer(ref _responseAwaiter);

        byte[] audioData = outAudioPath;
        AudioMessage audioMessage = new() { AudioData = audioData };
        string request = APIManager.Boxing(audioMessage);
        _ = await General.NetClient.Send(request);

        _AudioData = General.MessageSorter.ForPlayer().AudioData; 
        
        if (audioData != null)
        {
            _responseAwaiter.Elapsed -= OnTimedEvent;
            OnTimedEvent(default, default);
        }
    }

    byte[]? _AudioData = null;

    public void Controlable(bool enable)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            SpeakBtn.IsEnabled = enable;
            cancelBtn.IsEnabled = enable;
            UnspeakBtn.IsEnabled = enable;
            PressLb.IsEnabled = enable;
        });
    }

    void RecreateTimer(ref System.Timers.Timer timer)
    {

        timer.Stop();
        timer.Elapsed -= OnTimedEvent;
        timer.Dispose();
        timer = new System.Timers.Timer(50000); // 5000 миллисекунд = 5 секунд
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = false;
        timer.Enabled = true; // Запустить таймер
    }
    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        if (_AudioData == null)
        {
            Controlable(true);
            return;
        }
        _ = Task.Run(() => {
            new AudioPlayer().
                Play(_AudioData, () =>
                {
                    _AudioData = null;
                    Controlable(true);
                }
             );
        });
    }
}
