﻿using System.Diagnostics.CodeAnalysis;

namespace Lingualizr.Localisation.NumberToWords;

internal class AfrikaansNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap =
    {
        "nul",
        "een",
        "twee",
        "drie",
        "vier",
        "vyf",
        "ses",
        "sewe",
        "agt",
        "nege",
        "tien",
        "elf",
        "twaalf",
        "dertien",
        "veertien",
        "vyftien",
        "sestien",
        "sewentien",
        "agtien",
        "negentien"
    };
    private static readonly string[] _tensMap = { "nul", "tien", "twintig", "dertig", "veertig", "vyftig", "sestig", "sewentig", "tagtig", "negentig" };

    private static readonly Dictionary<int, string> _ordinalExceptions =
        new()
        {
            { 0, "nulste" },
            { 1, "eerste" },
            { 3, "derde" },
            { 7, "sewende" },
            { 8, "agste" },
            { 9, "negende" },
            { 10, "tiende" },
            { 14, "veertiende" },
            { 17, "sewentiende" },
            { 19, "negentiende" },
        };

    public override string Convert(long number)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        return Convert((int)number, false);
    }

    public override string ConvertToOrdinal(int number)
    {
        return Convert(number, true);
    }

    private string Convert(int number, bool isOrdinal)
    {
        if (number == 0)
        {
            return GetUnitValue(0, isOrdinal);
        }

        if (number < 0)
        {
            return string.Format("minus {0}", Convert(-number));
        }

        List<string> parts = new();

        if (number / 1000000000 > 0)
        {
            parts.Add(string.Format("{0} miljard", Convert(number / 1000000000)));
            number %= 1000000000;
        }

        if (number / 1000000 > 0)
        {
            parts.Add(string.Format("{0} miljoen", Convert(number / 1000000)));
            number %= 1000000;
        }

        if (number / 1000 > 0)
        {
            parts.Add(string.Format("{0} duisend", Convert(number / 1000)));
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            parts.Add(string.Format("{0} honderd", Convert(number / 100)));
            number %= 100;
        }

        if (number > 0)
        {
            if (number < 20)
            {
                if (parts.Count > 0)
                {
                    parts.Add("en");
                }

                parts.Add(GetUnitValue(number, isOrdinal));
            }
            else
            {
                int lastPartValue = number / 10 * 10;
                string lastPart = _tensMap[number / 10];
                if (number % 10 > 0)
                {
                    lastPart = string.Format("{0} en {1}", GetUnitValue(number % 10, false), isOrdinal ? GetUnitValue(lastPartValue, isOrdinal) : lastPart);
                }
                else if (number % 10 == 0)
                {
                    lastPart = string.Format("{0}{1}{2}", parts.Count > 0 ? "en " : string.Empty, lastPart, isOrdinal ? "ste" : string.Empty);
                }
                else if (isOrdinal)
                {
                    lastPart = lastPart.TrimEnd('~') + "ste";
                }

                parts.Add(lastPart);
            }
        }
        else if (isOrdinal)
        {
            parts[parts.Count - 1] += "ste";
        }

        string toWords = string.Join(" ", parts.ToArray());

        if (isOrdinal)
        {
            toWords = RemoveOnePrefix(toWords);
        }

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
            else if (number > 19)
            {
                return _tensMap[number / 10] + "ste";
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

    private static string RemoveOnePrefix(string toWords)
    {
        // one hundred => hundredth
        if (toWords.StartsWith("een", StringComparison.Ordinal) && !toWords.StartsWith("een en", StringComparison.Ordinal))
        {
            toWords = toWords.Remove(0, 4);
        }

        return toWords;
    }

    private static bool ExceptionNumbersToWords(int number, [MaybeNullWhen(false)] out string words)
    {
        return _ordinalExceptions.TryGetValue(number, out words);
    }
}
