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
        Logger = logger;
    }

    protected internal IReadOnlyList<IHomeBallsEntryCell> Cells { get; }

    protected internal ILogger? Logger { get; }

    public UInt16 SpeciesId { get; init; }

    public Byte FormId { get; init; }

    public Type ElementType => typeof(IHomeBallsEntryCell);

    public virtual Int32 Count => Cells.Count;

    public virtual IHomeBallsEntryCell this[UInt16 ballId] => throw new NotImplementedException();

    public virtual IEnumerator<IHomeBallsEntryCell> GetEnumerator() => Cells.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
