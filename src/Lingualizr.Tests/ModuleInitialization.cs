using System.Runtime.CompilerServices;

namespace Lingualizr.Tests;

public class ModuleInitialization
{
    [ModuleInitializer]
    public static void Initialize() =>
        VerifyDiffPlex.Initialize();
}
