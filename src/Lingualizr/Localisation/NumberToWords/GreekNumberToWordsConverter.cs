namespace Lingualizr.Localisation.NumberToWords;

internal class GreekNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private readonly string[] _unitMap = { "μηδέν", "ένα", "δύο", "τρία", "τέσσερα", "πέντε", "έξι", "επτά", "οκτώ", "εννέα", "δέκα", "έντεκα", "δώδεκα" };

    private readonly string[] _unitsMap = { "μηδέν", "ένα", "δύο", "τρείς", "τέσσερις", "πέντε", "έξι", "επτά", "οκτώ", "εννέα", "δέκα", "έντεκα", "δώδεκα" };

    private readonly string[] _tensMap = { string.Empty, "δέκα", "είκοσι", "τριάντα", "σαράντα", "πενήντα", "εξήντα", "εβδομήντα", "ογδόντα", "ενενήντα" };

    private readonly string[] _tensNoDiacriticsMap = { string.Empty, "δεκα", "εικοσι", "τριαντα", "σαραντα", "πενηντα", "εξηντα", "εβδομηντα", "ογδοντα", "ενενηντα" };

    private readonly string[] _hundredMap = { string.Empty, "εκατό", "διακόσια", "τριακόσια", "τετρακόσια", "πεντακόσια", "εξακόσια", "επτακόσια", "οκτακόσια", "εννιακόσια" };

    private readonly string[] _hundredsMap = { string.Empty, "εκατόν", "διακόσιες", "τριακόσιες", "τετρακόσιες", "πεντακόσιες", "εξακόσιες", "επτακόσιες", "οκτακόσιες", "Εενιακόσιες" };

    private static readonly Dictionary<long, string> _οrdinalMap =
        new()
        {
            { 0, string.Empty },
            { 1, "πρώτος" },
            { 2, "δεύτερος" },
            { 3, "τρίτος" },
            { 4, "τέταρτος" },
            { 5, "πέμπτος" },
            { 6, "έκτος" },
            { 7, "έβδομος" },
            { 8, "όγδοος" },
            { 9, "ένατος" },
            { 10, "δέκατος" },
            { 20, "εικοστός" },
            { 30, "τριακοστός" },
            { 40, "τεσσαρακοστός" },
            { 50, "πεντηκοστός" },
            { 60, "εξηκοστός" },
            { 70, "εβδομηκοστός" },
            { 80, "ογδοηκοστός" },
            { 90, "ενενηκοστός" },
            { 100, "εκατοστός" },
            { 200, "διακοσιοστός" },
            { 300, "τριακοσιοστός" },
            { 400, "τετρακοσιστός" },
            { 500, "πεντακοσιοστός" },
            { 600, "εξακοσιοστός" },
            { 700, "εφτακοσιοστός" },
            { 800, "οχτακοσιοστός" },
            { 900, "εννιακοσιοστός" },
            { 1000, "χιλιοστός" },
        };

    public override string Convert(long number)
    {
        return ConvertImpl(number, false);
    }

    public override string ConvertToOrdinal(int number)
    {
        if (number / 10 == 0)
        {
            return GetOneDigitOrdinal(number);
        }

        if (number / 10 > 0 && number / 10 < 10)
        {
            return GetTwoDigigOrdinal(number);
        }

        if (number / 100 > 0 && number / 100 < 10)
        {
            return GetThreeDigitOrdinal(number);
        }

        if (number / 1000 > 0 && number / 1000 < 10)
        {
            return GetFourDigitOrdinal(number);
        }

        return string.Empty;
    }

    private static string GetOneDigitOrdinal(int number)
    {
        if (!_οrdinalMap.TryGetValue(number, out var output))
        {
            return string.Empty;
        }

        return output;
    }

    private static string GetTwoDigigOrdinal(int number)
    {
        if (number == 11)
        {
            return "ενδέκατος";
        }

        if (number == 12)
        {
            return "δωδέκατος";
        }

        var decades = number / 10;

        if (!_οrdinalMap.TryGetValue(decades * 10, out var decadesString))
        {
            return string.Empty;
        }

        if (number - decades * 10 > 0)
        {
            return decadesString + " " + GetOneDigitOrdinal(number - decades * 10);
        }

        return decadesString;
    }

    private static string GetThreeDigitOrdinal(int number)
    {
        var hundrends = number / 100;

        if (!_οrdinalMap.TryGetValue(hundrends * 100, out var hundrentsString))
        {
            return string.Empty;
        }

        if (number - hundrends * 100 > 10)
        {
            return hundrentsString + " " + GetTwoDigigOrdinal(number - hundrends * 100);
        }

        if (number - hundrends * 100 > 0)
        {
            return hundrentsString + " " + GetOneDigitOrdinal(number - hundrends * 100);
        }

        return hundrentsString;
    }

    private static string GetFourDigitOrdinal(int number)
    {
        var thousands = number / 1000;

        if (!_οrdinalMap.TryGetValue(thousands * 1000, out var thousandsString))
        {
            return string.Empty;
        }

        if (number - thousands * 1000 > 100)
        {
            return thousandsString + " " + GetThreeDigitOrdinal(number - thousands * 1000);
        }

        if (number - thousands * 1000 > 10)
        {
            return thousandsString + " " + GetTwoDigigOrdinal(number - thousands * 1000);
        }

        if (number - thousands * 1000 > 0)
        {
            return thousandsString + " " + GetOneDigitOrdinal(number - thousands * 1000);
        }

        return thousandsString;
    }

    private string ConvertImpl(long number, bool returnPluralized)
    {
        if (number < 13)
        {
            return ConvertIntΒ13(number, returnPluralized);
        }
        else if (number < 100)
        {
            return ConvertIntBh(number, returnPluralized);
        }
        else if (number < 1000)
        {
            return ConvertIntBt(number, returnPluralized);
        }
        else if (number < 1000000)
        {
            return ConvertIntBm(number);
        }
        else if (number < 1000000000)
        {
            return ConvertIntBb(number);
        }
        else if (number < 1000000000000)
        {
            return ConvertIntBtr(number);
        }

        return string.Empty;
    }

    private string ConvertIntΒ13(long number, bool returnPluralized)
    {
        return returnPluralized ? _unitsMap[number] : _unitMap[number];
    }

    private string ConvertIntBh(long number, bool returnPluralized)
    {
        var result = number / 10 == 1 ? _tensNoDiacriticsMap[number / 10] : _tensMap[number / 10];

        if (number % 10 != 0)
        {
            if (number / 10 != 1)
            {
                result += " ";
            }

            result += ConvertImpl(number % 10, returnPluralized).ToLower();
        }

        return result;
    }

    private string ConvertIntBt(long number, bool returnPluralized)
    {
        string result;

        if (number / 100 == 1)
        {
            if (number % 100 == 0)
            {
                return _hundredMap[number / 100];
            }

            result = _hundredsMap[number / 100];
        }
        else
        {
            result = returnPluralized ? _hundredsMap[number / 100] : _hundredMap[number / 100];
        }

        if (number % 100 != 0)
        {
            result += $" {ConvertImpl(number % 100, returnPluralized).ToLower()}";
        }

        return result;
    }

    private string ConvertIntBm(long number)
    {
        if (number / 1000 == 1)
        {
            if (number % 1000 == 0)
            {
                return "χίλια";
            }

            return $"χίλια {ConvertImpl(number % 1000, false).ToLower()}";
        }

        var result = $"{ConvertImpl(number / 1000, true)} χιλιάδες";

        if (number % 1000 != 0)
        {
            result += $" {ConvertImpl(number % 1000, false).ToLower()}";
        }

        return result;
    }

    private string ConvertIntBb(long number)
    {
        if (number / 1000000 == 1)
        {
            if (number % 1000000 == 0)
            {
                return "ένα εκατομμύριο";
            }

            return $"ένα εκατομμύριο {ConvertImpl(number % 1000000, true).ToLower()}";
        }

        var result = $"{ConvertImpl(number / 1000000, false)} εκατομμύρια";

        if (number % 1000000 != 0)
        {
            result += $" {ConvertImpl(number % 1000000, false).ToLower()}";
        }

        return result;
    }

    private string ConvertIntBtr(long number)
    {
        if (number / 1000000000 == 1)
        {
            if (number % 1000000000 == 0)
            {
                return "ένα δισεκατομμύριο";
            }

            return $"ένα δισεκατομμύριο {ConvertImpl(number % 1000000000, true).ToLower()}";
        }

        var result = $"{ConvertImpl(number / 1000000000, false)} δισεκατομμύρια";

        if (number % 1000000000 != 0)
        {
            result += $" {ConvertImpl(number % 1000000000, false).ToLower()}";
        }

        return result;
    }
}
