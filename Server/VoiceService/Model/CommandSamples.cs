using ATNetAPI.CommandModels;
using Server.VoiceService.SoundSpeaker;

namespace Server.VoiceService.Model
{
    public class ActivateWordNoCommand : KeyWordsCommandPair
    {
        public ActivateWordNoCommand() : base(
            new() { "лада", "glados", "гладос", "владос","попадос", "нос", "босс", "газ","дос","дас", "главная", "глава" },
            default
            )
        { }
    }
    public class LightOn : KeyWordsCommandPair
    {
        public LightOn() : base(
            new() { "включи свет", "включить свет" },
            new(new PhysCommand(PhysCommand.DeviceType.Light, 1, 100), Speaker.Audio.LightOn)
            )
        { }
    }
    public class LightOff : KeyWordsCommandPair
    {
        public LightOff() : base(
            new() { "выключи свет", "выключить свет" },
            new(new PhysCommand(PhysCommand.DeviceType.Light, 1, 0), Speaker.Audio.LightOff)
            )
        { }
    }
    public class EyeOn : KeyWordsCommandPair
    {
        public EyeOn() : base(
            new() { "открой глаза", "открой глаз", "открыть глаза", "открыть глаз" },
            new(new PhysCommand(PhysCommand.DeviceType.Light, 2, 100), Speaker.Audio.LightOn)
            )
        { }
    }
    public class EyeOff : KeyWordsCommandPair
    {
        public EyeOff() : base(
            new() { "закрой глаза", "закрой глаз", "закрыть глаза", "закрыть глаз" },
            new(new PhysCommand(PhysCommand.DeviceType.Light, 2, 0), Speaker.Audio.LightOff)
            )
        { }
    }

    public class ShowItSelf : KeyWordsCommandPair
    {
        public ShowItSelf() : base(
            new() { "расскажи о себе","себе", "рассказать о себе", "представ" },
            new(new PhysCommand(PhysCommand.DeviceType.Animation, 2, 0), Speaker.Audio.ShowItSelf)
            )
        { }
    }
    public class TellJoke1 : KeyWordsCommandPair
    {
        public TellJoke1() : base(
            new() { "расскажи анекдот", "рассказать анекдот", "шут" },
            new(new PhysCommand(PhysCommand.DeviceType.Animation, 3, 0), Speaker.Audio.Joke1)
            )
        { }
    }
    public class TellFact1 : KeyWordsCommandPair
    {
        public TellFact1() : base(
            new() { "интересн", "факт" },
            new(new PhysCommand(PhysCommand.DeviceType.Animation, 4, 0), Speaker.Audio.Fact1)
            )
        { }
    }
    public class Dance1 : KeyWordsCommandPair
    {
        public Dance1() : base(
            new() { "танец", "танц" },
            new(new PhysCommand(PhysCommand.DeviceType.Animation, 5, 0), Speaker.Audio.Beatles)
            )
        { }
    }
    
}
