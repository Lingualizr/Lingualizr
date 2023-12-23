using System.Globalization;
using System.Text.RegularExpressions;

namespace Lingualizr.Transformer;

internal partial class ToTitleCase : ICulturedStringTransformer
{
    public string Transform(string input)
    {
        return Transform(input, null);
    }

    public string Transform(string input, CultureInfo? culture)
    {
        culture ??= CultureInfo.CurrentCulture;

        string result = input;
        MatchCollection matches = MatchRegex().Matches(input);
        bool firstWord = true;
        foreach (Match word in matches)
        {
            if (!AllCapitals(word.Value))
            {
                result = ReplaceWithTitleCase(word, result, culture, firstWord);
            }

            firstWord = false;
        }

        return result;
    }

    private static bool AllCapitals(string input)
    {
        return input.All(char.IsUpper);
    }

    private static string ReplaceWithTitleCase(Match word, string source, CultureInfo culture, bool firstWord)
    {
        List<string> articles = new() { "a", "an", "the" };
        List<string> conjunctions = new() { "and", "as", "but", "if", "nor", "or", "so", "yet" };
        List<string> prepositions = new() { "as", "at", "by", "for", "in", "of", "off", "on", "to", "up", "via" };

        string wordToConvert = word.Value;
        string replacement;

        if (firstWord || (!articles.Contains(wordToConvert) && !conjunctions.Contains(wordToConvert) && !prepositions.Contains(wordToConvert)))
        {
            replacement = culture.TextInfo.ToUpper(wordToConvert[0]) + culture.TextInfo.ToLower(wordToConvert.Remove(0, 1));
        }
        else
        {
            replacement = culture.TextInfo.ToLower(wordToConvert);
        }

        return source.Substring(0, word.Index) + replacement + source.Substring(word.Index + word.Length);
    }

    [GeneratedRegex(@"(\w|[^\u0000-\u007F])+'?\w*")]
    private static partial Regex MatchRegex();
}
