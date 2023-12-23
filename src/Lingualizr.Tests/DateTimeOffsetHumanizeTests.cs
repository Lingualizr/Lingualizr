using Lingualizr.Configuration;
using Lingualizr.DateTimeHumanizeStrategy;

namespace Lingualizr.Tests;

[UseCulture("en-US")]
public class DateTimeOffsetHumanizeTests
{
    [Fact]
    public void DefaultStrategy_SameOffset()
    {
        Configurator.DateTimeOffsetHumanizeStrategy = new DefaultDateTimeOffsetHumanizeStrategy();

        DateTimeOffset inputTime = new(2015, 07, 05, 04, 0, 0, TimeSpan.Zero);
        DateTimeOffset baseTime = new(2015, 07, 05, 03, 0, 0, TimeSpan.Zero);

        const string expectedResult = "an hour from now";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void DefaultStrategy_DifferentOffsets()
    {
        Configurator.DateTimeOffsetHumanizeStrategy = new DefaultDateTimeOffsetHumanizeStrategy();

        DateTimeOffset inputTime = new(2015, 07, 05, 03, 0, 0, new TimeSpan(2, 0, 0));
        DateTimeOffset baseTime = new(2015, 07, 05, 02, 30, 0, new TimeSpan(1, 0, 0));

        const string expectedResult = "30 minutes ago";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void PrecisionStrategy_SameOffset()
    {
        Configurator.DateTimeOffsetHumanizeStrategy = new PrecisionDateTimeOffsetHumanizeStrategy(0.75);

        DateTimeOffset inputTime = new(2015, 07, 05, 04, 0, 0, TimeSpan.Zero);
        DateTimeOffset baseTime = new(2015, 07, 04, 05, 0, 0, TimeSpan.Zero);

        const string expectedResult = "tomorrow";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void PrecisionStrategy_DifferentOffsets()
    {
        Configurator.DateTimeOffsetHumanizeStrategy = new PrecisionDateTimeOffsetHumanizeStrategy(0.75);

        DateTimeOffset inputTime = new(2015, 07, 05, 03, 45, 0, new TimeSpan(2, 0, 0));
        DateTimeOffset baseTime = new(2015, 07, 05, 02, 30, 0, new TimeSpan(-5, 0, 0));

        const string expectedResult = "6 hours ago";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Never()
    {
        DateTimeOffset? never = null;
        Assert.Equal("never", never.Humanize());
    }

    [Fact]
    public void Nullable_ExpectSame()
    {
        DateTimeOffset? never = new DateTimeOffset(2015, 12, 7, 9, 0, 0, TimeSpan.FromHours(1));

        Assert.Equal(never.Value.Humanize(), never.Humanize());
    }
}
