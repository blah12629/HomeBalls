namespace CEo.Pokemon.HomeBalls.Tests;

public abstract class HomeBallsBaseTest
{
    protected HomeBallsTestsActivator Activator { get; } = new();

    protected MockFileSystem FileSystem { get; } = new();
}
