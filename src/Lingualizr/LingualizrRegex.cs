using System.Text.RegularExpressions;

namespace Lingualizr;

internal static partial class LingualizrRegex
{
    [GeneratedRegex(@"[\p{Lu}]?[\p{Ll}]+|[0-9]+[\p{Ll}]*|[\p{Lu}]+(?=[\p{Lu}][\p{Ll}]|[0-9]|\b)|[\p{Lo}]+",
        RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture)]
    internal static partial Regex PascalCaseWordPartsRegex();

    [GeneratedRegex(@"\s[-_]|[-_]\s")]
    internal static partial Regex FreestandingSpacingCharRegex();

    [GeneratedRegex("^(?i:(?=[MDCLXVI])((M{0,3})((C[DM])|(D?C{0,3}))?((X[LC])|(L?XX{0,2})|L)?((I[VX])|(V?(II{0,2}))|V)?))$")]
    internal static partial Regex ValidRomanNumeralRegex();

    [GeneratedRegex("^([sS])[sS]*$")]
    internal static partial Regex LetterRegex();
}
