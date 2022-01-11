namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess;

public interface IHomeBallsLocalStorageDataSource :
    IHomeBallsLoadableDataSource,
    IAsyncLoadable<IHomeBallsLocalStorageDataSource>,
    INotifyDataDownloading,
    INotifyDataLoading
{
    new ValueTask<IHomeBallsLocalStorageDataSource> EnsureLoadedAsync(
        CancellationToken cancellationToken = default);
}

public class HomeBallsLocalStorageDataSource :
    IHomeBallsLocalStorageDataSource
{
    public HomeBallsLocalStorageDataSource(
        ILocalStorageService localStorage,
        IHomeBallsLocalStorageDataDownloader downloader,
        IHomeBallsProtobufTypeMap typeMap,
        ILogger? logger) :
        this(localStorage, new HomeBallsDataSourceMutable(), downloader, typeMap, logger) { }

    public HomeBallsLocalStorageDataSource(
        ILocalStorageService localStorage,
        IHomeBallsDataSourceMutable dataSource,
        IHomeBallsLocalStorageDataDownloader downloader,
        IHomeBallsProtobufTypeMap typeMap,
        ILogger? logger)
    {
        LocalStorage = localStorage;
        (SourceMutable, Source) = (dataSource, dataSource);
        Downloader = downloader;
        TypeMap = typeMap;
        EventRaiser = new EventRaiser().RaisedBy(this);
        Logger = logger;
    }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IHomeBallsDataSourceMutable SourceMutable { get; }

    protected internal IHomeBallsDataSource Source { get; }

    protected internal IHomeBallsLocalStorageDataDownloader Downloader { get; }

    protected internal IHomeBallsProtobufTypeMap TypeMap { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal Boolean IsLoaded { get; set; }

    public IReadOnlyCollection<IHomeBallsReadOnlyCollection<IHomeBallsEntity>> Entities => SourceMutable.Entities;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGameVersion> GameVersions => Source.GameVersions;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGeneration> Generations => Source.Generations;

    public virtual IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsItem> Items => Source.Items;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsItemCategory> ItemCategories => Source.ItemCategories;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsLanguage> Languages => Source.Languages;

    public virtual IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsMove> Moves => Source.Moves;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories => Source.MoveDamageCategories;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsNature> Natures => Source.Natures;

    public virtual IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilities => Source.PokemonAbilities;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups => Source.PokemonEggGroups;

    public virtual IHomeBallsReadOnlyDataSet<(UInt16 SpeciesId, Byte FormId), IHomeBallsPokemonForm> PokemonForms => Source.PokemonForms;

    public virtual IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies => Source.PokemonSpecies;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsStat> Stats => Source.Stats;

    public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsType> Types => Source.Types;

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

    public virtual async ValueTask<HomeBallsLocalStorageDataSource> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (IsLoaded) return this;
        await EnsureDownloadedAsync(cancellationToken);

        var start = EventRaiser.Raise(DataLoading);

        await Task.WhenAll(
            EnsureLoadedAsync(SourceMutable.GameVersions, cancellationToken),
            EnsureLoadedAsync(SourceMutable.Generations, cancellationToken),
            EnsureLoadedAsync(SourceMutable.ItemCategories, cancellationToken),
            EnsureLoadedAsync(SourceMutable.Items, cancellationToken),
            EnsureLoadedAsync(SourceMutable.Languages, cancellationToken),
            EnsureLoadedAsync(SourceMutable.MoveDamageCategories, cancellationToken),
            EnsureLoadedAsync(SourceMutable.Moves, cancellationToken),
            EnsureLoadedAsync(SourceMutable.Natures, cancellationToken),
            EnsureLoadedAsync(SourceMutable.PokemonAbilities, cancellationToken),
            EnsureLoadedAsync(SourceMutable.PokemonEggGroups, cancellationToken),
            EnsureLoadedAsync(SourceMutable.PokemonForms, cancellationToken),
            EnsureLoadedAsync(SourceMutable.PokemonSpecies, cancellationToken),
            EnsureLoadedAsync(SourceMutable.Stats, cancellationToken),
            EnsureLoadedAsync(SourceMutable.Types, cancellationToken));

        EventRaiser.Raise(DataLoaded, start.StartTime);
        return this;
    }

    protected internal virtual async Task<HomeBallsLocalStorageDataSource> EnsureLoadedAsync<TKey, TRecord>(
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        CancellationToken cancellationToken = default)
        where TKey : notnull
        where TRecord : notnull, IKeyed, IIdentifiable
    {
        var deserializationType = typeof(ICollection<>)
            .MakeGenericType(TypeMap.GetProtobufConcreteType(dataSet.ElementType));
        var dataString = await LocalStorage.GetItemAsync<String>(
            dataSet.ElementType.GetFullNameNonNull(),
            cancellationToken);

        var dataBytes = Convert.FromBase64String(dataString);
        using var memory = new MemoryStream(dataBytes);

        var loaded = (IEnumerable<TRecord>)ProtoBuf.Serializer
            .Deserialize(deserializationType, memory);

        dataSet.AddRange(loaded);
        return this;
    }

    protected internal virtual async Task<HomeBallsLocalStorageDataSource> EnsureDownloadedAsync(
        CancellationToken cancellationToken = default)
    {
        var toDownload = await GetEntitiesToDownloadAsync(cancellationToken);
        if (toDownload.Count == 0) return this;

        await Downloader.DownloadAsync(toDownload.Keys, cancellationToken);
        return this;
    }

    protected internal virtual async Task<IReadOnlyDictionary<String, Type>> GetEntitiesToDownloadAsync(
        CancellationToken cancellationToken = default)
    {
        var toDownload = new Dictionary<String, Type> { };

        foreach (var dataSet in SourceMutable.Entities)
        {
            var identifier = dataSet.ElementType.GetFullNameNonNull();
            if (await LocalStorage.ContainKeyAsync(identifier, cancellationToken)) continue;
            toDownload.Add(identifier, dataSet.ElementType);
        }

        return toDownload.AsReadOnly();
    }

    async ValueTask<IHomeBallsLocalStorageDataSource> IAsyncLoadable<IHomeBallsLocalStorageDataSource>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLocalStorageDataSource> IHomeBallsLocalStorageDataSource
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLoadableDataSource> IAsyncLoadable<IHomeBallsLoadableDataSource>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}