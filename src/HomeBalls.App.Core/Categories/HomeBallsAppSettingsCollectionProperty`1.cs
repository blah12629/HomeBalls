namespace CEo.Pokemon.HomeBalls.App.Categories;

public interface IHomeBallsAppSettingsCollectionProperty<T> :
    IHomeBallsAppSettingsProperty,
    IHomeBallsObservableList<T>,
    IIdentifiable,
    IAsyncLoadable<IHomeBallsAppSettingsCollectionProperty<T>> { }

public class HomeBallsAppSettingsCollectionProperty<T> :
    IHomeBallsAppSettingsCollectionProperty<T>,
    IAsyncLoadable<HomeBallsAppSettingsCollectionProperty<T>>
{
    public HomeBallsAppSettingsCollectionProperty(
        IHomeBallsObservableList<T> items,
        IList<T> itemsSilent,
        String propertyName,
        ILocalStorageService localStorage,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        this (items, itemsSilent, propertyName, propertyName, localStorage, eventRaiser, logger) { }

    public HomeBallsAppSettingsCollectionProperty(
        IHomeBallsObservableList<T> items,
        IList<T> itemsSilent,
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IEventRaiser eventRaiser,
        ILogger? logger = default)
    {
        (Items, ItemsSilent) = (items, itemsSilent);
        (PropertyName, Identifier, ElementType) = (propertyName, identifier, typeof(T));
        (LocalStorage, EventRaiser, Logger) = (localStorage, eventRaiser, logger);
        CollectionChanged += OnCollectionChanged;
    }

    public virtual T this[Int32 index] { get => Items[index]; set => Items[index] = value; }

    public Type ElementType { get; }

    public Int32 Count => Items.Count;

    public Boolean IsReadOnly => Items.IsReadOnly;

    public String Identifier { get; }

    public String PropertyName { get; }

    protected internal IHomeBallsObservableList<T> Items { get; }

    protected internal IList<T> ItemsSilent { get; }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public virtual IHomeBallsObservableList<T> Add(T item) => Items.Add(item);

    public virtual IHomeBallsObservableList<T> Clear() => Items.Clear();

    public virtual Boolean Contains(T item) => Items.Contains(item);

    public virtual IHomeBallsObservableList<T> CopyTo(T[] array, Int32 arrayIndex) =>
        Items.CopyTo(array, arrayIndex);

    public virtual async ValueTask<HomeBallsAppSettingsCollectionProperty<T>> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (!await LocalStorage.ContainKeyAsync(Identifier, cancellationToken))
            await SaveAsync(cancellationToken);

        Items.Clear();
        Items.AddRange(await LocalStorage.GetItemAsync<IEnumerable<T>>(Identifier, cancellationToken));
        return this;
    }

    public virtual IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

    public virtual Int32 IndexOf(T item) => Items.IndexOf(item);

    public virtual IHomeBallsObservableList<T> Insert(Int32 index, T item) =>
        Items.Insert(index, item);

    public virtual Boolean Remove(T item) => Items.Remove(item);

    public virtual IHomeBallsObservableList<T> RemoveAt(Int32 index) => Items.RemoveAt(index);

    public virtual Task SaveAsync(CancellationToken cancellationToken = default) =>
        LocalStorage.SetItemAsync(Identifier, ItemsSilent, cancellationToken).AsTask();

    protected internal virtual void OnItemsChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e) =>
        CollectionChanged?.Invoke(this, e);

    protected internal virtual void OnCollectionChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e) =>
        SaveAsync().Start();

    IHomeBallsObservableCollection<T> IHomeBallsObservableCollection<T>.Add(T item) => Add(item);

    IHomeBallsObservableCollection<T> IHomeBallsObservableCollection<T>.Clear() => Clear();

    IHomeBallsObservableCollection<T> IHomeBallsObservableCollection<T>.CopyTo(T[] array, Int32 arrayIndex) => CopyTo(array, arrayIndex);

    void IList<T>.Insert(Int32 index, T item) => Insert(index, item);

    void IList<T>.RemoveAt(Int32 index) => RemoveAt(index);

    void ICollection<T>.Add(T item) => Add(item);

    void ICollection<T>.Clear() => Clear();

    void ICollection<T>.CopyTo(T[] array, Int32 arrayIndex) => CopyTo(array, arrayIndex);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    async ValueTask<IHomeBallsAppSettingsCollectionProperty<T>> IAsyncLoadable<IHomeBallsAppSettingsCollectionProperty<T>>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}