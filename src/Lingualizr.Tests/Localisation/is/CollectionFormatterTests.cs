namespace Lingualizr.Tests.Localisation.@is;

[UseCulture("is")]
public class CollectionFormatterTests
{
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
        string humanized = "1 og 2";
        Assert.Equal(humanized, collection.Humanize());
    }

    [Fact]
    public void MoreThanTwoItems()
    {
        List<int> collection = new(new[] { 1, 2, 3 });
        string humanized = "1, 2 og 3";
        Assert.Equal(humanized, collection.Humanize());
    }
}
