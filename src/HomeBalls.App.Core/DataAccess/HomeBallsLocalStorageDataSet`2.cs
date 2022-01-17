namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess;

public interface IHomeBallsLocalStorageDataSet<TKey, TRecord> :
    IHomeBallsLoadableDataSet<TKey, TRecord>,
    INotifyDataDownloading
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable { }

public class HomeBallsLocalStorageDataSet<TKey, TRecord> :
    HomeBallsLoadableDataSet<TKey, TRecord>,
    IHomeBallsLocalStorageDataSet<TKey, TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    String? _identifier;

    public HomeBallsLocalStorageDataSet(
        ILocalStorageService localStorage,
        IHomeBallsProtobufTypeMap typeMap,
        IHomeBallsLocalStorageDownloader downloader,
        ILogger? logger = default) :
        this(
            localStorage, typeMap, downloader,
            new HomeBallsDataSet<TKey, TRecord> { }, logger) { }

    public HomeBallsLocalStorageDataSet(
        ILocalStorageService localStorage,
        IHomeBallsProtobufTypeMap typeMap,
        IHomeBallsLocalStorageDownloader downloader,
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        ILogger? logger = default) :
        base(dataSet, (dataSet, cancellationToken) => Task.CompletedTask, logger)
    {
        (LocalStorage, TypeMap, Downloader) = (localStorage, typeMap, downloader);
        LoadTask = LoadAsync;
    }

    protected internal String Identifier => _identifier ??=
        DataSet.ElementType.GetFullNameNonNull();

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IHomeBallsProtobufTypeMap TypeMap { get; }

    protected internal IHomeBallsLocalStorageDownloader Downloader { get; }

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

    protected internal virtual async Task<HomeBallsLocalStorageDataSet<TKey, TRecord>> LoadAsync(
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        CancellationToken cancellationToken = default)
    {
        await EnsureDownloadedAsync(cancellationToken);

        var deserializationType = typeof(IEnumerable<>)
            .MakeGenericType(TypeMap.GetProtobufConcreteType(DataSet.ElementType));
        var dataString = await LocalStorage.GetItemAsync<String>(Identifier, cancellationToken);

        IEnumerable<TRecord> loaded;
        await using (var memory = new MemoryStream(Convert.FromBase64String(dataString)))
            loaded = (IEnumerable<TRecord>)ProtoBuf.Serializer
                .Deserialize(deserializationType, memory);

        dataSet.AddRange(loaded);
        return this;
    }

    protected internal virtual async Task<HomeBallsLocalStorageDataSet<TKey, TRecord>> EnsureDownloadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (!await LocalStorage.ContainKeyAsync(Identifier))
            await Downloader.DownloadAsync(
                Identifier,
                Identifier.AddFileExtension(_Values.DefaultProtobufExtension),
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