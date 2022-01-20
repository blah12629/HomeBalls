namespace CEo.Pokemon.HomeBalls.Tests;

public class HomeBallsLoadableDataSetTests :
    HomeBallsLoadableDataSetTests<HomeBallsPokemonFormKey, IHomeBallsPokemonForm>
{
    public HomeBallsLoadableDataSetTests() : base() { }
}

public abstract class HomeBallsLoadableDataSetTests<TKey, TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    public interface IMockInterface
    {
        Task MockMethodAsync(CancellationToken cancellationToken);
    }

    protected HomeBallsLoadableDataSetTests()
    {
        DataSet = new HomeBallsDataSet<TKey, TRecord> { };
        MockInterface = Substitute.For<IMockInterface>();
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
        InlineData(nameof(INotifyDataLoaded.DataLoaded), 1),
        InlineData(nameof(INotifyDataLoading.DataLoading), 1),
        InlineData(nameof(INotifyDataLoaded.DataLoaded), 5),
        InlineData(nameof(INotifyDataLoading.DataLoading), 5)
    ]
    public async Task EnsureLoadedAsync_ShouldRaiseDataLoadEventOnce_WhenCalledNTimes(
        String eventName,
        Int32 callCount)
    {
        var raisingMonitor = Sut.Monitor();
        Sut.IsLoaded.Should().BeFalse();
        await Sut.EnsureLoadedAsync();
        raisingMonitor.Should().Raise(eventName);

        callCount -= 1;
        var nonRaisingMonitor = Sut.Monitor();
        for (var i = 0; i < callCount; i ++)
        {
            await Sut.EnsureLoadedAsync();
            nonRaisingMonitor.Should().NotRaise(eventName);
        }
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