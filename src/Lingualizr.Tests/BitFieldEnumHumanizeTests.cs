namespace Lingualizr.Tests;

[UseCulture("en")]
public class BitFieldEnumHumanizeTests
{
    [Fact]
    public void CanHumanizeSingleWordDescriptionAttribute()
    {
        Assert.Equal(BitFlagEnumTestsResources.MemberWithSingleWordDisplayAttribute, BitFieldEnumUnderTest.Red.Humanize());
    }

    [Fact]
    public void CanHumanizeMultipleWordDescriptionAttribute()
    {
        Assert.Equal(BitFlagEnumTestsResources.MemberWithMultipleWordDisplayAttribute, BitFieldEnumUnderTest.DarkGray.Humanize());
    }

    [Fact]
    public void CanHumanizeMultipleValueBitFieldEnum()
    {
        var xoredBitFlag = BitFieldEnumUnderTest.Red | BitFieldEnumUnderTest.DarkGray;
        Assert.Equal(BitFlagEnumTestsResources.ExpectedResultWhenBothValuesXored, xoredBitFlag.Humanize());
    }

    [Fact]
    public void CanHumanizeShortSingleWordDescriptionAttribute()
    {
        Assert.Equal(BitFlagEnumTestsResources.MemberWithSingleWordDisplayAttribute, ShortBitFieldEnumUnderTest.Red.Humanize());
    }

    [Fact]
    public void CanHumanizeShortMultipleWordDescriptionAttribute()
    {
        Assert.Equal(BitFlagEnumTestsResources.MemberWithMultipleWordDisplayAttribute, ShortBitFieldEnumUnderTest.DarkGray.Humanize());
    }

    [Fact]
    public void CanHumanizeShortMultipleValueBitFieldEnum()
    {
        var xoredBitFlag = ShortBitFieldEnumUnderTest.Red | ShortBitFieldEnumUnderTest.DarkGray;
        Assert.Equal(BitFlagEnumTestsResources.ExpectedResultWhenBothValuesXored, xoredBitFlag.Humanize());
    }

    [Fact]
    public void CanHumanizeBitFieldEnumWithZeroValue()
    {
        Assert.Equal(BitFlagEnumTestsResources.None, BitFieldEnumUnderTest.None.Humanize());
    }
}
