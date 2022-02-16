namespace CEo.Pokemon.HomeBalls.Collections.Tests;

public class HomeBallsObservableSetTests :
    HomeBallsObservableSetTests<HomeBallsEntryKey>
{
    public HomeBallsObservableSetTests(ITestOutputHelper output) : base(output) { }
}

public abstract class HomeBallsObservableSetTests<T> :
    HomeBallsBaseTests
    where T : class
{
    public interface IMockActions
    {
        void MockMethod(IList<T> items, Int32 index, T item);
    }

    public HomeBallsObservableSetTests(
        ITestOutputHelper output) :
        base(output)
    {
        Actions = Substitute.For<IMockActions>();
        InjectedItems = new List<T> { };
        Sut = new HomeBallsObservableSet<T>(Actions.MockMethod, InjectedItems);
    }

    protected IMockActions Actions { get; }

    protected List<T> InjectedItems { get; }

    protected HomeBallsObservableSet<T> Sut { get; }

    [Fact]
    public void Add_ShouldNotRaiseCollectionChanged_WhenAddedToInjectedList()
    {
        var monitor = Sut.Monitor();
        InjectedItems.Add(AutoFaker.Generate<T>());
        monitor.Should().NotRaise(nameof(Sut.CollectionChanged));
    }

    [Fact]
    public void Add_ShouldNotRaiseCollectionChanged_WhenSetContainsItem()
    {
        var item = AutoFaker.Generate<T>();
        Sut.Add(item);
        var monitor = Sut.Monitor();

        Sut.Add(item);
        monitor.Should().NotRaise(nameof(Sut.CollectionChanged));
    }

    [Fact]
    public void Add_ShouldCallAddExisting_WhenSetContainsItem()
    {
        var item = AutoFaker.Generate<T>();
        Sut.Add(item);
        var monitor = Sut.Monitor();

        Sut.Add(item);
        Actions.Received(1).MockMethod(
            Arg.Any<IList<T>>(),
            Arg.Any<Int32>(),
            Arg.Any<T>());
    }
}