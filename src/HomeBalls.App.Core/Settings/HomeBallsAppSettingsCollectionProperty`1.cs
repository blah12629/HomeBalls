namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppSettingsCollectionProperty<TValue> :
    IHomeBallsAppSettingsProperty,
    IHomeBallsObservableCollection<TValue>,
    IAsyncLoadable<IHomeBallsAppSettingsCollectionProperty<TValue>> { }

public class HomeBallsAppSettingsCollectionProperty<TValue> :
    HomeBallsAppSettingsPropertyEndpoint,
    IHomeBallsAppSettingsCollectionProperty<TValue>,
    IAsyncLoadable<HomeBallsAppSettingsCollectionProperty<TValue>>
{
    public HomeBallsAppSettingsCollectionProperty(
        IHomeBallsObservableCollection<TValue> collection,
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger)
    {
        Collection = collection;
        CollectionChanged += OnCollectionChanged;
    }

    public virtual Type ElementType => Collection.ElementType;

    public virtual Int32 Count => Collection.Count;

    public virtual Boolean IsReadOnly => Collection.IsReadOnly;

    protected internal IHomeBallsObservableCollection<TValue> Collection { get; }

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => Collection.CollectionChanged += value;
        remove => Collection.CollectionChanged -= value;
    }

    public virtual IHomeBallsObservableCollection<TValue> Add(TValue item) => Collection.Add(item);

    public virtual IHomeBallsObservableCollection<TValue> Clear() => Collection.Clear();

    public virtual Boolean Contains(TValue item) => Collection.Contains(item);

    public virtual IHomeBallsObservableCollection<TValue> CopyTo(TValue[] array, Int32 arrayIndex) => Collection.CopyTo(array, arrayIndex);

    new public virtual async ValueTask<HomeBallsAppSettingsCollectionProperty<TValue>> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    public virtual IEnumerator<TValue> GetEnumerator() => Collection.GetEnumerator();

    public virtual Boolean Remove(TValue item) => Collection.Remove(item);

    protected internal override async Task LoadAsync(CancellationToken cancellationToken = default)
    {
        var items = await LocalStorage.GetItemAsync<IEnumerable<TValue>>(Identifier, cancellationToken);
        Clear();
        foreach (var item in items) Add(item);
    }

    protected internal virtual async void OnCollectionChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        if (!IsLoading) await SaveAsync();
    }

    protected internal override Task SaveAsync(CancellationToken cancellationToken = default) => LocalStorage
        .SetItemAsync<IEnumerable<TValue>>(Identifier, this, cancellationToken)
        .AsTask();

    void ICollection<TValue>.Add(TValue item) => Add(item);

    void ICollection<TValue>.Clear() => Clear();

    void ICollection<TValue>.CopyTo(TValue[] array, Int32 arrayIndex) => CopyTo(array, arrayIndex);

    async ValueTask<IHomeBallsAppSettingsCollectionProperty<TValue>> IAsyncLoadable<IHomeBallsAppSettingsCollectionProperty<TValue>>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}