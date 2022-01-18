namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess;

public interface IHomeBallsLocalStorageEntryCollection :
    IHomeBallsReadOnlyCollection<IHomeBallsEntry>,
    IAsyncLoadable<IHomeBallsLocalStorageEntryCollection>,
    INotifyDataDownloading,
    INotifyDataLoading
{
    new ValueTask<IHomeBallsLocalStorageEntryCollection> EnsureLoadedAsync(
        CancellationToken cancellationToken = default);
}

public class HomeBallsLocalStorageEntryCollection :
    IHomeBallsLocalStorageEntryCollection,
    IAsyncLoadable<HomeBallsLocalStorageEntryCollection>
{
    public HomeBallsLocalStorageEntryCollection(
        ILocalStorageService localStorage,
        IHomeBallsLocalStorageDownloader downloader,
        ILogger? logger = default) :
        this(localStorage, new List<IHomeBallsEntry>(), downloader, logger) { }

    public HomeBallsLocalStorageEntryCollection(
        ILocalStorageService localStorage,
        ICollection<IHomeBallsEntry> entries,
        IHomeBallsLocalStorageDownloader downloader,
        ILogger? logger = default)
    {
        ElementType = typeof(IHomeBallsEntry);

        (LocalStorage, Downloader) = (localStorage, downloader);
        (Entries, EntriesMutable) = (entries.AsReadOnly(), entries);
        (EventRaiser, Logger) = (new EventRaiser().RaisedBy(this), logger);
    }

    public virtual Type ElementType { get; }

    public virtual Int32 Count => Entries.Count;

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IHomeBallsLocalStorageDownloader Downloader { get; }

    protected internal IReadOnlyCollection<IHomeBallsEntry> Entries { get; }

    protected internal ICollection<IHomeBallsEntry> EntriesMutable { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal Boolean IsLoaded { get; set; }

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

    public event EventHandler<TimedActionStartingEventArgs>? DataLoading;

    public event EventHandler<TimedActionEndedEventArgs>? DataLoaded;

    public virtual async ValueTask<HomeBallsLocalStorageEntryCollection> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (IsLoaded) return this;

        var identifier = typeof(IHomeBallsEntry).GetFullNameNonNull();
        await EnsureDownloadedAsync(identifier, cancellationToken);
        var start = EventRaiser.Raise(DataLoading);

        ICollection<ProtobufEntry> entries;
        var data = await LocalStorage.GetItemAsync<String>(identifier, cancellationToken);
        await using (var memory = new MemoryStream(Convert.FromBase64String(data)))
            entries = ProtoBuf.Serializer.Deserialize<ICollection<ProtobufEntry>>(memory);

        EntriesMutable.Clear();
        foreach (var entry in entries.Select(convert)) EntriesMutable.Add(entry);

        IsLoaded = true;
        EventRaiser.Raise(DataLoaded, start.StartTime);
        return this;

        static IHomeBallsEntry convert(ProtobufEntry entry) => entry.ToHomeBallsEntry();
    }

    protected internal virtual async Task<HomeBallsLocalStorageEntryCollection> EnsureDownloadedAsync(
        String identifier,
        CancellationToken cancellationToken)
    {
        if (await LocalStorage.ContainKeyAsync(identifier, cancellationToken)) return this;

        await Downloader.DownloadAsync(
            identifier,
            identifier.AddFileExtension(_Values.DefaultProtobufExtension),
            cancellationToken);
        return this;
    }

    public virtual IEnumerator<IHomeBallsEntry> GetEnumerator() => Entries.GetEnumerator();

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLocalStorageEntryCollection> IHomeBallsLocalStorageEntryCollection
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLocalStorageEntryCollection> IAsyncLoadable<IHomeBallsLocalStorageEntryCollection>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // public virtual Type ElementType => typeof(IHomeBallsEntry);

    // public virtual Int32 Count => Collection.Count;

    // protected internal ILocalStorageService LocalStorage { get; }

    // protected internal ICollection<IHomeBallsEntry> Collection { get; set; }

    // protected internal IHomeBallsLocalStorageDownloader Downloader { get; }

    // protected internal IEventRaiser EventRaiser { get; }

    // protected internal ILogger? Logger { get; }

    // public event EventHandler<TimedActionStartingEventArgs>? DataDownloading
    // {
    //     add => Downloader.DataDownloading += value;
    //     remove => Downloader.DataDownloading -= value;
    // }

    // public event EventHandler<TimedActionEndedEventArgs>? DataDownloaded
    // {
    //     add => Downloader.DataDownloaded += value;
    //     remove => Downloader.DataDownloaded -= value;
    // }

    // public event EventHandler<TimedActionStartingEventArgs>? DataLoading;

    // public event EventHandler<TimedActionEndedEventArgs>? DataLoaded;

    // public virtual async ValueTask<HomeBallsLocalStorageEntryCollection> EnsureLoadedAsync(
    //     CancellationToken cancellationToken = default)
    // {
    //     var identifier = typeof(IHomeBallsEntry).GetFullNameNonNull();
    //     await EnsureDownloadedAsync(identifier, cancellationToken);
    //     var start = EventRaiser.Raise(DataLoading);

    //     ICollection<ProtobufEntry> entries;
    //     var data = await LocalStorage.GetItemAsync<String>(identifier, cancellationToken);
    //     await using (var memory = new MemoryStream(Convert.FromBase64String(data)))
    //         entries = ProtoBuf.Serializer.Deserialize<ICollection<ProtobufEntry>>(memory);

    //     Collection = entries.Select(convert).ToList();
    //     EventRaiser.Raise(DataLoaded, start.StartTime);
    //     return this;

    //     static IHomeBallsEntry convert(ProtobufEntry entry) => entry.ToHomeBallsEntry();
    // }

    // protected internal virtual async ValueTask<HomeBallsLocalStorageEntryCollection> EnsureDownloadedAsync(
    //     String identifier,
    //     CancellationToken cancellationToken)
    // {
    //     if (await LocalStorage.ContainKeyAsync(identifier, cancellationToken)) return this;

    //     await Downloader.DownloadAsync(
    //         identifier,
    //         identifier.AddFileExtension(_Values.DefaultProtobufExtension),
    //         cancellationToken);
    //     return this;
    // }

    // public virtual IEnumerator<IHomeBallsEntry> GetEnumerator() =>
    //     Collection.GetEnumerator();

    // async ValueTask<IHomeBallsLocalStorageEntryCollection> IHomeBallsLocalStorageEntryCollection
    //     .EnsureLoadedAsync(CancellationToken cancellationToken) =>
    //     await EnsureLoadedAsync(cancellationToken);

    // async ValueTask<IHomeBallsLocalStorageEntryCollection> IAsyncLoadable<IHomeBallsLocalStorageEntryCollection>
    //     .EnsureLoadedAsync(CancellationToken cancellationToken) =>
    //     await EnsureLoadedAsync(cancellationToken);

    // async ValueTask IAsyncLoadable
    //     .EnsureLoadedAsync(CancellationToken cancellationToken) =>
    //     await EnsureLoadedAsync(cancellationToken);

    // IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}