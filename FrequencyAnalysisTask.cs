using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            var dictionaryBi = new SortedDictionary<(string, string), int>();
            var dictionaryThree = new SortedDictionary<(string, string), int>();

            if (text.All(x => x.Count < 2) )
            {
                return new Dictionary<string, string>();
            }
            foreach (var sentence in text)
            {
                FillNgramDicts(sentence, dictionaryBi, dictionaryThree);
            }

            PrepareResultDictionary(result, dictionaryBi);
            PrepareResultDictionary(result, dictionaryThree);

            return result;
        }

        private static void FillNgramDicts(List<string> sentence, SortedDictionary<(string, string), int> dictionaryBi, SortedDictionary<(string, string), int>  dictionaryThree)
        {
            var newBi = sentence.Where((e, i) => i < sentence.Count - 1).Select((e, i) => new { A = e, B = sentence[i + 1] })
                .ToList();
            var three = sentence.Where((e, i) => i < sentence.Count - 2)
                .Select((e, i) => new { A = e + " " + sentence[i + 1], B = sentence[i + 2] }).ToList();

            foreach (var item in newBi)
            {
                if (dictionaryBi.ContainsKey((item.A, item.B)))
                    dictionaryBi[(item.A, item.B)]++;
                else
                    dictionaryBi.Add((item.A, item.B), 1);
            }

            foreach (var item in three)
            {
                if (dictionaryThree.ContainsKey((item.A, item.B)))
                    dictionaryThree[(item.A, item.B)]++;
                else
                    dictionaryThree.Add((item.A, item.B), 1);
            }
        }

        private static void PrepareResultDictionary(Dictionary<string, string> result, SortedDictionary<(string, string), int> dictionaryTemp)
        {
            foreach (var pair in dictionaryTemp)
            {
                var tempCount = 0;
                if (result.ContainsKey(pair.Key.Item1))
                {
                    if (dictionaryTemp[pair.Key] > tempCount && dictionaryTemp[pair.Key] != pair.Value)
                        result[pair.Key.Item1] = pair.Key.Item2;
                }
                else
                {
                    result.Add(pair.Key.Item1, pair.Key.Item2);
                    tempCount = dictionaryTemp[pair.Key];
                }
            }
        }
   }
}