namespace CEo.Pokemon.HomeBalls.Collections;

public interface IHomeBallsObservableCollection<T> :
    IHomeBallsEnumerable<T>,
    ICollection<T>,
    INotifyCollectionChanged
{
    new IHomeBallsObservableCollection<T> Add(T item);

    new IHomeBallsObservableCollection<T> Clear();

    new IHomeBallsObservableCollection<T> CopyTo(T[] array, Int32 arrayIndex);
}

public interface IHomeBallsObservableList<T> :
    IHomeBallsObservableCollection<T>,
    IList<T>
{
    new IHomeBallsObservableList<T> Add(T item);

    new IHomeBallsObservableList<T> Clear();

    new IHomeBallsObservableList<T> CopyTo(T[] array, Int32 arrayIndex);

    new IHomeBallsObservableList<T> Insert(Int32 index, T item);

    new IHomeBallsObservableList<T> RemoveAt(Int32 index);
}

public class HomeBallsObservableList<T> :
    IHomeBallsObservableList<T>
{
    public HomeBallsObservableList(
        IEqualityComparer<T>? comparer = default,
        ILogger? logger = default) :
        this(new(), comparer, logger) { }

    public HomeBallsObservableList(
        List<T> items,
        IEqualityComparer<T>? comparer = default,
        ILogger? logger = default)
    {
        ElementType = typeof(T);
        (Items, ItemsSilent) = (new(items), items);
        (Logger, Comparer) = (logger, comparer);
    }

    public virtual T this[Int32 index]
    {
        get => Items[index];
        set => Items[index] = value;
    }

    public virtual Type ElementType { get; }

    public virtual Int32 Count => Items.Count;

    public virtual Boolean IsReadOnly => false;

    protected internal ObservableCollection<T> Items { get; }

    protected internal IList<T> ItemsSilent { get; }

    protected internal IEqualityComparer<T>? Comparer { get; }

    protected internal ILogger? Logger { get; }

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => Items.CollectionChanged += value;
        remove => Items.CollectionChanged -= value;
    }

    public virtual HomeBallsObservableList<T> Add(T item)
    {
        Items.Add(item);
        return this;
    }

    public virtual HomeBallsObservableList<T> Clear()
    {
        Items.Clear();
        return this;
    }

    public virtual Boolean Contains(T item) =>
        Comparer == default ? Items.Contains(item) : IndexOf(item) > -1;

    public virtual HomeBallsObservableList<T> CopyTo(T[] array, Int32 arrayIndex)
    {
        Items.CopyTo(array, arrayIndex);
        return this;
    }

    public virtual IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

    public virtual Int32 IndexOf(T item)
    {
        if (Comparer == default) return Items.IndexOf(item);

        for (var i = 0; i < Count; i ++)
            if (Comparer.Equals(item, Items[i]))
                return i;

        return -1;
    }

    public virtual HomeBallsObservableList<T> Insert(Int32 index, T item)
    {
        Items.Insert(index, item);
        return this;
    }

    public virtual Boolean Remove(T item)
    {
        if (Comparer == default) return Items.Remove(item);

        var index = IndexOf(item);
        if (index < 0) return false;
        RemoveAt(index);
        return true;
    }

    public virtual HomeBallsObservableList<T> RemoveAt(Int32 index)
    {
        Items.RemoveAt(index);
        return this;
    }

    IHomeBallsObservableList<T> IHomeBallsObservableList<T>.Add(T item) => Add(item);

    IHomeBallsObservableCollection<T> IHomeBallsObservableCollection<T>.Add(T item) => Add(item);

    void ICollection<T>.Add(T item) => Add(item);

    IHomeBallsObservableList<T> IHomeBallsObservableList<T>.Clear() => Clear();

    IHomeBallsObservableCollection<T> IHomeBallsObservableCollection<T>.Clear() => Clear();

    void ICollection<T>.Clear() => Clear();

    IHomeBallsObservableList<T> IHomeBallsObservableList<T>.CopyTo(T[] array, Int32 arrayIndex) => CopyTo(array, arrayIndex);

    IHomeBallsObservableCollection<T> IHomeBallsObservableCollection<T>.CopyTo(T[] array, Int32 arrayIndex) => CopyTo(array, arrayIndex);

    void ICollection<T>.CopyTo(T[] array, Int32 arrayIndex) => CopyTo(array, arrayIndex);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    IHomeBallsObservableList<T> IHomeBallsObservableList<T>.Insert(Int32 index, T item) => Insert(index, item);

    void IList<T>.Insert(Int32 index, T item) => Insert(index, item);

    IHomeBallsObservableList<T> IHomeBallsObservableList<T>.RemoveAt(Int32 index) => RemoveAt(index);

    void IList<T>.RemoveAt(Int32 index) => RemoveAt(index);
}