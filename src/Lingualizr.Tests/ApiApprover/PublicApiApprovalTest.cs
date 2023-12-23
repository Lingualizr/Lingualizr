using PublicApiGenerator;

namespace Lingualizr.Tests.ApiApprover;

[UsesVerify]
public class PublicApiApprovalTest
{
    [Fact]
    [UseCulture("en-US")]
    public Task Approve_Public_Api()
    {
        string publicApi = Filter(typeof(StringHumanizeExtensions).Assembly.GeneratePublicApi());
        return Verify(publicApi);
    }

    private static string Filter(string text)
    {
        return string.Join(
            Environment.NewLine,
            text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => !l.StartsWith("[assembly: AssemblyVersion("))
                .Where(l => !l.StartsWith("[assembly: AssemblyFileVersion("))
                .Where(l => !l.StartsWith("[assembly: AssemblyInformationalVersion("))
                .Where(l => !l.StartsWith("[assembly: System.Reflection.AssemblyMetadata(\"CommitHash\""))
                .Where(l => !l.StartsWith("[assembly: System.Reflection.AssemblyMetadata(\"RepositoryUrl\""))
                .Where(l => !string.IsNullOrWhiteSpace(l))
        );
    }
}
