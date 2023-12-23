namespace Lingualizr.Localisation.NumberToWords;

internal class SpanishNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _hundredsRootMap = { "cero", "ciento", "doscient", "trescient", "cuatrocient", "quinient", "seiscient", "setecient", "ochocient", "novecient", };

    private static readonly string[] _hundredthsRootMap = { string.Empty, "centésim", "ducentésim", "tricentésim", "cuadringentésim", "quingentésim", "sexcentésim", "septingentésim", "octingentésim", "noningentésim", };

    private static readonly string[] _ordinalsRootMap = { string.Empty, "primer", "segund", "tercer", "cuart", "quint", "sext", "séptim", "octav", "noven", };

    private static readonly string[] _tensMap = { "cero", "diez", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa", };

    private static readonly string[] _tenthsRootMap = { string.Empty, "décim", "vigésim", "trigésim", "cuadragésim", "quincuagésim", "sexagésim", "septuagésim", "octogésim", "nonagésim", };

    private static readonly string[] _thousandthsRootMap = { string.Empty, "milésim", "dosmilésim", "tresmilésim", "cuatromilésim", "cincomilésim", "seismilésim", "sietemilésim", "ochomilésim", "nuevemilésim", };

    private static readonly string[] _tupleMap =
    {
        "cero veces",
        "una vez",
        "doble",
        "triple",
        "cuádruple",
        "quíntuple",
        "séxtuple",
        "séptuple",
        "óctuple",
        "nonuplo",
        "décuplo",
        "undécuplo",
        "duodécuplo",
        "terciodécuplo",
    };

    private static readonly string[] _unitsMap =
    {
        "cero",
        "uno",
        "dos",
        "tres",
        "cuatro",
        "cinco",
        "seis",
        "siete",
        "ocho",
        "nueve",
        "diez",
        "once",
        "doce",
        "trece",
        "catorce",
        "quince",
        "dieciséis",
        "diecisiete",
        "dieciocho",
        "diecinueve",
        "veinte",
        "veintiuno",
        "veintidós",
        "veintitrés",
        "veinticuatro",
        "veinticinco",
        "veintiséis",
        "veintisiete",
        "veintiocho",
        "veintinueve",
    };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        return Convert(number, WordForm.Normal, gender, addAnd);
    }

    public override string Convert(long number, WordForm wordForm, GrammaticalGender gender, bool addAnd = true)
    {
        List<string> wordBuilder = new();

        if (number == 0)
        {
            return "cero";
        }

        if (number == long.MinValue)
        {
            return "menos nueve trillones doscientos veintitrés mil trescientos setenta y dos billones treinta y seis mil " + "ochocientos cincuenta y cuatro millones setecientos setenta y cinco mil ochocientos ocho";
        }

        if (number < 0)
        {
            return $"menos {Convert(-number)}";
        }

        wordBuilder.Add(ConvertGreaterThanMillion(number, out long remainder));
        wordBuilder.Add(ConvertThousands(remainder, out remainder, gender));
        wordBuilder.Add(ConvertHundreds(remainder, out remainder, gender));
        wordBuilder.Add(ConvertUnits(remainder, gender, wordForm));

        return BuildWord(wordBuilder);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        return ConvertToOrdinal(number, gender, WordForm.Normal);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender, WordForm wordForm)
    {
        List<string> wordBuilder = new();

        if (number == 0 || number == int.MinValue)
        {
            return "cero";
        }

        if (number < 0)
        {
            return ConvertToOrdinal(Math.Abs(number), gender);
        }

        if (IsRoundBillion(number))
        {
            return ConvertRoundBillionths(number, gender);
        }

        if (IsRoundMillion(number))
        {
            return ConvertToOrdinal(number / 1000, gender).Replace("milésim", "millonésim");
        }

        wordBuilder.Add(ConvertTensAndHunderdsOfThousandths(number, out int remainder, gender));
        wordBuilder.Add(ConvertThousandths(remainder, out remainder, gender));
        wordBuilder.Add(ConvertHundredths(remainder, out remainder, gender));
        wordBuilder.Add(ConvertTenths(remainder, out remainder, gender));
        wordBuilder.Add(ConvertOrdinalUnits(remainder, gender, wordForm));

        return BuildWord(wordBuilder);
    }

    public override string ConvertToTuple(int number)
    {
        number = Math.Abs(number);

        if (number < _tupleMap.Length)
        {
            return _tupleMap[number];
        }

        return Convert(number) + " veces";
    }

    private static string BuildWord(IReadOnlyList<string> wordParts)
    {
        List<string> parts = wordParts.ToList();
        parts.RemoveAll(string.IsNullOrEmpty);
        return string.Join(" ", parts);
    }

    private static string ConvertHundreds(in long inputNumber, out long remainder, GrammaticalGender gender)
    {
        string wordPart = string.Empty;
        remainder = inputNumber;

        if (inputNumber / 100 > 0)
        {
            wordPart = inputNumber == 100 ? "cien" : GetGenderedHundredsMap(gender)[(int)(inputNumber / 100)];

            remainder = inputNumber % 100;
        }

        return wordPart;
    }

    private static string ConvertHundredths(in int number, out int remainder, GrammaticalGender gender)
    {
        return ConvertMappedOrdinalNumber(number, 100, _hundredthsRootMap, out remainder, gender);
    }

    private static string ConvertMappedOrdinalNumber(in int number, in int divisor, IReadOnlyList<string> map, out int remainder, GrammaticalGender gender)
    {
        string wordPart = string.Empty;
        remainder = number;

        if (number / divisor > 0)
        {
            string genderedEnding = gender == GrammaticalGender.Feminine ? "a" : "o";
            wordPart = map[number / divisor] + genderedEnding;
            remainder = number % divisor;
        }

        return wordPart;
    }

    private static string ConvertOrdinalUnits(in int number, GrammaticalGender gender, WordForm wordForm)
    {
        if (number is > 0 and < 10)
        {
            Dictionary<GrammaticalGender, string> genderedEndingDict = new() { { GrammaticalGender.Feminine, "a" }, { GrammaticalGender.Masculine, HasOrdinalAbbreviation(number, wordForm) ? string.Empty : "o" }, };

            genderedEndingDict.Add(GrammaticalGender.Neuter, genderedEndingDict[GrammaticalGender.Masculine]);

            return _ordinalsRootMap[number] + genderedEndingDict[gender];
        }

        return string.Empty;
    }

    private static string ConvertTenths(in int number, out int remainder, GrammaticalGender gender)
    {
        return ConvertMappedOrdinalNumber(number, 10, _tenthsRootMap, out remainder, gender);
    }

    private static string ConvertThousandths(in int number, out int remainder, GrammaticalGender gender)
    {
        return ConvertMappedOrdinalNumber(number, 1000, _thousandthsRootMap, out remainder, gender);
    }

    private static string ConvertUnits(long inputNumber, GrammaticalGender gender, WordForm wordForm = WordForm.Normal)
    {
        string wordPart = string.Empty;

        if (inputNumber > 0)
        {
            _unitsMap[1] = GetGenderedOne(gender, wordForm);
            _unitsMap[21] = GetGenderedTwentyOne(gender, wordForm);

            if (inputNumber < 30)
            {
                wordPart = _unitsMap[inputNumber];
            }
            else
            {
                wordPart = _tensMap[inputNumber / 10];
                if (inputNumber % 10 > 0)
                {
                    wordPart += $" y {_unitsMap[inputNumber % 10]}";
                }
            }
        }

        return wordPart;
    }

    private static IReadOnlyList<string> GetGenderedHundredsMap(GrammaticalGender gender)
    {
        string genderedEnding = gender == GrammaticalGender.Feminine ? "as" : "os";
        List<string> map = new();
        map.AddRange(_hundredsRootMap.Take(2));

        for (int i = 2; i < _hundredsRootMap.Length; i++)
        {
            map.Add(_hundredsRootMap[i] + genderedEnding);
        }

        return map;
    }

    private static string GetGenderedOne(GrammaticalGender gender, WordForm wordForm = WordForm.Normal)
    {
        Dictionary<GrammaticalGender, string> genderedOne = new() { { GrammaticalGender.Feminine, "una" }, { GrammaticalGender.Masculine, wordForm == WordForm.Abbreviation ? "un" : "uno" }, };

        genderedOne.Add(GrammaticalGender.Neuter, genderedOne[GrammaticalGender.Masculine]);
        return genderedOne[gender];
    }

    private static string GetGenderedTwentyOne(GrammaticalGender gender, WordForm wordForm = WordForm.Normal)
    {
        Dictionary<GrammaticalGender, string> genderedtwentyOne = new() { { GrammaticalGender.Feminine, "veintiuna" }, { GrammaticalGender.Masculine, wordForm == WordForm.Abbreviation ? "veintiún" : "veintiuno" }, };

        genderedtwentyOne.Add(GrammaticalGender.Neuter, genderedtwentyOne[GrammaticalGender.Masculine]);
        return genderedtwentyOne[gender];
    }

    private static bool HasOrdinalAbbreviation(int number, WordForm wordForm)
    {
        return (number == 1 || number == 3) && wordForm == WordForm.Abbreviation;
    }

    private static bool IsRoundBillion(int number)
    {
        return number >= 1000_000_000 && number % 1_000_000 == 0;
    }

    private static bool IsRoundMillion(int number)
    {
        return number >= 1000000 && number % 1000000 == 0;
    }

    private static string PluralizeGreaterThanMillion(string singularWord)
    {
        return singularWord.TrimEnd('ó', 'n') + "ones";
    }

    private string ConvertGreaterThanMillion(in long inputNumber, out long remainder)
    {
        List<string> wordBuilder = new();

        const long oneTrillion = 1_000_000_000_000_000_000;
        const long oneBillion = 1_000_000_000_000;
        const long oneMillion = 1_000_000;

        remainder = inputNumber;

        Dictionary<string, long> numbersAndWordsDict = new()
        {
            { "trillón", oneTrillion },
            { "billón", oneBillion },
            { "millón", oneMillion },
        };

        foreach (KeyValuePair<string, long> numberAndWord in numbersAndWordsDict)
        {
            if (remainder / numberAndWord.Value > 0)
            {
                if (remainder / numberAndWord.Value == 1)
                {
                    wordBuilder.Add($"un {numberAndWord.Key}");
                }
                else
                {
                    wordBuilder.Add(
                        remainder / numberAndWord.Value % 10 == 1
                            ? $"{Convert(remainder / numberAndWord.Value, WordForm.Abbreviation, GrammaticalGender.Masculine)} {PluralizeGreaterThanMillion(numberAndWord.Key)}"
                            : $"{Convert(remainder / numberAndWord.Value)} {PluralizeGreaterThanMillion(numberAndWord.Key)}"
                    );
                }

                remainder %= numberAndWord.Value;
            }
        }

        return BuildWord(wordBuilder);
    }

    private string ConvertRoundBillionths(int number, GrammaticalGender gender)
    {
        string cardinalPart = Convert(number / 1_000_000, WordForm.Abbreviation, gender);
        string sep = number == 1_000_000_000 ? string.Empty : " ";
        string ordinalPart = ConvertToOrdinal(1_000_000, gender);
        return cardinalPart + sep + ordinalPart;
    }

    private string ConvertTensAndHunderdsOfThousandths(in int number, out int remainder, GrammaticalGender gender)
    {
        string wordPart = string.Empty;
        remainder = number;

        if (number / 10000 > 0)
        {
            wordPart = Convert(number / 1000 * 1000, gender);

            if (number < 30000 || isRoundNumber(number))
            {
                if (number == 21000)
                {
                    wordPart = wordPart.Replace("a", string.Empty).Replace("ú", "u");
                }

                wordPart = wordPart.Remove(wordPart.LastIndexOf(' '), 1);
            }

            wordPart += "ésim" + (gender == GrammaticalGender.Masculine ? "o" : "a");

            remainder = number % 1000;
        }

        return wordPart;

        static bool isRoundNumber(int number)
        {
            return (number % 10000 == 0 && number < 100000)
                || (number % 100000 == 0 && number < 1000000)
                || (number % 1000000 == 0 && number < 10000000)
                || (number % 10000000 == 0 && number < 100000000)
                || (number % 100000000 == 0 && number < 1000000000)
                || (number % 1000000000 == 0 && number < int.MaxValue);
        }
    }

    private string ConvertThousands(in long inputNumber, out long remainder, GrammaticalGender gender)
    {
        string wordPart = string.Empty;
        remainder = inputNumber;

        if (inputNumber / 1000 > 0)
        {
            if (inputNumber / 1000 == 1)
            {
                wordPart = "mil";
            }
            else
            {
                wordPart = gender == GrammaticalGender.Feminine ? $"{Convert(inputNumber / 1000, GrammaticalGender.Feminine)} mil" : $"{Convert(inputNumber / 1000, WordForm.Abbreviation, gender)} mil";
            }

            remainder = inputNumber % 1000;
        }

        return wordPart;
    }
}
