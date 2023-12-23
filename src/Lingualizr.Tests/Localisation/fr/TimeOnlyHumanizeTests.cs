namespace Lingualizr.Tests.Localisation.fr;

[UseCulture("fr")]
public class TimeOnlyHumanizeTests
{
    [Fact]
    public void DefaultStrategy_SameTime()
    {
        TimeOnly inputTime = new(13, 07, 05);
        TimeOnly baseTime = new(13, 07, 05);

        const string expectedResult = "maintenant";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void DefaultStrategy_HoursApart()
    {
        TimeOnly inputTime = new(13, 08, 05);
        TimeOnly baseTime = new(1, 08, 05);

        const string expectedResult = "dans 12 heures";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void DefaultStrategy_HoursAgo()
    {
        TimeOnly inputTime = new(13, 07, 02);
        TimeOnly baseTime = new(17, 07, 05);

        const string expectedResult = "il y a 4 heures";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void PrecisionStrategy_NextDay()
    {
        TimeOnly inputTime = new(18, 10, 49);
        TimeOnly baseTime = new(13, 07, 04);

        const string expectedResult = "dans 5 heures";
        string actualResult = inputTime.Humanize(baseTime);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Never()
    {
        TimeOnly? never = null;
        Assert.Equal("jamais", never.Humanize());
    }

    [Fact]
    public void Nullable_ExpectSame()
    {
        TimeOnly? never = new TimeOnly(23, 12, 7);
        Assert.Equal(never.Value.Humanize(), never.Humanize());
    }
}
