namespace Lingualizr.Tests.Localisation.de;

[UseCulture("de")]
public class DateToOrdinalWordsTests
{
    [Fact]
    public void OrdinalizeString()
    {
        Assert.Equal("1. Januar 2015", new DateTime(2015, 1, 1).ToOrdinalWords());
    }

    [Fact]
    public void OrdinalizeDateOnlyString()
    {
        Assert.Equal("1. Januar 2015", new DateOnly(2015, 1, 1).ToOrdinalWords());
    }
}
