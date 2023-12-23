namespace Lingualizr.Localisation.NumberToWords;

internal class MalteseNumberToWordsConvertor : GenderedNumberToWordsConverter
{
    private static readonly string[] _ordinalOverrideMap =
    {
        "0",
        "l-ewwel",
        "it-tieni",
        "it-tielet",
        "ir-raba'",
        "il-ħames",
        "is-sitt",
        "is-seba'",
        "it-tmien",
        "id-disa'",
        "l-għaxar",
        "il-ħdax",
        "it-tnax",
        "it-tlettax",
        "l-erbatax",
        "il-ħmistax",
        "is-sittax",
        "is-sbatax",
        "it-tmintax",
        "id-dsatax",
        "l-għoxrin",
    };

    private static readonly string[] _unitsMap =
    {
        "żero",
        "wieħed",
        "tnejn",
        "tlieta",
        "erbgħa",
        "ħamsa",
        "sitta",
        "sebgħa",
        "tmienja",
        "disgħa",
        "għaxra",
        "ħdax",
        "tnax",
        "tlettax",
        "erbatax",
        "ħmistax",
        "sittax",
        "sbatax",
        "tmintax",
        "dsatax",
    };

    private static readonly string[] _tensMap = { "zero", "għaxra", "għoxrin", "tletin", "erbgħin", "ħamsin", "sittin", "sebgħin", "tmenin", "disgħin", };

    private static readonly string[] _hundredsMap = { string.Empty, string.Empty, string.Empty, "tlett", "erbgħa", "ħames", "sitt", "sebgħa", "tminn", "disgħa", "għaxar", };

    private static readonly string[] _prefixMap =
    {
        string.Empty,
        string.Empty,
        string.Empty,
        "tlett",
        "erbat",
        "ħamest",
        "sitt",
        "sebat",
        "tmint",
        "disat",
        "għaxart",
        "ħdax-il",
        "tnax-il",
        "tletax-il",
        "erbatax-il",
        "ħmistax-il",
        "sittax-il",
        "sbatax-il",
        "tmintax-il",
        "dsatax-il",
    };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        bool negativeNumber = false;

        if (number < 0)
        {
            negativeNumber = true;
            number = number * -1;
        }

        if (number < 1000000000)
        {
            return GetMillions(number, gender) + (negativeNumber ? " inqas minn żero" : string.Empty);
        }

        long billions = number / 1000000000;
        long tensInBillions = billions % 100;
        long millions = number % 1000000000;

        string billionsText = GetPrefixText(billions, tensInBillions, "biljun", "żewġ biljuni", "biljuni", false, gender);
        string millionsText = GetMillions(millions, gender);

        if (millions == 0)
        {
            return billionsText;
        }

        return $"{billionsText} u {millionsText}" + (negativeNumber ? " inqas minn żero" : string.Empty);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        if (number <= 20)
        {
            return _ordinalOverrideMap[number];
        }

        string ordinal = Convert(number, gender);

        if (ordinal.StartsWith('d'))
        {
            return $"id-{Convert(number, gender)}";
        }

        if (ordinal.StartsWith('s'))
        {
            return $"is-{Convert(number, gender)}";
        }

        if (ordinal.StartsWith('t'))
        {
            return $"it-{Convert(number, gender)}";
        }

        if (ordinal.StartsWith('e'))
        {
            return $"l-{Convert(number, gender)}";
        }

        return $"il-{Convert(number, gender)}";
    }

    private static string GetTens(long value, bool usePrefixMap, bool usePrefixMapForLowerDigits, GrammaticalGender gender)
    {
        if (value == 1 && gender == GrammaticalGender.Feminine)
        {
            return "waħda";
        }

        if (value < 11 && usePrefixMap)
        {
            return usePrefixMapForLowerDigits ? _prefixMap[value] : _hundredsMap[value];
        }

        if (value > 10 && value < 20 && usePrefixMap)
        {
            return _prefixMap[value];
        }

        if (value < 20)
        {
            return _unitsMap[value];
        }

        long single = value % 10;
        long numberOfTens = value / 10;
        if (single == 0)
        {
            return _tensMap[numberOfTens];
        }

        return $"{_unitsMap[single]} u {_tensMap[numberOfTens]}";
    }

    private static string GetHundreds(long value, bool usePrefixMap, bool usePrefixMapForLowerValueDigits, GrammaticalGender gender)
    {
        if (value < 100)
        {
            return GetTens(value, usePrefixMap, usePrefixMapForLowerValueDigits, gender);
        }

        long tens = value % 100;
        long numberOfHundreds = value / 100;

        string hundredsText;
        if (numberOfHundreds == 1)
        {
            hundredsText = "mija";
        }
        else if (numberOfHundreds == 2)
        {
            hundredsText = "mitejn";
        }
        else
        {
            hundredsText = _hundredsMap[numberOfHundreds] + " mija";
        }

        if (tens == 0)
        {
            return hundredsText;
        }

        return $"{hundredsText} u {GetTens(tens, usePrefixMap, usePrefixMapForLowerValueDigits, gender)}";
    }

    private static string GetThousands(long value, GrammaticalGender gender)
    {
        if (value < 1000)
        {
            return GetHundreds(value, false, false, gender);
        }

        long thousands = value / 1000;
        long tensInThousands = thousands % 100;
        long hundreds = value % 1000;

        string thousandsInText = GetPrefixText(thousands, tensInThousands, "elf", "elfejn", "elef", true, gender);

        string hundredsInText = GetHundreds(hundreds, false, false, gender);

        if (hundreds == 0)
        {
            return thousandsInText;
        }

        return $"{thousandsInText} u {hundredsInText}";
    }

    private static string GetMillions(long value, GrammaticalGender gender)
    {
        if (value < 1000000)
        {
            return GetThousands(value, gender);
        }

        long millions = value / 1000000;
        long tensInMillions = millions % 100;
        long thousands = value % 1000000;

        string millionsText = GetPrefixText(millions, tensInMillions, "miljun", "żewġ miljuni", "miljuni", false, gender);
        string thousandsText = GetThousands(thousands, gender);

        if (thousands == 0)
        {
            return millionsText;
        }

        return $"{millionsText} u {thousandsText}";
    }

    private static string GetPrefixText(long thousands, long tensInThousands, string singular, string dual, string plural, bool usePrefixMapForLowerValueDigits, GrammaticalGender gender)
    {
        if (thousands == 1)
        {
            return singular;
        }

        if (thousands == 2)
        {
            return dual;
        }

        if (tensInThousands > 10)
        {
            return $"{GetHundreds(thousands, true, usePrefixMapForLowerValueDigits, gender)} {singular}";
        }

        if (thousands == 100)
        {
            return $"mitt {singular}";
        }

        if (thousands == 101)
        {
            return $"mija u {singular}";
        }

        return $"{GetHundreds(thousands, true, usePrefixMapForLowerValueDigits, gender)} {plural}";
    }
}
