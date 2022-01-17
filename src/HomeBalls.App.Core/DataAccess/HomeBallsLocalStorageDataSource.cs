namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess;

public interface IHomeBallsLocalStorageDataSource :
    IHomeBallsLoadableDataSource,
    IAsyncLoadable<IHomeBallsLocalStorageDataSource>,
    INotifyDataDownloading,
    INotifyDataLoading
{
    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsGameVersion> GameVersions { get; }

    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsGeneration> Generations { get; }

    new IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsItem> Items { get; }

    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsLanguage> Languages { get; }

    new IHomeBallsLocalStorageDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> Legalities { get; }

    new IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsMove> Moves { get; }

    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsNature> Natures { get; }

    new IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    new IHomeBallsLocalStorageDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> PokemonForms { get; }

    new IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsStat> Stats { get; }

    new IHomeBallsLocalStorageDataSet<Byte, IHomeBallsType> Types { get; }

    new ValueTask<IHomeBallsLocalStorageDataSource> EnsureLoadedAsync(
        CancellationToken cancellationToken = default);
}

public class HomeBallsLocalStorageDataSource :
    IHomeBallsLocalStorageDataSource,
    IAsyncLoadable<HomeBallsLocalStorageDataSource>
{
    public HomeBallsLocalStorageDataSource(
        ILocalStorageService localStorage,
        IHomeBallsProtobufTypeMap typeMap,
        IHomeBallsLocalStorageDownloader downloader,
        ILoggerFactory? loggerFactory = default)
    {
        LocalStorage = localStorage;
        TypeMap = typeMap;
        Downloader = downloader;
        EventRaiser = new EventRaiser().RaisedBy(this);
        LoggerFactory = loggerFactory;
        Logger = LoggerFactory?.CreateLogger(GetType());

        var loadables = new List<IAsyncLoadable> { };
        Loadables = loadables.AsReadOnly();

        GameVersions = createSet<Byte, IHomeBallsGameVersion>();
        Generations = createSet<Byte, IHomeBallsGeneration>();
        Items = createSet<UInt16, IHomeBallsItem>();
        ItemCategories = createSet<Byte, IHomeBallsItemCategory>();
        Languages = createSet<Byte, IHomeBallsLanguage>();
        Legalities = createSet<HomeBallsEntryKey, IHomeBallsEntryLegality>();
        Moves = createSet<UInt16, IHomeBallsMove>();
        MoveDamageCategories = createSet<Byte, IHomeBallsMoveDamageCategory>();
        Natures = createSet<Byte, IHomeBallsNature>();
        PokemonAbilities = createSet<UInt16, IHomeBallsPokemonAbility>();
        PokemonEggGroups = createSet<Byte, IHomeBallsPokemonEggGroup>();
        PokemonForms = createSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm>();
        PokemonSpecies = createSet<UInt16, IHomeBallsPokemonSpecies>();
        Stats = createSet<Byte, IHomeBallsStat>();
        Types = createSet<Byte, IHomeBallsType>();

        IHomeBallsLocalStorageDataSet<TKey, TRecord> createSet<TKey, TRecord>()
            where TKey : notnull, IEquatable<TKey>
            where TRecord : notnull, IKeyed<TKey>, IIdentifiable
        {
            var set = new HomeBallsLocalStorageDataSet<TKey, TRecord>(
                LocalStorage, TypeMap, Downloader,
                LoggerFactory?.CreateLogger(typeof(HomeBallsLocalStorageDataSet<TKey, TRecord>)));

            loadables.Add(set);
            return set;
        }
    }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IHomeBallsProtobufTypeMap TypeMap { get; }

    protected internal IHomeBallsLocalStorageDownloader Downloader { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal ILogger? Logger { get; }

    protected internal IReadOnlyList<IAsyncLoadable> Loadables { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsGameVersion> GameVersions { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsGeneration> Generations { get; }

    protected internal IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsItem> Items { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsLanguage> Languages { get; }

    protected internal IHomeBallsLocalStorageDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> Legalities { get; }

    protected internal IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsMove> Moves { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsNature> Natures { get; }

    protected internal IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    protected internal IHomeBallsLocalStorageDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> PokemonForms { get; }

    protected internal IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsStat> Stats { get; }

    protected internal IHomeBallsLocalStorageDataSet<Byte, IHomeBallsType> Types { get; }

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsGameVersion> IHomeBallsLocalStorageDataSource.GameVersions => GameVersions;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsGameVersion> IHomeBallsLoadableDataSource.GameVersions => GameVersions;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGameVersion> IHomeBallsDataSource.GameVersions => GameVersions;

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsGeneration> IHomeBallsLocalStorageDataSource.Generations => Generations;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsGeneration> IHomeBallsLoadableDataSource.Generations => Generations;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGeneration> IHomeBallsDataSource.Generations => Generations;

    IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsItem> IHomeBallsLocalStorageDataSource.Items => Items;

    IHomeBallsLoadableDataSet<UInt16, IHomeBallsItem> IHomeBallsLoadableDataSource.Items => Items;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsItem> IHomeBallsDataSource.Items => Items;

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsItemCategory> IHomeBallsLocalStorageDataSource.ItemCategories => ItemCategories;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsItemCategory> IHomeBallsLoadableDataSource.ItemCategories => ItemCategories;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsItemCategory> IHomeBallsDataSource.ItemCategories => ItemCategories;

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsLanguage> IHomeBallsLocalStorageDataSource.Languages => Languages;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsLanguage> IHomeBallsLoadableDataSource.Languages => Languages;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsLanguage> IHomeBallsDataSource.Languages => Languages;

    IHomeBallsLocalStorageDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> IHomeBallsLocalStorageDataSource.Legalities => Legalities;

    IHomeBallsLoadableDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> IHomeBallsLoadableDataSource.Legalities => Legalities;

    IHomeBallsReadOnlyDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> IHomeBallsDataSource.Legalities => Legalities;

    IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsMove> IHomeBallsLocalStorageDataSource.Moves => Moves;

    IHomeBallsLoadableDataSet<UInt16, IHomeBallsMove> IHomeBallsLoadableDataSource.Moves => Moves;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsMove> IHomeBallsDataSource.Moves => Moves;

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsMoveDamageCategory> IHomeBallsLocalStorageDataSource.MoveDamageCategories => MoveDamageCategories;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsMoveDamageCategory> IHomeBallsLoadableDataSource.MoveDamageCategories => MoveDamageCategories;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsMoveDamageCategory> IHomeBallsDataSource.MoveDamageCategories => MoveDamageCategories;

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsNature> IHomeBallsLocalStorageDataSource.Natures => Natures;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsNature> IHomeBallsLoadableDataSource.Natures => Natures;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsNature> IHomeBallsDataSource.Natures => Natures;

    IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsPokemonAbility> IHomeBallsLocalStorageDataSource.PokemonAbilities => PokemonAbilities;

    IHomeBallsLoadableDataSet<UInt16, IHomeBallsPokemonAbility> IHomeBallsLoadableDataSource.PokemonAbilities => PokemonAbilities;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonAbility> IHomeBallsDataSource.PokemonAbilities => PokemonAbilities;

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsPokemonEggGroup> IHomeBallsLocalStorageDataSource.PokemonEggGroups => PokemonEggGroups;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsPokemonEggGroup> IHomeBallsLoadableDataSource.PokemonEggGroups => PokemonEggGroups;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsPokemonEggGroup> IHomeBallsDataSource.PokemonEggGroups => PokemonEggGroups;

    IHomeBallsLocalStorageDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsLocalStorageDataSource.PokemonForms => PokemonForms;

    IHomeBallsLoadableDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsLoadableDataSource.PokemonForms => PokemonForms;

    IHomeBallsReadOnlyDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsDataSource.PokemonForms => PokemonForms;

    IHomeBallsLocalStorageDataSet<UInt16, IHomeBallsPokemonSpecies> IHomeBallsLocalStorageDataSource.PokemonSpecies => PokemonSpecies;

    IHomeBallsLoadableDataSet<UInt16, IHomeBallsPokemonSpecies> IHomeBallsLoadableDataSource.PokemonSpecies => PokemonSpecies;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonSpecies> IHomeBallsDataSource.PokemonSpecies => PokemonSpecies;

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsStat> IHomeBallsLocalStorageDataSource.Stats => Stats;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsStat> IHomeBallsLoadableDataSource.Stats => Stats;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsStat> IHomeBallsDataSource.Stats => Stats;

    IHomeBallsLocalStorageDataSet<Byte, IHomeBallsType> IHomeBallsLocalStorageDataSource.Types => Types;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsType> IHomeBallsLoadableDataSource.Types => Types;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsType> IHomeBallsDataSource.Types => Types;

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
        var start = EventRaiser.Raise(DataLoading);
        await Task.WhenAll(Loadables.Select(async loadable =>
            await loadable.EnsureLoadedAsync(cancellationToken)));

        return this;
    }

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLocalStorageDataSource> IHomeBallsLocalStorageDataSource
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLoadableDataSource> IAsyncLoadable<IHomeBallsLoadableDataSource>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLocalStorageDataSource> IAsyncLoadable<IHomeBallsLocalStorageDataSource>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}

// public class HomeBallsLocalStorageDataSource :
//     IHomeBallsLocalStorageDataSource
// {
//     public HomeBallsLocalStorageDataSource(
//         ILocalStorageService localStorage,
//         IHomeBallsLocalStorageDownloader downloader,
//         IHomeBallsProtobufTypeMap typeMap,
//         ILogger? logger) :
//         this(localStorage, new HomeBallsDataSourceMutable(), downloader, typeMap, logger) { }

//     public HomeBallsLocalStorageDataSource(
//         ILocalStorageService localStorage,
//         IHomeBallsDataSourceMutable dataSource,
//         IHomeBallsLocalStorageDownloader downloader,
//         IHomeBallsProtobufTypeMap typeMap,
//         ILogger? logger)
//     {
//         LocalStorage = localStorage;
//         (SourceMutable, Source) = (dataSource, dataSource);
//         Downloader = downloader;
//         TypeMap = typeMap;
//         EventRaiser = new EventRaiser().RaisedBy(this);
//         Logger = logger;
//         IsLoaded = new Dictionary<String, Boolean> { };
//     }

//     protected internal ILocalStorageService LocalStorage { get; }

//     protected internal IHomeBallsDataSourceMutable SourceMutable { get; }

//     protected internal IHomeBallsDataSource Source { get; }

//     protected internal IHomeBallsLocalStorageDownloader Downloader { get; }

//     protected internal IHomeBallsProtobufTypeMap TypeMap { get; }

//     protected internal IEventRaiser EventRaiser { get; }

//     protected internal ILogger? Logger { get; }

//     protected internal IDictionary<String, Boolean> IsLoaded { get; }

//     public IReadOnlyCollection<IHomeBallsReadOnlyCollection<IHomeBallsEntity>> Entities => SourceMutable.Entities;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGameVersion> GameVersions => Source.GameVersions;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGeneration> Generations => Source.Generations;

//     public virtual IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsItem> Items => Source.Items;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsItemCategory> ItemCategories => Source.ItemCategories;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsLanguage> Languages => Source.Languages;

//     public virtual IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsMove> Moves => Source.Moves;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories => Source.MoveDamageCategories;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsNature> Natures => Source.Natures;

//     public virtual IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilities => Source.PokemonAbilities;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups => Source.PokemonEggGroups;

//     public virtual IHomeBallsReadOnlyDataSet<(UInt16 SpeciesId, Byte FormId), IHomeBallsPokemonForm> PokemonForms => Source.PokemonForms;

//     public virtual IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies => Source.PokemonSpecies;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsStat> Stats => Source.Stats;

//     public virtual IHomeBallsReadOnlyDataSet<Byte, IHomeBallsType> Types => Source.Types;

//     public event EventHandler<TimedActionStartingEventArgs>? DataDownloading
//     {
//         add => Downloader.DataDownloading += value;
//         remove => Downloader.DataDownloading -= value;
//     }

//     public event EventHandler<TimedActionEndedEventArgs>? DataDownloaded
//     {
//         add => Downloader.DataDownloaded += value;
//         remove => Downloader.DataDownloaded -= value;
//     }

//     public event EventHandler<TimedActionStartingEventArgs>? DataLoading;

//     public event EventHandler<TimedActionEndedEventArgs>? DataLoaded;

//     public virtual async ValueTask<HomeBallsLocalStorageDataSource> EnsureLoadedAsync(
//         CancellationToken cancellationToken = default)
//     {
//         var loadingTasks = new[]
//         {
//             EnsureLoadedAsync(SourceMutable.GameVersions, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.Generations, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.ItemCategories, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.Items, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.Languages, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.MoveDamageCategories, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.Moves, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.Natures, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.PokemonAbilities, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.PokemonEggGroups, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.PokemonForms, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.PokemonSpecies, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.Stats, cancellationToken),
//             EnsureLoadedAsync(SourceMutable.Types, cancellationToken)
//         };

//         foreach (var task in loadingTasks) await task;
//         return this;
//     }

//     public virtual ValueTask<HomeBallsLocalStorageDataSource> EnsureLoadedAsync<TKey, TRecord>(
//         Func<IHomeBallsDataSourceMutable, IHomeBallsDataSet<TKey, TRecord>> dataSetNavigation,
//         CancellationToken cancellationToken = default)
//         where TKey : notnull
//         where TRecord : notnull, IKeyed, IIdentifiable =>
//         EnsureLoadedAsync(dataSetNavigation(SourceMutable), cancellationToken);

//     protected internal virtual async ValueTask<HomeBallsLocalStorageDataSource> EnsureLoadedAsync<TKey, TRecord>(
//         IHomeBallsDataSet<TKey, TRecord> dataSet,
//         CancellationToken cancellationToken = default)
//         where TKey : notnull
//         where TRecord : notnull, IKeyed, IIdentifiable
//     {
//         var identifier = dataSet.ElementType.GetFullNameNonNull();
//         IsLoaded.TryAdd(identifier, false);
//         if (IsLoaded[identifier]) return this;

//         await EnsureDownloadedAsync(identifier, cancellationToken);
//         var start = EventRaiser.Raise(DataLoading, identifier);

//         var deserializationType = typeof(IEnumerable<>)
//             .MakeGenericType(TypeMap.GetProtobufConcreteType(dataSet.ElementType));
//         var dataString = await LocalStorage.GetItemAsync<String>(identifier, cancellationToken);

//         IEnumerable<TRecord> loaded;
//         await using (var memory = new MemoryStream(Convert.FromBase64String(dataString)))
//             loaded = (IEnumerable<TRecord>)ProtoBuf.Serializer
//                 .Deserialize(deserializationType, memory);

//         dataSet.AddRange(loaded);
//         IsLoaded[identifier] = true;
//         EventRaiser.Raise(DataLoaded, start.StartTime, identifier);
//         return this;
//     }

//     protected internal virtual async Task<HomeBallsLocalStorageDataSource> EnsureDownloadedAsync(
//         String identifier,
//         CancellationToken cancellationToken = default)
//     {
//         if (await LocalStorage.ContainKeyAsync(identifier, cancellationToken)) return this;

//         await Downloader.DownloadAsync(
//             identifier,
//             identifier.AddFileExtension(_Values.DefaultProtobufExtension),
//             cancellationToken);
//         return this;
//     }

//     async ValueTask<IHomeBallsLocalStorageDataSource> IAsyncLoadable<IHomeBallsLocalStorageDataSource>
//         .EnsureLoadedAsync(CancellationToken cancellationToken) =>
//         await EnsureLoadedAsync(cancellationToken);

//     async ValueTask IAsyncLoadable
//         .EnsureLoadedAsync(CancellationToken cancellationToken) =>
//         await EnsureLoadedAsync(cancellationToken);

//     async ValueTask<IHomeBallsLocalStorageDataSource> IHomeBallsLocalStorageDataSource
//         .EnsureLoadedAsync(CancellationToken cancellationToken) =>
//         await EnsureLoadedAsync(cancellationToken);

//     async ValueTask<IHomeBallsLoadableDataSource> IAsyncLoadable<IHomeBallsLoadableDataSource>
//         .EnsureLoadedAsync(CancellationToken cancellationToken) =>
//         await EnsureLoadedAsync(cancellationToken);

//     async ValueTask<IHomeBallsLocalStorageDataSource> IHomeBallsLocalStorageDataSource
//         .EnsureLoadedAsync<TKey, TRecord>(
//             Func<IHomeBallsDataSourceMutable, IHomeBallsDataSet<TKey, TRecord>> dataSetNavigation,
//             CancellationToken cancellationToken) =>
//         await EnsureLoadedAsync(dataSetNavigation, cancellationToken);
// }