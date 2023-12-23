namespace Lingualizr.Localisation.NumberToWords;

internal class JapaneseNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap1 = { string.Empty, string.Empty, "二", "三", "四", "五", "六", "七", "八", "九" };
    private static readonly string[] _unitsMap2 = { string.Empty, "十", "百", "千" };
    private static readonly string[] _unitsMap3 = { string.Empty, "万", "億", "兆", "京", "垓", "𥝱", "穣", "溝", "澗", "正", "載", "極", "恒河沙", "阿僧祇", "那由他", "不可思議", "無量大数", };

    public override string Convert(long number)
    {
        return ConvertImpl(number, false);
    }

    public override string ConvertToOrdinal(int number)
    {
        return ConvertImpl(number, true);
    }

    private static string ConvertImpl(long number, bool isOrdinal)
    {
        if (number == 0)
        {
            return isOrdinal ? "〇番目" : "〇";
        }

        if (number < 0)
        {
            return string.Format("マイナス {0}", ConvertImpl(-number, false));
        }

        var parts = new List<string>();
        var groupLevel = 0;
        while (number > 0)
        {
            var groupNumber = number % 10000;
            number /= 10000;

            var n0 = groupNumber % 10;
            var n1 = (groupNumber % 100 - groupNumber % 10) / 10;
            var n2 = (groupNumber % 1000 - groupNumber % 100) / 100;
            var n3 = (groupNumber - groupNumber % 1000) / 1000;

            parts.Add(
                _unitsMap1[n3]
                    + (n3 == 0 ? string.Empty : _unitsMap2[3])
                    + _unitsMap1[n2]
                    + (n2 == 0 ? string.Empty : _unitsMap2[2])
                    + _unitsMap1[n1]
                    + (n1 == 0 ? string.Empty : _unitsMap2[1])
                    + (n0 == 1 ? "一" : _unitsMap1[n0])
                    + (groupNumber == 0 ? string.Empty : _unitsMap3[groupLevel])
            );

            groupLevel++;
        }

        parts.Reverse();
        var toWords = string.Join(string.Empty, parts.ToArray());

        if (isOrdinal)
        {
            toWords = string.Format("{0}番目", toWords);
        }

        return toWords;
    }
}
