﻿using System.Globalization;
using Lingualizr.Configuration;
using Lingualizr.DateTimeHumanizeStrategy;
using Lingualizr.Localisation;

namespace Lingualizr.Tests;

public class DateHumanize
{
    private static readonly object _lockObject = new();

    private static void VerifyWithCurrentDate(string expectedString, TimeSpan deltaFromNow, CultureInfo? culture)
    {
        DateTime utcNow = DateTime.UtcNow;
        DateTime localNow = DateTime.Now;

        // feels like the only way to avoid breaking tests because CPU ticks over is to inject the base date
        VerifyWithDate(expectedString, deltaFromNow, culture, localNow, utcNow);
    }

    private static void VerifyWithDateInjection(string expectedString, TimeSpan deltaFromNow, CultureInfo? culture)
    {
        DateTime utcNow = new(2013, 6, 20, 9, 58, 22, DateTimeKind.Utc);
        DateTime now = new(2013, 6, 20, 11, 58, 22, DateTimeKind.Local);

        VerifyWithDate(expectedString, deltaFromNow, culture, now, utcNow);
    }

    private static void VerifyWithDate(string expectedString, TimeSpan deltaFromBase, CultureInfo? culture, DateTime? baseDate, DateTime? baseDateUtc)
    {
        Assert.Equal(expectedString, baseDateUtc?.Add(deltaFromBase).Humanize(utcDate: true, dateToCompareAgainst: baseDateUtc, culture: culture));
        Assert.Equal(expectedString, baseDate?.Add(deltaFromBase).Humanize(false, baseDate, culture: culture));

        // Compared with default utcDate
        Assert.Equal(expectedString, baseDateUtc?.Add(deltaFromBase).Humanize(utcDate: null, dateToCompareAgainst: baseDateUtc, culture: culture));
        Assert.Equal(expectedString, baseDate?.Add(deltaFromBase).Humanize(null, baseDate, culture: culture));
    }

    public static void Verify(string expectedString, int unit, TimeUnit timeUnit, Tense tense, double? precision = null, CultureInfo? culture = null, DateTime? baseDate = null, DateTime? baseDateUtc = null)
    {
        // We lock this as these tests can be multi-threaded and we're setting a static
        lock (_lockObject)
        {
            if (precision.HasValue)
            {
                Configurator.DateTimeHumanizeStrategy = new PrecisionDateTimeHumanizeStrategy(precision.Value);
            }
            else
            {
                Configurator.DateTimeHumanizeStrategy = new DefaultDateTimeHumanizeStrategy();
            }

            TimeSpan deltaFromNow = default(TimeSpan);
            unit = Math.Abs(unit);

            if (tense == Tense.Past)
            {
                unit = -unit;
            }

            switch (timeUnit)
            {
                case TimeUnit.Millisecond:
                    deltaFromNow = TimeSpan.FromMilliseconds(unit);
                    break;
                case TimeUnit.Second:
                    deltaFromNow = TimeSpan.FromSeconds(unit);
                    break;
                case TimeUnit.Minute:
                    deltaFromNow = TimeSpan.FromMinutes(unit);
                    break;
                case TimeUnit.Hour:
                    deltaFromNow = TimeSpan.FromHours(unit);
                    break;
                case TimeUnit.Day:
                    deltaFromNow = TimeSpan.FromDays(unit);
                    break;
                case TimeUnit.Month:
                    deltaFromNow = TimeSpan.FromDays(unit * 31);
                    break;
                case TimeUnit.Year:
                    deltaFromNow = TimeSpan.FromDays(unit * 366);
                    break;
            }

            if (baseDate == null)
            {
                VerifyWithCurrentDate(expectedString, deltaFromNow, culture);
                VerifyWithDateInjection(expectedString, deltaFromNow, culture);
            }
            else
            {
                VerifyWithDate(expectedString, deltaFromNow, culture, baseDate, baseDateUtc);
            }
        }
    }
}
