namespace CEo.Pokemon.HomeBalls.App.Core.Tests;

public class HomeBallsEntryTableTests
{
    public HomeBallsEntryTableTests()
    {
        SutCell = new HomeBallsEntryCell
        {
            BallId = 1,
            ObtainedStatus = HomeBallsEntryObtainedStatus.NotObtained
        };
        SutColumn = new HomeBallsEntryColumn(new List<IHomeBallsEntryCell> { SutCell })
        {
            SpeciesId = 1,
            FormId = 1
        };
        Sut = new HomeBallsEntryTable(
            new List<IHomeBallsEntryColumn> { SutColumn },
            new Dictionary<(UInt16, Byte), Int32> { [(1, 1)] = 0 }.AsReadOnly(),
            new HomeBallsEntryCollection());
    }

    protected IHomeBallsEntryCell SutCell { get; }

    protected IHomeBallsEntryColumn SutColumn { get; }

    protected HomeBallsEntryTable Sut { get; }

    [Fact]
    public void Add_ShouldRaiseCellPropertyChanged_WhenNewEntryAdded()
    {
        var cellMonitor = SutCell.Monitor();
        Sut.Add(new HomeBallsEntry
        {
            SpeciesId = 1,
            FormId = 1,
            BallId = 1,
            HasHiddenAbility = true
        });
        cellMonitor.Should().Raise(nameof(SutCell.PropertyChanged));
    }
}