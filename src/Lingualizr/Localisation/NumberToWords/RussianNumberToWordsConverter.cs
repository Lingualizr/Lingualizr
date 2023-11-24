using Lingualizr.Localisation.GrammaticalNumber;

namespace Lingualizr.Localisation.NumberToWords;

internal class RussianNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _hundredsMap = { "ноль", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };
    private static readonly string[] _tensMap = { "ноль", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
    private static readonly string[] _unitsMap =
    {
        "ноль",
        "один",
        "два",
        "три",
        "четыре",
        "пять",
        "шесть",
        "семь",
        "восемь",
        "девять",
        "десять",
        "одиннадцать",
        "двенадцать",
        "тринадцать",
        "четырнадцать",
        "пятнадцать",
        "шестнадцать",
        "семнадцать",
        "восемнадцать",
        "девятнадцать"
    };
    private static readonly string[] _unitsOrdinalPrefixes =
    {
        string.Empty,
        string.Empty,
        "двух",
        "трёх",
        "четырёх",
        "пяти",
        "шести",
        "семи",
        "восьми",
        "девяти",
        "десяти",
        "одиннадцати",
        "двенадцати",
        "тринадцати",
        "четырнадцати",
        "пятнадцати",
        "шестнадцати",
        "семнадцати",
        "восемнадцати",
        "девятнадцати"
    };
    private static readonly string[] _tensOrdinalPrefixes = { string.Empty, "десяти", "двадцати", "тридцати", "сорока", "пятидесяти", "шестидесяти", "семидесяти", "восьмидесяти", "девяносто" };
    private static readonly string[] _tensOrdinal = { string.Empty, "десят", "двадцат", "тридцат", "сороков", "пятидесят", "шестидесят", "семидесят", "восьмидесят", "девяност" };
    private static readonly string[] _unitsOrdinal =
    {
        string.Empty,
        "перв",
        "втор",
        "трет",
        "четверт",
        "пят",
        "шест",
        "седьм",
        "восьм",
        "девят",
        "десят",
        "одиннадцат",
        "двенадцат",
        "тринадцат",
        "четырнадцат",
        "пятнадцат",
        "шестнадцат",
        "семнадцат",
        "восемнадцат",
        "девятнадцат"
    };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number == 0)
        {
            return "ноль";
        }

        var parts = new List<string>();

        if (number < 0)
        {
            parts.Add("минус");
        }

        CollectParts(parts, ref number, 1000000000000000000, GrammaticalGender.Masculine, "квинтиллион", "квинтиллиона", "квинтиллионов");
        CollectParts(parts, ref number, 1000000000000000, GrammaticalGender.Masculine, "квадриллион", "квадриллиона", "квадриллионов");
        CollectParts(parts, ref number, 1000000000000, GrammaticalGender.Masculine, "триллион", "триллиона", "триллионов");
        CollectParts(parts, ref number, 1000000000, GrammaticalGender.Masculine, "миллиард", "миллиарда", "миллиардов");
        CollectParts(parts, ref number, 1000000, GrammaticalGender.Masculine, "миллион", "миллиона", "миллионов");
        CollectParts(parts, ref number, 1000, GrammaticalGender.Feminine, "тысяча", "тысячи", "тысяч");

        if (number > 0)
        {
            CollectPartsUnderOneThousand(parts, number, gender);
        }

        return string.Join(" ", parts);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        if (number == 0)
        {
            return "нулев" + GetEndingForGender(gender, number);
        }

        var parts = new List<string>();

        if (number < 0)
        {
            parts.Add("минус");
            number = -number;
        }

        var numberLong = (long)number;
        CollectOrdinalParts(parts, ref numberLong, 1000000000, GrammaticalGender.Masculine, "миллиардн" + GetEndingForGender(gender, numberLong), "миллиард", "миллиарда", "миллиардов");
        CollectOrdinalParts(parts, ref numberLong, 1000000, GrammaticalGender.Masculine, "миллионн" + GetEndingForGender(gender, numberLong), "миллион", "миллиона", "миллионов");
        CollectOrdinalParts(parts, ref numberLong, 1000, GrammaticalGender.Feminine, "тысячн" + GetEndingForGender(gender, numberLong), "тысяча", "тысячи", "тысяч");

        if (numberLong >= 100)
        {
            var ending = GetEndingForGender(gender, numberLong);
            var hundreds = numberLong / 100;
            numberLong %= 100;
            if (numberLong == 0)
            {
                parts.Add(_unitsOrdinalPrefixes[hundreds] + "сот" + ending);
            }
            else
            {
                parts.Add(_hundredsMap[hundreds]);
            }
        }

        if (numberLong >= 20)
        {
            var ending = GetEndingForGender(gender, numberLong);
            var tens = numberLong / 10;
            numberLong %= 10;
            if (numberLong == 0)
            {
                parts.Add(_tensOrdinal[tens] + ending);
            }
            else
            {
                parts.Add(_tensMap[tens]);
            }
        }

        if (numberLong > 0)
        {
            parts.Add(_unitsOrdinal[numberLong] + GetEndingForGender(gender, numberLong));
        }

        return string.Join(" ", parts);
    }

    private static void CollectPartsUnderOneThousand(ICollection<string> parts, long number, GrammaticalGender gender)
    {
        if (number >= 100)
        {
            var hundreds = number / 100;
            number %= 100;
            parts.Add(_hundredsMap[hundreds]);
        }

        if (number >= 20)
        {
            var tens = number / 10;
            parts.Add(_tensMap[tens]);
            number %= 10;
        }

        if (number > 0)
        {
            if (number == 1 && gender == GrammaticalGender.Feminine)
            {
                parts.Add("одна");
            }
            else if (number == 1 && gender == GrammaticalGender.Neuter)
            {
                parts.Add("одно");
            }
            else if (number == 2 && gender == GrammaticalGender.Feminine)
            {
                parts.Add("две");
            }
            else
            {
                parts.Add(_unitsMap[number]);
            }
        }
    }

    private static string GetPrefix(long number)
    {
        var parts = new List<string>();

        if (number >= 100)
        {
            var hundreds = number / 100;
            number %= 100;
            if (hundreds != 1)
            {
                parts.Add(_unitsOrdinalPrefixes[hundreds] + "сот");
            }
            else
            {
                parts.Add("сто");
            }
        }

        if (number >= 20)
        {
            var tens = number / 10;
            number %= 10;
            parts.Add(_tensOrdinalPrefixes[tens]);
        }

        if (number > 0)
        {
            parts.Add(number == 1 ? "одно" : _unitsOrdinalPrefixes[number]);
        }

        return string.Join(string.Empty, parts);
    }

    private static void CollectParts(ICollection<string> parts, ref long number, long divisor, GrammaticalGender gender, params string[] forms)
    {
        var result = Math.Abs(number / divisor);
        if (result == 0)
        {
            return;
        }

        number = Math.Abs(number % divisor);

        CollectPartsUnderOneThousand(parts, result, gender);
        parts.Add(ChooseOneForGrammaticalNumber(result, forms));
    }

    private static void CollectOrdinalParts(ICollection<string> parts, ref long number, int divisor, GrammaticalGender gender, string prefixedForm, params string[] forms)
    {
        if (number < divisor)
        {
            return;
        }

        var result = number / divisor;
        number %= divisor;
        if (number == 0)
        {
            if (result == 1)
            {
                parts.Add(prefixedForm);
            }
            else
            {
                parts.Add(GetPrefix(result) + prefixedForm);
            }
        }
        else
        {
            CollectPartsUnderOneThousand(parts, result, gender);
            parts.Add(ChooseOneForGrammaticalNumber(result, forms));
        }
    }

    private static int GetIndex(RussianGrammaticalNumber number)
    {
        if (number == RussianGrammaticalNumber.Singular)
        {
            return 0;
        }

        if (number == RussianGrammaticalNumber.Paucal)
        {
            return 1;
        }

        return 2;
    }

    private static string ChooseOneForGrammaticalNumber(long number, string[] forms)
    {
        return forms[GetIndex(RussianGrammaticalNumberDetector.Detect(number))];
    }

    private static string GetEndingForGender(GrammaticalGender gender, long number)
    {
        switch (gender)
        {
            case GrammaticalGender.Masculine:
                if (number == 0 || number == 2 || number == 6 || number == 7 || number == 8 || number == 40)
                {
                    return "ой";
                }

                if (number == 3)
                {
                    return "ий";
                }

                return "ый";
            case GrammaticalGender.Feminine:
                if (number == 3)
                {
                    return "ья";
                }

                return "ая";
            case GrammaticalGender.Neuter:
                if (number == 3)
                {
                    return "ье";
                }

                return "ое";
            default:
                throw new ArgumentOutOfRangeException(nameof(gender));
        }
    }
}
