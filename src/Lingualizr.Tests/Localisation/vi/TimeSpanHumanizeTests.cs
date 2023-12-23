using Lingualizr.Localisation;

namespace Lingualizr.Tests.Localisation.vi;

[UseCulture("vi")]
public class TimeSpanHumanizeTests
{
    [Theory]
    [Trait("Translation", "Google")]
    [InlineData(366, "1 năm")]
    [InlineData(731, "2 năm")]
    [InlineData(1096, "3 năm")]
    [InlineData(4018, "11 năm")]
    public void Years(int days, string expected)
    {
        Assert.Equal(expected, TimeSpan.FromDays(days).Humanize(maxUnit: TimeUnit.Year));
    }

    [Theory]
    [Trait("Translation", "Google")]
    [InlineData(31, "1 tháng")]
    [InlineData(61, "2 tháng")]
    [InlineData(92, "3 tháng")]
    [InlineData(335, "11 tháng")]
    public void Months(int days, string expected)
    {
        Assert.Equal(expected, TimeSpan.FromDays(days).Humanize(maxUnit: TimeUnit.Year));
    }

    [Theory]
    [InlineData(14, "2 tuần")]
    [InlineData(7, "1 tuần")]
    [InlineData(2, "2 ngày")]
    [InlineData(1, "1 ngày")]
    public void Days(int days, string expected)
    {
        string actual = TimeSpan.FromDays(days).Humanize();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2, "2 giờ")]
    [InlineData(1, "1 giờ")]
    public void Hours(int hours, string expected)
    {
        string actual = TimeSpan.FromHours(hours).Humanize();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2, "2 phút")]
    [InlineData(1, "1 phút")]
    public void Minutes(int minutes, string expected)
    {
        string actual = TimeSpan.FromMinutes(minutes).Humanize();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2, "2 giây")]
    [InlineData(1, "1 giây")]
    public void Seconds(int seconds, string expected)
    {
        string actual = TimeSpan.FromSeconds(seconds).Humanize();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2, "2 phần ngàn giây")]
    [InlineData(1, "1 phần ngàn giây")]
    public void Milliseconds(int ms, string expected)
    {
        string actual = TimeSpan.FromMilliseconds(ms).Humanize();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NoTime()
    {
        TimeSpan noTime = TimeSpan.Zero;
        string actual = noTime.Humanize();
        Assert.Equal("0 phần ngàn giây", actual);
    }

    [Fact]
    public void NoTimeToWords()
    {
        TimeSpan noTime = TimeSpan.Zero;
        string actual = noTime.Humanize(toWords: true);
        Assert.Equal("không giờ", actual);
    }
}
