namespace Lingualizr.Localisation.NumberToWords.Italian;

internal class ItalianOrdinalNumberCruncher
{
    public ItalianOrdinalNumberCruncher(int number, GrammaticalGender gender)
    {
        FullNumber = number;
        Gender = gender;
        _genderSuffix = gender == GrammaticalGender.Feminine ? "a" : "o";
    }

    public string Convert()
    {
        // it's easier to treat zero as a completely distinct case
        if (FullNumber == 0)
        {
            return "zero";
        }

        if (FullNumber <= 9)
        {
            // units ordinals, 1 to 9, are totally different than the rest: treat them as a distinct case
            return _unitsUnder10NumberToText[FullNumber] + _genderSuffix;
        }

        var cardinalCruncher = new ItalianCardinalNumberCruncher(FullNumber, Gender);

        var words = cardinalCruncher.Convert();

        var tensAndUnits = FullNumber % 100;

        if (tensAndUnits == 10)
        {
            // for numbers ending in 10, cardinal and ordinal endings are different, suffix doesn't work
            words = words.Remove(words.Length - _lengthOf10AsCardinal) + "decim" + _genderSuffix;
        }
        else
        {
            // truncate last vowel
            words = words.Remove(words.Length - 1);

            var units = FullNumber % 10;

            // reintroduce *unaccented* last vowel in some corner cases
            if (units == 3)
            {
                words += 'e';
            }
            else if (units == 6)
            {
                words += 'i';
            }

            var lowestThreeDigits = FullNumber % 1000;
            var lowestSixDigits = FullNumber % 1000000;
            var lowestNineDigits = FullNumber % 1000000000;

            if (lowestNineDigits == 0)
            {
                // if exact billions, cardinal number words are joined
                words = words.Replace(" miliard", "miliard");

                // if 1 billion, numeral prefix is removed completely
                if (FullNumber == 1000000000)
                {
                    words = words.Replace("un", string.Empty);
                }
            }
            else if (lowestSixDigits == 0)
            {
                // if exact millions, cardinal number words are joined
                words = words.Replace(" milion", "milion");

                // if 1 million, numeral prefix is removed completely
                if (FullNumber == 1000000)
                {
                    words = words.Replace("un", string.Empty);
                }
            }
            else if (lowestThreeDigits == 0 && FullNumber > 1000)
            {
                // if exact thousands, double the final 'l', apart from 1000 already having that
                words += 'l';
            }

            // append common ordinal suffix
            words += "esim" + _genderSuffix;
        }

        return words;
    }

    protected readonly int FullNumber;
    protected readonly GrammaticalGender Gender;
    private readonly string _genderSuffix;

    /// <summary>
    /// Lookup table converting units number to text. Index 1 for 1, index 2 for 2, up to index 9.
    /// </summary>
    private static readonly string[] _unitsUnder10NumberToText = new string[] { string.Empty, "prim", "second", "terz", "quart", "quint", "sest", "settim", "ottav", "non", };

    private static readonly int _lengthOf10AsCardinal = "dieci".Length;
}
