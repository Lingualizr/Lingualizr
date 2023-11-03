using System.Globalization;

namespace Lingualizr.Localisation.NumberToWords;

internal class SerbianNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] UnitsMap = { "nula", "jedan", "dva", "tri", "četiri", "pet", "šest", "sedam", "osam", "devet", "deset", "jedanaest", "dvanaest", "trinaest", "četrnaest", "petnaest", "šestnaest", "sedemnaest", "osemnaest", "devetnaest" };
    private static readonly string[] TensMap = { "nula", "deset", "dvadeset", "trideset", "četrdeset", "petdeset", "šestdeset", "sedamdeset", "osamdeset", "devetdeset" };

    private readonly CultureInfo _culture;

    public SerbianNumberToWordsConverter(CultureInfo culture)
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
            return "nula";
        }

        if (numberInt < 0)
        {
            return string.Format("- {0}", Convert(-numberInt));
        }

        var parts = new List<string>();
        var billions = numberInt / 1000000000;

        if (billions > 0)
        {
            parts.Add(Part("milijarda", "dve milijarde", "{0} milijarde", "{0} milijarda", billions));
            numberInt %= 1000000000;

            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        var millions = numberInt / 1000000;

        if (millions > 0)
        {
            parts.Add(Part("milion", "dva miliona", "{0} miliona", "{0} miliona", millions));
            numberInt %= 1000000;

            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        var thousands = numberInt / 1000;

        if (thousands > 0)
        {
            parts.Add(Part("hiljadu", "dve hiljade", "{0} hiljade", "{0} hiljada", thousands));
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
                parts.Add(UnitsMap[numberInt]);
            }
            else
            {
                parts.Add(TensMap[numberInt / 10]);
                var units = numberInt % 10;

                if (units > 0)
                {
                    parts.Add(string.Format(" {0}", UnitsMap[units]));
                }
            }
        }

        return string.Join(string.Empty, parts);
    }

    public override string ConvertToOrdinal(int number)
    {
        // TODO: In progress
        return number.ToString(_culture);
    }

    private string Part(string singular, string dual, string trialQuadral, string plural, int number)
    {
        switch (number)
        {
            case 1:
                return singular;
            case 2:
                return dual;
            case 3:
            case 4:
                return string.Format(trialQuadral, Convert(number));
            default:
                return string.Format(plural, Convert(number));
        }
    }
}
