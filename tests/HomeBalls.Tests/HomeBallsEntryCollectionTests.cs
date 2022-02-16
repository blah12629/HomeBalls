namespace CEo.Pokemon.HomeBalls.Tests;

public class HomeBallsEntryCollectionTests : HomeBallsBaseTests
{
    public HomeBallsEntryCollectionTests(
        ITestOutputHelper output) :
        base(output)
    {
        Sut = new();

        var existingEntry = new HomeBallsEntry { Id = (1, 1, 1) };
        existingEntry.HasHiddenAbility.Value = true;
        Sut.Add(existingEntry);
    }

    protected HomeBallsEntryCollection Sut { get; }

    [Fact]
    public void Add_ShouldNotIncrementCount_WhenCollectionContainsEntry()
    {
        var count0 = Sut.Count;
        Sut.Add(new HomeBallsEntry { Id = (1, 1, 1) });
        count0.Should().Be(Sut.Count);
    }

    [Fact]
    public void Add_ShouldRaiseExistingEntryPropertyChanged_WhenCollectionContainsEntry()
    {
        var existingEntry = Sut.Items[0];
        var monitor = existingEntry.HasHiddenAbility.Monitor();

        var newEntry = new HomeBallsEntry { Id = (1, 1, 1) };
        newEntry.HasHiddenAbility.Value = false;
        Sut.Add(newEntry);
        monitor.Should().Raise(nameof(existingEntry.HasHiddenAbility.ValueChanged));
    }

    [Fact]
    public void Contains_ShouldBeTrue_WhenOnlySpeciesIdFormIdAndBallIdMatches()
    {
        var entry1 = new HomeBallsEntry { Id = (1, 1, 1) };
        Sut.Contains(entry1).Should().BeTrue();
    }

    [Fact]
    public void Remove_ShouldRemoveEntry_WhenCollectionContainsEntry()
    {
        var entry1 = new HomeBallsEntry { Id = (1, 1, 1) };
        var monitor = Sut.Monitor();

        Sut.Remove(entry1).Should().BeTrue();
        monitor.Should().Raise(nameof(Sut.CollectionChanged));
    }
}