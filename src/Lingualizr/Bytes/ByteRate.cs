﻿using System.Globalization;
using Lingualizr.Localisation;

namespace Lingualizr.Bytes;

/// <summary>
/// Class to hold a ByteSize and a measurement interval, for the purpose of calculating the rate of transfer
/// </summary>
public class ByteRate
{
    /// <summary>
    /// Quantity of bytes
    /// </summary>
    /// <returns></returns>
    public ByteSize Size { get; private set; }

    /// <summary>
    /// Interval that bytes were transferred in
    /// </summary>
    /// <returns></returns>
    public TimeSpan Interval { get; private set; }

    /// <summary>
    /// Create a ByteRate with given quantity of bytes across an interval
    /// </summary>
    /// <param name="size"></param>
    /// <param name="interval"></param>
    public ByteRate(ByteSize size, TimeSpan interval)
    {
        this.Size = size;
        this.Interval = interval;
    }

    /// <summary>
    /// Calculate rate for the quantity of bytes and interval defined by this instance
    /// </summary>
    /// <param name="timeUnit">Unit of time to calculate rate for (defaults is per second)</param>
    /// <returns></returns>
    public string Humanize(TimeUnit timeUnit = TimeUnit.Second)
    {
        return Humanize(null, timeUnit);
    }

    /// <summary>
    /// Calculate rate for the quantity of bytes and interval defined by this instance
    /// </summary>
    /// <param name="format">The string format to use for the number of bytes</param>
    /// <param name="timeUnit">Unit of time to calculate rate for (defaults is per second)</param>
    /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
    /// <returns></returns>
    public string Humanize(string? format, TimeUnit timeUnit = TimeUnit.Second, CultureInfo? culture = null)
    {
        TimeSpan displayInterval = timeUnit switch
        {
            TimeUnit.Second => TimeSpan.FromSeconds(1),
            TimeUnit.Minute => TimeSpan.FromMinutes(1),
            TimeUnit.Hour => TimeSpan.FromHours(1),
            _ => throw new NotSupportedException("timeUnit must be Second, Minute, or Hour"),
        };
        return new ByteSize(Size.Bytes / Interval.TotalSeconds * displayInterval.TotalSeconds).Humanize(format, culture) + '/' + timeUnit.ToSymbol(culture);
    }
}
