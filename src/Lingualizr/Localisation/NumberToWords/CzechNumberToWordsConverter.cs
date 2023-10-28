using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lingualizr.Localisation.NumberToWords;

internal class CzechNumberToWordsConverter : GenderedNumberToWordsConverter
{
    private static readonly string[] _billionsMap = { "miliarda", "miliardy", "miliard" };
    private static readonly string[] _millionsMap = { "milion", "miliony", "milionů" };
    private static readonly string[] _thousandsMap = { "tisíc", "tisíce", "tisíc" };
    private static readonly string[] _hundredsMap = { "nula", "sto", "dvě stě", "tři sta", "čtyři sta", "pět set", "šest set", "sedm set", "osm set", "devět set" };
    private static readonly string[] _tensMap = { "nula", "deset", "dvacet", "třicet", "čtyřicet", "padesát", "šedesát", "sedmdesát", "osmdesát", "devadesát" };
    private static readonly string[] _unitsMap = { "nula", "jeden", "dva", "tři", "čtyři", "pět", "šest", "sedm", "osm", "devět", "deset", "jedenáct", "dvanáct", "třináct", "čtrnáct", "patnáct", "šestnáct", "sedmnáct", "osmnáct", "devatenáct" };

    private static readonly string[] _unitsMasculineOverrideMap = { "jeden", "dva" };
    private static readonly string[] _unitsFeminineOverrideMap = { "jedna", "dvě" };
    private static readonly string[] _unitsNeuterOverride = { "jedno", "dvě" };
    private static readonly string[] _unitsIntraOverride = { "jedna", "dva" };

    private readonly CultureInfo _culture;

    public CzechNumberToWordsConverter(CultureInfo culture)
    {
        _culture = culture;
    }

    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number == 0)
        {
            return UnitByGender(number, gender);
        }

        var parts = new List<string>();
        if (number < 0)
        {
            parts.Add("mínus");
            number = -number;
        }

        CollectThousandAndAbove(parts, ref number, 1_000_000_000, GrammaticalGender.Feminine, _billionsMap);
        CollectThousandAndAbove(parts, ref number, 1_000_000, GrammaticalGender.Masculine, _millionsMap);
        CollectThousandAndAbove(parts, ref number, 1_000, GrammaticalGender.Masculine, _thousandsMap);

        CollectLessThanThousand(parts, number, gender);

        return string.Join(" ", parts);
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        return number.ToString(_culture);
    }

    private static string UnitByGender(long number, GrammaticalGender? gender)
    {
        if (number != 1 && number != 2)
        {
            return _unitsMap[number];
        }

        return gender switch
        {
            GrammaticalGender.Masculine => _unitsMasculineOverrideMap[number - 1],
            GrammaticalGender.Feminine => _unitsFeminineOverrideMap[number - 1],
            GrammaticalGender.Neuter => _unitsNeuterOverride[number - 1],
            null => _unitsIntraOverride[number - 1],
            _ => throw new ArgumentOutOfRangeException(nameof(gender)),
        };
    }

    private static void CollectLessThanThousand(List<string> parts, long number, GrammaticalGender? gender)
    {
        if (number >= 100)
        {
            parts.Add(_hundredsMap[number / 100]);
            number %= 100;
        }

        if (number >= 20)
        {
            parts.Add(_tensMap[number / 10]);
            number %= 10;
        }

        if (number > 0)
        {
            parts.Add(UnitByGender(number, gender));
        }
    }

    private static void CollectThousandAndAbove(List<string> parts, ref long number, long divisor, GrammaticalGender gender, string[] map)
    {
        var n = number / divisor;

        if (n <= 0)
        {
            return;
        }

        CollectLessThanThousand(parts, n, n < 19 ? gender : (GrammaticalGender?)null);

        var units = n % 1000;

        if (units == 1)
        {
            parts.Add(map[0]);
        }
        else if (units > 1 && units < 5)
        {
            parts.Add(map[1]);
        }
        else
        {
            parts.Add(map[2]);
        }

        number %= divisor;
    }
}
