﻿using System.Globalization;
using Lingualizr.Configuration;
using Lingualizr.Localisation;
using Lingualizr.Localisation.Formatters;

namespace Lingualizr;

/// <summary>
/// Humanizes TimeSpan into human readable form
/// </summary>
public static class TimeSpanHumanizeExtensions
{
    private const int DaysInAWeek = 7;
    private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
    private const double DaysInAMonth = DaysInAYear / 12;

    /// <summary>
    /// Turns a TimeSpan into a human readable form. E.g. 1 day.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <param name="precision">The maximum number of time units to return. Defaulted is 1 which means the largest unit is returned</param>
    /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
    /// <param name="maxUnit">The maximum unit of time to output. The default value is <see cref="TimeUnit.Week"/>. The time units <see cref="TimeUnit.Month"/> and <see cref="TimeUnit.Year"/> will give approximations for time spans bigger 30 days by calculating with 365.2425 days a year and 30.4369 days a month.</param>
    /// <param name="minUnit">The minimum unit of time to output.</param>
    /// <param name="collectionSeparator">The separator to use when combining humanized time parts. If null, the default collection formatter for the current culture is used.</param>
    /// <param name="toWords">Uses words instead of numbers if true. E.g. one day.</param>
    /// <returns></returns>
    public static string Humanize(
        this TimeSpan timeSpan,
        int precision = 1,
        CultureInfo? culture = null,
        TimeUnit maxUnit = TimeUnit.Week,
        TimeUnit minUnit = TimeUnit.Millisecond,
        string? collectionSeparator = ", ",
        bool toWords = false
    )
    {
        return Humanize(timeSpan, precision, false, culture, maxUnit, minUnit, collectionSeparator, toWords);
    }

    /// <summary>
    /// Turns a TimeSpan into a human readable form. E.g. 1 day.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <param name="precision">The maximum number of time units to return.</param>
    /// <param name="countEmptyUnits">Controls whether empty time units should be counted towards maximum number of time units. Leading empty time units never count.</param>
    /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
    /// <param name="maxUnit">The maximum unit of time to output. The default value is <see cref="TimeUnit.Week"/>. The time units <see cref="TimeUnit.Month"/> and <see cref="TimeUnit.Year"/> will give approximations for time spans bigger than 30 days by calculating with 365.2425 days a year and 30.4369 days a month.</param>
    /// <param name="minUnit">The minimum unit of time to output.</param>
    /// <param name="collectionSeparator">The separator to use when combining humanized time parts. If null, the default collection formatter for the current culture is used.</param>
    /// <param name="toWords">Uses words instead of numbers if true. E.g. one day.</param>
    /// <returns></returns>
    public static string Humanize(
        this TimeSpan timeSpan,
        int precision,
        bool countEmptyUnits,
        CultureInfo? culture = null,
        TimeUnit maxUnit = TimeUnit.Week,
        TimeUnit minUnit = TimeUnit.Millisecond,
        string? collectionSeparator = ", ",
        bool toWords = false
    )
    {
        IEnumerable<string?> timeParts = CreateTheTimePartsWithUpperAndLowerLimits(timeSpan, culture, maxUnit, minUnit, toWords);
        timeParts = SetPrecisionOfTimeSpan(timeParts, precision, countEmptyUnits);

        return ConcatenateTimeSpanParts(timeParts!, culture, collectionSeparator);
    }

    private static IEnumerable<string> CreateTheTimePartsWithUpperAndLowerLimits(TimeSpan timespan, CultureInfo? culture, TimeUnit maxUnit, TimeUnit minUnit, bool toWords = false)
    {
        IFormatter cultureFormatter = Configurator.GetFormatter(culture);
        bool firstValueFound = false;
        IEnumerable<TimeUnit> timeUnitsEnumTypes = GetEnumTypesForTimeUnit();
        List<string> timeParts = new();

        foreach (TimeUnit timeUnitType in timeUnitsEnumTypes)
        {
            string? timepart = GetTimeUnitPart(timeUnitType, timespan, maxUnit, minUnit, cultureFormatter, toWords);

            if (timepart != null || firstValueFound)
            {
                firstValueFound = true;
                timeParts.Add(timepart!);
            }
        }

        if (IsContainingOnlyNullValue(timeParts))
        {
            string noTimeValueCultureFormatted = toWords ? cultureFormatter.TimeSpanHumanize_Zero() : cultureFormatter.TimeSpanHumanize(minUnit, 0, toWords);
            timeParts = CreateTimePartsWithNoTimeValue(noTimeValueCultureFormatted);
        }

        return timeParts;
    }

    private static IEnumerable<TimeUnit> GetEnumTypesForTimeUnit()
    {
        IEnumerable<TimeUnit> enumTypeEnumerator = (IEnumerable<TimeUnit>)Enum.GetValues(typeof(TimeUnit));
        return enumTypeEnumerator.Reverse();
    }

    private static string? GetTimeUnitPart(TimeUnit timeUnitToGet, TimeSpan timespan, TimeUnit maximumTimeUnit, TimeUnit minimumTimeUnit, IFormatter cultureFormatter, bool toWords = false)
    {
        if (timeUnitToGet <= maximumTimeUnit && timeUnitToGet >= minimumTimeUnit)
        {
            int numberOfTimeUnits = GetTimeUnitNumericalValue(timeUnitToGet, timespan, maximumTimeUnit);
            return BuildFormatTimePart(cultureFormatter, timeUnitToGet, numberOfTimeUnits, toWords);
        }

        return null;
    }

