using System.Globalization;

namespace Lingualizr.Localisation.NumberToWords;

internal class PolishNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _hundredsMap = { "zero", "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset", };

    private static readonly string[] _tensMap = { "zero", "dziesięć", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt", };

    private static readonly string[] _unitsMap =
    {
        "zero",
        "jeden",
        "dwa",
        "trzy",
        "cztery",
        "pięć",
        "sześć",
        "siedem",
        "osiem",
        "dziewięć",
        "dziesięć",
        "jedenaście",
        "dwanaście",
        "trzynaście",
        "czternaście",
        "piętnaście",
        "szesnaście",
        "siedemnaście",
        "osiemnaście",
        "dziewiętnaście",
    };

    private static readonly string[][] _powersOfThousandMap =
    {
        new[] { "tysiąc", "tysiące", "tysięcy" },
        new[] { "milion", "miliony", "milionów" },
        new[] { "miliard", "miliardy", "miliardów" },
        new[] { "bilion", "biliony", "bilionów" },
        new[] { "biliard", "biliardy", "biliardów" },
        new[] { "trylion", "tryliony", "trylionów" },
    };

    private const long MaxPossibleDivisor = 1_000_000_000_000_000_000;

    private readonly CultureInfo _culture;

    public PolishNumberToWordsConverter(CultureInfo culture)
    {
        _culture = culture;
    }

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number == 0)
        {
            return "zero";
        }

        List<string> parts = new();
        CollectParts(parts, number, gender);

        return string.Join(" ", parts);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        return number.ToString(_culture);
    }

    private static void CollectParts(ICollection<string> parts, long input, GrammaticalGender gender)
    {
        int inputSign = 1;
        if (input < 0)
        {
            parts.Add("minus");
            inputSign = -1;
        }

        long number = input;
        long divisor = MaxPossibleDivisor;
        int power = _powersOfThousandMap.Length - 1;
        while (divisor > 0)
        {
            int multiplier = (int)Math.Abs(number / divisor);
            if (divisor > 1)
            {
                if (multiplier > 1)
                {
                    CollectPartsUnderThousand(parts, multiplier, GrammaticalGender.Masculine);
                }

                if (multiplier > 0)
                {
                    parts.Add(GetPowerOfThousandNameForm(multiplier, power));
                }
            }
            else if (multiplier > 0)
            {
                if (multiplier == 1 && Math.Abs(input) != 1)
                {
                    gender = GrammaticalGender.Masculine;
                }

                CollectPartsUnderThousand(parts, multiplier, gender);
            }

            number -= multiplier * divisor * inputSign;
            divisor /= 1000;
            power--;
        }
    }

    private static void CollectPartsUnderThousand(ICollection<string> parts, int number, GrammaticalGender gender)
    {
        int hundredsDigit = number / 100;
        int tensDigit = number % 100 / 10;
        int unitsDigit = number % 10;

        if (hundredsDigit >= 1)
        {
            parts.Add(_hundredsMap[hundredsDigit]);
        }

        if (tensDigit >= 2)
        {
            parts.Add(_tensMap[tensDigit]);
        }

        if (tensDigit != 1 && unitsDigit == 2)
        {
            string genderedForm = gender == GrammaticalGender.Feminine ? "dwie" : "dwa";
            parts.Add(genderedForm);
        }
        else if (number == 1)
        {
            string genderedForm = gender switch
            {
                GrammaticalGender.Masculine => "jeden",
                GrammaticalGender.Feminine => "jedna",
                GrammaticalGender.Neuter => "jedno",
                _ => throw new ArgumentOutOfRangeException(nameof(gender)),
            };
            parts.Add(genderedForm);
        }
        else
        {
            int unit = unitsDigit + 10 * (tensDigit == 1 ? 1 : 0);
            if (unit > 0)
            {
                parts.Add(_unitsMap[unit]);
            }
        }
    }

    private static string GetPowerOfThousandNameForm(int multiplier, int power)
    {
        const int singularIndex = 0;
        const int pluralIndex = 1;
        const int genitiveIndex = 2;
        if (multiplier == 1)
        {
            return _powersOfThousandMap[power][singularIndex];
        }

        int multiplierUnitsDigit = multiplier % 10;
        int multiplierTensDigit = multiplier % 100 / 10;
        if (multiplierTensDigit == 1 || multiplierUnitsDigit <= 1 || multiplierUnitsDigit >= 5)
        {
            return _powersOfThousandMap[power][genitiveIndex];
        }

        return _powersOfThousandMap[power][pluralIndex];
    }
}
