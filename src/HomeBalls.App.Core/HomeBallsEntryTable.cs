namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryTable :
    IHomeBallsEntryCollection
{
    IReadOnlyList<IHomeBallsEntryColumn> Columns { get; }
}

public class HomeBallsEntryTable :
    IHomeBallsEntryTable
{
    public HomeBallsEntryTable(
        IReadOnlyList<IHomeBallsEntryColumn> loadedColumns,
        IReadOnlyDictionary<(UInt16 SpeciesId, Byte FormId), Int32> columnsIndexMap,
        IHomeBallsEntryCollection entries,
        ILogger? logger)
    {
        (Columns, ColumnsIndexMap) = (loadedColumns, columnsIndexMap);
        Entries = entries;
        EventRaiser = new EventRaiser();
        Logger = logger;

        Entries.CollectionChanged += OnEntriesChanged;
    }

    public IReadOnlyList<IHomeBallsEntryColumn> Columns { get; }

    protected IReadOnlyDictionary<(UInt16 SpeciesId, Byte FormId), Int32> ColumnsIndexMap { get; }

    public virtual Type ElementType => Entries.ElementType;

    public virtual Int32 Count => Entries.Count;

    public virtual Boolean IsReadOnly => Entries.IsReadOnly;

    protected internal IHomeBallsEntryCollection Entries { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public virtual void Add(IHomeBallsEntry item) => Entries.Add(item);

    public virtual void Clear() => Entries.Clear();

    public virtual Boolean Contains(IHomeBallsEntry item) => Entries.Contains(item);

    public virtual void CopyTo(IHomeBallsEntry[] array, Int32 arrayIndex) =>
        Entries.CopyTo(array, arrayIndex);

    public virtual IEnumerator<IHomeBallsEntry> GetEnumerator() =>
        Entries.GetEnumerator();

    public virtual bool Remove(IHomeBallsEntry item) => Entries.Remove(item);

    protected virtual void OnEntriesChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Move) return;

        InvokeOnEntries(e.OldItems, (entry, cell) =>
            cell.ObtainedStatus = HomeBallsEntryObtainedStatus.NotObtained);

        InvokeOnEntries(e.NewItems, (entry, cell) =>
            cell.ObtainedStatus = entry.HasHiddenAbility ?
                HomeBallsEntryObtainedStatus.ObtainedWithHiddenAbility :
                HomeBallsEntryObtainedStatus.ObtainedWithoutHiddenAbility);
    }

    protected internal virtual void InvokeOnEntries(
        IList? items,
        Action<IHomeBallsEntry, IHomeBallsEntryCell> cellAction)
    {
        if (items != default) foreach (var entry in items.Cast<IHomeBallsEntry>())
        {
            var index = ColumnsIndexMap[(entry.SpeciesId, entry.FormId)];
            cellAction(entry, Columns[index][entry.BallId]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}