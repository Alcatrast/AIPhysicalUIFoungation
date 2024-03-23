namespace Server.VoiceService.Model
{
    public class KeyWordsCommandPair
    {
        private List<string> _keyWords;
        public CommandCommentPair Pair { get; set; }

        public KeyWordsCommandPair(List<string> keyWords, CommandCommentPair pair)
        {
            _keyWords = keyWords;
            Pair = pair;
        }

        public bool IsCommandDefinedIn(string input)
        {
            bool result = false;
            foreach(var keyWord in _keyWords) {
            if (input.Contains(keyWord))
                { result = true; break; }
            } return result;
        }
    }
}
