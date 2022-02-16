namespace CEo.Pokemon.HomeBalls.App.DataAccess;

public interface IHomeBallsLocalStorageDataSource :
    IHomeBallsLoadableDataSource,
    IAsyncLoadable<IHomeBallsLocalStorageDataSource>,
    INotifyDataDownloading,
    INotifyDataLoading
{
    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsGameVersion> GameVersions { get; }

    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsGeneration> Generations { get; }

    new IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsItem> Items { get; }

    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsLanguage> Languages { get; }

    new IHomeBallsLocalStorageDataSourceProperty<HomeBallsEntryKey, IHomeBallsEntryLegality> Legalities { get; }

    new IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsMove> Moves { get; }

    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsNature> Natures { get; }

    new IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    new IHomeBallsLocalStorageDataSourceProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> PokemonForms { get; }

    new IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsStat> Stats { get; }

    new IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsType> Types { get; }

    new IHomeBallsLocalStorageDataSourceProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> BreedablePokemonForms { get; }

    new IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonSpecies> BreedablePokemonSpecies { get; }

    new IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsItem> Pokeballs { get; }

    new ValueTask<IHomeBallsLocalStorageDataSource> EnsureLoadedAsync(
        CancellationToken cancellationToken = default);
}

public partial class HomeBallsLocalStorageDataSource :
    IHomeBallsLocalStorageDataSource,
    IAsyncLoadable<HomeBallsLocalStorageDataSource>
{
    public HomeBallsLocalStorageDataSource(
        ILocalStorageService localStorage,
        IHomeBallsLocalStorageDownloader downloader,
        IProtoBufSerializer serializer,
        IHomeBallsEntryKeyComparer entryComparer,
        IHomeBallsPokemonFormKeyComparer pokemonComparer,
        IHomeBallsItemIdComparer itemComparer,
        ILoggerFactory? loggerFactory = default)
    {
        (LocalStorage, Downloader, Serializer) = (localStorage, downloader, serializer);
        (EntryComparer, PokemonComparer, ItemComparer) = (entryComparer, pokemonComparer, itemComparer);
        EventRaiser = new EventRaiser().RaisedBy(this);
        LoggerFactory = loggerFactory;
        Logger = LoggerFactory?.CreateLogger(GetType());

        var loadables = new List<IAsyncLoadable> { };
        Loadables = loadables.AsReadOnly();

        GameVersions = createSet<Byte, HomeBallsGameVersion>(nameof(GameVersions));
        Generations = createSet<Byte, HomeBallsGeneration>(nameof(Generations));
        Items = createSet<UInt16, HomeBallsItem>(nameof(Items), ItemComparer);
        ItemCategories = createSet<Byte, HomeBallsItemCategory>(nameof(ItemCategories));
        Languages = createSet<Byte, HomeBallsLanguage>(nameof(Languages));
        Legalities = createSet<HomeBallsEntryKey, HomeBallsEntryLegality>(nameof(Legalities), EntryComparer);
        Moves = createSet<UInt16, HomeBallsMove>(nameof(Moves));
        MoveDamageCategories = createSet<Byte, HomeBallsMoveDamageCategory>(nameof(MoveDamageCategories));
        Natures = createSet<Byte, HomeBallsNature>(nameof(Natures));
        PokemonAbilities = createSet<UInt16, HomeBallsPokemonAbility>(nameof(PokemonAbilities));
        PokemonEggGroups = createSet<Byte, HomeBallsPokemonEggGroup>(nameof(PokemonEggGroups));
        PokemonForms = createSet<HomeBallsPokemonFormKey, HomeBallsPokemonForm>(nameof(PokemonForms), PokemonComparer);
        PokemonSpecies = createSet<UInt16, HomeBallsPokemonSpecies>(nameof(PokemonSpecies));
        Stats = createSet<Byte, HomeBallsStat>(nameof(Stats));
        Types = createSet<Byte, HomeBallsType>(nameof(Types));
        BreedablePokemonForms = createSet<HomeBallsPokemonFormKey, HomeBallsPokemonForm>(nameof(BreedablePokemonForms), PokemonComparer);
        BreedablePokemonSpecies = createSet<UInt16, HomeBallsPokemonSpecies>(nameof(BreedablePokemonSpecies));
        Pokeballs = createSet<UInt16, HomeBallsItem>(nameof(Pokeballs), ItemComparer);

        IHomeBallsLocalStorageDataSourceProperty<TKey, TRecord> createSet<TKey, TRecord>(
            String propertyName,
            IComparer<TKey>? comparer = default)
            where TKey : notnull, IEquatable<TKey>
            where TRecord : notnull, IKeyed<TKey>, IIdentifiable
        {
            var set = new HomeBallsLocalStorageDataSourceProperty<TKey, TRecord>(
                propertyName, LocalStorage, Downloader, Serializer,
                LoggerFactory?.CreateLogger(typeof(HomeBallsLocalStorageDataSourceProperty<TKey, TRecord>)),
                comparer);

            loadables.Add(set);
            set.DataLoaded += (sender, e) => DataLoaded?.Invoke(sender, e with { PropertyName = propertyName });
            set.DataLoading += (sender, e) => DataLoading?.Invoke(sender, e with { PropertyName = propertyName });
            return set;
        }
    }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IHomeBallsLocalStorageDownloader Downloader { get; }

    protected internal IProtoBufSerializer Serializer { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal IHomeBallsEntryKeyComparer EntryComparer { get; }

    protected internal IHomeBallsPokemonFormKeyComparer PokemonComparer { get; }

    protected internal IHomeBallsItemIdComparer ItemComparer { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal ILogger? Logger { get; }

    protected internal IReadOnlyList<IAsyncLoadable> Loadables { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsGameVersion> GameVersions { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsGeneration> Generations { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsItem> Items { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsLanguage> Languages { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<HomeBallsEntryKey, IHomeBallsEntryLegality> Legalities { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsMove> Moves { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsNature> Natures { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> PokemonForms { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsStat> Stats { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsType> Types { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> BreedablePokemonForms { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonSpecies> BreedablePokemonSpecies { get; }

    protected internal IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsItem> Pokeballs { get; }

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