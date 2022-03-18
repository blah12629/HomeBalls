namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryBodyRow :
    IHomeBallsEntryRow<IHomeBallsEntryBodyCell>,
    IKeyed<HomeBallsPokemonFormKey> { }

public class HomeBallsEntryBodyRow :
    HomeBallsEntryRow<IHomeBallsEntryBodyCell>,
    IHomeBallsEntryBodyRow
{
    public HomeBallsEntryBodyRow(
        HomeBallsPokemonFormKey id,
        IReadOnlyList<IHomeBallsEntryBodyCell> cells,
        IReadOnlyDictionary<UInt16, Int32> indexMap,
        ILogger? logger = default) :
        base(cells, indexMap, logger) =>
        Id = id;

    public HomeBallsPokemonFormKey Id { get; }
}