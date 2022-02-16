namespace CEo.Pokemon.HomeBalls.Tests;

public class HomeBallsEntryTests : HomeBallsBaseTests
{
    public HomeBallsEntryTests(
        ITestOutputHelper output) :
        base(output)
    {
        EggMoveIds = new();
        Sut = new HomeBallsEntry(false, EggMoveIds);
    }

    protected List<UInt16> EggMoveIds { get; }

    protected HomeBallsEntry Sut { get; }


    [Theory, InlineData(12)]
    public void AvailableEggMoveIds_ShouldRaiseCollectionChanged_WhenValueAdded(
        UInt16 addedId)
    {
        var monitor = Sut.AvailableEggMoveIds.Monitor();
        Sut.AvailableEggMoveIds.Add(addedId);
        monitor.Should().Raise(nameof(Sut.AvailableEggMoveIds.CollectionChanged));
    }

    [Theory, InlineData(12)]
    public void AvailableEggMoveIds_ShouldRaiseCollectionChanged_WhenValueRemoved(
        UInt16 removedId)
    {
        Sut.AvailableEggMoveIds.Add(removedId);
        var monitor = Sut.AvailableEggMoveIds.Monitor();
        Sut.AvailableEggMoveIds.Remove(removedId);
        monitor.Should().Raise(nameof(Sut.AvailableEggMoveIds.CollectionChanged));
    }

    [Theory, InlineData(12)]
    public void AvailableEggMoveIds_ShouldNotRaiseCollectionChanged_WhenExistingValueAdded(
        UInt16 existingId)
    {
        Sut.AvailableEggMoveIds.Add(existingId);
        var monitor = Sut.AvailableEggMoveIds.Monitor();
        Sut.AvailableEggMoveIds.Add(existingId);
        monitor.Should().NotRaise(nameof(Sut.AvailableEggMoveIds.CollectionChanged));
    }

    [Fact]
    public void AvailableEggMoveIds_ShouldNotRaiseCollectionChanged_WhenOriginalListAdded()
    {
        var monitor = Sut.AvailableEggMoveIds.Monitor();
        EggMoveIds.Add(12);
        EggMoveIds.Add(13);
        monitor.Should().NotRaise(nameof(Sut.AvailableEggMoveIds.CollectionChanged));
    }
}