namespace CEo.Pokemon.HomeBalls.App.Settings.Tests;

public abstract class HomeBallsAppSettingsPropertyTests : HomeBallsBaseTests
{
    protected const String DataLoadiedName = "DataLoaded";

    protected const String DataLoadingName = "DataLoading";

    HomeBallsAppSettingsProperty? _sut;

    protected HomeBallsAppSettingsPropertyTests(
        ITestOutputHelper output) :
        base(output)
    {
        LocalStorage = Substitute.For<ILocalStorageService>();
        JSRuntime = Substitute.For<IJSRuntime>();
    }

    protected ILocalStorageService LocalStorage { get; }

    protected IJSRuntime JSRuntime { get; }

    protected HomeBallsAppSettingsProperty Sut
    {
        get => _sut ?? throw new NullReferenceException();
        set => _sut = value;
    }

    [Theory, InlineData(DataLoadingName), InlineData(DataLoadiedName)]
    public Task EnsureLoadedAsync_ShouldRaise_WhenIsLoadedIsFalse(String eventName) =>
        EnsureLoadedAsync_RaiseTests(
            () => Sut.IsLoaded = false,
            monitor => monitor.Should().Raise(eventName));

    [Theory, InlineData(DataLoadingName), InlineData(DataLoadiedName)]
    public Task EnsureLoadedAsync_ShouldRaise_WhenIsLoadingIsFalse(String eventName) =>
        EnsureLoadedAsync_RaiseTests(
            () => Sut.IsLoading = false,
            monitor => monitor.Should().Raise(eventName));

    [Theory, InlineData(DataLoadingName), InlineData(DataLoadiedName)]
    public Task EnsureLoadedAsync_ShouldNotRaise_WhenIsLoadedIsTrue(String eventName) =>
        EnsureLoadedAsync_RaiseTests(
            () => Sut.IsLoaded = true,
            monitor => monitor.Should().NotRaise(eventName));

    [Theory, InlineData(DataLoadingName), InlineData(DataLoadiedName)]
    public Task EnsureLoadedAsync_ShouldNotRaise_WhenIsLoadingIsTrue(String eventName) =>
        EnsureLoadedAsync_RaiseTests(
            () => Sut.IsLoading = true,
            monitor => monitor.Should().NotRaise(eventName));

    protected virtual async Task EnsureLoadedAsync_RaiseTests(
        Action arrange,
        Action<IMonitor<HomeBallsAppSettingsProperty>> assertion)
    {
        arrange();
        var monitor = Sut.Monitor();
        await Sut.EnsureLoadedAsync();
        assertion(monitor);
    }
}