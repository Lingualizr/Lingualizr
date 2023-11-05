namespace Lingualizr.Localisation;

/// <summary>
///
/// </summary>
public partial class ResourceKeys
{
    private const string Single = "Single";
    private const string Multiple = "Multiple";

    private static void ValidateRange(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
    }
}
