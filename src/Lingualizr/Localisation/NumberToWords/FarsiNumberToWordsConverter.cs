﻿namespace Lingualizr.Localisation.NumberToWords;

internal class FarsiNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _farsiHundredsMap = { "صفر", "صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
    private static readonly string[] _farsiTensMap = { "صفر", "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
    private static readonly string[] _farsiUnitsMap = { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه", "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };

    public override string Convert(long number)
    {
        if (number < 0)
        {
            return string.Format("منفی {0}", Convert(-number));
        }

        if (number == 0)
        {
            return "صفر";
        }

        Dictionary<long, Func<long, string>> farsiGroupsMap =
            new()
            {
                { (long)Math.Pow(10, 18), n => string.Format("{0} تریلیون", Convert(n)) },
                { (long)Math.Pow(10, 15), n => string.Format("{0} بیلیارد", Convert(n)) },
                { (long)Math.Pow(10, 12), n => string.Format("{0} بیلیون", Convert(n)) },
                { (long)Math.Pow(10, 9), n => string.Format("{0} میلیارد", Convert(n)) },
                { (long)Math.Pow(10, 6), n => string.Format("{0} میلیون", Convert(n)) },
                { (long)Math.Pow(10, 3), n => string.Format("{0} هزار", Convert(n)) },
                { (long)Math.Pow(10, 2), n => _farsiHundredsMap[n] },
            };

        List<string> parts = new();
        foreach (long group in farsiGroupsMap.Keys)
        {
            if (number / group > 0)
            {
                parts.Add(farsiGroupsMap[group](number / group));
                number %= group;
            }
        }

        if (number >= 20)
        {
            parts.Add(_farsiTensMap[number / 10]);
            number %= 10;
        }

        if (number > 0)
        {
            parts.Add(_farsiUnitsMap[number]);
        }

        return string.Join(" و ", parts);
    }

    public override string ConvertToOrdinal(int number)
    {
        if (number == 1)
        {
            return "اول";
        }

        if (number == 3)
        {
            return "سوم";
        }

        if (number % 10 == 3 && number != 13)
        {
            return Convert(number / 10 * 10) + " و سوم";
        }

        string word = Convert(number);
        return string.Format("{0}{1}", word, word.EndsWith('ی') ? " ام" : "م");
    }
}
