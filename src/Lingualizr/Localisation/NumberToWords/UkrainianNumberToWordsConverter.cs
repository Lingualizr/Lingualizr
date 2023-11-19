using Lingualizr.Localisation.GrammaticalNumber;

namespace Lingualizr.Localisation.NumberToWords;

internal class UkrainianNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _hundredsMap = { "нуль", "сто", "двісті", "триста", "чотириста", "п'ятсот", "шістсот", "сімсот", "вісімсот", "дев'ятсот" };
    private static readonly string[] _tensMap = { "нуль", "десять", "двадцять", "тридцять", "сорок", "п'ятдесят", "шістдесят", "сімдесят", "вісімдесят", "дев'яносто" };
    private static readonly string[] _unitsMap = { "нуль", "один", "два", "три", "чотири", "п'ять", "шість", "сім", "вісім", "дев'ять", "десять", "одинадцять", "дванадцять", "тринадцять", "чотирнадцять", "п'ятнадцять", "шістнадцять", "сімнадцять", "вісімнадцять", "дев'ятнадцять" };
    private static readonly string[] _unitsOrdinalPrefixes = { string.Empty, string.Empty, "двох", "трьох", "чотирьох", "п'яти", "шести", "семи", "восьми", "дев'яти", "десяти", "одинадцяти", "дванадцяти", "тринадцяти", "чотирнадцяти", "п'ятнадцяти", "шістнадцяти", "сімнадцяти", "вісімнадцяти", "дев'ятнадцяти", "двадцяти" };
    private static readonly string[] _tensOrdinalPrefixes = { string.Empty, "десяти", "двадцяти", "тридцяти", "сорока", "п'ятдесяти", "шістдесяти", "сімдесяти", "вісімдесяти", "дев'яносто" };
    private static readonly string[] _tensOrdinal = { string.Empty, "десят", "двадцят", "тридцят", "сороков", "п'ятдесят", "шістдесят", "сімдесят", "вісімдесят", "дев'яност" };
    private static readonly string[] _unitsOrdinal = { "нульов", "перш", "друг", "трет", "четверт", "п'ят", "шост", "сьом", "восьм", "дев'ят", "десят", "одинадцят", "дванадцят", "тринадцят", "чотирнадцят", "п'ятнадцят", "шістнадцят", "сімнадцят", "вісімнадцят", "дев'ятнадцят" };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number == 0)
        {
            return "нуль";
        }

        var parts = new List<string>();

        if (number < 0)
        {
            parts.Add("мінус");
        }

        CollectParts(parts, ref number, 1000000000000000000, GrammaticalGender.Masculine, "квінтильйон", "квінтильйона", "квінтильйонів");
        CollectParts(parts, ref number, 1000000000000000, GrammaticalGender.Masculine, "квадрильйон", "квадрильйона", "квадрильйонів");
        CollectParts(parts, ref number, 1000000000000, GrammaticalGender.Masculine, "трильйон", "трильйона", "трильйонів");
        CollectParts(parts, ref number, 1000000000, GrammaticalGender.Masculine, "мільярд", "мільярда", "мільярдів");
        CollectParts(parts, ref number, 1000000, GrammaticalGender.Masculine, "мільйон", "мільйона", "мільйонів");
        CollectParts(parts, ref number, 1000, GrammaticalGender.Feminine, "тисяча", "тисячі", "тисяч");

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
            return "нульов" + GetEndingForGender(gender, number);
        }

        var parts = new List<string>();

        if (number < 0)
        {
            parts.Add("мінус");
            number = -number;
        }

        CollectOrdinalParts(parts, ref number, 1000000000, GrammaticalGender.Masculine, "мільярдн" + GetEndingForGender(gender, number), "мільярд", "мільярда", "мільярдів");
        CollectOrdinalParts(parts, ref number, 1000000, GrammaticalGender.Masculine, "мільйонн" + GetEndingForGender(gender, number), "мільйон", "мільйона", "мільйонів");
        CollectOrdinalParts(parts, ref number, 1000, GrammaticalGender.Feminine, "тисячн" + GetEndingForGender(gender, number), "тисяча", "тисячі", "тисяч");

        if (number >= 100)
        {
            var ending = GetEndingForGender(gender, number);
            var hundreds = number / 100;
            number %= 100;
            if (number == 0)
            {
                parts.Add(_unitsOrdinalPrefixes[hundreds] + "сот" + ending);
            }
            else
            {
                parts.Add(_hundredsMap[hundreds]);
            }
        }

        if (number >= 20)
        {
            var ending = GetEndingForGender(gender, number);
            var tens = number / 10;
            number %= 10;
            if (number == 0)
            {
                parts.Add(_tensOrdinal[tens] + ending);
            }
            else
            {
                parts.Add(_tensMap[tens]);
            }
        }

        if (number > 0)
        {
            parts.Add(_unitsOrdinal[number] + GetEndingForGender(gender, number));
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
                parts.Add("одне");
            }
            else if (number == 2 && gender == GrammaticalGender.Feminine)
            {
                parts.Add("дві");
            }
            else
            {
                parts.Add(_unitsMap[number]);
            }
        }
    }

    private static string GetPrefix(int number)
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

    private static void CollectOrdinalParts(ICollection<string> parts, ref int number, int divisor, GrammaticalGender gender, string prefixedForm, params string[] forms)
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

    private static string GetEndingForGender(GrammaticalGender gender, int number)
    {
        switch (gender)
        {
            case GrammaticalGender.Masculine:
                if (number == 3)
                {
                    return "ій";
                }

                return "ий";
            case GrammaticalGender.Feminine:
                if (number == 3)
                {
                    return "я";
                }

                return "а";
            case GrammaticalGender.Neuter:
                if (number == 3)
                {
                    return "є";
                }

                return "е";
            default:
                throw new ArgumentOutOfRangeException(nameof(gender));
        }
    }
}
