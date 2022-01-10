namespace CEo.Pokemon.HomeBalls.Tests;

public class HomeBallsEntryTests
{
    protected HomeBallsEntry Sut { get; } = new();

    [Theory, InlineData(12)]
    public void AvailableEggMoveIds_ShouldRaisePropertyChanged_WhenValueAdded(
        UInt16 addedId)
    {
        var monitor = Sut.Monitor();
        Sut.AvailableEggMoveIds.Add(addedId);
        monitor.Should().Raise(nameof(Sut.PropertyChanged));
    }

    [Theory, InlineData(12)]
    public void AvailableEggMoveIds_ShouldRaisePropertyChanged_WhenValueRemoved(
        UInt16 removedId)
    {
        Sut.AvailableEggMoveIds.Add(removedId);
        var monitor = Sut.Monitor();
        Sut.AvailableEggMoveIds.Remove(removedId);
        monitor.Should().Raise(nameof(Sut.PropertyChanged));
    }

    [Theory, InlineData(12)]
    public void AvailableEggMoveIds_ShouldNotRaisePropertyChanged_WhenExistingValueAdded(
        UInt16 existingId)
    {
        Sut.AvailableEggMoveIds.Add(existingId);
        var monitor = Sut.Monitor();
        Sut.AvailableEggMoveIds.Add(existingId);
        monitor.Should().NotRaise(nameof(Sut.PropertyChanged));
    }

    [Fact]
    public void LastUpdatedOn_ShouldNotRaisePropertyChanged()
    {
        var monitor = Sut.Monitor();
        Sut.LastUpdatedOn = DateTime.Now;
        monitor.Should().NotRaise(nameof(Sut.PropertyChanged));
    }
}