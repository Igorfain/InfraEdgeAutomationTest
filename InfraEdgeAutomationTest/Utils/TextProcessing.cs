using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace InfraEdgeAutomationTest.Utils
{
    public static class TextProcessor
    {
        public static Dictionary<string, int> Process(string rawText)
        {
            string clean = Regex.Replace(rawText.ToLower(), @"\[.*?\]|[^a-zA-Z0-9\s]", " ");
            var words = clean.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

            return words.GroupBy(w => w)
                        .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
