namespace Lingualizr.Tests.FluentDate;

public class InDateTests
{
    [Fact]
    public void InJanuary()
    {
        Assert.Equal(new DateOnly(DateTime.Now.Year, 1, 1), InDate.January);
    }

    [Fact]
    public void InJanuaryOf2009()
    {
        Assert.Equal(new DateOnly(2009, 1, 1), InDate.JanuaryOf(2009));
    }

    [Fact]
    public void InFebruary()
    {
        Assert.Equal(new DateOnly(DateTime.Now.Year, 2, 1), InDate.February);
    }

    [Fact]
    public void InTheYear()
    {
        Assert.Equal(new DateOnly(2009, 1, 1), InDate.TheYear(2009));
    }

    [Fact]
    public void InFiveDays()
    {
        DateOnly baseDate = OnDate.January.The21st;
        DateOnly date = InDate.Five.DaysFrom(baseDate);
        Assert.Equal(baseDate.AddDays(5), date);
    }
}
