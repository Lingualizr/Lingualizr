namespace Lingualizr.Tests.Localisation.ro_Ro;

[UseCulture("ro-RO")]
public class CollectionFormatterTests
{
    [Fact]
    public void OneItem()
    {
        List<int> collection = [1];
        string humanized = "1";
        Assert.Equal(humanized, collection.Humanize());
    }

    [Fact]
    public void TwoItems()
    {
        List<int> collection = [1, 2];
        string humanized = "1 și 2";
        Assert.Equal(humanized, collection.Humanize());
    }

    [Fact]
    public void MoreThanTwoItems()
    {
        List<int> collection = [1, 2, 3];
        string humanized = "1, 2 și 3";
        Assert.Equal(humanized, collection.Humanize());
    }
}
