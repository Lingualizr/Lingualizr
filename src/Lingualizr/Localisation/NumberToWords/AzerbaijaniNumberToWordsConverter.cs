using System;
using System.Collections.Generic;

namespace Lingualizr.Localisation.NumberToWords;

internal class AzerbaijaniNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap = { "sıfır", "bir", "iki", "üç", "dörd", "beş", "altı", "yeddi", "səkkiz", "doqquz" };
    private static readonly string[] _tensMap = { "sıfır", "on", "iyirmi", "otuz", "qırx", "əlli", "altmış", "yetmiş", "səksən", "doxsan" };

    private static readonly Dictionary<char, string> _ordinalSuffix = new Dictionary<char, string>
    {
        { 'ı', "ıncı" },
        { 'i', "inci" },
        { 'u', "uncu" },
        { 'ü', "üncü" },
        { 'o', "uncu" },
        { 'ö', "üncü" },
        { 'e', "inci" },
        { 'a', "ıncı" },
        { 'ə', "inci" },
    };

    public override string Convert(long number)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        var numberInt = (int)number;
        if (numberInt == 0)
        {
            return _unitsMap[0];
        }

        if (numberInt < 0)
        {
            return string.Format("mənfi {0}", Convert(-numberInt));
        }

        var parts = new List<string>();

        if ((numberInt / 1000000000) > 0)
        {
            parts.Add(string.Format("{0} milyard", Convert(numberInt / 1000000000)));
            numberInt %= 1000000000;
        }

        if ((numberInt / 1000000) > 0)
        {
            parts.Add(string.Format("{0} milyon", Convert(numberInt / 1000000)));
            numberInt %= 1000000;
        }

        var thousand = numberInt / 1000;
        if (thousand > 0)
        {
            parts.Add(string.Format("{0} min", thousand > 1 ? Convert(thousand) : string.Empty).Trim());
            numberInt %= 1000;
        }

        var hundred = numberInt / 100;
        if (hundred > 0)
        {
            parts.Add(string.Format("{0} yüz", hundred > 1 ? Convert(hundred) : string.Empty).Trim());
            numberInt %= 100;
        }

        if ((numberInt / 10) > 0)
        {
            parts.Add(_tensMap[numberInt / 10]);
            numberInt %= 10;
        }

        if (numberInt > 0)
        {
            parts.Add(_unitsMap[numberInt]);
        }

        var toWords = string.Join(" ", parts.ToArray());

        return toWords;
    }

    public override string ConvertToOrdinal(int number)
    {
        var word = Convert(number);
        var wordSuffix = string.Empty;
        var suffixFoundOnLastVowel = false;

        for (var i = word.Length - 1; i >= 0; i--)
        {
            if (_ordinalSuffix.TryGetValue(word[i], out wordSuffix))
            {
                suffixFoundOnLastVowel = i == word.Length - 1;
                break;
            }
        }

        if (word[word.Length - 1] == 't')
        {
            word = word.Substring(0, word.Length - 1) + 'd';
        }

        if (suffixFoundOnLastVowel)
        {
            word = word.Substring(0, word.Length - 1);
        }

        return string.Format("{0}{1}", word, wordSuffix);
    }
}
