namespace CEo.Pokemon.HomeBalls.App.Core.Tests;

public class AddableTests : AddableTests<IHomeBallsEntity> { }

public abstract class AddableTests<T>
    where T : class
{
    public interface IMockMethods
    {
        void MockAdd(T item);
    }

    public AddableTests()
    {
        MockMethods = Substitute.For<IMockMethods>();
        Sut = new(MockMethods.MockAdd);
    }

    protected IMockMethods MockMethods { get; }

    protected Addable<T> Sut { get; }

    [Fact]
    public void Add_ShouldCallAddAction()
    {
        var item = Substitute.For<T>();
        Sut.Add(item);
        MockMethods.ReceivedWithAnyArgs(1).MockAdd(Arg.Any<T>());
    }

    [Fact]
    public void Add_ShouldIncrementCount()
    {
        var count = Sut.Count;
        Sut.Add(Substitute.For<T>());
        Sut.Count.Should().Be(count + 1);
    }
}