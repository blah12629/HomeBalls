namespace CEo.Pokemon.HomeBalls.Tests;

public class HomeBallsLoadableDataSetTests :
    HomeBallsLoadableDataSetTests<HomeBallsPokemonFormKey, IHomeBallsPokemonForm>
{
    public HomeBallsLoadableDataSetTests() : base(default, default) { }
}

public abstract class HomeBallsLoadableDataSetTests<TKey, TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    public interface IMockInterface
    {
        Task MockMethodAsync(CancellationToken cancellationToken);
    }

    protected HomeBallsLoadableDataSetTests(
        IHomeBallsDataSet<TKey, TRecord>? dataSet,
        IMockInterface? mockInterface)
    {
        DataSet = dataSet ?? Substitute.For<IHomeBallsDataSet<TKey, TRecord>>();
        MockInterface = mockInterface ?? Substitute.For<IMockInterface>();
        Sut = new HomeBallsLoadableDataSet<TKey, TRecord>(
            DataSet,
            (dataSet, cancellationToken) =>
                MockInterface.MockMethodAsync(cancellationToken));
    }

    protected IHomeBallsDataSet<TKey, TRecord> DataSet { get; }

    protected IMockInterface MockInterface { get; }

    protected HomeBallsLoadableDataSet<TKey, TRecord> Sut { get; }

    [
        Theory,
        InlineData(nameof(INotifyDataLoaded.DataLoaded)),
        InlineData(nameof(INotifyDataLoading.DataLoading))
    ]
    public async Task EnsureLoadedAsync_ShouldRaiseDataLoadEvent_WhenIsLoadedFalse(
        String eventName)
    {
        var monitor = Sut.Monitor();
        Sut.IsLoaded.Should().BeFalse();
        await Sut.EnsureLoadedAsync();
        monitor.Should().Raise(eventName);
    }

    [Fact]
    public async Task EnsureLoadedAsync_ShouldSetIsLoadedToTrue()
    {
        await Sut.EnsureLoadedAsync();
        Sut.IsLoaded.Should().BeTrue();
    }

    [Fact]
    public async Task EnsureLoadedAsync_ShouldOnlyExecuteLoadTaskOnce_WhenCalledMultipleTimes()
    {
        await Sut.EnsureLoadedAsync();
        await Sut.EnsureLoadedAsync();
        await Sut.EnsureLoadedAsync();
        MockInterface.Received(1);
    }
}