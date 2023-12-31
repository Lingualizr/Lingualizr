﻿using System.Text;

namespace Lingualizr.Localisation.NumberToWords;

internal class UzbekLatnNumberToWordConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap = { "nol", "bir", "ikki", "uch", "to`rt", "besh", "olti", "yetti", "sakkiz", "to`qqiz" };
    private static readonly string[] _tensMap = { "nol", "o`n", "yigirma", "o`ttiz", "qirq", "ellik", "oltmish", "yetmish", "sakson", "to`qson" };

    private static readonly string[] _ordinalSuffixes = new string[] { "inchi", "nchi" };

    public override string Convert(long number)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        int numberInt = (int)number;
        if (numberInt < 0)
        {
            return string.Format("minus {0}", Convert(-numberInt, true));
        }

        return Convert(numberInt, true);
    }

    private static string Convert(int number, bool checkForHoundredRule)
    {
        if (number == 0)
        {
            return _unitsMap[0];
        }

        if (checkForHoundredRule && number == 100)
        {
            return "yuz";
        }

        StringBuilder sb = new();

        if (number / 1000000000 > 0)
        {
            sb.AppendFormat("{0} milliard ", Convert(number / 1000000000, false));
            number %= 1000000000;
        }

        if (number / 1000000 > 0)
        {
            sb.AppendFormat("{0} million ", Convert(number / 1000000, true));
            number %= 1000000;
        }

        int thousand = number / 1000;
        if (thousand > 0)
        {
            sb.AppendFormat("{0} ming ", Convert(thousand, true));
            number %= 1000;
        }

        int hundred = number / 100;
        if (hundred > 0)
        {
            sb.AppendFormat("{0} yuz ", Convert(hundred, false));
            number %= 100;
        }

        if (number / 10 > 0)
        {
            sb.AppendFormat("{0} ", _tensMap[number / 10]);
            number %= 10;
        }

        if (number > 0)
        {
            sb.AppendFormat("{0} ", _unitsMap[number]);
        }

        return sb.ToString().Trim();
    }

    public override string ConvertToOrdinal(int number)
    {
        string word = Convert(number);
        int i = 0;
        if (string.IsNullOrEmpty(word))
        {
            return string.Empty;
        }

        char lastChar = word[word.Length - 1];
        if (lastChar == 'i' || lastChar == 'a')
        {
            i = 1;
        }

        return string.Format("{0}{1}", word, _ordinalSuffixes[i]);
    }
}
