namespace CEo.Pokemon.HomeBalls.Tests;

public abstract class HomeBallsBaseTests
{
    protected HomeBallsBaseTests(ITestOutputHelper output) => Output = output;

    protected ITestOutputHelper Output { get; }
}