using System.Globalization;

namespace Lingualizr.Localisation.NumberToWords;

internal class SlovenianNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap =
    {
        "nič",
        "ena",
        "dva",
        "tri",
        "štiri",
        "pet",
        "šest",
        "sedem",
        "osem",
        "devet",
        "deset",
        "enajst",
        "dvanajst",
        "trinajst",
        "štirinajst",
        "petnajst",
        "šestnajst",
        "sedemnajst",
        "osemnajst",
        "devetnajst"
    };
    private static readonly string[] _tensMap = { "nič", "deset", "dvajset", "trideset", "štirideset", "petdeset", "šestdeset", "sedemdeset", "osemdeset", "devetdeset" };

    private readonly CultureInfo _culture;

    public SlovenianNumberToWordsConverter(CultureInfo culture)
    {
        _culture = culture;
    }

    public override string Convert(long number)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        var numberInt = (int)number;
        if (numberInt == 0)
        {
            return "nič";
        }

        if (numberInt < 0)
        {
            return string.Format("minus {0}", Convert(-numberInt));
        }

        var parts = new List<string>();

        var billions = numberInt / 1000000000;
        if (billions > 0)
        {
            parts.Add(Part("milijarda", "dve milijardi", "{0} milijarde", "{0} milijard", billions));
            numberInt %= 1000000000;
            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        var millions = numberInt / 1000000;
        if (millions > 0)
        {
            parts.Add(Part("milijon", "dva milijona", "{0} milijone", "{0} milijonov", millions));
            numberInt %= 1000000;
            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        var thousands = numberInt / 1000;
        if (thousands > 0)
        {
            parts.Add(Part("tisoč", "dva tisoč", "{0} tisoč", "{0} tisoč", thousands));
            numberInt %= 1000;
            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        var hundreds = numberInt / 100;
        if (hundreds > 0)
        {
            parts.Add(Part("sto", "dvesto", "{0}sto", "{0}sto", hundreds));
            numberInt %= 100;
            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        if (numberInt > 0)
        {
            if (numberInt < 20)
            {
                if (numberInt > 1)
                {
                    parts.Add(_unitsMap[numberInt]);
                }
                else
                {
                    parts.Add("ena");
                }
            }
            else
            {
                var units = numberInt % 10;
                if (units > 0)
                {
                    parts.Add(string.Format("{0}in", _unitsMap[units]));
                }

                parts.Add(_tensMap[numberInt / 10]);
            }
        }

        return string.Join(string.Empty, parts);
    }

    public override string ConvertToOrdinal(int number)
    {
        return number.ToString(_culture);
    }

    private string Part(string singular, string dual, string trialQuadral, string plural, int number)
    {
        if (number == 1)
        {
            return singular;
        }

        if (number == 2)
        {
            return dual;
        }

        if (number == 3 || number == 4)
        {
            return string.Format(trialQuadral, Convert(number));
        }

        return string.Format(plural, Convert(number));
    }
}
