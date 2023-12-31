using System.Diagnostics.CodeAnalysis;

namespace Lingualizr.Localisation.NumberToWords;

internal class NorwegianBokmalNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _unitsMap = { "null", "en", "to", "tre", "fire", "fem", "seks", "sju", "åtte", "ni", "ti", "elleve", "tolv", "tretten", "fjorten", "femten", "seksten", "sytten", "atten", "nitten" };
    private static readonly string[] _tensMap = { "null", "ti", "tjue", "tretti", "førti", "femti", "seksti", "sytti", "åtti", "nitti" };

    private static readonly Dictionary<int, string> _ordinalExceptions =
        new()
        {
            { 0, "nullte" },
            { 1, "første" },
            { 2, "andre" },
            { 3, "tredje" },
            { 4, "fjerde" },
            { 5, "femte" },
            { 6, "sjette" },
            { 11, "ellevte" },
            { 12, "tolvte" },
        };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        return Convert((int)number, false, gender);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        return Convert(number, true, gender);
    }

    private string Convert(int number, bool isOrdinal, GrammaticalGender gender)
    {
        if (number == 0)
        {
            return GetUnitValue(0, isOrdinal);
        }

        if (number < 0)
        {
            return string.Format("minus {0}", Convert(-number, isOrdinal, gender));
        }

        if (number == 1)
        {
            switch (gender)
            {
                case GrammaticalGender.Feminine:
                    return "ei";
                case GrammaticalGender.Neuter:
                    return "et";
            }
        }

        List<string> parts = new();

        bool millionOrMore = false;

        const int billion = 1000000000;
        if (number / billion > 0)
        {
            millionOrMore = true;
            bool isExactOrdinal = isOrdinal && number % billion == 0;
            parts.Add(Part("{0} milliard" + (isExactOrdinal ? string.Empty : "er"), (isExactOrdinal ? string.Empty : "en ") + "milliard", number / billion, !isExactOrdinal));
            number %= billion;
        }

        const int million = 1000000;
        if (number / million > 0)
        {
            millionOrMore = true;
            bool isExactOrdinal = isOrdinal && number % million == 0;
            parts.Add(Part("{0} million" + (isExactOrdinal ? string.Empty : "er"), (isExactOrdinal ? string.Empty : "en ") + "million", number / million, !isExactOrdinal));
            number %= million;
        }

        bool thousand = false;
        if (number / 1000 > 0)
        {
            thousand = true;
            parts.Add(Part("{0}tusen", number % 1000 < 100 ? "tusen" : "ettusen", number / 1000));
            number %= 1000;
        }

        bool hundred = false;
        if (number / 100 > 0)
        {
            hundred = true;
            parts.Add(Part("{0}hundre", thousand || millionOrMore ? "ethundre" : "hundre", number / 100));
            number %= 100;
        }

        if (number > 0)
        {
            if (parts.Count != 0)
            {
                if (millionOrMore && !hundred && !thousand)
                {
                    parts.Add("og ");
                }
                else
                {
                    parts.Add("og");
                }
            }

            if (number < 20)
            {
                parts.Add(GetUnitValue(number, isOrdinal));
            }
            else
            {
                string lastPart = _tensMap[number / 10];
                if (number % 10 > 0)
                {
                    lastPart += string.Format("{0}", GetUnitValue(number % 10, isOrdinal));
                }
                else if (isOrdinal)
                {
                    lastPart = lastPart.TrimEnd('e') + "ende";
                }

                parts.Add(lastPart);
            }
        }
        else if (isOrdinal)
        {
            parts[parts.Count - 1] += (number == 0 ? string.Empty : "en") + (millionOrMore ? "te" : "de");
        }

        string toWords = string.Join(string.Empty, parts.ToArray()).Trim();

        return toWords;
    }

    private static string GetUnitValue(int number, bool isOrdinal)
    {
        if (isOrdinal)
        {
            if (ExceptionNumbersToWords(number, out string? exceptionString))
            {
                return exceptionString;
            }
            else if (number < 13)
            {
                return _unitsMap[number].TrimEnd('e') + "ende";
            }
            else
            {
                return _unitsMap[number] + "de";
            }
        }
        else
        {
            return _unitsMap[number];
        }
    }

    private static bool ExceptionNumbersToWords(int number, [MaybeNullWhen(false)] out string words)
    {
        return _ordinalExceptions.TryGetValue(number, out words);
    }

    private string Part(string pluralFormat, string singular, int number, bool postfixSpace = false)
    {
        string postfix = postfixSpace ? " " : string.Empty;
        if (number == 1)
        {
            return singular + postfix;
        }

        return string.Format(pluralFormat, Convert(number)) + postfix;
    }
}
