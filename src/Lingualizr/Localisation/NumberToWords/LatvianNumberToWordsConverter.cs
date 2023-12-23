namespace Lingualizr.Localisation.NumberToWords;

internal class LatvianNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _unitsMap =
    {
        "nulle",
        "vien",
        "div",
        "trīs",
        "četr",
        "piec",
        "seš",
        "septiņ",
        "astoņ",
        "deviņ",
        "desmit",
        "vienpadsmit",
        "divpadsmit",
        "trīspadsmit",
        "četrpadsmit",
        "piecpadsmit",
        "sešpadsmit",
        "septiņpadsmit",
        "astoņpadsmit",
        "deviņpadsmit"
    };
    private static readonly string[] _tensMap = { "nulle", "desmit", "divdesmit", "trīsdesmit", "četrdesmit", "piecdesmit", "sešdesmit", "septiņdesmit", "astoņdesmit", "deviņdesmit" };
    private static readonly string[] _hundredsMap = { "nulle", "simt", "divsimt", "trīssimt", "četrsimt", "piecsimt", "sešsimt", "septiņsimt", "astoņsimt", "deviņsimt" };
    private static readonly string[] _unitsOrdinal =
    {
        string.Empty,
        "pirm",
        "otr",
        "treš",
        "ceturt",
        "piekt",
        "sest",
        "septīt",
        "astot",
        "devīt",
        "desmit",
        "vienpadsmit",
        "divpadsmit",
        "trīspadsmit",
        "četrpadsmit",
        "piecpadsmit",
        "sešpadsmit",
        "septiņpadsmit",
        "astoņpadsmit",
        "deviņpadsmit",
        "divdesmit"
    };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        var parts = new List<string>();

        if (number / 1000000 > 0)
        {
            string millionPart;
            if (number == 1000000)
            {
                millionPart = "miljons";
            }
            else
            {
                millionPart = Convert(number / 1000000, GrammaticalGender.Masculine) + " miljoni";
            }

            number %= 1000000;
            parts.Add(millionPart);
        }

        if (number / 1000 > 0)
        {
            string thousandsPart;
            if (number == 1000)
            {
                thousandsPart = "tūkstotis";
            }
            else if (number > 1000 && number < 2000)
            {
                thousandsPart = "tūkstoš";
            }
            else
            {
                thousandsPart = Convert(number / 1000, GrammaticalGender.Masculine) + " tūkstoši";
            }

            parts.Add(thousandsPart);
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            string hundredsPart;
            if (number == 100)
            {
                hundredsPart = parts.Contains("tūkstoš") ? "viens simts" : "simts";
            }
            else if (number > 100 && number < 200)
            {
                hundredsPart = "simtu";
            }
            else
            {
                hundredsPart = Convert(number / 100, GrammaticalGender.Masculine) + " simti";
            }

            parts.Add(hundredsPart);
            number %= 100;
        }

        if (number > 19)
        {
            var tensPart = _tensMap[number / 10];
            parts.Add(tensPart);
            number %= 10;
        }

        if (number > 0)
        {
            parts.Add(_unitsMap[number] + GetCardinalEndingForGender(gender, number));
        }

        return string.Join(" ", parts);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        if (number == 0)
        {
            return "nulle";
        }

        var parts = new List<string>();

        if (number < 0)
        {
            parts.Add("mīnus");
            number = -number;
        }

        var numberLong = (long)number;

        if (numberLong / 1000000 > 0)
        {
            string millionPart;
            if (numberLong == 1000000)
            {
                millionPart = "miljon" + GetOrdinalEndingForGender(gender);
            }
            else
            {
                millionPart = Convert(numberLong / 1000000, GrammaticalGender.Masculine) + " miljon" + GetOrdinalEndingForGender(gender);
            }

            numberLong %= 1000000;
            parts.Add(millionPart);
        }

        if (numberLong / 1000 > 0)
        {
            string thousandsPart;
            if (numberLong % 1000 == 0)
            {
                if (numberLong == 1000)
                {
                    thousandsPart = "tūkstoš" + GetOrdinalEndingForGender(gender);
                }
                else
                {
                    thousandsPart = Convert(numberLong / 1000, GrammaticalGender.Masculine) + " tūkstoš" + GetOrdinalEndingForGender(gender);
                }
            }
            else
            {
                if (numberLong > 1000 && numberLong < 2000)
                {
                    thousandsPart = "tūkstoš";
                }
                else
                {
                    thousandsPart = Convert(numberLong / 1000, GrammaticalGender.Masculine) + " tūkstoši";
                }
            }

            parts.Add(thousandsPart);
            numberLong %= 1000;
        }

        if (numberLong / 100 > 0)
        {
            string hundredsPart;
            if (numberLong % 100 == 0)
            {
                hundredsPart = _hundredsMap[numberLong / 100] + GetOrdinalEndingForGender(gender);
            }
            else
            {
                if (numberLong > 100 && numberLong < 200)
                {
                    hundredsPart = "simtu";
                }
                else
                {
                    hundredsPart = Convert(numberLong / 100, GrammaticalGender.Masculine) + " simti";
                }
            }

            parts.Add(hundredsPart);
            numberLong %= 100;
        }

        if (numberLong > 19)
        {
            var tensPart = _tensMap[numberLong / 10];
            if (numberLong % 10 == 0)
            {
                tensPart += GetOrdinalEndingForGender(gender);
            }

            parts.Add(tensPart);
            numberLong %= 10;
        }

        if (numberLong > 0)
        {
            parts.Add(_unitsOrdinal[numberLong] + GetOrdinalEndingForGender(gender));
        }

        return string.Join(" ", parts);
    }

    private static string GetOrdinalEndingForGender(GrammaticalGender gender)
    {
        switch (gender)
        {
            case GrammaticalGender.Masculine:
            {
                return "ais";
            }

            case GrammaticalGender.Feminine:
            {
                return "ā";
            }

            default:
                throw new ArgumentOutOfRangeException(nameof(gender));
        }
    }

    private static string GetCardinalEndingForGender(GrammaticalGender gender, long number)
    {
        switch (gender)
        {
            case GrammaticalGender.Masculine:
                if (number == 1)
                {
                    return "s";
                }

                if (number != 3 && number < 10)
                {
                    return "i";
                }

                return string.Empty;
            case GrammaticalGender.Feminine:
                if (number == 1)
                {
                    return "a";
                }

                if (number != 3 && number < 10)
                {
                    return "as";
                }

                return string.Empty;
            default:
                throw new ArgumentOutOfRangeException(nameof(gender));
        }
    }
}
