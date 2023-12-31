﻿namespace Lingualizr.Localisation.NumberToWords;

internal class BulgarianNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _unitsMap =
    {
        "нула",
        "един",
        "две",
        "три",
        "четири",
        "пет",
        "шест",
        "седем",
        "осем",
        "девет",
        "десет",
        "единадесет",
        "дванадесет",
        "тринадесет",
        "четиринадесет",
        "петнадесет",
        "шестнадесет",
        "седемнадесет",
        "осемнадесет",
        "деветнадесет",
    };

    private static readonly string[] _tensMap = { "нула", "десет", "двадесет", "тридесет", "четиридесет", "петдесет", "шестдесет", "седемдесет", "осемдесет", "деветдесет", };

    private static readonly string[] _hundredsMap = { "нула", "сто", "двеста", "триста", "четиристотин", "петстотин", "шестстотин", "седемстотин", "осемстотин", "деветстотин", };

    private static readonly string[] _hundredsOrdinalMap = { string.Empty, "стот", "двест", "трист", "четиристот", "петстот", "шестстот", "седемстот", "осемстот", "деветстот", };

    private static readonly string[] _unitsOrdinal =
    {
        string.Empty,
        "първ",
        "втор",
        "трет",
        "четвърт",
        "пет",
        "шест",
        "седм",
        "осм",
        "девeт",
        "десeт",
        "единадесет",
        "дванадесет",
        "тринадесет",
        "четиринадесет",
        "петнадесет",
        "шестнадесет",
        "седемнадесет",
        "осемнадесет",
        "деветнадесет",
    };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        return ConvertInternal(number, gender, false);
    }

    private static string ConvertInternal(long input, GrammaticalGender gender, bool isOrdinal)
    {
        if (input > int.MaxValue || input < int.MinValue)
        {
            throw new NotImplementedException();
        }

        if (input == 0)
        {
            return isOrdinal ? "нулев" + GetEndingForGender(gender, input) : "нула";
        }

        List<string> parts = new();

        if (input < 0)
        {
            parts.Add("минус");
            input = -input;
        }

        string lastOrdinalSubstitution = string.Empty;

        if (input / 1000000000000 > 0)
        {
            if (isOrdinal)
            {
                lastOrdinalSubstitution = ConvertInternal(input / 1000000000000, gender, false) + " трилион" + GetEndingForGender(gender, input);
            }

            input %= 1000000000000;
        }

        if (input / 1000000000 > 0)
        {
            parts.Add(ConvertInternal(input / 1000000000, gender, false) + $" {(input < 2000000000 ? "милиард" : "милиарда")}");

            if (isOrdinal)
            {
                lastOrdinalSubstitution = ConvertInternal(input / 1000000000, gender, false) + " милиард" + GetEndingForGender(gender, input);
            }

            input %= 1000000000;
        }

        if (input / 1000000 > 0)
        {
            parts.Add(ConvertInternal(input / 1000000, gender, false) + $" {(input < 2000000 ? "милион" : "милиона")}");

            if (isOrdinal)
            {
                lastOrdinalSubstitution = ConvertInternal(input / 1000000, gender, false) + " милион" + GetEndingForGender(gender, input);
            }

            input %= 1000000;
        }

        if (input / 1000 > 0)
        {
            if (input < 2000)
            {
                parts.Add("хиляда");
            }
            else
            {
                parts.Add(ConvertInternal(input / 1000, gender, false) + " хиляди");
            }

            if (isOrdinal)
            {
                lastOrdinalSubstitution = ConvertInternal(input / 1000, gender, false) + " хиляд" + GetEndingForGender(gender, input);
            }

            input %= 1000;
        }

        if (input / 100 > 0)
        {
            parts.Add(_hundredsMap[(int)input / 100]);

            if (isOrdinal)
            {
                lastOrdinalSubstitution = _hundredsOrdinalMap[(int)input / 100] + GetEndingForGender(gender, input);
            }

            input %= 100;
        }

        if (input > 19)
        {
            parts.Add(_tensMap[input / 10]);

            if (isOrdinal)
            {
                lastOrdinalSubstitution = _tensMap[(int)input / 10] + GetEndingForGender(gender, input);
            }

            input %= 10;
        }

        if (input > 0)
        {
            parts.Add(_unitsMap[input]);

            if (isOrdinal)
            {
                lastOrdinalSubstitution = _unitsOrdinal[input] + GetEndingForGender(gender, input);
            }
        }

        if (parts.Count > 1)
        {
            parts.Insert(parts.Count - 1, "и");
        }

        if (isOrdinal && !string.IsNullOrWhiteSpace(lastOrdinalSubstitution))
        {
            parts[parts.Count - 1] = lastOrdinalSubstitution;
        }

        return string.Join(" ", parts);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        return ConvertInternal(number, gender, true);
    }

    private static string GetEndingForGender(GrammaticalGender gender, long input)
    {
        if (input == 0)
        {
            return gender switch
            {
                GrammaticalGender.Masculine => string.Empty,
                GrammaticalGender.Feminine => "а",
                GrammaticalGender.Neuter => "о",
                _ => throw new ArgumentOutOfRangeException(nameof(gender)),
            };
        }

        if (input < 99)
        {
            return gender switch
            {
                GrammaticalGender.Masculine => "и",
                GrammaticalGender.Feminine => "а",
                GrammaticalGender.Neuter => "о",
                _ => throw new ArgumentOutOfRangeException(nameof(gender)),
            };
        }
        else
        {
            return gender switch
            {
                GrammaticalGender.Masculine => "ен",
                GrammaticalGender.Feminine => "на",
                GrammaticalGender.Neuter => "но",
                _ => throw new ArgumentOutOfRangeException(nameof(gender)),
            };
        }
    }
}
