using System;

using Lingualizr.Localisation.NumberToWords.Italian;

namespace Lingualizr.Localisation.NumberToWords;

internal class ItalianNumberToWordsConverter : GenderedNumberToWordsConverter
{
    public override string Convert(long number, GrammaticalGender gender, bool addAnd = true)
    {
        if (number > int.MaxValue || number < int.MinValue)
        {
            throw new NotImplementedException();
        }

        var numberInt = (int)number;

        if (numberInt < 0)
        {
            return "meno " + Convert(Math.Abs(numberInt), gender);
        }

        var cruncher = new ItalianCardinalNumberCruncher(numberInt, gender);

        return cruncher.Convert();
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        var cruncher = new ItalianOrdinalNumberCruncher(number, gender);

        return cruncher.Convert();
    }
}
