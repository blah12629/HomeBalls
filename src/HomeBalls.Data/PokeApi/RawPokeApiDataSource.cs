namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiDataSource :
    IFileLoadable<IRawPokeApiDataSource>,
    IAsyncLoadable<IRawPokeApiDataSource>
{
    IReadOnlyDictionary<Type, IRawPokeApiDataLoadable> DataSets { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiAbility> Abilities { get; }

    IRawPokeApiDataSet<RawPokeApiAbilityName> AbilityNames { get; }

    IRawPokeApiDataSet<RawPokeApiEggGroupProse> EggGroupProse { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiEggGroup> EggGroups { get; }

    IRawPokeApiDataSet<RawPokeApiEvolutionChain> EvolutionChains { get; }

    IRawPokeApiDataSet<RawPokeApiGenerationName> GenerationNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiGeneration> Generations { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiItemCategory> ItemCategories { get; }

    IRawPokeApiDataSet<RawPokeApiItemName> ItemNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiItem> Items { get; }

    IRawPokeApiDataSet<RawPokeApiLanguageName> LanguageNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiLanguage> Languages { get; }

    IRawPokeApiDataSet<RawPokeApiMoveDamageClassProse> MoveDamageClassProse { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiMoveDamageClass> MoveDamageClasses { get; }

    IRawPokeApiDataSet<RawPokeApiMoveName> MoveNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiMove> Moves { get; }

    IRawPokeApiDataSet<RawPokeApiNatureName> NatureNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiNature> Natures { get; }

    IRawPokeApiDataSet<RawPokeApiPokemonAbility> PokemonAbilities { get; }

    IRawPokeApiDataSet<RawPokeApiPokemonEggGroup> PokemonEggGroups { get; }

    IRawPokeApiDataSet<RawPokeApiPokemonEvolution> PokemonEvolutions { get; }

    IRawPokeApiDataSet<RawPokeApiPokemonFormName> PokemonFormNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiPokemonForm> PokemonForms { get; }

    IRawPokeApiDataSet<RawPokeApiPokemonGameIndex> PokemonGameIndeces { get; }

    IRawPokeApiDataSet<RawPokeApiPokemonSpeciesName> PokemonSpeciesNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiPokemonSpecies> PokemonSpecies { get; }

    IRawPokeApiDataSet<RawPokeApiPokemonType> PokemonTypes { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiPokemon> Pokemon { get; }

    IRawPokeApiDataSet<RawPokeApiStatName> StatNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiStat> Stats { get; }

    IRawPokeApiDataSet<RawPokeApiTypeName> TypeNames { get; }

    IRawPokeApiDataSet<UInt16, RawPokeApiType> Types { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiVersionGroup> VersionGroups { get; }

    IRawPokeApiDataSet<RawPokeApiVersionName> VersionNames { get; }

    IRawPokeApiDataSet<Byte, RawPokeApiVersion> Versions { get; }
}

public partial class RawPokeApiDataSource :
    IRawPokeApiDataSource
{
    public RawPokeApiDataSource(
        IFileSystem fileSystem,
        HttpClient rawPokeApiGithubClient,
        IRawPokeApiFileNameService fileNameService,
        ICsvHelperFactory csvHelperFactory,
        ILogger? logger = default)
    {
        FileSystem = fileSystem;
        RawPokeApiGithubClient = rawPokeApiGithubClient;
        FileNameService = fileNameService;
        CsvHelperFactory = csvHelperFactory;
        Logger = logger;

        var sets = new Dictionary<Type, IRawPokeApiDataLoadable> { };
        Abilities = createDictionary<UInt16, RawPokeApiAbility>();
        AbilityNames = createList<RawPokeApiAbilityName>();
        EggGroupProse = createList<RawPokeApiEggGroupProse>();
        EggGroups = createDictionary<Byte, RawPokeApiEggGroup>();
        EvolutionChains = createList<RawPokeApiEvolutionChain>();
        GenerationNames = createList<RawPokeApiGenerationName>();
        Generations = createDictionary<Byte, RawPokeApiGeneration>();
        ItemCategories = createDictionary<Byte, RawPokeApiItemCategory>();
        ItemNames = createList<RawPokeApiItemName>();
        Items = createDictionary<UInt16, RawPokeApiItem>();
        LanguageNames = createList<RawPokeApiLanguageName>();
        Languages = createDictionary<Byte, RawPokeApiLanguage>();
        MoveDamageClassProse = createList<RawPokeApiMoveDamageClassProse>();
        MoveDamageClasses = createDictionary<Byte, RawPokeApiMoveDamageClass>();
        MoveNames = createList<RawPokeApiMoveName>();
        Moves = createDictionary<UInt16, RawPokeApiMove>();
        NatureNames = createList<RawPokeApiNatureName>();
        Natures = createDictionary<Byte, RawPokeApiNature>();
        PokemonAbilities = createList<RawPokeApiPokemonAbility>();
        PokemonEggGroups = createList<RawPokeApiPokemonEggGroup>();
        PokemonEvolutions = createList<RawPokeApiPokemonEvolution>();
        PokemonFormNames = createList<RawPokeApiPokemonFormName>();
        PokemonForms = createDictionary<UInt16, RawPokeApiPokemonForm>();
        PokemonGameIndeces = createList<RawPokeApiPokemonGameIndex>();
        PokemonSpeciesNames = createList<RawPokeApiPokemonSpeciesName>();
        PokemonSpecies = createDictionary<UInt16, RawPokeApiPokemonSpecies>();
        PokemonTypes = createList<RawPokeApiPokemonType>();
        Pokemon = createDictionary<UInt16, RawPokeApiPokemon>();
        StatNames = createList<RawPokeApiStatName>();
        Stats = createDictionary<Byte, RawPokeApiStat>();
        TypeNames = createList<RawPokeApiTypeName>();
        Types = createDictionary<UInt16, RawPokeApiType>();
        VersionGroups = createDictionary<Byte, RawPokeApiVersionGroup>();
        VersionNames = createList<RawPokeApiVersionName>();
        Versions = createDictionary<Byte, RawPokeApiVersion>();
        DataSets = sets.AsReadOnly();

        IRawPokeApiDataSet<TRecord> createList<TRecord>()
            where TRecord : notnull, RawPokeApiRecord =>
            addSets<IRawPokeApiDataSet<TRecord>, TRecord>(
                new RawPokeApiDataSet<TRecord>(FileSystem, RawPokeApiGithubClient, FileNameService, CsvHelperFactory, Logger));

        IRawPokeApiDataSet<TKey, TRecord> createDictionary<TKey, TRecord>()
            where TKey : notnull, IEquatable<TKey>
            where TRecord : notnull, RawPokeApiRecord, IKeyed<TKey>, IIdentifiable =>
            addSets<IRawPokeApiDataSet<TKey, TRecord>, TRecord>(
                new RawPokeApiDataSet<TKey, TRecord>(FileSystem, RawPokeApiGithubClient, FileNameService, CsvHelperFactory, Logger));

        TLoadable addSets<TLoadable, TRecord>(TLoadable loadable)
            where TLoadable : notnull, IRawPokeApiDataLoadable
        {
            sets.Add(typeof(TRecord), loadable);
            return loadable;
        }
    }

    public IReadOnlyDictionary<Type, IRawPokeApiDataLoadable> DataSets { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiAbility> Abilities { get; }

    public IRawPokeApiDataSet<RawPokeApiAbilityName> AbilityNames { get; }

    public IRawPokeApiDataSet<RawPokeApiEggGroupProse> EggGroupProse { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiEggGroup> EggGroups { get; }

    public IRawPokeApiDataSet<RawPokeApiEvolutionChain> EvolutionChains { get; }

    public IRawPokeApiDataSet<RawPokeApiGenerationName> GenerationNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiGeneration> Generations { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiItemCategory> ItemCategories { get; }

    public IRawPokeApiDataSet<RawPokeApiItemName> ItemNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiItem> Items { get; }

    public IRawPokeApiDataSet<RawPokeApiLanguageName> LanguageNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiLanguage> Languages { get; }

    public IRawPokeApiDataSet<RawPokeApiMoveDamageClassProse> MoveDamageClassProse { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiMoveDamageClass> MoveDamageClasses { get; }

    public IRawPokeApiDataSet<RawPokeApiMoveName> MoveNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiMove> Moves { get; }

    public IRawPokeApiDataSet<RawPokeApiNatureName> NatureNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiNature> Natures { get; }

    public IRawPokeApiDataSet<RawPokeApiPokemonAbility> PokemonAbilities { get; }

    public IRawPokeApiDataSet<RawPokeApiPokemonEggGroup> PokemonEggGroups { get; }

    public IRawPokeApiDataSet<RawPokeApiPokemonEvolution> PokemonEvolutions { get; }

    public IRawPokeApiDataSet<RawPokeApiPokemonFormName> PokemonFormNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiPokemonForm> PokemonForms { get; }

    public IRawPokeApiDataSet<RawPokeApiPokemonGameIndex> PokemonGameIndeces { get; }

    public IRawPokeApiDataSet<RawPokeApiPokemonSpeciesName> PokemonSpeciesNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiPokemonSpecies> PokemonSpecies { get; }

    public IRawPokeApiDataSet<RawPokeApiPokemonType> PokemonTypes { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiPokemon> Pokemon { get; }

    public IRawPokeApiDataSet<RawPokeApiStatName> StatNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiStat> Stats { get; }

    public IRawPokeApiDataSet<RawPokeApiTypeName> TypeNames { get; }

    public IRawPokeApiDataSet<UInt16, RawPokeApiType> Types { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiVersionGroup> VersionGroups { get; }

    public IRawPokeApiDataSet<RawPokeApiVersionName> VersionNames { get; }

    public IRawPokeApiDataSet<Byte, RawPokeApiVersion> Versions { get; }

    protected internal IFileSystem FileSystem { get; }

    protected internal HttpClient RawPokeApiGithubClient { get; }

    protected internal IRawPokeApiFileNameService FileNameService { get; }

    protected internal ICsvHelperFactory CsvHelperFactory { get; }

    protected internal virtual ILogger? Logger { get; }

    public virtual async ValueTask<IRawPokeApiDataSource> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(DataSets.Values.Select(set =>
            set.EnsureLoadedAsync(cancellationToken).AsTask()));

        return this;
    }

    public virtual RawPokeApiDataSource InDirectory(String directory)
    {
        foreach (var set in DataSets.Values) set.InDirectory(directory);
        return this;
    }

    async ValueTask IAsyncLoadable.EnsureLoadedAsync(
        CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    void IFileLoadable.InDirectory(String directory) => InDirectory(directory);

    IRawPokeApiDataSource IFileLoadable<IRawPokeApiDataSource>
        .InDirectory(String directory) =>
        InDirectory(directory);
}