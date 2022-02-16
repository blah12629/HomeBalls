namespace CEo.Pokemon.HomeBalls.Collections.Tests;

public class HomeBallsObservableListTests :
    HomeBallsObservableListTests<HomeBallsEntry>
{
    public HomeBallsObservableListTests(ITestOutputHelper output) : base(output) { }
}

public abstract class HomeBallsObservableListTests<T> :
    HomeBallsBaseTests
{
    protected HomeBallsObservableListTests(
        ITestOutputHelper output) :
        base(output)
    {
        Items = new List<T> { AutoFaker.Generate<T>() };
        Comparer = Substitute.For<IEqualityComparer<T>>();
        Sut = new HomeBallsObservableList<T>(Items, Comparer);

        Comparer.Configure().Equals(Arg.Any<T?>(), Arg.Any<T?>()).Returns(true);
    }

    protected List<T> Items { get; }

    protected IEqualityComparer<T> Comparer { get; }

    protected HomeBallsObservableList<T> Sut { get; }

    [Fact]
    public void Sut_ShouldNotRaiseCollectionChanged_WhenAddedOnItemsList()
    {
        var monitor = Sut.Monitor();
        Items.Add(AutoFaker.Generate<T>());
        monitor.Should().NotRaise(nameof(Sut.CollectionChanged));
    }

    [Fact]
    public void IndexOf_ShouldUseComparer_WhenComparerIsNotNull() =>
        Sut_ShouldUseComparer_whenComparerIsNotNull(() => Sut.IndexOf(Items[0]));

    [Fact]
    public void Contains_ShouldUserComparer_WhenComparerIsNotNull() =>
        Sut_ShouldUseComparer_whenComparerIsNotNull(() => Sut.Contains(Items[0]));

    [Fact]
    public void Remove_ShouldUseComparer_WhenComparerIsNotNull() =>
        Sut_ShouldUseComparer_whenComparerIsNotNull(() => Sut.Remove(Items[0]));

    protected internal void Sut_ShouldUseComparer_whenComparerIsNotNull(Action action)
    {
        action();
        Comparer.Received(1).Equals(Arg.Any<T>(), Arg.Any<T>());
    }
}