using System;

namespace Lingualizr.Localisation.NumberToWords;

internal class SwedishNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] UnitsMap = { "noll", "ett", "två", "tre", "fyra", "fem", "sex", "sju", "åtta", "nio", "tio", "elva", "tolv", "tretton", "fjorton", "femton", "sexton", "sjutton", "arton", "nitton" };
    private static readonly string[] TensMap = { "noll", "tio", "tjugo", "trettio", "fyrtio", "femtio", "sextio", "sjuttio", "åttio", "nittio", "hundra" };

    private class Fact
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string Prefix { get; set; }

        public string Postfix { get; set; }

        public bool DisplayOneUnit { get; set; }

        public GrammaticalGender Gender { get; set; } = GrammaticalGender.Neuter;
    }

    private static readonly Fact[] Hunderds =
    {
        new Fact { Value = 1000000000, Name = "miljard", Prefix = " ", Postfix = " ", DisplayOneUnit = true, Gender = GrammaticalGender.Masculine },
        new Fact { Value = 1000000,    Name = "miljon", Prefix = " ", Postfix = " ", DisplayOneUnit = true, Gender = GrammaticalGender.Masculine },
        new Fact { Value = 1000,       Name = "tusen", Prefix = " ",  Postfix = " ", DisplayOneUnit = true },
        new Fact { Value = 100,        Name = "hundra", Prefix = string.Empty,  Postfix = string.Empty,  DisplayOneUnit = false },
    };

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        var numberInt = (int)number;

        if (numberInt == 0)
        {
            return UnitsMap[0];
        }

        if (numberInt < 0)
        {
            return string.Format("minus {0}", Convert(-numberInt, gender));
        }

        var word = string.Empty;

        foreach (var m in Hunderds)
        {
            var divided = numberInt / m.Value;

            if (divided <= 0)
            {
                continue;
            }

            if (divided == 1 && !m.DisplayOneUnit)
            {
                word += m.Name;
            }
            else
            {
                word += Convert(divided, m.Gender) + m.Prefix + m.Name;
            }

            // pluralise 1M+
            if (divided > 1 && number >= 1_000_000)
            {
                word += "er";
            }

            numberInt %= m.Value;
            if (numberInt > 0)
            {
                word += m.Postfix;
            }
        }

        if (numberInt > 0)
        {
            if (numberInt < 20)
            {
                if (numberInt == 1 && gender == GrammaticalGender.Masculine)
                {
                    word += "en";
                }
                else
                {
                    word += UnitsMap[numberInt];
                }
            }
            else
            {
                var tens = TensMap[numberInt / 10];
                var unit = numberInt % 10;
                if (unit > 0)
                {
                    var units = UnitsMap[unit];
                    word += tens + units;
                }
                else
                {
                    word += tens;
                }
            }
        }

        return word;
    }

    public override string Convert(long number)
    {
        return Convert(number, GrammaticalGender.Neuter);
    }

    private static string[] ordinalNumbers = new[]
    {
        "nollte",
        "första",
        "andra",
        "tredje",
        "fjärde",
        "femte",
        "sjätte",
        "sjunde",
        "åttonde",
        "nionde",
        "tionde",
        "elfte",
        "tolfte",
        "trettonde",
        "fjortonde",
        "femtonde",
        "sextonde",
        "sjuttonde",
        "artonde",
        "nittonde",
        "tjugonde",
    };

    public override string ConvertToOrdinal(int number)
    {
        var word = string.Empty;

        if (number < 0)
        {
            return $"minus {ConvertToOrdinal(-number)}";
        }

        if (number <= 20)
        {
            return ordinalNumbers[number];
        }

        // 21+
        if (number <= 100)
        {
            var tens = TensMap[number / 10];
            var unit = number % 10;
            if (unit > 0)
            {
                word += tens + ConvertToOrdinal(unit);
            }
            else if (number == 100)
            {
                word += tens + "de";
            }
            else
            {
                word += tens + "nde";
            }

            return word;
        }

        // 101+
        foreach (var m in Hunderds)
        {
            var divided = number / m.Value;

            if (divided <= 0)
            {
                continue;
            }

            if (divided == 1 && !m.DisplayOneUnit)
            {
                word += m.Name;
            }
            else
            {
                word += Convert(divided, m.Gender) + m.Prefix + m.Name;
            }

            // suffix -de/-te
            if (divided > 0 && (number % m.Value) == 0)
            {
                switch (number)
                {
                    case 1_000_000:
                        word += "te";
                        break;
                    default:
                        word += "de";
                        break;
                }
            }

            number %= m.Value;
            if (number > 0)
            {
                word += ConvertToOrdinal(number);
            }
        }

        return word;
    }
}
