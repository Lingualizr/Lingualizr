using Lingualizr.Configuration;
using Lingualizr.DateTimeHumanizeStrategy;

namespace Lingualizr.Tests;

[UseCulture("en-US")]
public class TimeOnlyHumanizeTests
{
    [Fact]
    public void DefaultStrategy_SameTime()
    {
        Configurator.TimeOnlyHumanizeStrategy = new DefaultTimeOnlyHumanizeStrategy();

        TimeOnly inputTime = new(13, 07, 05);
        TimeOnly baseTime = new(13, 07, 05);

        const string expectedResult = "now";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void DefaultStrategy_HoursApart()
    {
        Configurator.TimeOnlyHumanizeStrategy = new DefaultTimeOnlyHumanizeStrategy();

        TimeOnly inputTime = new(13, 08, 05);
        TimeOnly baseTime = new(1, 08, 05);

        const string expectedResult = "12 hours from now";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void DefaultStrategy_HoursAgo()
    {
        Configurator.TimeOnlyHumanizeStrategy = new DefaultTimeOnlyHumanizeStrategy();

        TimeOnly inputTime = new(13, 07, 02);
        TimeOnly baseTime = new(17, 07, 05);

        const string expectedResult = "4 hours ago";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void PrecisionStrategy_NextDay()
    {
        Configurator.TimeOnlyHumanizeStrategy = new PrecisionTimeOnlyHumanizeStrategy(0.75);

        TimeOnly inputTime = new(18, 10, 49);
        TimeOnly baseTime = new(13, 07, 04);

        const string expectedResult = "5 hours from now";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Never()
    {
        TimeOnly? never = null;
        Assert.Equal("never", never.Humanize());
    }

    [Fact]
    public void Nullable_ExpectSame()
    {
        TimeOnly? never = new TimeOnly(23, 12, 7);

        Assert.Equal(never.Value.Humanize(), never.Humanize());
    }
}
