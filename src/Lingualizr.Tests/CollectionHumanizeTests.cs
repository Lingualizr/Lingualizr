﻿namespace Lingualizr.Tests;

public class SomeClass
{
    public required string SomeString { get; init; }

    public int SomeInt { get; init; }

    public override string ToString()
    {
        return "ToString";
    }
}

[UseCulture("en")]
public class CollectionHumanizeTests
{
    [Fact]
    public void HumanizeReturnsOnlyNameWhenCollectionContainsOneItem()
    {
        List<string> collection = new() { "A String" };

        Assert.Equal("A String", collection.Humanize());
    }

    [Fact]
    public void HumanizeUsesSeparatorWhenMoreThanOneItemIsInCollection()
    {
        List<string> collection = new() { "A String", "Another String", };

        Assert.Equal("A String or Another String", collection.Humanize("or"));
    }

    [Fact]
    public void HumanizeDefaultsSeparatorToAnd()
    {
        List<string> collection = new() { "A String", "Another String", };

        Assert.Equal("A String and Another String", collection.Humanize());
    }

    [Fact]
    public void HumanizeUsesOxfordComma()
    {
        List<string> collection = new() { "A String", "Another String", "A Third String", };

        Assert.Equal("A String, Another String, or A Third String", collection.Humanize("or"));
    }

    private readonly List<SomeClass> _testCollection =
        new()
        {
            new SomeClass { SomeInt = 1, SomeString = "One" },
            new SomeClass { SomeInt = 2, SomeString = "Two" },
            new SomeClass { SomeInt = 3, SomeString = "Three" },
        };

    [Fact]
    public void HumanizeDefaultsToToString()
    {
        Assert.Equal("ToString, ToString, or ToString", _testCollection.Humanize("or"));
    }

    [Fact]
    public void HumanizeUsesStringDisplayFormatter()
    {
        string humanized = _testCollection.Humanize(sc => string.Format("SomeObject #{0} - {1}", sc.SomeInt, sc.SomeString));
        Assert.Equal("SomeObject #1 - One, SomeObject #2 - Two, and SomeObject #3 - Three", humanized);
    }

    [Fact]
    public void HumanizeUsesObjectDisplayFormatter()
    {
        string humanized = _testCollection.Humanize(sc => sc.SomeInt);
        Assert.Equal("1, 2, and 3", humanized);
    }

    [Fact]
    public void HumanizeUsesStringDisplayFormatterWhenSeparatorIsProvided()
    {
        string humanized = _testCollection.Humanize(sc => string.Format("SomeObject #{0} - {1}", sc.SomeInt, sc.SomeString), "or");
        Assert.Equal("SomeObject #1 - One, SomeObject #2 - Two, or SomeObject #3 - Three", humanized);
    }

    [Fact]
    public void HumanizeUsesObjectDisplayFormatterWhenSeparatorIsProvided()
    {
        string humanized = _testCollection.Humanize(sc => sc.SomeInt, "or");
        Assert.Equal("1, 2, or 3", humanized);
    }

    [Fact]
    public void HumanizeHandlesNullItemsWithoutAnException()
    {
        Assert.Null(Record.Exception(() => new object?[] { null, null }.Humanize()));
    }

    [Fact]
    public void HumanizeHandlesNullStringDisplayFormatterReturnsWithoutAnException()
    {
        Assert.Null(Record.Exception(() => new[] { "A", "B", "C" }.Humanize(_ => null)));
    }

    [Fact]
    public void HumanizeHandlesNullObjectDisplayFormatterReturnsWithoutAnException()
    {
        Assert.Null(Record.Exception(() => new[] { "A", "B", "C" }.Humanize(_ => (object?)null)));
    }

    [Fact]
    public void HumanizeRunsStringDisplayFormatterOnNulls()
    {
        Assert.Equal("1, (null), and 3", new int?[] { 1, null, 3 }.Humanize(i => i?.ToString() ?? "(null)"));
    }

    [Fact]
    public void HumanizeRunsObjectDisplayFormatterOnNulls()
    {
        Assert.Equal("1, 2, and 3", new int?[] { 1, null, 3 }.Humanize(i => i ?? 2));
    }

    [Fact]
    public void HumanizeRemovesEmptyItemsByDefault()
    {
        Assert.Equal("A and C", new[] { "A", " ", "C" }.Humanize(_dummyFormatter));
    }

    [Fact]
    public void HumanizeTrimsItemsByDefault()
    {
        Assert.Equal("A, B, and C", new[] { "A", "  B  ", "C" }.Humanize(_dummyFormatter));
    }

    /// <summary>
    /// Use the dummy formatter to ensure tests are testing formatter output rather than input
    /// </summary>
    private static readonly Func<string, string> _dummyFormatter = input => input;
}
