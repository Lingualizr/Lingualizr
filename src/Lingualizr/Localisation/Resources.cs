﻿using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Lingualizr.Localisation;

/// <summary>
/// Provides access to the resources of Lingualizr
/// </summary>
public static class Resources
{
    private static readonly ResourceManager _resourceManager = new("Lingualizr.Properties.Resources", typeof(Resources).GetTypeInfo().Assembly);

    /// <summary>
    /// Returns the value of the specified string resource
    /// </summary>
    /// <param name="resourceKey">The name of the resource to retrieve.</param>
    /// <param name="culture">The culture of the resource to retrieve. If not specified, current thread's UI culture is used.</param>
    /// <returns>The value of the resource localized for the specified culture.</returns>
    public static string GetResource(string resourceKey, CultureInfo? culture = null)
    {
        string? result = _resourceManager.GetString(resourceKey, culture);

        ArgumentNullException.ThrowIfNull(result);

        return result;
    }
}
