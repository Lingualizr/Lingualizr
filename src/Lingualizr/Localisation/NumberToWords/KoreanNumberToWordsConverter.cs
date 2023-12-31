﻿namespace Lingualizr.Localisation.NumberToWords;

internal class KoreanNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap1 = { string.Empty, string.Empty, "이", "삼", "사", "오", "육", "칠", "팔", "구" };
    private static readonly string[] _unitsMap2 = { string.Empty, "십", "백", "천" };
    private static readonly string[] _unitsMap3 = { string.Empty, "만", "억", "조", "경", "해", "자", "양", "구", "간", "정", "재", "극", "항하사", "아승기", "나유타", "불가사의", "무량대수" };

    private static readonly Dictionary<long, string> _ordinalExceptions =
        new()
        {
            { 0, "영번째" },
            { 1, "첫번째" },
            { 2, "두번째" },
            { 3, "세번째" },
            { 4, "네번째" },
            { 5, "다섯번째" },
            { 6, "여섯번째" },
            { 7, "일곱번째" },
            { 8, "여덟번째" },
            { 9, "아홉번째" },
            { 10, "열번째" },
            { 11, "열한번째" },
            { 12, "열두번째" },
            { 13, "열세번째" },
            { 14, "열네번째" },
            { 15, "열다섯번째" },
            { 16, "열여섯번째" },
            { 17, "열일곱번째" },
            { 18, "열여덟번째" },
            { 19, "열아홉째" },
        };

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
        if (isOrdinal && number < 20 && _ordinalExceptions.TryGetValue(number, out string? words))
        {
            return words;
        }

        if (number == 0)
        {
            return "영";
        }

        if (number < 0)
        {
            return string.Format("마이너스 {0}", ConvertImpl(-number, false));
        }

        List<string> parts = new();
        int groupLevel = 0;
        while (number > 0)
        {
            long groupNumber = number % 10000;
            number /= 10000;

            long n0 = groupNumber % 10;
            long n1 = (groupNumber % 100 - groupNumber % 10) / 10;
            long n2 = (groupNumber % 1000 - groupNumber % 100) / 100;
            long n3 = (groupNumber - groupNumber % 1000) / 1000;

            parts.Add(
                _unitsMap1[n3]
                    + (n3 == 0 ? string.Empty : _unitsMap2[3])
                    + _unitsMap1[n2]
                    + (n2 == 0 ? string.Empty : _unitsMap2[2])
                    + _unitsMap1[n1]
                    + (n1 == 0 ? string.Empty : _unitsMap2[1])
                    + (n0 == 1 ? "일" : _unitsMap1[n0])
                    + (groupNumber == 0 ? string.Empty : _unitsMap3[groupLevel])
            );

            groupLevel++;
        }

        parts.Reverse();
        string toWords = string.Join(string.Empty, parts.ToArray());

        if (isOrdinal)
        {
            toWords = string.Format("{0}번째", toWords);
        }

        return toWords;
    }
}
