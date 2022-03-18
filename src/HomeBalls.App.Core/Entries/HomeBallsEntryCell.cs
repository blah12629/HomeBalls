namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryCell : IKeyed<UInt16> { }

public abstract class HomeBallsEntryCell : IHomeBallsEntryCell
{
    protected HomeBallsEntryCell(
        UInt16 id,
        ILogger? logger = default) =>
        (Id, Logger) = (id, logger);

    public UInt16 Id { get; }

    protected internal ILogger? Logger { get; }
}