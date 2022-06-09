using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            // Play with values in quotas
            var patternToEscape = new Regex(@""".*""");
            text = patternToEscape.Replace(text, m => m.Value.ToLower());

            var regex = @"([a-zа-яA-ZА-ЯЁё]+)(\s*[.!?;:()]+\s*)";
            var textWithSplitter = Regex.Replace(text, regex, "$1|$3");

            // Extract single-char sentences
            var regexForSingleCharSentence = @"(\|[A-ZА-Я][.!&();:])+(\s*)";
            textWithSplitter = Regex.Replace(textWithSplitter, regexForSingleCharSentence, "$1|");
            var sentences = textWithSplitter.Split('|');

            var regexWords = new Regex(@"([a-zа-яA-ZА-ЯЁё']+)");

            var result = sentences.Select(x => regexWords.Matches(x).Cast<Match>().Select(m => m.Value.ToLower()).ToList()).ToList();

            return result.Where(x => x.Count > 0).ToList();
        }
    }
}