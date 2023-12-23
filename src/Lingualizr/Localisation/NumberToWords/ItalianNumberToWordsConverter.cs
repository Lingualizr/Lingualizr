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

        int numberInt = (int)number;

        if (numberInt < 0)
        {
            return "meno " + Convert(Math.Abs(numberInt), gender);
        }

        ItalianCardinalNumberCruncher cruncher = new(numberInt, gender);

        return cruncher.Convert();
    }

    public override string ConvertToOrdinal(int number, GrammaticalGender gender)
    {
        ItalianOrdinalNumberCruncher cruncher = new(number, gender);

        return cruncher.Convert();
    }
}
