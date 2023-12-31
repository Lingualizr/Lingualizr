﻿namespace Lingualizr;

public partial class InDate
{
    /// <summary>
    /// Returns the first of January of the provided year
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    public static DateOnly TheYear(int year)
    {
        return new DateOnly(year, 1, 1);
    }
}
