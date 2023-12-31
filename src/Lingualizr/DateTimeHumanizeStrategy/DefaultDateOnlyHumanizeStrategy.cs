﻿using System.Globalization;

namespace Lingualizr.DateTimeHumanizeStrategy;

/// <summary>
/// The default 'distance of time' -> words calculator.
/// </summary>
public class DefaultDateOnlyHumanizeStrategy : IDateOnlyHumanizeStrategy
{
    /// <summary>
    /// Calculates the distance of time in words between two provided dates
    /// </summary>
    public string Humanize(DateOnly input, DateOnly comparisonBase, CultureInfo? culture)
    {
        return DateTimeHumanizeAlgorithms.DefaultHumanize(input, comparisonBase, culture);
    }
}
