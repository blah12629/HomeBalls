namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryColumn :
    IKeyed<HomeBallsPokemonFormKey>,
    IIdentifiable
{
    IReadOnlyCollection<IHomeBallsEntryCell> Cells { get; }

    IHomeBallsEntryCell GetCell(UInt16 ballId);
}

public class HomeBallsEntryColumn :
    IHomeBallsEntryColumn
{
    static IReadOnlyDictionary<UInt16, Int32> CreateCellsIndexMap(
        IReadOnlyList<IHomeBallsEntryCell> cells) =>
        cells.Select((cell, index) => (Cell: cell, Index: index))
            .ToDictionary(pair => pair.Cell.Id.BallId, pair => pair.Index)
            .AsReadOnly();

    public HomeBallsEntryColumn(
        HomeBallsPokemonFormKey id,
        String identifier,
        IReadOnlyList<IHomeBallsEntryCell> cells,
        ILogger? logger = default) :
        this(id, identifier, cells, CreateCellsIndexMap(cells), logger) { }

    public HomeBallsEntryColumn(
        HomeBallsPokemonFormKey id,
        String identifier,
        IReadOnlyList<IHomeBallsEntryCell> cells,
        IReadOnlyDictionary<UInt16, Int32> cellsIndexMap,
        ILogger? logger = default)
    {
        (Id, Identifier) = (id, identifier);
        (Cells, CellsIndexable, CellsIndexMap) = (cells, cells, cellsIndexMap);
        Logger = logger;
    }

    public HomeBallsPokemonFormKey Id { get; }

    public String Identifier { get; }

    public IReadOnlyCollection<IHomeBallsEntryCell> Cells { get; }

    protected internal ILogger? Logger { get; }

    protected internal IReadOnlyList<IHomeBallsEntryCell> CellsIndexable { get; }

    protected internal IReadOnlyDictionary<UInt16, Int32> CellsIndexMap { get; }

    public virtual IHomeBallsEntryCell GetCell(UInt16 ballId) =>
        CellsIndexable[CellsIndexMap[ballId]];
}