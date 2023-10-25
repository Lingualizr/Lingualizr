namespace Lingualizr;

/// <summary>
/// Provides hint for Lingualizr as to whether a word is singular, plural or with unknown plurality
/// </summary>
public enum Plurality
{
    /// <summary>
    /// The word is singular
    /// </summary>
    Singular,

    /// <summary>
    /// The word is plural
    /// </summary>
    Plural,

    /// <summary>
    /// I am unsure of the plurality
    /// </summary>
    CouldBeEither,
}
