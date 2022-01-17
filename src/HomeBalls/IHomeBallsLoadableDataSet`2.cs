namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsLoadableDataSet<TKey, TRecord> :
    IHomeBallsReadOnlyDataSet<TKey, TRecord>,
    IAsyncLoadable<IHomeBallsLoadableDataSet<TKey, TRecord>>,
    INotifyDataLoading
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    Boolean IsLoaded { get; }

    IHomeBallsLoadableDataSet<TKey, TRecord> Clear();
}

public class HomeBallsLoadableDataSet<TKey, TRecord> :
    IHomeBallsLoadableDataSet<TKey, TRecord>,
    IAsyncLoadable<HomeBallsLoadableDataSet<TKey, TRecord>>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    public HomeBallsLoadableDataSet(
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        Func<IHomeBallsDataSet<TKey, TRecord>, CancellationToken, Task> loadTask,
        ILogger? logger = default)
    {
        DataSet = dataSet;
        LoadTask = loadTask;
        (EventRaiser, Logger) = (new EventRaiser().RaisedBy(this), logger);
    }

    protected internal virtual Func<IHomeBallsDataSet<TKey, TRecord>, CancellationToken, Task> LoadTask { get; set; }

    protected internal IHomeBallsDataSet<TKey, TRecord> DataSet { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public virtual TRecord this[TKey key] => DataSet[key];

    public virtual TRecord this[String identifier] => DataSet[identifier];

    public virtual Boolean IsLoaded { get; protected set; }

    public virtual IReadOnlyCollection<TKey> Keys => DataSet.Keys;

    public virtual IReadOnlyCollection<String> Identifiers => DataSet.Identifiers;

    public virtual IReadOnlyCollection<TRecord> Values => DataSet.Values;

    public virtual Type ElementType => DataSet.ElementType;

    public virtual Int32 Count => DataSet.Count;

    public event EventHandler<TimedActionStartingEventArgs>? DataLoading;

    public event EventHandler<TimedActionEndedEventArgs>? DataLoaded;

    public virtual HomeBallsLoadableDataSet<TKey, TRecord> Clear()
    {
        DataSet.Clear();
        IsLoaded = false;
        return this;
    }

    public virtual Boolean ContainsIdentifier(String identifier) =>
        DataSet.ContainsIdentifier(identifier);

    public virtual Boolean ContainsKey(TKey key) =>
        DataSet.ContainsKey(key);

    public virtual async ValueTask<HomeBallsLoadableDataSet<TKey, TRecord>> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (IsLoaded) return this;

        var start = RaiseDataLoading(DataLoading);
        await LoadTask.Invoke(DataSet, cancellationToken);

        RaiseDataLoaded(DataLoaded, start);
        IsLoaded = true;
        return this;
    }

    public virtual IEnumerator<TRecord> GetEnumerator() => DataSet.GetEnumerator();

    protected internal virtual TimedActionEndedEventArgs RaiseDataLoaded(
        EventHandler<TimedActionEndedEventArgs>? handler,
        TimedActionStartingEventArgs args) =>
        EventRaiser.Raise(handler, args.StartTime);

    protected internal virtual TimedActionStartingEventArgs RaiseDataLoading(
        EventHandler<TimedActionStartingEventArgs>? handler) =>
        EventRaiser.Raise(handler);

    public virtual Boolean TryGetValue(
        TKey key,
        [MaybeNullWhen(false)] out TRecord value) =>
        DataSet.TryGetValue(key, out value);

    public virtual Boolean TryGetValue(
        String identifier,
        [MaybeNullWhen(false)] out TRecord value) =>
        DataSet.TryGetValue(identifier, out value);

    IHomeBallsLoadableDataSet<TKey, TRecord> IHomeBallsLoadableDataSet<TKey, TRecord>.Clear() => Clear();

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLoadableDataSet<TKey, TRecord>> IAsyncLoadable<IHomeBallsLoadableDataSet<TKey, TRecord>>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}