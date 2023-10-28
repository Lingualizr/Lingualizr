using System;
using System.Collections.Generic;

namespace Lingualizr.Localisation.NumberToWords;

internal abstract class GermanNumberToWordsConverterBase : GenderedNumberToWordsConverter
{
    private readonly string[] _unitsMap = { "null", "ein", "zwei", "drei", "vier", "fünf", "sechs", "sieben", "acht", "neun", "zehn", "elf", "zwölf", "dreizehn", "vierzehn", "fünfzehn", "sechzehn", "siebzehn", "achtzehn", "neunzehn" };
    private readonly string[] _tensMap = { "null", "zehn", "zwanzig", "dreißig", "vierzig", "fünfzig", "sechzig", "siebzig", "achtzig", "neunzig" };
    private readonly string[] _unitsOrdinal = { string.Empty, "ers", "zwei", "drit", "vier", "fünf", "sechs", "sieb", "ach", "neun", "zehn", "elf", "zwölf", "dreizehn", "vierzehn", "fünfzehn", "sechzehn", "siebzehn", "achtzehn", "neunzehn" };
    private readonly string[] _hundredOrdinalSingular = { "einhundert" };
    private readonly string[] _hundredOrdinalPlural = { "{0}hundert" };
    private readonly string[] _thousandOrdinalSingular = { "eintausend" };
    private readonly string[] _thousandOrdinalPlural = { "{0}tausend" };
    private readonly string[] _millionOrdinalSingular = { "einmillion", "einemillion" };
    private readonly string[] _millionOrdinalPlural = { "{0}million", "{0}millionen" };
    private readonly string[] _billionOrdinalSingular = { "einmilliard", "einemilliarde" };
    private readonly string[] _billionOrdinalPlural = { "{0}milliard", "{0}milliarden" };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number == 0)
        {
            return _unitsMap[number];
        }

        var parts = new List<string>();
        if (number < 0)
        {
            parts.Add("minus ");
            number = -number;
        }

        CollectParts(parts, ref number, 1000000000000000000, true, "{0} Trillionen", "eine Trillion");
        CollectParts(parts, ref number, 1000000000000000, true, "{0} Billiarden", "eine Billiarde");
        CollectParts(parts, ref number, 1000000000000, true, "{0} Billionen", "eine Billion");
        CollectParts(parts, ref number, 1000000000, true, "{0} Milliarden", "eine Milliarde");
        CollectParts(parts, ref number, 1000000, true, "{0} Millionen", "eine Million");
        CollectParts(parts, ref number, 1000, false, "{0}tausend", "eintausend");
        CollectParts(parts, ref number, 100, false, "{0}hundert", "einhundert");

        if (number > 0)
        {
            if (number < 20)
            {
                if (number == 1 && gender == GrammaticalGender.Feminine)
                {
                    parts.Add("eine");
                }
                else
                {
                    parts.Add(_unitsMap[number]);
                }
            }
            else
            {
                var units = number % 10;
                if (units > 0)
                {
                    parts.Add(string.Format("{0}und", _unitsMap[units]));
                }

                parts.Add(GetTens(number / 10));
            }
        }

        return string.Join(string.Empty, parts);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        if (number == 0)
        {
            return _unitsMap[number] + GetEndingForGender(gender);
        }

        var parts = new List<string>();
        if (number < 0)
        {
            parts.Add("minus ");
            number = -number;
        }

        CollectOrdinalParts(parts, ref number, 1000000000, true, _billionOrdinalPlural, _billionOrdinalSingular);
        CollectOrdinalParts(parts, ref number, 1000000, true, _millionOrdinalPlural, _millionOrdinalSingular);
        CollectOrdinalParts(parts, ref number, 1000, false, _thousandOrdinalPlural, _thousandOrdinalSingular);
        CollectOrdinalParts(parts, ref number, 100, false, _hundredOrdinalPlural, _hundredOrdinalSingular);

        if (number > 0)
        {
            parts.Add(number < 20 ? _unitsOrdinal[number] : Convert(number));
        }

        if (number == 0 || number >= 20)
        {
            parts.Add("s");
        }

        parts.Add(GetEndingForGender(gender));

        return string.Join(string.Empty, parts);
    }

    private void CollectParts(ICollection<string> parts, ref long number, long divisor, bool addSpaceBeforeNextPart, string pluralFormat, string singular)
    {
        if (number / divisor > 0)
        {
            parts.Add(Part(pluralFormat, singular, number / divisor));
            number %= divisor;
            if (addSpaceBeforeNextPart && number > 0)
            {
                parts.Add(" ");
            }
        }
    }

    private void CollectOrdinalParts(ICollection<string> parts, ref int number, int divisor, bool evaluateNoRest, string[] pluralFormats, string[] singulars)
    {
        if (number / divisor > 0)
        {
            var noRest = evaluateNoRest ? NoRestIndex(number % divisor) : 0;
            parts.Add(Part(pluralFormats[noRest], singulars[noRest], number / divisor));
            number %= divisor;
        }
    }

    private string Part(string pluralFormat, string singular, long number)
    {
        if (number == 1)
        {
            return singular;
        }

        return string.Format(pluralFormat, Convert(number));
    }

    private static int NoRestIndex(int number)
    {
        return number == 0 ? 0 : 1;
    }

    private static string GetEndingForGender(GrammaticalGender gender)
    {
        switch (gender)
        {
            case GrammaticalGender.Masculine:
                return "ter";
            case GrammaticalGender.Feminine:
                return "te";
            case GrammaticalGender.Neuter:
                return "tes";
            default:
                throw new ArgumentOutOfRangeException(nameof(gender));
        }
    }

    protected virtual string GetTens(long tens)
    {
        return _tensMap[tens];
    }
}
