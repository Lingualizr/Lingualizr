using Lingualizr.Bytes;

namespace Lingualizr.Tests.Bytes;

public class ComparingTests
{
    [Theory]
    [InlineData(13, 23, -1)]
    [InlineData(23, 23, 0)]
    [InlineData(45, 23, 1)]
    public void CompareStrongTyped(double value, double valueToCompareWith, int expectedResult)
    {
        ByteSize valueSize = new(value);
        ByteSize otherSize = new(valueToCompareWith);
        int result = valueSize.CompareTo(otherSize);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(13, 23, -1)]
    [InlineData(23, 23, 0)]
    [InlineData(45, 23, 1)]
    public void CompareUntyped(double value, double valueToCompareWith, int expectedResult)
    {
        ByteSize valueSize = new(value);
        object otherSize = new ByteSize(valueToCompareWith);
        int result = valueSize.CompareTo(otherSize);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(new[] { "1GB", "3KB", "5MB" }, new[] { "3KB", "5MB", "1GB" })]
    [InlineData(new[] { "1MB", "3KB", "5MB" }, new[] { "3KB", "1MB", "5MB" })]
    public void SortList(IEnumerable<string> values, IEnumerable<string> expected)
    {
        List<ByteSize> list = values.Select(ByteSize.Parse).ToList();
        list.Sort();

        Assert.Equal(expected.Select(ByteSize.Parse), list);
    }
}
