namespace Lingualizr.Localisation.NumberToWords;

/// <summary>
/// Dutch spelling of numbers is not really officially regulated.
/// There are a few different rules that can be applied.
/// Used the rules as stated here.
/// http://www.beterspellen.nl/website/?pag=110
/// </summary>
internal class DutchNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap =
    {
        "nul",
        "een",
        "twee",
        "drie",
        "vier",
        "vijf",
        "zes",
        "zeven",
        "acht",
        "negen",
        "tien",
        "elf",
        "twaalf",
        "dertien",
        "veertien",
        "vijftien",
        "zestien",
        "zeventien",
        "achttien",
        "negentien",
    };

    private static readonly string[] _tensMap = { "nul", "tien", "twintig", "dertig", "veertig", "vijftig", "zestig", "zeventig", "tachtig", "negentig" };

    private class Fact
    {
        public long Value { get; set; }

        public string Name { get; set; } = null!;

        public string Prefix { get; set; } = null!;

        public string Postfix { get; set; } = null!;

        public bool DisplayOneUnit { get; set; }
    }

    private static readonly Fact[] _hunderds =
    {
        new()
        {
            Value = 1_000_000_000_000_000_000L,
            Name = "triljoen",
            Prefix = " ",
            Postfix = " ",
            DisplayOneUnit = true,
        },
        new()
        {
            Value = 1_000_000_000_000_000L,
            Name = "biljard",
            Prefix = " ",
            Postfix = " ",
            DisplayOneUnit = true,
        },
        new()
        {
            Value = 1_000_000_000_000L,
            Name = "biljoen",
            Prefix = " ",
            Postfix = " ",
            DisplayOneUnit = true,
        },
        new()
        {
            Value = 1000000000,
            Name = "miljard",
            Prefix = " ",
            Postfix = " ",
            DisplayOneUnit = true,
        },
        new()
        {
            Value = 1000000,
            Name = "miljoen",
            Prefix = " ",
            Postfix = " ",
            DisplayOneUnit = true,
        },
        new()
        {
            Value = 1000,
            Name = "duizend",
            Prefix = string.Empty,
            Postfix = " ",
            DisplayOneUnit = false,
        },
        new()
        {
            Value = 100,
            Name = "honderd",
            Prefix = string.Empty,
            Postfix = string.Empty,
            DisplayOneUnit = false,
        },
    };

    public override string Convert(long number)
    {
        if (number == 0)
        {
            return _unitsMap[0];
        }

        if (number < 0)
        {
            return string.Format("min {0}", Convert(-number));
        }

        string word = string.Empty;

        foreach (Fact? m in _hunderds)
        {
            long divided = number / m.Value;

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
                word += Convert(divided) + m.Prefix + m.Name;
            }

            number %= m.Value;
            if (number > 0)
            {
                word += m.Postfix;
            }
        }

        if (number > 0)
        {
            if (number < 20)
            {
                word += _unitsMap[number];
            }
            else
            {
                string tens = _tensMap[number / 10];
                long unit = number % 10;
                if (unit > 0)
                {
                    string units = _unitsMap[unit];
                    bool trema = units.EndsWith('e');
                    word += units + (trema ? "ën" : "en") + tens;
                }
                else
                {
                    word += tens;
                }
            }
        }

        return word;
    }

    private static readonly Dictionary<string, string> _ordinalExceptions =
        new()
        {
            { "een", "eerste" },
            { "drie", "derde" },
            { "miljoen", "miljoenste" },
        };

    private static readonly char[] _endingCharForSte = { 't', 'g', 'd' };

    public override string ConvertToOrdinal(int number)
    {
        string word = Convert(number);

        foreach (KeyValuePair<string, string> kv in _ordinalExceptions.Where(kv => word.EndsWith(kv.Key)))
        {
            // replace word with exception
#pragma warning disable S1751
            return word.Substring(0, word.Length - kv.Key.Length) + kv.Value;
#pragma warning restore S1751
        }

        // achtste
        // twintigste, dertigste, veertigste, ...
        // honderdste, duizendste, ...
        if (word.LastIndexOfAny(_endingCharForSte) == word.Length - 1)
        {
            return word + "ste";
        }

        return word + "de";
    }
}
