using System.Globalization;
using Lingualizr.Localisation;

namespace Lingualizr.Tests.Localisation.@is;

public class ResourcesTests
{
    [Fact]
    [UseCulture("is")]
    public void GetCultureSpecificTranslationsWithImplicitCulture()
    {
        string format = Resources.GetResource("DateHumanize_MultipleYearsAgo");
        Assert.Equal("fyrir {0} árum", format);
    }

    [Fact]
    public void GetCultureSpecificTranslationsWithExplicitCulture()
    {
        string format = Resources.GetResource("DateHumanize_SingleYearAgo", new CultureInfo("is"));
        Assert.Equal("fyrir einu ári", format);
    }
}
