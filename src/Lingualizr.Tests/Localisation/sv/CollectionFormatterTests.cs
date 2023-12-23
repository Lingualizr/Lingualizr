namespace Lingualizr.Tests.Localisation.sv;

[UseCulture("sv-SE")]
public class CollectionFormatterTests
{
    [Fact]
    public void MoreThanTwoItems()
    {
        List<int> collection = new(new[] { 1, 2, 3 });
        string humanized = "1, 2 och 3";
        Assert.Equal(humanized, collection.Humanize());
    }

    [Fact]
    public void OneItem()
    {
        List<int> collection = new(new[] { 1 });
        string humanized = "1";
        Assert.Equal(humanized, collection.Humanize());
    }

    [Fact]
    public void TwoItems()
    {
        List<int> collection = new(new[] { 1, 2 });
        string humanized = "1 och 2";
        Assert.Equal(humanized, collection.Humanize());
    }
}
