namespace Lingualizr.Tests.FluentDate;

public class InTests
{
    [Fact]
    public void InJanuary()
    {
        Assert.Equal(new DateTime(DateTime.Now.Year, 1, 1), In.January);
    }

    [Fact]
    public void InJanuaryOf2009()
    {
        Assert.Equal(new DateTime(2009, 1, 1), In.JanuaryOf(2009));
    }

    [Fact]
    public void InFebruary()
    {
        Assert.Equal(new DateTime(DateTime.Now.Year, 2, 1), In.February);
    }

    [Fact]
    public void InTheYear()
    {
        Assert.Equal(new DateTime(2009, 1, 1), In.TheYear(2009));
    }

    [Fact]
    public void InFiveDays()
    {
        DateTime baseDate = On.January.The21st;
        DateTime date = In.Five.DaysFrom(baseDate);
        Assert.Equal(baseDate.AddDays(5), date);
    }
}
