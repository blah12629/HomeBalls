namespace CEo.Pokemon.HomeBalls.Tests;

public class HomeBallsEntryCollectionTests
{
    public HomeBallsEntryCollectionTests()
    {
        Sut = new();

        var existingEntry = new HomeBallsEntry { SpeciesId = 1, FormId = 1, BallId = 1 };
        existingEntry.HasHiddenAbility = true;
        Sut.Add(existingEntry);
    }

    protected HomeBallsEntryCollection Sut { get; }

    [Fact]
    public void Add_ShouldNotIncrementCount_WhenCollectionContainsEntry()
    {
        var count0 = Sut.Count;
        Sut.Add(new HomeBallsEntry { SpeciesId = 1, FormId = 1, BallId = 1 });
        count0.Should().Be(Sut.Count);
    }

    [Fact]
    public void Add_ShouldRaiseExistingEntryPropertyChanged_WhenCollectionContainsEntry()
    {
        var existingEntry = Sut.Entries[0];
        var monitor = existingEntry.Monitor();

        var newEntry = new HomeBallsEntry { SpeciesId = 1, FormId = 1, BallId = 1 };
        newEntry.HasHiddenAbility = false;
        Sut.Add(newEntry);
        monitor.Should().Raise(nameof(existingEntry.PropertyChanged));
    }

    [Fact]
    public void Contains_ShouldBeTrue_WhenOnlySpeciesIdFormIdAndBallIdMatches()
    {
        var entry1 = new HomeBallsEntry { SpeciesId = 1, FormId = 1, BallId = 1 };
        Sut.Contains(entry1).Should().BeTrue();
    }

    [Fact]
    public void Remove_ShouldRemoveEntry_WhenCollectionContainsEntry()
    {
        var entry1 = new HomeBallsEntry { SpeciesId = 1, FormId = 1, BallId = 1 };
        var monitor = Sut.Monitor();

        Sut.Remove(entry1).Should().BeTrue();
        monitor.Should().Raise(nameof(Sut.CollectionChanged));
    }
}