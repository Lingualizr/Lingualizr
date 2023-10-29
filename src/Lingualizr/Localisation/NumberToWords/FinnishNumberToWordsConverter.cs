using System;
using System.Collections.Generic;

namespace Lingualizr.Localisation.NumberToWords;

internal class FinnishNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap = { "nolla", "yksi", "kaksi", "kolme", "neljä", "viisi", "kuusi", "seitsemän", "kahdeksan", "yhdeksän", "kymmenen" };
    private static readonly string[] _ordinalUnitsMap = { "nollas", "ensimmäinen", "toinen", "kolmas", "neljäs", "viides", "kuudes", "seitsemäs", "kahdeksas", "yhdeksäs", "kymmenes" };

    private static readonly Dictionary<int, string> _ordinalExceptions = new Dictionary<int, string>
    {
        { 1, "yhdes" },
        { 2, "kahdes" },
    };

    public override string Convert(long number)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        var numberInt = (int)number;

        if (numberInt < 0)
        {
            return string.Format("miinus {0}", Convert(-numberInt));
        }

        if (numberInt == 0)
        {
            return _unitsMap[0];
        }

        var parts = new List<string>();

        if ((numberInt / 1000000000) > 0)
        {
            parts.Add(numberInt / 1000000000 == 1
                ? "miljardi "
                : string.Format("{0}miljardia ", Convert(numberInt / 1000000000)));

            numberInt %= 1000000000;
        }

        if ((numberInt / 1000000) > 0)
        {
            parts.Add(numberInt / 1000000 == 1
                ? "miljoona "
                : string.Format("{0}miljoonaa ", Convert(numberInt / 1000000)));

            numberInt %= 1000000;
        }

        if ((numberInt / 1000) > 0)
        {
            parts.Add(numberInt / 1000 == 1
                ? "tuhat "
                : string.Format("{0}tuhatta ", Convert(numberInt / 1000)));

            numberInt %= 1000;
        }

        if ((numberInt / 100) > 0)
        {
            parts.Add(numberInt / 100 == 1
                ? "sata"
                : string.Format("{0}sataa", Convert(numberInt / 100)));

            numberInt %= 100;
        }

        if (numberInt >= 20 && (numberInt / 10) > 0)
        {
            parts.Add(string.Format("{0}kymmentä", Convert(numberInt / 10)));
            numberInt %= 10;
        }
        else if (numberInt > 10 && numberInt < 20)
        {
            parts.Add(string.Format("{0}toista", _unitsMap[numberInt % 10]));
        }

        if (numberInt > 0 && numberInt <= 10)
        {
            parts.Add(_unitsMap[numberInt]);
        }

        return string.Join(string.Empty, parts).Trim();
    }

    private static string GetOrdinalUnit(int number, bool useExceptions)
    {
        if (useExceptions && _ordinalExceptions.ContainsKey(number))
        {
            return _ordinalExceptions[number];
        }

        return _ordinalUnitsMap[number];
    }

    private string ToOrdinal(int number, bool useExceptions)
    {
        if (number == 0)
        {
            return _ordinalUnitsMap[0];
        }

        var parts = new List<string>();

        if ((number / 1000000000) > 0)
        {
            parts.Add(string.Format("{0}miljardis", (number / 1000000000) == 1 ? string.Empty : ToOrdinal(number / 1000000000, true)));
            number %= 1000000000;
        }

        if ((number / 1000000) > 0)
        {
            parts.Add(string.Format("{0}miljoonas", (number / 1000000) == 1 ? string.Empty : ToOrdinal(number / 1000000, true)));
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            parts.Add(string.Format("{0}tuhannes", (number / 1000) == 1 ? string.Empty : ToOrdinal(number / 1000, true)));
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            parts.Add(string.Format("{0}sadas", (number / 100) == 1 ? string.Empty : ToOrdinal(number / 100, true)));
            number %= 100;
        }

        if (number >= 20 && (number / 10) > 0)
        {
            parts.Add(string.Format("{0}kymmenes", ToOrdinal(number / 10, true)));
            number %= 10;
        }
        else if (number > 10 && number < 20)
        {
            parts.Add(string.Format("{0}toista", GetOrdinalUnit(number % 10, true)));
        }

        if (number > 0 && number <= 10)
        {
            parts.Add(GetOrdinalUnit(number, useExceptions));
        }

        return string.Join(string.Empty, parts);
    }

    public override string ConvertToOrdinal(int number)
    {
        return ToOrdinal(number, false);
    }
}
