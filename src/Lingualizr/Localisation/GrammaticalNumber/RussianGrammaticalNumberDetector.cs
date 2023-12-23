namespace Lingualizr.Localisation.GrammaticalNumber;

internal static class RussianGrammaticalNumberDetector
{
    public static RussianGrammaticalNumber Detect(long number)
    {
        long tens = number % 100 / 10;
        if (tens != 1)
        {
            long unity = number % 10;

            // 1, 21, 31, 41 ... 91, 101, 121 ...
            if (unity == 1)
            {
                return RussianGrammaticalNumber.Singular;
            }

            // 2, 3, 4, 22, 23, 24 ...
            if (unity > 1 && unity < 5)
            {
                return RussianGrammaticalNumber.Paucal;
            }
        }

        return RussianGrammaticalNumber.Plural;
    }
}