    private static int GetTimeUnitNumericalValue(TimeUnit timeUnitToGet, TimeSpan timespan, TimeUnit maximumTimeUnit)
    {
        bool isTimeUnitToGetTheMaximumTimeUnit = timeUnitToGet == maximumTimeUnit;
        switch (timeUnitToGet)
        {
            case TimeUnit.Millisecond:
                return GetNormalCaseTimeAsInteger(timespan.Milliseconds, timespan.TotalMilliseconds, isTimeUnitToGetTheMaximumTimeUnit);
            case TimeUnit.Second:
                return GetNormalCaseTimeAsInteger(timespan.Seconds, timespan.TotalSeconds, isTimeUnitToGetTheMaximumTimeUnit);
            case TimeUnit.Minute:
                return GetNormalCaseTimeAsInteger(timespan.Minutes, timespan.TotalMinutes, isTimeUnitToGetTheMaximumTimeUnit);
            case TimeUnit.Hour:
                return GetNormalCaseTimeAsInteger(timespan.Hours, timespan.TotalHours, isTimeUnitToGetTheMaximumTimeUnit);
            case TimeUnit.Day:
                return GetSpecialCaseDaysAsInteger(timespan, maximumTimeUnit);
            case TimeUnit.Week:
                return GetSpecialCaseWeeksAsInteger(timespan, isTimeUnitToGetTheMaximumTimeUnit);
            case TimeUnit.Month:
                return GetSpecialCaseMonthAsInteger(timespan, isTimeUnitToGetTheMaximumTimeUnit);
            case TimeUnit.Year:
                return GetSpecialCaseYearAsInteger(timespan);
            default:
                return 0;
        }
    }

    private static int GetSpecialCaseMonthAsInteger(TimeSpan timespan, bool isTimeUnitToGetTheMaximumTimeUnit)
    {
        if (isTimeUnitToGetTheMaximumTimeUnit)
        {
            return (int)(timespan.Days / DaysInAMonth);
        }
        else
        {
            double remainingDays = timespan.Days % DaysInAYear;
            return (int)(remainingDays / DaysInAMonth);
        }
    }

    private static int GetSpecialCaseYearAsInteger(TimeSpan timespan)
    {
        return (int)(timespan.Days / DaysInAYear);
    }

    private static int GetSpecialCaseWeeksAsInteger(TimeSpan timespan, bool isTimeUnitToGetTheMaximumTimeUnit)
    {
        if (isTimeUnitToGetTheMaximumTimeUnit || timespan.Days < DaysInAMonth)
        {
            return timespan.Days / DaysInAWeek;
        }

        return 0;
    }

    private static int GetSpecialCaseDaysAsInteger(TimeSpan timespan, TimeUnit maximumTimeUnit)
    {
        if (maximumTimeUnit == TimeUnit.Day)
        {
            return timespan.Days;
        }

        if (timespan.Days < DaysInAMonth || maximumTimeUnit == TimeUnit.Week)
        {
            int remainingDays = timespan.Days % DaysInAWeek;
            return remainingDays;
        }

        return (int)(timespan.Days % DaysInAMonth);
    }

    private static int GetNormalCaseTimeAsInteger(int timeNumberOfUnits, double totalTimeNumberOfUnits, bool isTimeUnitToGetTheMaximumTimeUnit)
    {
        if (isTimeUnitToGetTheMaximumTimeUnit)
        {
            try
            {
                return (int)totalTimeNumberOfUnits;
            }
            catch
            {
                // To be implemented so that TimeSpanHumanize method accepts double type as unit
                return 0;
            }
        }

        return timeNumberOfUnits;
    }

    private static string? BuildFormatTimePart(IFormatter cultureFormatter, TimeUnit timeUnitType, int amountOfTimeUnits, bool toWords = false)
    {
        // Always use positive units to account for negative timespans
        return amountOfTimeUnits != 0 ? cultureFormatter.TimeSpanHumanize(timeUnitType, Math.Abs(amountOfTimeUnits), toWords) : null;
    }

    private static List<string> CreateTimePartsWithNoTimeValue(string noTimeValue)
    {
        return new List<string>() { noTimeValue };
    }

    private static bool IsContainingOnlyNullValue(IEnumerable<string?> timeParts)
    {
        return timeParts.All(x => x == null);
    }

    private static IEnumerable<string> SetPrecisionOfTimeSpan(IEnumerable<string?> timeParts, int precision, bool countEmptyUnits)
    {
        if (!countEmptyUnits)
        {
            timeParts = timeParts.Where(x => x != null);
        }

        timeParts = timeParts.Take(precision);
        if (countEmptyUnits)
        {
            timeParts = timeParts.Where(x => x != null);
        }

        return timeParts!;
    }

    private static string ConcatenateTimeSpanParts(IEnumerable<string> timeSpanParts, CultureInfo? culture, string? collectionSeparator)
    {
        if (collectionSeparator == null)
        {
            return Configurator.CollectionFormatters.ResolveForCulture(culture).Humanize(timeSpanParts);
        }

        return string.Join(collectionSeparator, timeSpanParts);
    }
}
