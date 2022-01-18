namespace CEo.Pokemon.HomeBalls.App.Core.Tests;

public class HomeBallsEntryTableTests
{
    public HomeBallsEntryTableTests()
    {
        SutCell = new HomeBallsEntryCell
        {
            BallId = 1,
            ObtainedStatus = HomeBallsEntryObtainedStatus.NotObtained,
            LegalityStatus = HomeBallsEntryLegalityStatus.ObtainableWithoutHiddenAbility
        };
        SutColumn = new HomeBallsEntryColumn(new List<IHomeBallsEntryCell> { SutCell }) { Id = new(1, 1) };
        Sut = new HomeBallsEntryTable(
            new List<IHomeBallsEntryColumn> { SutColumn },
            new Dictionary<HomeBallsPokemonFormKey, Int32> { [new(1, 1)] = 0 }.AsReadOnly());
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