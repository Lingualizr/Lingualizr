using System.Globalization;
using Lingualizr.Localisation;

namespace Lingualizr.Tests.Localisation;

public class ResourcesTests
{
    [Fact]
    [UseCulture("ro")]
    public void CanGetCultureSpecificTranslationsWithImplicitCulture()
    {
        string format = Resources.GetResource("DateHumanize_MultipleYearsAgo");
        Assert.Equal("acum {0}{1} ani", format);
    }

    [Fact]
    public void CanGetCultureSpecificTranslationsWithExplicitCulture()
    {
        string format = Resources.GetResource("DateHumanize_MultipleYearsAgo", new CultureInfo("ro"));
        Assert.Equal("acum {0}{1} ani", format);
    }
}
