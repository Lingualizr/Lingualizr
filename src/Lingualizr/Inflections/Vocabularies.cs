namespace Lingualizr.Inflections;

/// <summary>
/// Container for registered Vocabularies.  At present, only a single vocabulary is supported: Default.
/// </summary>
public static class Vocabularies
{
    private static readonly Lazy<Vocabulary> _instance = new(BuildDefault, LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// The default vocabulary used for singular/plural irregularities.
    /// Rules can be added to this vocabulary and will be picked up by called to Singularize() and Pluralize().
    /// At this time, multiple vocabularies and removing existing rules are not supported.
    /// </summary>
    public static Vocabulary Default => _instance.Value;

    private static Vocabulary BuildDefault()
    {
        Vocabulary @default = new();

        @default.AddPlural("$", "s");
        @default.AddPlural("s$", "s");
        @default.AddPlural("(ax|test)is$", "$1es");
        @default.AddPlural("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)us$", "$1i");
        @default.AddPlural("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)$", "$1es");
        @default.AddPlural("(buffal|tomat|volcan|ech|embarg|her|mosquit|potat|torped|vet)o$", "$1oes");
        @default.AddPlural("([dti])um$", "$1a");
        @default.AddPlural("sis$", "ses");
        @default.AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
        @default.AddPlural("(hive)$", "$1s");
        @default.AddPlural("([^aeiouy]|qu)y$", "$1ies");
        @default.AddPlural("(x|ch|ss|sh)$", "$1es");
        @default.AddPlural("(matr|vert|ind|d)(ix|ex)$", "$1ices");
        @default.AddPlural("(^[m|l])ouse$", "$1ice");
        @default.AddPlural("^(ox)$", "$1en");
        @default.AddPlural("(quiz)$", "$1zes");
        @default.AddPlural("(buz|blit|walt)z$", "$1zes");
        @default.AddPlural("(hoo|lea|loa|thie)f$", "$1ves");
        @default.AddPlural("(alumn|alg|larv|vertebr)a$", "$1ae");
        @default.AddPlural("(criteri|phenomen)on$", "$1a");

        @default.AddSingular("s$", string.Empty);
        @default.AddSingular("(n)ews$", "$1ews");
        @default.AddSingular("([dti])a$", "$1um");
        @default.AddSingular("(analy|ba|diagno|parenthe|progno|synop|the|ellip|empha|neuro|oa|paraly)ses$", "$1sis");
        @default.AddSingular("([^f])ves$", "$1fe");
        @default.AddSingular("(hive)s$", "$1");
        @default.AddSingular("(tive)s$", "$1");
        @default.AddSingular("([lr]|hoo|lea|loa|thie)ves$", "$1f");
        @default.AddSingular("(^zomb)?([^aeiouy]|qu)ies$", "$2y");
        @default.AddSingular("(s)eries$", "$1eries");
        @default.AddSingular("(m)ovies$", "$1ovie");
        @default.AddSingular("(x|ch|ss|sh)es$", "$1");
        @default.AddSingular("(^[m|l])ice$", "$1ouse");
        @default.AddSingular("(?<!^[a-z])(o)es$", "$1");
        @default.AddSingular("(shoe)s$", "$1");
        @default.AddSingular("(cris|ax|test)es$", "$1is");
        @default.AddSingular("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)i$", "$1us");
        @default.AddSingular("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)es$", "$1");
        @default.AddSingular("^(ox)en", "$1");
        @default.AddSingular("(matr|d)ices$", "$1ix");
        @default.AddSingular("(vert|ind)ices$", "$1ex");
        @default.AddSingular("(quiz)zes$", "$1");
        @default.AddSingular("(buz|blit|walt)zes$", "$1z");
        @default.AddSingular("(alumn|alg|larv|vertebr)ae$", "$1a");
        @default.AddSingular("(criteri|phenomen)a$", "$1on");
        @default.AddSingular("([b|r|c]ook|room|smooth)ies$", "$1ie");

        @default.AddIrregular("person", "people");
        @default.AddIrregular("man", "men");
        @default.AddIrregular("human", "humans");
        @default.AddIrregular("child", "children");
        @default.AddIrregular("sex", "sexes");
        @default.AddIrregular("glove", "gloves");
        @default.AddIrregular("move", "moves");
        @default.AddIrregular("goose", "geese");
        @default.AddIrregular("wave", "waves");
        @default.AddIrregular("foot", "feet");
        @default.AddIrregular("tooth", "teeth");
        @default.AddIrregular("curriculum", "curricula");
        @default.AddIrregular("database", "databases");
        @default.AddIrregular("zombie", "zombies");
        @default.AddIrregular("personnel", "personnel");
        // Fix #789
        @default.AddIrregular("cache", "caches");

        // Fix 975
        @default.AddIrregular("ex", "exes", matchEnding: false);
        @default.AddIrregular("is", "are", matchEnding: false);
        @default.AddIrregular("that", "those", matchEnding: false);
        @default.AddIrregular("this", "these", matchEnding: false);
        @default.AddIrregular("bus", "buses", matchEnding: false);
        @default.AddIrregular("die", "dice", matchEnding: false);
        @default.AddIrregular("tie", "ties", matchEnding: false);

        @default.AddUncountable("staff");
        @default.AddUncountable("training");
        @default.AddUncountable("equipment");
        @default.AddUncountable("information");
        @default.AddUncountable("corn");
        @default.AddUncountable("milk");
        @default.AddUncountable("rice");
        @default.AddUncountable("money");
        @default.AddUncountable("species");
        @default.AddUncountable("series");
        @default.AddUncountable("fish");
        @default.AddUncountable("sheep");
        @default.AddUncountable("deer");
        @default.AddUncountable("aircraft");
        @default.AddUncountable("oz");
        @default.AddUncountable("tsp");
        @default.AddUncountable("tbsp");
        @default.AddUncountable("ml");
        @default.AddUncountable("l");
        @default.AddUncountable("water");
        @default.AddUncountable("waters");
        @default.AddUncountable("semen");
        @default.AddUncountable("sperm");
        @default.AddUncountable("bison");
        @default.AddUncountable("grass");
        @default.AddUncountable("hair");
        @default.AddUncountable("mud");
        @default.AddUncountable("elk");
        @default.AddUncountable("luggage");
        @default.AddUncountable("moose");
        @default.AddUncountable("offspring");
        @default.AddUncountable("salmon");
        @default.AddUncountable("shrimp");
        @default.AddUncountable("someone");
        @default.AddUncountable("swine");
        @default.AddUncountable("trout");
        @default.AddUncountable("tuna");
        @default.AddUncountable("corps");
        @default.AddUncountable("scissors");
        @default.AddUncountable("means");
        @default.AddUncountable("mail");

        // Fix 1132
        @default.AddUncountable("metadata");

        return @default;
    }
}
