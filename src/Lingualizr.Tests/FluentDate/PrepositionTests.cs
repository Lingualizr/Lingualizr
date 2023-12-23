using Lingualizr.FluentDate;

namespace Lingualizr.Tests.FluentDate;

public class PrepositionTests
{
    [Fact]
    public void AtMidnight()
    {
        DateTime now = DateTime.Now;
        DateTime midnight = now.AtMidnight();
        Assert.Equal(new DateTime(now.Year, now.Month, now.Day), midnight);
    }

    [Fact]
    public void AtNoon()
    {
        DateTime now = DateTime.Now;
        DateTime noon = now.AtNoon();
        Assert.Equal(new DateTime(now.Year, now.Month, now.Day, 12, 0, 0), noon);
    }

    [Fact]
    public void InYear()
    {
        DateTime now = DateTime.Now;
        DateTime in2011 = now.In(2011);
        Assert.Equal(new DateTime(2011, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond), in2011);
    }
}
