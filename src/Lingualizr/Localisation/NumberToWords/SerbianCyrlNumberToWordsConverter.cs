using System.Globalization;

namespace Lingualizr.Localisation.NumberToWords;

internal class SerbianCyrlNumberToWordsConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] UnitsMap = { "нула", "један", "два", "три", "четири", "пет", "шест", "седам", "осам", "девет", "десет", "једанест", "дванаест", "тринаест", "четрнаест", "петнаест", "шеснаест", "седамнаест", "осамнаест", "деветнаест" };
    private static readonly string[] TensMap = { "нула", "десет", "двадесет", "тридесет", "четрдесет", "петдесет", "шестдесет", "седамдесет", "осамдесет", "деветдесет" };

    private readonly CultureInfo _culture;

    public SerbianCyrlNumberToWordsConverter(CultureInfo culture)
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
            return "нула";
        }

        if (numberInt < 0)
        {
            return string.Format("- {0}", Convert(-numberInt));
        }

        var parts = new List<string>();
        var billions = numberInt / 1000000000;

        if (billions > 0)
        {
            parts.Add(Part("милијарда", "две милијарде", "{0} милијарде", "{0} милијарда", billions));
            numberInt %= 1000000000;

            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        var millions = numberInt / 1000000;

        if (millions > 0)
        {
            parts.Add(Part("милион", "два милиона", "{0} милиона", "{0} милиона", millions));
            numberInt %= 1000000;

            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        var thousands = numberInt / 1000;

        if (thousands > 0)
        {
            parts.Add(Part("хиљаду", "две хиљаде", "{0} хиљаде", "{0} хиљада", thousands));
            numberInt %= 1000;

            if (numberInt > 0)
            {
                parts.Add(" ");
            }
        }

        var hundreds = numberInt / 100;

        if (hundreds > 0)
        {
            parts.Add(Part("сто", "двесто", "{0}сто", "{0}сто", hundreds));
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
