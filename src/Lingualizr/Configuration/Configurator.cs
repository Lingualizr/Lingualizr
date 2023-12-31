﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Lingualizr.DateTimeHumanizeStrategy;
using Lingualizr.Localisation.CollectionFormatters;
using Lingualizr.Localisation.DateToOrdinalWords;
using Lingualizr.Localisation.Formatters;
using Lingualizr.Localisation.NumberToWords;
using Lingualizr.Localisation.Ordinalizers;
using Lingualizr.Localisation.TimeToClockNotation;

namespace Lingualizr.Configuration;

/// <summary>
/// Provides a configuration point for Lingualizr
/// </summary>
public static class Configurator
{
    private static readonly LocaliserRegistry<ICollectionFormatter> _collectionFormatters = new CollectionFormatterRegistry();

    /// <summary>
    /// A registry of formatters used to format collections based on the current locale
    /// </summary>
    public static LocaliserRegistry<ICollectionFormatter> CollectionFormatters
    {
        get { return _collectionFormatters; }
    }

    private static readonly LocaliserRegistry<IFormatter> _formatters = new FormatterRegistry();

    /// <summary>
    /// A registry of formatters used to format strings based on the current locale
    /// </summary>
    public static LocaliserRegistry<IFormatter> Formatters
    {
        get { return _formatters; }
    }

    private static readonly LocaliserRegistry<INumberToWordsConverter> _numberToWordsConverters = new NumberToWordsConverterRegistry();

    /// <summary>
    /// A registry of number to words converters used to localise ToWords and ToOrdinalWords methods
    /// </summary>
    public static LocaliserRegistry<INumberToWordsConverter> NumberToWordsConverters
    {
        get { return _numberToWordsConverters; }
    }

    private static readonly LocaliserRegistry<IOrdinalizer> _ordinalizers = new OrdinalizerRegistry();

    /// <summary>
    /// A registry of ordinalizers used to localise Ordinalize method
    /// </summary>
    public static LocaliserRegistry<IOrdinalizer> Ordinalizers
    {
        get { return _ordinalizers; }
    }

    private static readonly LocaliserRegistry<IDateToOrdinalWordConverter> _dateToOrdinalWordConverters = new DateToOrdinalWordsConverterRegistry();

    /// <summary>
    /// A registry of ordinalizers used to localise Ordinalize method
    /// </summary>
    public static LocaliserRegistry<IDateToOrdinalWordConverter> DateToOrdinalWordsConverters
    {
        get { return _dateToOrdinalWordConverters; }
    }

    private static readonly LocaliserRegistry<IDateOnlyToOrdinalWordConverter> _dateOnlyToOrdinalWordConverters = new DateOnlyToOrdinalWordsConverterRegistry();

    /// <summary>
    /// A registry of ordinalizers used to localise Ordinalize method
    /// </summary>
    public static LocaliserRegistry<IDateOnlyToOrdinalWordConverter> DateOnlyToOrdinalWordsConverters
    {
        get { return _dateOnlyToOrdinalWordConverters; }
    }

    private static readonly LocaliserRegistry<ITimeOnlyToClockNotationConverter> _timeOnlyToClockNotationConverters = new TimeOnlyToClockNotationConvertersRegistry();

    /// <summary>
    /// A registry of time to clock notation converters used to localise ToClockNotation methods
    /// </summary>
    public static LocaliserRegistry<ITimeOnlyToClockNotationConverter> TimeOnlyToClockNotationConverters
    {
        get { return _timeOnlyToClockNotationConverters; }
    }

    internal static ICollectionFormatter CollectionFormatter
    {
        get { return CollectionFormatters.ResolveForUiCulture(); }
    }

    /// <summary>
    /// The formatter to be used
    /// </summary>
    /// <param name="culture">The culture to retrieve formatter for. Null means that current thread's UI culture should be used.</param>
    internal static IFormatter GetFormatter(CultureInfo? culture)
    {
        return Formatters.ResolveForCulture(culture);
    }

    /// <summary>
    /// The converter to be used
    /// </summary>
    /// <param name="culture">The culture to retrieve number to words converter for. Null means that current thread's UI culture should be used.</param>
    internal static INumberToWordsConverter GetNumberToWordsConverter(CultureInfo? culture)
    {
        return NumberToWordsConverters.ResolveForCulture(culture);
    }

    /// <summary>
    /// The ordinalizer to be used
    /// </summary>
    internal static IOrdinalizer Ordinalizer
    {
        get { return Ordinalizers.ResolveForUiCulture(); }
    }

    /// <summary>
    /// The ordinalizer to be used
    /// </summary>
    internal static IDateToOrdinalWordConverter DateToOrdinalWordsConverter
    {
        get { return DateToOrdinalWordsConverters.ResolveForUiCulture(); }
    }

    /// <summary>
    /// The ordinalizer to be used
    /// </summary>
    internal static IDateOnlyToOrdinalWordConverter DateOnlyToOrdinalWordsConverter
    {
        get { return DateOnlyToOrdinalWordsConverters.ResolveForUiCulture(); }
    }

    internal static ITimeOnlyToClockNotationConverter TimeOnlyToClockNotationConverter
    {
        get { return TimeOnlyToClockNotationConverters.ResolveForUiCulture(); }
    }

    /// <summary>
    /// The strategy to be used for DateTime.Humanize
    /// </summary>
    public static IDateTimeHumanizeStrategy DateTimeHumanizeStrategy { get; set; } = new DefaultDateTimeHumanizeStrategy();

    /// <summary>
    /// The strategy to be used for DateTimeOffset.Humanize
    /// </summary>
    public static IDateTimeOffsetHumanizeStrategy DateTimeOffsetHumanizeStrategy { get; set; } = new DefaultDateTimeOffsetHumanizeStrategy();

    /// <summary>
    /// The strategy to be used for DateOnly.Humanize
    /// </summary>
    public static IDateOnlyHumanizeStrategy DateOnlyHumanizeStrategy { get; set; } = new DefaultDateOnlyHumanizeStrategy();

    /// <summary>
    /// The strategy to be used for TimeOnly.Humanize
    /// </summary>
    public static ITimeOnlyHumanizeStrategy TimeOnlyHumanizeStrategy { get; set; } = new DefaultTimeOnlyHumanizeStrategy();

    private static readonly Func<PropertyInfo, bool> _defaultEnumDescriptionPropertyLocator = p => p.Name == "Description";
    private static Func<PropertyInfo, bool> _enumDescriptionPropertyLocator = _defaultEnumDescriptionPropertyLocator;

    /// <summary>
    /// A predicate function for description property of attribute to use for Enum.Humanize
    /// </summary>
    [NotNull]
    public static Func<PropertyInfo, bool>? EnumDescriptionPropertyLocator
    {
        get { return _enumDescriptionPropertyLocator; }
        set { _enumDescriptionPropertyLocator = value ?? _defaultEnumDescriptionPropertyLocator; }
    }
}
