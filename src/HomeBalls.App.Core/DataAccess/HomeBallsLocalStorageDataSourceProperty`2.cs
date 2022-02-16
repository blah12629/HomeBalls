namespace CEo.Pokemon.HomeBalls.App.DataAccess;

public interface IHomeBallsLocalStorageDataSourceProperty<TKey, out TRecord> :
    IHomeBallsDataSourceLoadableProperty<TKey, TRecord>,
    IIdentifiable,
    INotifyDataLoading
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable { }

public class HomeBallsLocalStorageDataSourceProperty<TKey, TRecord> :
    HomeBallsLoadableDataSet<TKey, TRecord>,
    IHomeBallsLocalStorageDataSourceProperty<TKey, TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    public HomeBallsLocalStorageDataSourceProperty(
        String propertyName,
        ILocalStorageService localStorage,
        IHomeBallsLocalStorageDownloader downloader,
        IProtoBufSerializer serializer,
        ILogger? logger = default,
        IComparer<TKey>? keyComparer = default) :
        this(
            propertyName, localStorage, downloader, serializer,
            new HomeBallsDataSet<TKey, TRecord> { }, logger, keyComparer) { }

    public HomeBallsLocalStorageDataSourceProperty(
        String propertyName,
        ILocalStorageService localStorage,
        IHomeBallsLocalStorageDownloader downloader,
        IProtoBufSerializer serializer,
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        ILogger? logger = default,
        IComparer<TKey>? keyComparer = default) :
        base(dataSet, (dataSet, cancellationToken) => Task.CompletedTask, logger)
    {
        PropertyName = propertyName;
        (LocalStorage, Downloader, Serializer) = (localStorage, downloader, serializer);
        Comparer = keyComparer;
        LoadTask = LoadAsync;
    }

    public String PropertyName { get; }

    public String Identifier => PropertyName;

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IHomeBallsLocalStorageDownloader Downloader { get; }

    protected internal IProtoBufSerializer Serializer { get; }

    protected internal IComparer<TKey>? Comparer { get; }

    public event EventHandler<TimedActionStartingEventArgs>? DataDownloading
    {
        add => Downloader.DataDownloading += value;
        remove => Downloader.DataDownloading -= value;
    }

    public event EventHandler<TimedActionEndedEventArgs>? DataDownloaded
    {
        add => Downloader.DataDownloaded += value;
        remove => Downloader.DataDownloaded -= value;
    }

    protected internal virtual async Task<HomeBallsLocalStorageDataSourceProperty<TKey, TRecord>> LoadAsync(
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        CancellationToken cancellationToken = default)
    {
        await EnsureDownloadedAsync(cancellationToken);
        var data = await LocalStorage.GetItemAsync<Byte[]>(Identifier, cancellationToken);

        var memory = data.AsMemory();
        var loaded = (IEnumerable<TRecord>)Serializer.ForStaticTypes.Deserialize(
            typeof(ICollection<>).MakeGenericType(typeof(TRecord)),
            memory);
        if (Comparer != default) loaded = loaded.OrderBy(record => record.Id, Comparer);

        dataSet.AddRange(loaded);
        return this;
    }

    protected internal virtual async Task<HomeBallsLocalStorageDataSourceProperty<TKey, TRecord>> EnsureDownloadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (await LocalStorage.ContainKeyAsync(Identifier)) return this;

        await Downloader.DownloadAsync(
            this,
            Identifier.AddFileExtension(DefaultProtobufExtension),
            cancellationToken);
        return this;
    }

    protected override TimedActionEndedEventArgs RaiseDataLoaded(
        EventHandler<TimedActionEndedEventArgs>? handler,
        TimedActionStartingEventArgs args) =>
        EventRaiser.Raise(handler, args.StartTime, Identifier);

    protected override TimedActionStartingEventArgs RaiseDataLoading(
        EventHandler<TimedActionStartingEventArgs>? handler) =>
        EventRaiser.Raise(handler, Identifier);
}