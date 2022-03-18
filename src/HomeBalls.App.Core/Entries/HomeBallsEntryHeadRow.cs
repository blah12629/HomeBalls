namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryHeadRow :
    IHomeBallsEntryRow<IHomeBallsEntryHeadCell> { }

public class HomeBallsEntryHeadRow :
    HomeBallsEntryRow<IHomeBallsEntryHeadCell>,
    IHomeBallsEntryHeadRow
{
    public HomeBallsEntryHeadRow(
        IReadOnlyList<IHomeBallsEntryHeadCell> cells,
        IReadOnlyDictionary<UInt16, Int32> indexMap,
        ILogger? logger = default) :
        base(cells, indexMap, logger) { }
}