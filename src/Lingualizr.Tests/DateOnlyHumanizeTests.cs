using Lingualizr.Configuration;
using Lingualizr.DateTimeHumanizeStrategy;

namespace Lingualizr.Tests;

[UseCulture("en-US")]
public class DateOnlyHumanizeTests
{
    [Fact]
    public void DefaultStrategy_SameDate()
    {
        Configurator.DateOnlyHumanizeStrategy = new DefaultDateOnlyHumanizeStrategy();

        DateOnly inputTime = new(2015, 07, 05);
        DateOnly baseTime = new(2015, 07, 05);

        const string expectedResult = "now";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void DefaultStrategy_MonthApart()
    {
        Configurator.DateOnlyHumanizeStrategy = new DefaultDateOnlyHumanizeStrategy();

        DateOnly inputTime = new(2015, 08, 05);
        DateOnly baseTime = new(2015, 07, 05);

        const string expectedResult = "one month from now";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void DefaultStrategy_DaysAgo()
    {
        Configurator.DateOnlyHumanizeStrategy = new DefaultDateOnlyHumanizeStrategy();

        DateOnly inputTime = new(2015, 07, 02);
        DateOnly baseTime = new(2015, 07, 05);

        const string expectedResult = "3 days ago";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void PrecisionStrategy_NextDay()
    {
        Configurator.DateOnlyHumanizeStrategy = new PrecisionDateOnlyHumanizeStrategy();

        DateOnly inputTime = new(2015, 07, 05);
        DateOnly baseTime = new(2015, 07, 04);

        const string expectedResult = "tomorrow";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Never()
    {
        DateOnly? never = null;
        Assert.Equal("never", never.Humanize());
    }

    [Fact]
    public void Nullable_ExpectSame()
    {
        DateOnly? never = new DateOnly(2015, 12, 7);

        Assert.Equal(never.Value.Humanize(), never.Humanize());
    }
}
