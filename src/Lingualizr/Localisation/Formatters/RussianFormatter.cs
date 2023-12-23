using Lingualizr.Localisation.GrammaticalNumber;

namespace Lingualizr.Localisation.Formatters;

internal class RussianFormatter : DefaultFormatter
{
    public RussianFormatter()
        : base("ru") { }

    protected override string GetResourceKey(string resourceKey, int number)
    {
        RussianGrammaticalNumber grammaticalNumber = RussianGrammaticalNumberDetector.Detect(number);
        string suffix = GetSuffix(grammaticalNumber);
        return resourceKey + suffix;
    }

    private static string GetSuffix(RussianGrammaticalNumber grammaticalNumber)
    {
        if (grammaticalNumber == RussianGrammaticalNumber.Singular)
        {
            return "_Singular";
        }

        if (grammaticalNumber == RussianGrammaticalNumber.Paucal)
        {
            return "_Paucal";
        }

        return string.Empty;
    }
}
