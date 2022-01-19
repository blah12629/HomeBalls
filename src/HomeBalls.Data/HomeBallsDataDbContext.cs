namespace CEo.Pokemon.HomeBalls.Data;

public class HomeBallsDataDbContext :
    DbContext,
    IHomeBallsLoadableDataSource,
    IAsyncLoadable<HomeBallsDataDbContext>,
    INotifyDataLoading
{
    public HomeBallsDataDbContext(
        DbContextOptions options,
        ILoggerFactory? loggerFactory) :
        base(options)
    {
        LoggerFactory = loggerFactory;
        Logger = LoggerFactory?.CreateLogger<HomeBallsDataDbContext>();

        var loadables = new List<IAsyncLoadable> { };
        Loadables = loadables.AsReadOnly();

        GameVersionsLoadable = createDbSet<Byte, IHomeBallsGameVersion, EFCoreGameVersion>(
            () => Set<EFCoreGameVersion>(),
            (version, cancellationToken) => version
                .Include(version => version.Names)
                .ToListAsync(cancellationToken));
        GenerationsLoadable = createDbSet<Byte, IHomeBallsGeneration, EFCoreGeneration>(
            () => Set<EFCoreGeneration>(),
            (generation, cancellationToken) => generation
                .Include(generation => generation.Names)
                .ToListAsync(cancellationToken));
        ItemsLoadable = createDbSet<UInt16, IHomeBallsItem, EFCoreItem>(
            () => Set<EFCoreItem>(),
            (item, cancellationToken) => item
                .Include(item => item.Names)
                .ToListAsync(cancellationToken));
        ItemCategoriesLoadable = createDbSet<Byte, IHomeBallsItemCategory, EFCoreItemCategory>(
            () => Set<EFCoreItemCategory>(),
            (category, cancellationToken) => category
                .ToListAsync(cancellationToken));
        LanguagesLoadable = createDbSet<Byte, IHomeBallsLanguage, EFCoreLanguage>(
            () => Set<EFCoreLanguage>(),
            (language, cancellationToken) => language
                .Include(language => language.Names)
                .ToListAsync(cancellationToken));
        LegalitiesLoadable = createDbSet<HomeBallsEntryKey, IHomeBallsEntryLegality, EFCoreEntryLegality>(
            () => Set<EFCoreEntryLegality>(),
            (language, cancellationToken) => language
                .ToListAsync(cancellationToken));
        MovesLoadable = createDbSet<UInt16, IHomeBallsMove, EFCoreMove>(
            () => Set<EFCoreMove>(),
            (move, cancellationToken) => move
                .Include(move => move.Names)
                .ToListAsync(cancellationToken));
        MoveDamageCategoriesLoadable = createDbSet<Byte, IHomeBallsMoveDamageCategory, EFCoreMoveDamageCategory>(
            () => Set<EFCoreMoveDamageCategory>(),
            (category, cancellationToken) => category
                .Include(category => category.Names)
                .ToListAsync(cancellationToken));
        NaturesLoadable = createDbSet<Byte, IHomeBallsNature, EFCoreNature>(
            () => Set<EFCoreNature>(),
            (nature, cancellationToken) => nature
                .Include(nature => nature.Names)
                .ToListAsync(cancellationToken));
        PokemonAbilitiesLoadable = createDbSet<UInt16, IHomeBallsPokemonAbility, EFCorePokemonAbility>(
            () => Set<EFCorePokemonAbility>(),
            (ability, cancellationToken) => ability
                .Include(ability => ability.Names)
                .ToListAsync(cancellationToken));
        PokemonEggGroupsLoadable = createDbSet<Byte, IHomeBallsPokemonEggGroup, EFCorePokemonEggGroup>(
            () => Set<EFCorePokemonEggGroup>(),
            (group, cancellationToken) => group
                .Include(group => group.Names)
                .ToListAsync(cancellationToken));
        PokemonFormsLoadable = createDbSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm, EFCorePokemonForm>(
            () => Set<EFCorePokemonForm>(),
            (form, cancellationToken) => form
                .Include(form => form.Names)
                .Include(form => form.Abilities)
                .Include(form => form.EggGroups)
                .Include(form => form.Types)
                .AsSplitQuery()
                .ToListAsync(cancellationToken));
        PokemonSpeciesLoadable = createDbSet<UInt16, IHomeBallsPokemonSpecies, EFCorePokemonSpecies>(
            () => Set<EFCorePokemonSpecies>(),
            (species, cancellationToken) => species
                .Include(species => species.Names)
                .ToListAsync(cancellationToken));
        StatsLoadable = createDbSet<Byte, IHomeBallsStat, EFCoreStat>(
            () => Set<EFCoreStat>(),
            (stat, cancellationToken) => stat
                .Include(stat => stat.Names)
                .ToListAsync(cancellationToken));
        TypesLoadable = createDbSet<Byte, IHomeBallsType, EFCoreType>(
            () => Set<EFCoreType>(),
            (type, cancellationToken) => type
                .Include(type => type.Names)
                .ToListAsync(cancellationToken));

        HomeBallsLoadableDataDbSet<TKey, TEntity, TRecord> createDbSet<TKey, TEntity, TRecord>(
            Func<DbSet<TRecord>> getDbSet,
            Func<DbSet<TRecord>, CancellationToken, Task<List<TRecord>>> getEntitiesTask)
            where TKey : notnull, IEquatable<TKey>
            where TEntity : notnull, IKeyed<TKey>, IIdentifiable
            where TRecord : class, TEntity
        {
            var dbSet = new HomeBallsLoadableDataDbSet<TKey, TEntity, TRecord>(
                getDbSet,
                async (dbSet, cancellationToken) => await getEntitiesTask.Invoke(dbSet, cancellationToken),
                LoggerFactory?.CreateLogger(typeof(HomeBallsLoadableDataDbSet<TKey, TEntity, TRecord>)));

            loadables.Add(dbSet);
            dbSet.DataLoading += (sender, e) => DataLoading?.Invoke(sender, e);
            dbSet.DataLoaded += (sender, e) => DataLoaded?.Invoke(sender, e);
            return dbSet;
        }
    }

    protected internal virtual ILoggerFactory? LoggerFactory { get; }

    protected internal virtual ILogger? Logger { get; }

    protected internal IReadOnlyCollection<IAsyncLoadable> Loadables { get; }

    public virtual DbSet<EFCoreGameVersion> GameVersions => Set<EFCoreGameVersion>();

    public virtual DbSet<EFCoreGeneration> Generations => Set<EFCoreGeneration>();

    public virtual DbSet<EFCoreItem> Items => Set<EFCoreItem>();

    public virtual DbSet<EFCoreItemCategory> ItemCategories => Set<EFCoreItemCategory>();

    public virtual DbSet<EFCoreLanguage> Languages => Set<EFCoreLanguage>();

    public virtual DbSet<EFCoreEntryLegality> Legalities => Set<EFCoreEntryLegality>();

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

    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsGameVersion, EFCoreGameVersion> GameVersionsLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsGeneration, EFCoreGeneration> GenerationsLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<UInt16, IHomeBallsItem, EFCoreItem> ItemsLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsItemCategory, EFCoreItemCategory> ItemCategoriesLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsLanguage, EFCoreLanguage> LanguagesLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<HomeBallsEntryKey, IHomeBallsEntryLegality, EFCoreEntryLegality> LegalitiesLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<UInt16, IHomeBallsMove, EFCoreMove> MovesLoadable { get; }
    
    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsMoveDamageCategory, EFCoreMoveDamageCategory> MoveDamageCategoriesLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsNature, EFCoreNature> NaturesLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<UInt16, IHomeBallsPokemonAbility, EFCorePokemonAbility> PokemonAbilitiesLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsPokemonEggGroup, EFCorePokemonEggGroup> PokemonEggGroupsLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm, EFCorePokemonForm> PokemonFormsLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<UInt16, IHomeBallsPokemonSpecies, EFCorePokemonSpecies> PokemonSpeciesLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsStat, EFCoreStat> StatsLoadable { get; }

    protected internal virtual HomeBallsLoadableDataDbSet<Byte, IHomeBallsType, EFCoreType> TypesLoadable { get; }

    IHomeBallsLoadableDataSet<Byte, IHomeBallsGameVersion> IHomeBallsLoadableDataSource.GameVersions => GameVersionsLoadable;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsGeneration> IHomeBallsLoadableDataSource.Generations => GenerationsLoadable;

    IHomeBallsLoadableDataSet<UInt16, IHomeBallsItem> IHomeBallsLoadableDataSource.Items => ItemsLoadable;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsItemCategory> IHomeBallsLoadableDataSource.ItemCategories => ItemCategoriesLoadable;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsLanguage> IHomeBallsLoadableDataSource.Languages => LanguagesLoadable;

    IHomeBallsLoadableDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> IHomeBallsLoadableDataSource.Legalities => LegalitiesLoadable;

    IHomeBallsLoadableDataSet<UInt16, IHomeBallsMove> IHomeBallsLoadableDataSource.Moves => MovesLoadable;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsMoveDamageCategory> IHomeBallsLoadableDataSource.MoveDamageCategories => MoveDamageCategoriesLoadable;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsNature> IHomeBallsLoadableDataSource.Natures => NaturesLoadable;

    IHomeBallsLoadableDataSet<UInt16, IHomeBallsPokemonAbility> IHomeBallsLoadableDataSource.PokemonAbilities => PokemonAbilitiesLoadable;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsPokemonEggGroup> IHomeBallsLoadableDataSource.PokemonEggGroups => PokemonEggGroupsLoadable;

    IHomeBallsLoadableDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsLoadableDataSource.PokemonForms => PokemonFormsLoadable;

    IHomeBallsLoadableDataSet<UInt16, IHomeBallsPokemonSpecies> IHomeBallsLoadableDataSource.PokemonSpecies => PokemonSpeciesLoadable;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsStat> IHomeBallsLoadableDataSource.Stats => StatsLoadable;

    IHomeBallsLoadableDataSet<Byte, IHomeBallsType> IHomeBallsLoadableDataSource.Types => TypesLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGameVersion> IHomeBallsDataSource.GameVersions => GameVersionsLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGeneration> IHomeBallsDataSource.Generations => GenerationsLoadable;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsItem> IHomeBallsDataSource.Items => ItemsLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsItemCategory> IHomeBallsDataSource.ItemCategories => ItemCategoriesLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsLanguage> IHomeBallsDataSource.Languages => LanguagesLoadable;

    IHomeBallsReadOnlyDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> IHomeBallsDataSource.Legalities => LegalitiesLoadable;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsMove> IHomeBallsDataSource.Moves => MovesLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsMoveDamageCategory> IHomeBallsDataSource.MoveDamageCategories => MoveDamageCategoriesLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsNature> IHomeBallsDataSource.Natures => NaturesLoadable;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonAbility> IHomeBallsDataSource.PokemonAbilities => PokemonAbilitiesLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsPokemonEggGroup> IHomeBallsDataSource.PokemonEggGroups => PokemonEggGroupsLoadable;

    IHomeBallsReadOnlyDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsDataSource.PokemonForms => PokemonFormsLoadable;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonSpecies> IHomeBallsDataSource.PokemonSpecies => PokemonSpeciesLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsStat> IHomeBallsDataSource.Stats => StatsLoadable;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsType> IHomeBallsDataSource.Types => TypesLoadable;

    public event EventHandler<TimedActionStartingEventArgs>? DataLoading;

    public event EventHandler<TimedActionEndedEventArgs>? DataLoaded;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfigurations(modelBuilder);
    }

    protected internal virtual void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        ApplyConfiguration<EFCoreEntryLegalityConfiguration, EFCoreEntryLegality>(modelBuilder);
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

    public virtual async ValueTask<HomeBallsDataDbContext> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(Loadables.Select(async loadable =>
            await loadable.EnsureLoadedAsync(cancellationToken)));

        return this;
    }

    async ValueTask IAsyncLoadable.EnsureLoadedAsync(CancellationToken cancellationToken) => await EnsureLoadedAsync(cancellationToken);

    async ValueTask<IHomeBallsLoadableDataSource> IAsyncLoadable<IHomeBallsLoadableDataSource>.EnsureLoadedAsync(CancellationToken cancellationToken)  => await EnsureLoadedAsync(cancellationToken);
}