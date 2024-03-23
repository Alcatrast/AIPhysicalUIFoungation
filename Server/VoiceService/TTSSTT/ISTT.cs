namespace Server.VoiceService.TTSSTT
{
    internal interface ISTT
    {
        public string GetText(byte[] audioData);
    }
}
