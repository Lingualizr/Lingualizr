namespace Lingualizr.Localisation.NumberToWords;

internal class ChineseNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

    public override string Convert(long number)
    {
        return Convert(number, false, IsSpecial(number));
    }

    public override string ConvertToOrdinal(int number)
    {
        return Convert(number, true, IsSpecial(number));
    }

    private static bool IsSpecial(long number) => number > 10 && number < 20;

    private static string Convert(long number, bool isOrdinal, bool isSpecial)
    {
        if (number == 0)
        {
            return _unitsMap[0];
        }

        if (number < 0)
        {
            return string.Format("负 {0}", Convert(-number, false, false));
        }

        List<string> parts = new();

        if (number / 1000000000000 > 0)
        {
            string format = "{0}兆";
            if (number % 1000000000000 < 100000000000 && number % 1000000000000 > 0)
            {
                format = "{0}兆零";
            }

            parts.Add(string.Format(format, Convert(number / 1000000000000, false, false)));
            number %= 1000000000000;
        }

        if (number / 100000000 > 0)
        {
            string format = "{0}亿";
            if (number % 100000000 < 10000000 && number % 100000000 > 0)
            {
                format = "{0}亿零";
            }

            parts.Add(string.Format(format, Convert(number / 100000000, false, false)));
            number %= 100000000;
        }

        if (number / 10000 > 0)
        {
            string format = "{0}万";
            if (number % 10000 < 1000 && number % 10000 > 0)
            {
                format = "{0}万零";
            }

            parts.Add(string.Format(format, Convert(number / 10000, false, false)));
            number %= 10000;
        }

        if (number / 1000 > 0)
        {
            string format = "{0}千";
            if (number % 1000 < 100 && number % 1000 > 0)
            {
                format = "{0}千零";
            }

            parts.Add(string.Format(format, Convert(number / 1000, false, false)));
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            string format = "{0}百";
            if (number % 100 < 10 && number % 100 > 0)
            {
                format = "{0}百零";
            }

            parts.Add(string.Format(format, Convert(number / 100, false, false)));
            number %= 100;
        }

        if (number > 0)
        {
            if (number <= 10)
            {
                parts.Add(_unitsMap[number]);
            }
            else
            {
                string lastPart = string.Format("{0}十", _unitsMap[number / 10]);
                if (number % 10 > 0)
                {
                    lastPart += string.Format("{0}", _unitsMap[number % 10]);
                }

                parts.Add(lastPart);
            }
        }

        string toWords = string.Join(string.Empty, parts.ToArray());

        if (isSpecial)
        {
            toWords = toWords.Substring(1);
        }

        if (isOrdinal)
        {
            toWords = string.Format("第 {0}", toWords);
        }

        return toWords;
    }
}
