namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsEntryCollection :
    IHomeBallsEnumerable<IHomeBallsEntry>,
    ICollection<IHomeBallsEntry>,
    INotifyCollectionChanged { }

public class HomeBallsEntryCollection :
    IHomeBallsEntryCollection
{
    public HomeBallsEntryCollection()
    {
        Entries = new();
        Entries.CollectionChanged += OnEntriesChanged;

        EventRaiser = new EventRaiser();
    }

    protected internal ObservableCollection<IHomeBallsEntry> Entries { get; }

    protected internal IEventRaiser EventRaiser { get; }

    public virtual Type ElementType => typeof(IHomeBallsEntry);

    public virtual Int32 Count => Entries.Count;

    Boolean ICollection<IHomeBallsEntry>.IsReadOnly => false;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public virtual void Add(IHomeBallsEntry item)
    {
        var index = IndexOf(item);

        if (index < 0) Entries.Add(item);
        else UpdateEntry(index, item);
    }

    protected internal virtual void UpdateEntry(Int32 index, IHomeBallsEntry item)
    {
        var entry = Entries[index];
        entry.HasHiddenAbility = item.HasHiddenAbility;
        foreach (var id in item.AvailableEggMoveIds) entry.AvailableEggMoveIds.Add(id);
    }

    public virtual void Clear() => Entries.Clear();

    public virtual Boolean Contains(IHomeBallsEntry item) => IndexOf(item) > -1;

    protected internal virtual Int32 IndexOf(IHomeBallsEntry item)
    {
        for (var i = 0; i < Count; i ++) if (Entries[i].Equals(item)) return i;
        return -1;
    }

    public virtual void CopyTo(IHomeBallsEntry[] array, Int32 arrayIndex) =>
        Entries.CopyTo(array, arrayIndex);

    public virtual IEnumerator<IHomeBallsEntry> GetEnumerator() => Entries.GetEnumerator();

    public virtual Boolean Remove(IHomeBallsEntry item)
    {
        var index = IndexOf(item);
        if (index < 0) return false;

        Entries.RemoveAt(index);
        return true;
    }

    protected internal virtual void OnEntriesChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e) =>
        CollectionChanged?.Invoke(sender, e);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}