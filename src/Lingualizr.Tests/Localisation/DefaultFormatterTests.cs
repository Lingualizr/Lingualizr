namespace Lingualizr.Tests.Localisation;

public class DefaultFormatterTests
{
    [Fact]
    [UseCulture("iv")]
    public void HandlesNotImplementedCollectionFormattersGracefully()
    {
        DateTime[] a = new[] { DateTime.UtcNow, DateTime.UtcNow.AddDays(10) };
        string b = a.Humanize();

        Assert.Equal(a[0] + " & " + a[1], b);
    }
}
