namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryTable
{
    IAddable<IHomeBallsEntryLegality> Legalities { get;}

    IAddable<IHomeBallsEntry> Entries { get; }

    IMutableNotifyingProperty<IHomeBallsEntryHeadRow?> Header { get; }

    IHomeBallsEntryBodyRowCollection Rows { get; }
}

public class HomeBallsEntryTable : IHomeBallsEntryTable
{
    public HomeBallsEntryTable(
        ILogger? logger = default)
    {
        Logger = logger;
        EventRaiser = new EventRaiser(Logger).RaisedBy(this);

        Legalities = new Addable<IHomeBallsEntryLegality>(AddLegality, Logger);
        Entries = new Addable<IHomeBallsEntry>(AddEntry, Logger);
        Header = new MutableNotifyingProperty<IHomeBallsEntryHeadRow?>(default, nameof(Header), EventRaiser, Logger);
        Rows = new HomeBallsEntryBodyRowCollection(EventRaiser, Logger);
    }

    public IAddable<IHomeBallsEntryLegality> Legalities { get; }

    public IAddable<IHomeBallsEntry> Entries { get; }

    public IMutableNotifyingProperty<IHomeBallsEntryHeadRow?> Header { get; }

    public IHomeBallsEntryBodyRowCollection Rows { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal virtual void AddLegality(IHomeBallsEntryLegality legality)
    {
        var cell = Rows[legality.Id];
        cell.IsLegal.Value = legality.IsObtainable;
        cell.IsLegalWithHiddenAbility.Value = legality.IsObtainableWithHiddenAbility;
    }

    protected internal virtual void AddEntry(IHomeBallsEntry entry)
    {
        var cell = Rows[entry.Id];
        cell.IsObtained.Value = true;
        setHasHiddenAbility();
        entry.HasHiddenAbility.ValueChanged += (sender, e) => setHasHiddenAbility();

        void setHasHiddenAbility() => cell.IsObtainedWithHiddenAbility.Value = entry.HasHiddenAbility.Value;
    }
}