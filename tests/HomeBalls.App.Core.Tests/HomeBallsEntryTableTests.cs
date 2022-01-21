namespace CEo.Pokemon.HomeBalls.App.Core.Tests;

public class HomeBallsEntryTableTests
{
    public HomeBallsEntryTableTests()
    {
        SutCell = new HomeBallsEntryCell(new(1, 1, 1), String.Empty)
        {
            IsObtained = false,
            IsLegal = true,
            IsLegalWithHiddenAbility = false
        };

        var cells = new List<IHomeBallsEntryCell> { SutCell }.AsReadOnly();
        SutColumn = new HomeBallsEntryColumn(new(1, 1), String.Empty, cells);

        Sut = new HomeBallsEntryTable();
        Sut.Columns.Add(SutColumn);
    }

    protected IHomeBallsEntryCell SutCell { get; }

    protected IHomeBallsEntryColumn SutColumn { get; }

    protected HomeBallsEntryTable Sut { get; }

    [Fact]
    public void Add_ShouldRaiseCellPropertyChanged_WhenNewEntryAdded()
    {
        var cellMonitor = SutCell.Monitor();
        Sut.Entries.Add(new HomeBallsEntry
        {
            SpeciesId = 1,
            FormId = 1,
            BallId = 1,
            HasHiddenAbility = true
        });
        cellMonitor.Should().Raise(nameof(SutCell.PropertyChanged));
    }

    [Fact]
    public void Add_ShouldRaiseCellPropertyChanged_WhenNewLegalityAdded()
    {
        var cellMonitor = SutCell.Monitor();
        var legality = Substitute.For<IHomeBallsEntryLegality>();
        legality.Configure().Id.ReturnsForAnyArgs(new HomeBallsEntryKey(1, 1, 1));
        legality.Configure().SpeciesId.ReturnsForAnyArgs<UInt16>(1);
        legality.Configure().FormId.ReturnsForAnyArgs<Byte>(1);
        legality.Configure().BallId.ReturnsForAnyArgs<UInt16>(1);
        legality.Configure().IsObtainable.ReturnsForAnyArgs(true);
        legality.Configure().IsObtainableWithHiddenAbility.ReturnsForAnyArgs(true);

        Sut.Legalities.Add(legality);
        cellMonitor.Should().Raise(nameof(SutCell.PropertyChanged));
    }
}