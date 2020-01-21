using System.Linq;
using System.Text.RegularExpressions;

namespace Hackathon.Boilerplate.Foundation.ML
{
    public static class TextHelper
    {
        public static string[] GetWords(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var normalizedString = text
                    .TrimAndToLower()
                    .StripHTML()
                    .StripSpaces();

                var punctuation = normalizedString.Where(char.IsPunctuation).Distinct().ToArray();

                var res = normalizedString.Split().Select(x => x.Trim(punctuation)).ToArray();
                var cleanWords = res.Except(StopWords.stopWordsList).ToArray();

                return cleanWords;
            }

            return null;
        }

        public static string TrimAndToLower(this string input)
        {
            return input?.Trim().ToLower();
        }

        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static string StripSpaces(this string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }

    }
}