namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryColumn :
    IHomeBallsReadOnlyCollection<IHomeBallsEntryCell>
{
    UInt16 SpeciesId { get; }

    Byte FormId { get; }

    IHomeBallsEntryCell this[UInt16 ballId] { get; }
}

public class HomeBallsEntryColumn :
    IHomeBallsEntryColumn
{
    public HomeBallsEntryColumn(
        IReadOnlyList<IHomeBallsEntryCell> cells,
        ILogger? logger = default)
    {
        Cells = cells;
        IndexMap = cells
            .Select((cell, index) => (Id: cell.BallId, Index: index))
            .ToDictionary(pair => pair.Id, pair => pair.Index);
        Logger = logger;
    }

    public HomeBallsEntryColumn(
        IReadOnlyList<IHomeBallsEntryCell> cells,
        IReadOnlyDictionary<UInt16, Int32> indexMap,
        ILogger? logger = default)
    {
        (Cells, IndexMap) = (cells, indexMap);
        Logger = logger;
    }

    protected internal IReadOnlyList<IHomeBallsEntryCell> Cells { get; }

    protected internal IReadOnlyDictionary<UInt16, Int32> IndexMap { get; }

    protected internal ILogger? Logger { get; }

    public UInt16 SpeciesId { get; init; }

    public Byte FormId { get; init; }

    public Type ElementType => typeof(IHomeBallsEntryCell);

    public virtual Int32 Count => Cells.Count;

    public virtual IHomeBallsEntryCell this[UInt16 ballId] => Cells[IndexMap[ballId]];

    public virtual IEnumerator<IHomeBallsEntryCell> GetEnumerator() => Cells.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
