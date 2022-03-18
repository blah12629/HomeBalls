namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryBodyRowCollection :
    IHomeBallsEnumerable<IHomeBallsEntryBodyRow>,
    IAddable<IHomeBallsEntryBodyRow>,
    INotifyCollectionChanged
{
    IHomeBallsEntryBodyRow this[Int32 index] { get; }

    IHomeBallsEntryBodyRow this[HomeBallsPokemonFormKey formKey] { get; }

    IHomeBallsEntryBodyCell this[HomeBallsEntryKey entryKey] { get; }

    Int32 IndexOf(HomeBallsPokemonFormKey formKey);
}

public class HomeBallsEntryBodyRowCollection :
    IHomeBallsEntryBodyRowCollection
{
    public HomeBallsEntryBodyRowCollection(
        IEventRaiser? eventRaiser = default,
        ILogger? logger = default)
    {
        Logger = logger;
        EventRaiser = eventRaiser ?? new EventRaiser(Logger).RaisedBy(this);

        RowList = new HomeBallsObservableList<IHomeBallsEntryBodyRow>(logger: Logger);
        RowIndexMap = new Dictionary<HomeBallsPokemonFormKey, Int32> { };
    }

    public IHomeBallsEntryBodyRow this[Int32 index] => RowList[index];

    public IHomeBallsEntryBodyRow this[HomeBallsPokemonFormKey formKey] => this[IndexOf(formKey)];

    public IHomeBallsEntryBodyCell this[HomeBallsEntryKey entryKey] => this[(HomeBallsPokemonFormKey)entryKey][entryKey.BallId];

    public virtual Type ElementType => typeof(IHomeBallsEntryBodyRow);

    public virtual UInt32 Count => Convert.ToUInt32(RowList.Count);

    protected internal virtual IHomeBallsObservableList<IHomeBallsEntryBodyRow> RowList { get; }

    protected internal virtual IDictionary<HomeBallsPokemonFormKey, Int32> RowIndexMap { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => RowList.CollectionChanged += value;
        remove => RowList.CollectionChanged -= value;
    }

    public virtual HomeBallsEntryBodyRowCollection Add(IHomeBallsEntryBodyRow item)
    {
        var index = RowList.Count;
        RowList.Add(item);
        RowIndexMap.Add(item.Id, index);
        return this;
    }

    public virtual IEnumerator<IHomeBallsEntryBodyRow> GetEnumerator() => RowList.GetEnumerator();

    public virtual Int32 IndexOf(HomeBallsPokemonFormKey formKey) => RowIndexMap[formKey];

    IAddable<IHomeBallsEntryBodyRow> IAddable<IHomeBallsEntryBodyRow>.Add(IHomeBallsEntryBodyRow item) => Add(item);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}