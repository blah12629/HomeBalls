namespace CEo.Pokemon.HomeBalls.Data;

public class HomeBallsDataDbContext :
    DbContext,
    IHomeBallsLoadableDataSource
{
    public HomeBallsDataDbContext(
        DbContextOptions options,
        ILoggerFactory? loggerFactory) :
        base(options)
    {
        LoggerFactory = loggerFactory;
        Logger = LoggerFactory?.CreateLogger<HomeBallsDataDbContext>();
        DataCache = new HomeBallsDataSourceMutable();
    }

    protected internal virtual ILoggerFactory? LoggerFactory { get; }

    protected internal virtual ILogger? Logger { get; }

    public virtual DbSet<EFCoreGameVersion> GameVersions => Set<EFCoreGameVersion>();

    public virtual DbSet<EFCoreGeneration> Generations => Set<EFCoreGeneration>();

    public virtual DbSet<EFCoreItem> Items => Set<EFCoreItem>();

    public virtual DbSet<EFCoreItemCategory> ItemCategories => Set<EFCoreItemCategory>();

    public virtual DbSet<EFCoreLanguage> Languages => Set<EFCoreLanguage>();

    public virtual DbSet<EFCoreMove> Moves => Set<EFCoreMove>();
    
    public virtual DbSet<EFCoreMoveDamageCategory> MoveDamageCategories => Set<EFCoreMoveDamageCategory>();

    public virtual DbSet<EFCoreNature> Natures => Set<EFCoreNature>();

    public virtual DbSet<EFCorePokemonAbility> PokemonAbilities => Set<EFCorePokemonAbility>();

    public virtual DbSet<EFCorePokemonEggGroup> PokemonEggGroups => Set<EFCorePokemonEggGroup>();

    public virtual DbSet<EFCorePokemonForm> PokemonForms => Set<EFCorePokemonForm>();

    public virtual DbSet<EFCorePokemonFormComponent> PokemonFormComponents => Set<EFCorePokemonFormComponent>();

    public virtual DbSet<EFCorePokemonSpecies> PokemonSpecies => Set<EFCorePokemonSpecies>();

    public virtual DbSet<EFCoreStat> Stats => Set<EFCoreStat>();

    public virtual DbSet<EFCoreString> Strings => Set<EFCoreString>();

    public virtual DbSet<EFCoreType> Types => Set<EFCoreType>();

    protected internal IHomeBallsDataSourceMutable DataCache { get; }

    IReadOnlyCollection<IHomeBallsReadOnlyCollection<IHomeBallsEntity>> IHomeBallsDataSource.Entities => DataCache.Entities;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGameVersion> IHomeBallsDataSource.GameVersions => DataCache.GameVersions;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGeneration> IHomeBallsDataSource.Generations => DataCache.Generations;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsItem> IHomeBallsDataSource.Items => DataCache.Items;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsItemCategory> IHomeBallsDataSource.ItemCategories => DataCache.ItemCategories;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsLanguage> IHomeBallsDataSource.Languages => DataCache.Languages;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsMove> IHomeBallsDataSource.Moves => DataCache.Moves;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsMoveDamageCategory> IHomeBallsDataSource.MoveDamageCategories => DataCache.MoveDamageCategories;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsNature> IHomeBallsDataSource.Natures => DataCache.Natures;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonAbility> IHomeBallsDataSource.PokemonAbilities => DataCache.PokemonAbilities;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsPokemonEggGroup> IHomeBallsDataSource.PokemonEggGroups => DataCache.PokemonEggGroups;

    IHomeBallsReadOnlyDataSet<(UInt16 SpeciesId, Byte FormId), IHomeBallsPokemonForm> IHomeBallsDataSource.PokemonForms => DataCache.PokemonForms;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonSpecies> IHomeBallsDataSource.PokemonSpecies => DataCache.PokemonSpecies;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsStat> IHomeBallsDataSource.Stats => DataCache.Stats;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsType> IHomeBallsDataSource.Types => DataCache.Types;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfigurations(modelBuilder);
    }

    protected internal virtual void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        ApplyConfiguration<EFCoreGameVersionConfiguration, EFCoreGameVersion>(modelBuilder);
        ApplyConfiguration<EFCoreGenerationConfiguration, EFCoreGeneration>(modelBuilder);
        ApplyConfiguration<EFCoreItemConfiguration, EFCoreItem>(modelBuilder);
        ApplyConfiguration<EFCoreItemCategoryConfiguration, EFCoreItemCategory>(modelBuilder);
        ApplyConfiguration<EFCoreLanguageConfiguration, EFCoreLanguage>(modelBuilder);
        ApplyConfiguration<EFCoreMoveConfiguration, EFCoreMove>(modelBuilder);
        ApplyConfiguration<EFCoreMoveDamageCategoryConfiguration, EFCoreMoveDamageCategory>(modelBuilder);
        ApplyConfiguration<EFCoreNatureConfiguration, EFCoreNature>(modelBuilder);
        ApplyConfiguration<EFCorePokemonAbilityConfiguration, EFCorePokemonAbility>(modelBuilder);
        ApplyConfiguration<EFCorePokemonAbilitySlotConfiguration, EFCorePokemonAbilitySlot>(modelBuilder);
        ApplyConfiguration<EFCorePokemonEggGroupConfiguration, EFCorePokemonEggGroup>(modelBuilder);
        ApplyConfiguration<EFCorePokemonEggGroupSlotConfiguration, EFCorePokemonEggGroupSlot>(modelBuilder);
        ApplyConfiguration<EFCorePokemonFormConfiguration, EFCorePokemonForm>(modelBuilder);
        ApplyConfiguration<EFCorePokemonFormComponentConfiguration, EFCorePokemonFormComponent>(modelBuilder);
        ApplyConfiguration<EFCorePokemonSpeciesConfiguration, EFCorePokemonSpecies>(modelBuilder);
        ApplyConfiguration<EFCorePokemonTypeSlotConfiguration, EFCorePokemonTypeSlot>(modelBuilder);
        ApplyConfiguration<EFCoreStatConfiguration, EFCoreStat>(modelBuilder);
        ApplyConfiguration<EFCoreStringConfiguration, EFCoreString>(modelBuilder);
        ApplyConfiguration<EFCoreTypeConfiguration, EFCoreType>(modelBuilder);
    }

    protected internal virtual void ApplyConfiguration<TConfiguration, TRecord>(
        ModelBuilder modelBuilder)
        where TConfiguration : notnull, EFCoreRecordConfiguration<TRecord>
        where TRecord : notnull, EFCoreBaseRecord =>
        modelBuilder.ApplyConfiguration(CreateConfifguration<TConfiguration, TRecord>());

    protected internal virtual TConfiguration CreateConfifguration<TConfiguration, TRecord>()
        where TConfiguration : notnull, EFCoreRecordConfiguration<TRecord>
        where TRecord : notnull, EFCoreBaseRecord =>
        (TConfiguration)Activator.CreateInstance(typeof(TConfiguration), new Object?[]
        {
            default(IList<Expression<Action<EntityTypeBuilder<TRecord>>>>),
            LoggerFactory?.CreateLogger<TConfiguration>()
        })!;

    public virtual async Task<HomeBallsDataDbContext> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(
            EnsureDataSetLoadedAsync(
                DataCache.GameVersions,
                GameVersions.Include(version => version.Names),
                cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.Generations, Generations, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.Items, Items, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.ItemCategories, ItemCategories, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.Languages, Languages, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.Moves, Moves, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.MoveDamageCategories, MoveDamageCategories, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.Natures, Natures, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.PokemonAbilities, PokemonAbilities, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.PokemonEggGroups, PokemonEggGroups, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.PokemonForms, PokemonForms, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.PokemonSpecies, PokemonSpecies, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.Stats, Stats, cancellationToken),
            EnsureDataSetLoadedAsync(DataCache.Types, Types, cancellationToken));

        return this;
    }

    protected internal virtual async Task<HomeBallsDataDbContext> EnsureDataSetLoadedAsync<TKey, TRecord, TEntity>(
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        IQueryable<TEntity> dataQueryable,
        CancellationToken cancellationToken = default)
        where TKey : notnull
        where TRecord : notnull, IKeyed, IIdentifiable
        where TEntity : class, TRecord
    {
        var query = dataQueryable;

        if (typeof(TEntity).IsAssignableTo(typeof(INamed)))
            query = query.Include(named => ((INamed)named).Names);

        if (typeof(TEntity).IsAssignableTo(typeof(EFCorePokemonForm)))
            query = query
                .Include(form => ((EFCorePokemonForm)(Object)form).Types)
                .Include(form => ((EFCorePokemonForm)(Object)form).Abilities)
                .Include(form => ((EFCorePokemonForm)(Object)form).EggGroups);

        dataSet.Clear().AddRange(await dataQueryable.ToListAsync());
        return this;
    }

    async ValueTask<IHomeBallsLoadableDataSource> IAsyncLoadable<IHomeBallsLoadableDataSource>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}