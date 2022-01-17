namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public class RawPokeApiDataInitializer :
    IPokeApiDataInitializer
{
    public RawPokeApiDataInitializer(
        IRawPokeApiDataSource rawData,
        IRawPokeApiHomeBallsConverter converter,
        ILogger? logger = default)
    {
        (RawData, Converter) = (rawData, converter);
        Logger = logger;

        var recordCollections = new List<IEnumerable<EFCoreBaseRecord>> { };
        GameVersions = add(new HomeBallsDataSet<Byte, EFCoreGameVersion> { });
        Generations = add(new HomeBallsDataSet<Byte, EFCoreGeneration> { });
        Items = add(new HomeBallsDataSet<UInt16, EFCoreItem> { });
        ItemCategories = add(new HomeBallsDataSet<Byte, EFCoreItemCategory> { });
        Languages = add(new HomeBallsDataSet<Byte, EFCoreLanguage> { });
        Moves = add(new HomeBallsDataSet<UInt16, EFCoreMove> { });
        MoveDamageCategories = add(new HomeBallsDataSet<Byte, EFCoreMoveDamageCategory> { });
        Natures = add(new HomeBallsDataSet<Byte, EFCoreNature> { });
        PokemonAbilities = add(new HomeBallsDataSet<UInt16, EFCorePokemonAbility> { });
        PokemonEggGroups = add(new HomeBallsDataSet<Byte, EFCorePokemonEggGroup> { });
        PokemonForms = add(new HomeBallsDataSet<HomeBallsPokemonFormKey, EFCorePokemonForm>());
        PokemonSpecies = add(new HomeBallsDataSet<UInt16, EFCorePokemonSpecies> { });
        Stats = add(new HomeBallsDataSet<Byte, EFCoreStat> { });
        Types = add(new HomeBallsDataSet<Byte, EFCoreType> { });
        RecordCollections = recordCollections.AsReadOnly();

        IHomeBallsDataSet<TKey, TRecord> add<TKey, TRecord>(
            IHomeBallsDataSet<TKey, TRecord> dataSet)
            where TKey : notnull, IEquatable<TKey>
            where TRecord : notnull, EFCoreBaseRecord, IKeyed<TKey>, IIdentifiable
        {
            recordCollections.Add(dataSet);
            return dataSet;
        }
    }

    protected internal IReadOnlyCollection<IEnumerable<EFCoreBaseRecord>> RecordCollections { get; }

    protected internal IHomeBallsDataSet<Byte, EFCoreGameVersion> GameVersions { get; }

    protected internal IHomeBallsDataSet<Byte, EFCoreGeneration> Generations { get; }

    protected internal IHomeBallsDataSet<Byte, EFCoreLanguage> Languages { get; }

    protected internal IHomeBallsDataSet<UInt16, EFCoreItem> Items { get; }

    protected internal IHomeBallsDataSet<Byte, EFCoreItemCategory> ItemCategories { get; }

    protected internal IHomeBallsDataSet<UInt16, EFCoreMove> Moves { get; }

    protected internal IHomeBallsDataSet<Byte, EFCoreMoveDamageCategory> MoveDamageCategories { get; }

    protected internal IHomeBallsDataSet<Byte, EFCoreNature> Natures { get; }

    protected internal IHomeBallsDataSet<UInt16, EFCorePokemonAbility> PokemonAbilities { get; }

    protected internal IHomeBallsDataSet<Byte, EFCorePokemonEggGroup> PokemonEggGroups { get; }

    // protected internal IHomeBallsDataSet<(UInt16 SpeciesId, Byte FormId), EFCorePokemonForm> PokemonForms { get; }
    protected internal IHomeBallsDataSet<HomeBallsPokemonFormKey, EFCorePokemonForm> PokemonForms { get; }

    protected internal IHomeBallsDataSet<UInt16, EFCorePokemonSpecies> PokemonSpecies { get; }

    protected internal IHomeBallsDataSet<Byte, EFCoreStat> Stats { get; }

    protected internal IHomeBallsDataSet<Byte, EFCoreType> Types { get; }

    protected internal IRawPokeApiDataSource RawData { get; }

    protected internal IRawPokeApiHomeBallsConverter Converter { get; }

    protected internal ILogger? Logger { get; }

    public virtual async Task<RawPokeApiDataInitializer> StartConversionAsync(
        CancellationToken cancellationToken = default)
    {
        await RawData.EnsureLoadedAsync(cancellationToken);
        await Task.WhenAll(new[]
        {
            // AddRangeAsync(GameVersions, new IAsyncLoadable[] { RawData.Versions, RawData.VersionGroups, RawData.VersionNames }, () => Converter.Convert(RawData.Versions, RawData.VersionGroups, RawData.VersionNames)),
            // AddRangeAsync(Generations, new IAsyncLoadable[] { RawData.Generations, RawData.GenerationNames }, () => Converter.Convert(RawData.Generations, RawData.GenerationNames)),
            // AddRangeAsync(Items, new IAsyncLoadable[] { RawData.Items, RawData.ItemNames }, () => Converter.Convert(RawData.Items, RawData.ItemNames)),
            // AddRangeAsync(ItemCategories, new IAsyncLoadable[] { RawData.ItemCategories }, () => Converter.Convert(RawData.ItemCategories)),
            // AddRangeAsync(Languages, new IAsyncLoadable[] { RawData.Languages, RawData.LanguageNames }, () => Converter.Convert(RawData.Languages, RawData.LanguageNames)),
            // AddRangeAsync(Moves, new IAsyncLoadable[] { RawData.Moves, RawData.MoveNames }, () => Converter.Convert(RawData.Moves, RawData.MoveNames)),
            // AddRangeAsync(MoveDamageCategories, new IAsyncLoadable[] { RawData.MoveDamageClasses, RawData.MoveDamageClassProse }, () => Converter.Convert(RawData.MoveDamageClasses, RawData.MoveDamageClassProse)),
            // AddRangeAsync(Natures, new IAsyncLoadable[] { RawData.Natures, RawData.NatureNames }, () => Converter.Convert(RawData.Natures, RawData.NatureNames)),
            // AddRangeAsync(PokemonAbilities, new IAsyncLoadable[] { RawData.Abilities, RawData.AbilityNames }, () => Converter.Convert(RawData.Abilities, RawData.AbilityNames)),
            // AddRangeAsync(PokemonEggGroups, new IAsyncLoadable[] { RawData.EggGroups, RawData.EggGroupProse }, () => Converter.Convert(RawData.EggGroups, RawData.EggGroupProse)),
            // AddRangeAsync(PokemonForms, new IAsyncLoadable[] { RawData.PokemonForms, RawData.Pokemon, RawData.PokemonSpecies, RawData.PokemonAbilities, RawData.PokemonEggGroups, RawData.PokemonTypes, RawData.PokemonFormNames }, () => Converter.Convert(RawData.PokemonForms, RawData.Pokemon, RawData.PokemonSpecies, RawData.PokemonAbilities, RawData.PokemonEggGroups, RawData.PokemonTypes, RawData.PokemonFormNames)),
            // AddRangeAsync(PokemonSpecies, new IAsyncLoadable[] { RawData.PokemonSpecies, RawData.PokemonSpeciesNames }, () => Converter.Convert(RawData.PokemonSpecies, RawData.PokemonSpeciesNames)),
            // AddRangeAsync(Stats, new IAsyncLoadable[] { RawData.Stats, RawData.StatNames }, () => Converter.Convert(RawData.Stats, RawData.StatNames)),
            // AddRangeAsync(Types, new IAsyncLoadable[] { RawData.Types, RawData.TypeNames }, () => Converter.Convert(RawData.Types, RawData.TypeNames))
            AddRangeAsync(GameVersions, () => Converter.Convert(RawData.Versions, RawData.VersionGroups, RawData.VersionNames)),
            AddRangeAsync(Generations, () => Converter.Convert(RawData.Generations, RawData.GenerationNames)),
            AddRangeAsync(Items, () => Converter.Convert(RawData.Items, RawData.ItemNames)),
            AddRangeAsync(ItemCategories, () => Converter.Convert(RawData.ItemCategories)),
            AddRangeAsync(Languages, () => Converter.Convert(RawData.Languages, RawData.LanguageNames)),
            AddRangeAsync(Moves, () => Converter.Convert(RawData.Moves, RawData.MoveNames)),
            AddRangeAsync(MoveDamageCategories, () => Converter.Convert(RawData.MoveDamageClasses, RawData.MoveDamageClassProse)),
            AddRangeAsync(Natures, () => Converter.Convert(RawData.Natures, RawData.NatureNames)),
            AddRangeAsync(PokemonAbilities, () => Converter.Convert(RawData.Abilities, RawData.AbilityNames)),
            AddRangeAsync(PokemonEggGroups, () => Converter.Convert(RawData.EggGroups, RawData.EggGroupProse)),
            AddRangeAsync(PokemonForms, () => Converter.Convert(RawData.PokemonForms, RawData.Pokemon, RawData.PokemonSpecies, RawData.PokemonAbilities, RawData.PokemonEggGroups, RawData.PokemonTypes, RawData.PokemonFormNames)),
            AddRangeAsync(PokemonSpecies, () => Converter.Convert(RawData.PokemonSpecies, RawData.PokemonSpeciesNames)),
            AddRangeAsync(Stats, () => Converter.Convert(RawData.Stats, RawData.StatNames)),
            AddRangeAsync(Types, () => Converter.Convert(RawData.Types, RawData.TypeNames))
        });

        return this;
    }

    // // This will cause file access conflicts when the same data set is used on different dataUsed instances.
    // protected internal virtual async Task<RawPokeApiDataInitializer> AddRangeAsync<TKey, TRecord>(
    //     IHomeBallsDataSet<TKey, TRecord> dataSet,
    //     IEnumerable<IAsyncLoadable> dataUsed,
    //     Func<IEnumerable<TRecord>> getData,
    //     CancellationToken cancellationToken = default)
    //     where TKey : notnull
    //     where TRecord : notnull, IKeyed, IIdentifiable
    //     {
    //         await Task.WhenAll(dataUsed.Select(async data =>
    //             await data.EnsureLoadedAsync(cancellationToken)));

    //         dataSet.Clear().AddRange(getData());
    //         return this;
    //     }

    protected internal virtual Task<RawPokeApiDataInitializer> AddRangeAsync<TKey, TRecord>(
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        Func<IEnumerable<TRecord>> getData,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TRecord : notnull, IKeyed<TKey>, IIdentifiable
        {
            dataSet.Clear().AddRange(getData());
            return Task.FromResult(this);
        }

    public virtual Task<RawPokeApiDataInitializer> PostProcessDataAsync(
        CancellationToken cancellationToken = default) =>
        Task.FromResult(this);

    public virtual async Task<RawPokeApiDataInitializer> SaveToDataDbContextAsync(
        HomeBallsDataDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        await dbContext.AddRangeAsync(
            RecordCollections.SelectMany(record => record),
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        dbContext.ChangeTracker.Clear();
        return this;
    }

    async Task<IPokeApiDataInitializer> IHomeBallsDataInitializer<IPokeApiDataInitializer>
        .StartConversionAsync(CancellationToken cancellationToken) =>
        await StartConversionAsync(cancellationToken);

    async Task<IPokeApiDataInitializer> IHomeBallsDataInitializer<IPokeApiDataInitializer>
        .SaveToDataDbContextAsync(
            HomeBallsDataDbContext dbContext,
            CancellationToken cancellationToken) =>
        await SaveToDataDbContextAsync(dbContext, cancellationToken);

    async Task IHomeBallsDataInitializer
        .StartConversionAsync(CancellationToken cancellationToken) =>
        await StartConversionAsync(cancellationToken);

    async Task IHomeBallsDataInitializer
        .SaveToDataDbContextAsync(
            HomeBallsDataDbContext dbContext,
            CancellationToken cancellationToken) =>
        await SaveToDataDbContextAsync(dbContext, cancellationToken);

    async Task<IPokeApiDataInitializer> IHomeBallsDataInitializer<IPokeApiDataInitializer>
        .PostProcessDataAsync(CancellationToken cancellationToken) =>
        await PostProcessDataAsync(cancellationToken);        

    async Task IHomeBallsDataInitializer
        .PostProcessDataAsync(CancellationToken cancellationToken) =>
        await PostProcessDataAsync(cancellationToken);
}