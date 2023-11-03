using System.Text;

namespace Lingualizr.Localisation.NumberToWords;

internal class UzbekCyrlNumberToWordConverter : GenderlessNumberToWordsConverter
{
    private static readonly string[] _unitsMap = { "нол", "бир", "икки", "уч", "тўрт", "беш", "олти", "етти", "саккиз", "тўққиз" };
    private static readonly string[] _tensMap = { "нол", "ўн", "йигирма", "ўттиз", "қирқ", "эллик", "олтмиш", "етмиш", "саксон", "тўқсон" };

    private static readonly string[] _ordinalSuffixes = new string[] { "инчи", "нчи" };

    public override string Convert(long number)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        var numberInt = (int)number;
        if (numberInt < 0)
        {
            return string.Format("минус {0}", Convert(-numberInt, true));
        }

        return Convert(numberInt, true);
    }

    private string Convert(int number, bool checkForHoundredRule)
    {
        if (number == 0)
        {
            return _unitsMap[0];
        }

        if (checkForHoundredRule && number == 100)
        {
            return "юз";
        }

        var sb = new StringBuilder();

        if ((number / 1000000000) > 0)
        {
            sb.AppendFormat("{0} миллиард ", Convert(number / 1000000000, false));
            number %= 1000000000;
        }

        if ((number / 1000000) > 0)
        {
            sb.AppendFormat("{0} миллион ", Convert(number / 1000000, true));
            number %= 1000000;
        }

        var thousand = number / 1000;
        if (thousand > 0)
        {
            sb.AppendFormat("{0} минг ", Convert(thousand, true));
            number %= 1000;
        }

        var hundred = number / 100;
        if (hundred > 0)
        {
            sb.AppendFormat("{0} юз ", Convert(hundred, false));
            number %= 100;
        }

        if ((number / 10) > 0)
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
        var word = Convert(number);
        var i = 0;
        if (string.IsNullOrEmpty(word))
        {
            return string.Empty;
        }

        var lastChar = word[word.Length - 1];
        if (lastChar == 'и' || lastChar == 'а')
        {
            i = 1;
        }

        return string.Format("{0}{1}", word, _ordinalSuffixes[i]);
    }
}
