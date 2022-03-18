namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryHeadCell : IHomeBallsEntryCell { }

public class HomeBallsEntryHeadCell :
    HomeBallsEntryCell,
    IHomeBallsEntryHeadCell
{
    public HomeBallsEntryHeadCell(
        UInt16 id,
        ILogger? logger = default) :
        base(id, logger) { }
}