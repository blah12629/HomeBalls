namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryRow<TCell> :
    IReadOnlyList<TCell>
    where TCell : notnull, IHomeBallsEntryCell
{
    TCell this[UInt16 ballId] { get; }
}

public abstract class HomeBallsEntryRow<TCell> :
    IHomeBallsEntryRow<TCell>
    where TCell : notnull, IHomeBallsEntryCell
{
    public HomeBallsEntryRow(
        IReadOnlyList<TCell> cells,
        IReadOnlyDictionary<UInt16, Int32> indexMap,
        ILogger? logger = default) =>
        (Cells, IndexMap, Logger) = (cells, indexMap, logger);

    public virtual TCell this[Int32 index] => Cells[index];

    public virtual TCell this[UInt16 ballId] => this[IndexMap[ballId]];

    public virtual Int32 Count => Cells.Count;

    protected internal IReadOnlyList<TCell> Cells { get; }

    protected internal IReadOnlyDictionary<UInt16, Int32> IndexMap { get; }

    protected internal ILogger? Logger { get; }

    public virtual IEnumerator<TCell> GetEnumerator()  => Cells.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
