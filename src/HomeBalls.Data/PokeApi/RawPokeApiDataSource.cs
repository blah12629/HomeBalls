using CEo.Pokemon.HomeBalls.Entities;

namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiDataSource :
    IFileLoadable<IRawPokeApiDataSource>,
    IAsyncLoadable<IRawPokeApiDataSource>,
    INotifyDataLoading,
    INotifyDataDownloading
{
    IRawPokeApiDataSet<UInt16, RawPokeApiAbility> Abilities { get; }

    IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiAbilityName> AbilityNames { get; }

    IRawPokeApiDataSet<(Byte, Byte), RawPokeApiEggGroupProse> EggGroupProse { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiEggGroup> EggGroups { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiEvolutionChain> EvolutionChains { get; }

    IRawPokeApiDataSet<(Byte, Byte), RawPokeApiGenerationName> GenerationNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiGeneration> Generations { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiItemCategory> ItemCategories { get; }

    IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiItemName> ItemNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiItem> Items { get; }

    IRawPokeApiDataSet<(Byte, Byte), RawPokeApiLanguageName> LanguageNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiLanguage> Languages { get; }

    IRawPokeApiDataSet<(Byte, Byte), RawPokeApiMoveDamageClassProse> MoveDamageClassProse { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiMoveDamageClass> MoveDamageClasses { get; }

    IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiMoveName> MoveNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiMove> Moves { get; }

    IRawPokeApiDataSet<(Byte, Byte), RawPokeApiNatureName> NatureNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiNature> Natures { get; }

    IRawPokeApiDataSet<(UInt16, UInt16), RawPokeApiPokemonAbility> PokemonAbilities { get; }

    IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiPokemonEggGroup> PokemonEggGroups { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiPokemonEvolution> PokemonEvolutions { get; }

    IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiPokemonFormName> PokemonFormNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiPokemonForm> PokemonForms { get; }

    IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiPokemonGameIndex> PokemonGameIndeces { get; }

    IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiPokemonSpeciesName> PokemonSpeciesNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiPokemonSpecies> PokemonSpecies { get; }

    IRawPokeApiDataSet<(UInt16, UInt16), RawPokeApiPokemonType> PokemonTypes { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiPokemon> Pokemon { get; }

    IRawPokeApiDataSet<(Byte, Byte), RawPokeApiStatName> StatNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiStat> Stats { get; }

    IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiTypeName> TypeNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiType> Types { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiVersionGroup> VersionGroups { get; }

    IRawPokeApiDataSet<(Byte, Byte), RawPokeApiVersionName> VersionNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiVersion> Versions { get; }
}

public partial class RawPokeApiDataSource :
    IRawPokeApiDataSource
{
    public RawPokeApiDataSource(
        IFileSystem fileSystem,
        IRawPokeApiDownloader downloader,
        ICsvHelperFactory serializer,
        IHomeBallsIdentifierService identifiers,
        String dataRootDirectory = DefaultDataRoot,
        ILoggerFactory? loggerFactory = default)
    {
        DataRoot = dataRootDirectory;

        FileSystem = fileSystem;
        (FileSystem, Downloader, Serializer) = (fileSystem, downloader, serializer);
        IdentifierService = identifiers;
        LoggerFactory = loggerFactory;
        Logger = LoggerFactory?.CreateLogger<RawPokeApiDataSource>();
        EventRaiser = new EventRaiser().RaisedBy(this);

        var loadables = new List<IAsyncLoadable> { };
        var files = new List<IFileLoadable> { };
        (Loadables, FileLoadables) = (loadables.AsReadOnly(), files.AsReadOnly());

        Abilities = create<UInt16, RawPokeApiAbility>(nameof(Abilities));
        AbilityNames = create<(UInt16, Byte), RawPokeApiAbilityName>(nameof(AbilityNames));
        EggGroupProse = create<(Byte, Byte), RawPokeApiEggGroupProse>(nameof(EggGroupProse));
        EggGroups = create<Byte, RawPokeApiEggGroup>(nameof(EggGroups));
        EvolutionChains = create<UInt16, RawPokeApiEvolutionChain>(nameof(EvolutionChains));
        GenerationNames = create<(Byte, Byte), RawPokeApiGenerationName>(nameof(GenerationNames));
        Generations = create<Byte, RawPokeApiGeneration>(nameof(Generations));
        ItemCategories = create<Byte, RawPokeApiItemCategory>(nameof(ItemCategories));
        ItemNames = create<(UInt16, Byte), RawPokeApiItemName>(nameof(ItemNames));
        Items = create<UInt16, RawPokeApiItem>(nameof(Items));
        LanguageNames = create<(Byte, Byte), RawPokeApiLanguageName>(nameof(LanguageNames));
        Languages = create<Byte, RawPokeApiLanguage>(nameof(Languages));
        MoveDamageClassProse = create<(Byte, Byte), RawPokeApiMoveDamageClassProse>(nameof(MoveDamageClassProse));
        MoveDamageClasses = create<Byte, RawPokeApiMoveDamageClass>(nameof(MoveDamageClasses));
        MoveNames = create<(UInt16, Byte), RawPokeApiMoveName>(nameof(MoveNames));
        Moves = create<UInt16, RawPokeApiMove>(nameof(Moves));
        NatureNames = create<(Byte, Byte), RawPokeApiNatureName>(nameof(NatureNames));
        Natures = create<Byte, RawPokeApiNature>(nameof(Natures));
        PokemonAbilities = create<(UInt16, UInt16), RawPokeApiPokemonAbility>(nameof(PokemonAbilities));
        PokemonEggGroups = create<(UInt16, Byte), RawPokeApiPokemonEggGroup>(nameof(PokemonEggGroups));
        PokemonEvolutions = create<UInt16, RawPokeApiPokemonEvolution>(nameof(PokemonEvolutions));
        PokemonFormNames = create<(UInt16, Byte), RawPokeApiPokemonFormName>(nameof(PokemonFormNames));
        PokemonForms = create<UInt16, RawPokeApiPokemonForm>(nameof(PokemonForms));
        PokemonGameIndeces = create<(UInt16, Byte), RawPokeApiPokemonGameIndex>(nameof(PokemonGameIndeces));
        PokemonSpeciesNames = create<(UInt16, Byte), RawPokeApiPokemonSpeciesName>(nameof(PokemonSpeciesNames));
        PokemonSpecies = create<UInt16, RawPokeApiPokemonSpecies>(nameof(PokemonSpecies));
        PokemonTypes = create<(UInt16, UInt16), RawPokeApiPokemonType>(nameof(PokemonTypes));
        Pokemon = create<UInt16, RawPokeApiPokemon>(nameof(Pokemon));
        StatNames = create<(Byte, Byte), RawPokeApiStatName>(nameof(StatNames));
        Stats = create<Byte, RawPokeApiStat>(nameof(Stats));
        TypeNames = create<(UInt16, Byte), RawPokeApiTypeName>(nameof(TypeNames));
        Types = create<UInt16, RawPokeApiType>(nameof(Types));
        VersionGroups = create<Byte, RawPokeApiVersionGroup>(nameof(VersionGroups));
        VersionNames = create<(Byte, Byte), RawPokeApiVersionName>(nameof(VersionNames));
        Versions = create<Byte, RawPokeApiVersion>(nameof(Versions));

        IRawPokeApiDataSet<TKey, TRecord> create<TKey, TRecord>(
            String propertyName)
            where TKey : notnull, IEquatable<TKey>
            where TRecord : notnull, IKeyed<TKey>, IIdentifiable
        {
            var set = new RawPokeApiDataSet<TKey, TRecord>(
                FileSystem, Downloader, Serializer, IdentifierService,
                DataRoot,
                LoggerFactory?.CreateLogger<RawPokeApiDataSet<TKey, TRecord>>());
            loadables.Add(set);
            files.Add(set);

            set.DataLoading += (sender, e) => OnDataLoading(sender, e with { PropertyName = propertyName });
            set.DataLoaded += (sender, e) => OnDataLoaded(sender, e with { PropertyName = propertyName });
            return set;
        }
    }

    public IRawPokeApiDataSet<UInt16, RawPokeApiAbility> Abilities { get; }

    public IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiAbilityName> AbilityNames { get; }

    public IRawPokeApiDataSet<(Byte, Byte), RawPokeApiEggGroupProse> EggGroupProse { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiEggGroup> EggGroups { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiEvolutionChain> EvolutionChains { get; }

    public IRawPokeApiDataSet<(Byte, Byte), RawPokeApiGenerationName> GenerationNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiGeneration> Generations { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiItemCategory> ItemCategories { get; }

    public IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiItemName> ItemNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiItem> Items { get; }

    public IRawPokeApiDataSet<(Byte, Byte), RawPokeApiLanguageName> LanguageNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiLanguage> Languages { get; }

    public IRawPokeApiDataSet<(Byte, Byte), RawPokeApiMoveDamageClassProse> MoveDamageClassProse { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiMoveDamageClass> MoveDamageClasses { get; }

    public IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiMoveName> MoveNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiMove> Moves { get; }

    public IRawPokeApiDataSet<(Byte, Byte), RawPokeApiNatureName> NatureNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiNature> Natures { get; }

    public IRawPokeApiDataSet<(UInt16, UInt16), RawPokeApiPokemonAbility> PokemonAbilities { get; }

    public IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiPokemonEggGroup> PokemonEggGroups { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiPokemonEvolution> PokemonEvolutions { get; }

    public IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiPokemonFormName> PokemonFormNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiPokemonForm> PokemonForms { get; }

    public IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiPokemonGameIndex> PokemonGameIndeces { get; }

    public IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiPokemonSpeciesName> PokemonSpeciesNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiPokemonSpecies> PokemonSpecies { get; }

    public IRawPokeApiDataSet<(UInt16, UInt16), RawPokeApiPokemonType> PokemonTypes { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiPokemon> Pokemon { get; }

    public IRawPokeApiDataSet<(Byte, Byte), RawPokeApiStatName> StatNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiStat> Stats { get; }

    public IRawPokeApiDataSet<(UInt16, Byte), RawPokeApiTypeName> TypeNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiType> Types { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiVersionGroup> VersionGroups { get; }

    public IRawPokeApiDataSet<(Byte, Byte), RawPokeApiVersionName> VersionNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiVersion> Versions { get; }

    protected internal virtual IReadOnlyCollection<IAsyncLoadable> Loadables { get; }

    protected internal virtual IReadOnlyCollection<IFileLoadable> FileLoadables { get; }

    protected internal IFileSystem FileSystem { get; }

    protected internal IRawPokeApiDownloader Downloader { get; }

    protected internal ICsvHelperFactory Serializer { get; }

    protected internal IHomeBallsIdentifierService IdentifierService { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal ILogger? Logger { get; }

    protected internal String DataRoot { get; set; }

    public event EventHandler<TimedActionStartingEventArgs>? DataLoading;

    public event EventHandler<TimedActionEndedEventArgs>? DataLoaded;

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

    public virtual async ValueTask<IRawPokeApiDataSource> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        var tasks = Loadables.Select(loadable =>
            loadable.EnsureLoadedAsync(cancellationToken).AsTask());

        await Task.WhenAll(tasks);
        return this;
    }

    public virtual RawPokeApiDataSource InDirectory(String directory)
    {
        DataRoot = directory;
        foreach (var set in FileLoadables) set.InDirectory(directory);
        return this;
    }

    protected internal virtual void OnDataLoading(
        Object? sender,
        TimedActionStartingEventArgs e) =>
        DataLoading?.Invoke(this, e);

    protected internal virtual void OnDataLoaded(
        Object? sender,
        TimedActionEndedEventArgs e) =>
        DataLoaded?.Invoke(this, e);

    async ValueTask IAsyncLoadable.EnsureLoadedAsync(
        CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    void IFileLoadable.InDirectory(String directory) => InDirectory(directory);

    IRawPokeApiDataSource IFileLoadable<IRawPokeApiDataSource>
        .InDirectory(String directory) =>
        InDirectory(directory);
}