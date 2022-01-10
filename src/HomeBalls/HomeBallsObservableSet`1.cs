namespace CEo.Pokemon.HomeBalls;

public class HomeBallsObservableSet<T> :
    ICollection<T>,
    INotifyCollectionChanged
{
    public HomeBallsObservableSet()
    {
        EventRaiser = new EventRaiser().RaisedBy(this);
        Items = new ObservableCollection<T> { };

        Items.CollectionChanged += (s, e) => EventRaiser.Raise(CollectionChanged, e);
    }

    protected internal ObservableCollection<T> Items { get; }

    protected internal IEventRaiser EventRaiser { get; }

    public virtual Int32 Count => Items.Count;

    Boolean ICollection<T>.IsReadOnly => false;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public virtual void Add(T item)
    {
        if (Contains(item)) return;
        Items.Add(item);
    }

    public virtual void Clear() => Items.Clear();

    public virtual Boolean Contains(T item) => Items.Contains(item);

    public virtual void CopyTo(T[] array, Int32 arrayIndex) =>
        Items.CopyTo(array, arrayIndex);

    public virtual IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

    public virtual Boolean Remove(T item) => Items.Remove(item);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}