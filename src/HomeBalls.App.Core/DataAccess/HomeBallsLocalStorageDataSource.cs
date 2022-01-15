namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess;

public interface IHomeBallsLocalStorageDataSource :
    IHomeBallsLoadableDataSource,
    IAsyncLoadable<IHomeBallsLocalStorageDataSource>,
    INotifyDataDownloading,
    INotifyDataLoading
{
    new ValueTask<IHomeBallsLocalStorageDataSource> EnsureLoadedAsync(
        CancellationToken cancellationToken = default);

    ValueTask<IHomeBallsLocalStorageDataSource> EnsureLoadedAsync<TKey, TRecord>(
        Func<IHomeBallsDataSourceMutable, IHomeBallsDataSet<TKey, TRecord>> dataSetNavigation,
        CancellationToken cancellationToken = default)
        where TKey : notnull
        where TRecord : notnull, IKeyed, IIdentifiable;
}

public class HomeBallsLocalStorageDataSource :
    IHomeBallsLocalStorageDataSource
{
    public HomeBallsLocalStorageDataSource(
        ILocalStorageService localStorage,
        IHomeBallsLocalStorageDownloader downloader,
        IHomeBallsProtobufTypeMap typeMap,
        ILogger? logger) :
        this(localStorage, new HomeBallsDataSourceMutable(), downloader, typeMap, logger) { }

    public HomeBallsLocalStorageDataSource(
        ILocalStorageService localStorage,
        IHomeBallsDataSourceMutable dataSource,
        IHomeBallsLocalStorageDownloader downloader,
        IHomeBallsProtobufTypeMap typeMap,
        ILogger? logger)
    {
        LocalStorage = localStorage;
        (SourceMutable, Source) = (dataSource, dataSource);
        Downloader = downloader;
        TypeMap = typeMap;
        EventRaiser = new EventRaiser().RaisedBy(this);
        Logger = logger;
        IsLoaded = new Dictionary<String, Boolean> { };
    }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IHomeBallsDataSourceMutable SourceMutable { get; }

    protected internal IHomeBallsDataSource Source { get; }

    protected internal IHomeBallsLocalStorageDownloader Downloader { get; }

    protected internal IHomeBallsProtobufTypeMap TypeMap { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal IDictionary<String, Boolean> IsLoaded { get; }

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
        var loadingTasks = new[]
        {
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
            EnsureLoadedAsync(SourceMutable.Types, cancellationToken)
        };

        foreach (var task in loadingTasks) await task;
        return this;
    }

    public virtual ValueTask<HomeBallsLocalStorageDataSource> EnsureLoadedAsync<TKey, TRecord>(
        Func<IHomeBallsDataSourceMutable, IHomeBallsDataSet<TKey, TRecord>> dataSetNavigation,
        CancellationToken cancellationToken = default)
        where TKey : notnull
        where TRecord : notnull, IKeyed, IIdentifiable =>
        EnsureLoadedAsync(dataSetNavigation(SourceMutable), cancellationToken);

    protected internal virtual async ValueTask<HomeBallsLocalStorageDataSource> EnsureLoadedAsync<TKey, TRecord>(
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        CancellationToken cancellationToken = default)
        where TKey : notnull
        where TRecord : notnull, IKeyed, IIdentifiable
    {
        var identifier = dataSet.ElementType.GetFullNameNonNull();
        IsLoaded.TryAdd(identifier, false);
        if (IsLoaded[identifier]) return this;

        await EnsureDownloadedAsync(identifier, cancellationToken);
        var start = EventRaiser.Raise(DataLoading, identifier);

        var deserializationType = typeof(IEnumerable<>)
            .MakeGenericType(TypeMap.GetProtobufConcreteType(dataSet.ElementType));
        var dataString = await LocalStorage.GetItemAsync<String>(identifier, cancellationToken);

        IEnumerable<TRecord> loaded;
        await using (var memory = new MemoryStream(Convert.FromBase64String(dataString)))
            loaded = (IEnumerable<TRecord>)ProtoBuf.Serializer
                .Deserialize(deserializationType, memory);

        dataSet.AddRange(loaded);
        IsLoaded[identifier] = true;
        EventRaiser.Raise(DataLoaded, start.StartTime, identifier);
        return this;
    }

    protected internal virtual async Task<HomeBallsLocalStorageDataSource> EnsureDownloadedAsync(
        String identifier,
        CancellationToken cancellationToken = default)
    {
        if (await LocalStorage.ContainKeyAsync(identifier, cancellationToken)) return this;

        await Downloader.DownloadAsync(
            identifier,
            identifier.AddFileExtension(_Values.DefaultProtobufExtension),
            cancellationToken);
        return this;
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

    async ValueTask<IHomeBallsLocalStorageDataSource> IHomeBallsLocalStorageDataSource
        .EnsureLoadedAsync<TKey, TRecord>(
            Func<IHomeBallsDataSourceMutable, IHomeBallsDataSet<TKey, TRecord>> dataSetNavigation,
            CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(dataSetNavigation, cancellationToken);
}