using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var phrase = phraseBeginning.Split().ToList();
            var count = phrase.Count + wordsCount;

            while (phrase.Count < count)
            {
                if (phrase.Count > 1)
                {
                    var key = string.Join(" ", phrase.Skip(phrase.Count - 2));
                    if (nextWords.ContainsKey(key))
                    {
                        phrase.Add(nextWords[key]);
                    }
                    else if (nextWords.ContainsKey(phrase.Last()))
                    {
                        phrase.Add(nextWords[phrase.Last()]);
                    }
                    else
                    {
                        return string.Join(" ", phrase);
                    }
                }
                else
                {
                    if (nextWords.ContainsKey(phrase.Last()))
                    {
                        phrase.Add(nextWords[phrase.Last()]);
                    }
                    else
                    {
                        return string.Join(" ", phrase);
                    }
                }
            }

            return string.Join(" ", phrase);
        }
    }
}