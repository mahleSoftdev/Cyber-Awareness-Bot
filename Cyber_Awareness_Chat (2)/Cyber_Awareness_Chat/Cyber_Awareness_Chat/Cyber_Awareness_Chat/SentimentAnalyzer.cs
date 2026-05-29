using System.Collections.Generic;

namespace Cyber_Awareness_Chat
{
    public class SentimentAnalyzer
    {
        private readonly List<string> worriedWords;
        private readonly List<string> frustratedWords;
        private readonly List<string> curiousWords;

        public SentimentAnalyzer()
        {
            worriedWords = new List<string>
            {
                "worried", "scared", "afraid", "nervous", "anxious",
                "unsafe", "dangerous", "terrified", "concern", "concerned",
                "stress", "stressed", "overwhelmed", "frightened"
            };

            frustratedWords = new List<string>
            {
                "frustrated", "annoyed", "angry", "upset", "useless",
                "stupid", "hate", "irritated", "fed up", "confused",
                "dont understand", "don't understand", "makes no sense"
            };

            curiousWords = new List<string>
            {
                "how", "what", "why", "when", "where", "curious",
                "explain", "tell me", "interested", "learn", "understand",
                "want to know", "wondering", "can you"
            };
        }

        public string DetectSentiment(string input)
        {
            string lower = input.ToLower();

            foreach (string word in worriedWords)
                if (lower.Contains(word)) return "worried";

            foreach (string word in frustratedWords)
                if (lower.Contains(word)) return "frustrated";

            foreach (string word in curiousWords)
                if (lower.Contains(word)) return "curious";

            return "neutral";
        }
    }
}