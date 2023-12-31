﻿using Lingualizr.Bytes;
using Lingualizr.Localisation;

namespace Lingualizr.Tests.Localisation.de.Bytes;

[UseCulture("de-DE")]
public class ByteRateTests
{
    [Theory]
    [InlineData(400, 1, "400 B/s")]
    [InlineData(4 * 1024, 1, "4 kB/s")]
    [InlineData(4 * 1024 * 1024, 1, "4 MB/s")]
    [InlineData(4 * 2 * 1024 * 1024, 2, "4 MB/s")]
    [InlineData(4 * 1024, 0.1, "40 kB/s")]
    [InlineData(15 * 60 * 1024 * 1024, 60, "15 MB/s")]
    public void HumanizesRates(long inputBytes, double perSeconds, string expectedValue)
    {
        ByteSize size = new(inputBytes);
        TimeSpan interval = TimeSpan.FromSeconds(perSeconds);

        string rate = size.Per(interval).Humanize();

        Assert.Equal(expectedValue, rate);
    }

    [Theory]
    [InlineData(1, 1, TimeUnit.Second, "1 MB/s")]
    [InlineData(1, 60, TimeUnit.Minute, "1 MB/min")]
    [InlineData(1, 60 * 60, TimeUnit.Hour, "1 MB/h")]
    [InlineData(10, 1, TimeUnit.Second, "10 MB/s")]
    [InlineData(10, 60, TimeUnit.Minute, "10 MB/min")]
    [InlineData(10, 60 * 60, TimeUnit.Hour, "10 MB/h")]
    [InlineData(1, 10 * 1, TimeUnit.Second, "102,4 kB/s")]
    [InlineData(1, 10 * 60, TimeUnit.Minute, "102,4 kB/min")]
    [InlineData(1, 10 * 60 * 60, TimeUnit.Hour, "102,4 kB/h")]
    public void TimeUnitTests(long megabytes, double measurementIntervalSeconds, TimeUnit displayInterval, string expectedValue)
    {
        ByteSize size = ByteSize.FromMegabytes(megabytes);
        TimeSpan measurementInterval = TimeSpan.FromSeconds(measurementIntervalSeconds);

        ByteRate rate = size.Per(measurementInterval);
        string text = rate.Humanize(displayInterval);

        Assert.Equal(expectedValue, text);
    }

    [Theory]
    [InlineData(19854651984, 1, TimeUnit.Second, null, "18,49 GB/s")]
    [InlineData(19854651984, 1, TimeUnit.Second, "#.##", "18,49 GB/s")]
    public void FormattedTimeUnitTests(long bytes, int measurementIntervalSeconds, TimeUnit displayInterval, string? format, string expectedValue)
    {
        ByteSize size = ByteSize.FromBytes(bytes);
        TimeSpan measurementInterval = TimeSpan.FromSeconds(measurementIntervalSeconds);
        ByteRate rate = size.Per(measurementInterval);
        string text = rate.Humanize(format, displayInterval);

        Assert.Equal(expectedValue, text);
    }

    [Theory]
    [InlineData(TimeUnit.Millisecond)]
    [InlineData(TimeUnit.Day)]
    [InlineData(TimeUnit.Month)]
    [InlineData(TimeUnit.Week)]
    [InlineData(TimeUnit.Year)]
    public void ThowsOnUnsupportedData(TimeUnit units)
    {
        ByteRate dummyRate = ByteSize.FromBits(1).Per(TimeSpan.FromSeconds(1));

        Assert.Throws<NotSupportedException>(() =>
        {
            dummyRate.Humanize(units);
        });
    }
}
