namespace Lingualizr.Localisation.NumberToWords;

internal class TurkishNumberToWordConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap = { "sıfır", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz" };
    private static readonly string[] _tensMap = { "sıfır", "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };

    private static readonly Dictionary<char, string> _ordinalSuffix =
        new()
        {
            { 'ı', "ıncı" },
            { 'i', "inci" },
            { 'u', "uncu" },
            { 'ü', "üncü" },
            { 'o', "uncu" },
            { 'ö', "üncü" },
            { 'e', "inci" },
            { 'a', "ıncı" },
        };

    private static readonly Dictionary<char, string> _tupleSuffix =
        new()
        {
            { 'ı', "lı" },
            { 'i', "li" },
            { 'u', "lu" },
            { 'ü', "lü" },
            { 'o', "lu" },
            { 'ö', "lü" },
            { 'e', "li" },
            { 'a', "lı" },
        };

    public override string Convert(long number)
    {
        if (number == 0)
        {
            return _unitsMap[0];
        }

        if (number < 0)
        {
            return string.Format("eksi {0}", Convert(-number));
        }

        List<string> parts = new();

        if (number / 1000000000000000000 > 0)
        {
            parts.Add(string.Format("{0} kentilyon", Convert(number / 1000000000000000000)));
            number %= 1000000000000000000;
        }

        if (number / 1000000000000000 > 0)
        {
            parts.Add(string.Format("{0} katrilyon", Convert(number / 1000000000000000)));
            number %= 1000000000000000;
        }

        if (number / 1000000000000 > 0)
        {
            parts.Add(string.Format("{0} trilyon", Convert(number / 1000000000000)));
            number %= 1000000000000;
        }

        if (number / 1000000000 > 0)
        {
            parts.Add(string.Format("{0} milyar", Convert(number / 1000000000)));
            number %= 1000000000;
        }

        if (number / 1000000 > 0)
        {
            parts.Add(string.Format("{0} milyon", Convert(number / 1000000)));
            number %= 1000000;
        }

        long thousand = number / 1000;
        if (thousand > 0)
        {
            parts.Add(string.Format("{0} bin", thousand > 1 ? Convert(thousand) : string.Empty).Trim());
            number %= 1000;
        }

        long hundred = number / 100;
        if (hundred > 0)
        {
            parts.Add(string.Format("{0} yüz", hundred > 1 ? Convert(hundred) : string.Empty).Trim());
            number %= 100;
        }

        if (number / 10 > 0)
        {
            parts.Add(_tensMap[number / 10]);
            number %= 10;
        }

        if (number > 0)
        {
            parts.Add(_unitsMap[number]);
        }

        string toWords = string.Join(" ", parts.ToArray());

        return toWords;
    }

    public override string ConvertToOrdinal(int number)
    {
        string word = Convert(number);
        string? wordSuffix = string.Empty;
        bool suffixFoundOnLastVowel = false;

        for (int i = word.Length - 1; i >= 0; i--)
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

    public override string ConvertToTuple(int number)
    {
        switch (number)
        {
            case 1:
                return "tek";
            case 2:
                return "çift";
            default:
                string word = Convert(number);
                string? wordSuffix = string.Empty;

                for (int i = word.Length - 1; i >= 0; i--)
                {
                    if (_tupleSuffix.TryGetValue(word[i], out wordSuffix))
                    {
                        break;
                    }
                }

                return string.Format("{0}{1}", word, wordSuffix);
        }
    }
}
