namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IHomeBallsDataSourceGenerator
{
    Task<IHomeBallsDataSet<TKey, TEntity>> GenerateDataSetAsync<TKey, TEntity>(
        IQueryable<TEntity> data,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity :
            class,
            HomeBalls.Entities.IKeyed<TKey>,
            HomeBalls.Entities.IIdentifiable,
            new();

    Task<IHomeBallsDataDataSource> GenerateDataSourceAsync(
        Func<IHomeBallsDataDbContext> getData,
        CancellationToken cancellationToken = default);
}

public class HomeBallsDataSourceGenerator : IHomeBallsDataSourceGenerator
{
    public HomeBallsDataSourceGenerator(
        IHomeBallsPokemonFormKeyComparer pokemonComparer,
        IHomeBallsItemIdComparer itemComparer,
        ILogger? logger = default)
    {
        (PokemonComparer, ItemComparer) = (pokemonComparer, itemComparer);
        Logger = logger;
    }

    protected internal IHomeBallsPokemonFormKeyComparer PokemonComparer { get; }

    protected internal IHomeBallsItemIdComparer ItemComparer { get; }

    protected internal ILogger? Logger { get; }

    public async Task<IHomeBallsDataDataSource> GenerateDataSourceAsync(
        Func<IHomeBallsDataDbContext> getData,
        CancellationToken cancellationToken = default)
    {
        await using var data = getData();
        return new HomeBallsDataDataSource(Logger)
        {
            GameVersions = await GenerateDataSetAsync(data.GameVersions, cancellationToken),
            Generations = await GenerateDataSetAsync(data.Generations, cancellationToken),
            Items = await GenerateDataSetAsync(data.Items, cancellationToken),
            ItemCategories = await GenerateDataSetAsync(data.ItemCategories, cancellationToken),
            Languages = await GenerateDataSetAsync(data.Languages, cancellationToken),
            Legalities = await GenerateDataSetAsync(data.Legalities, cancellationToken),
            Moves = await GenerateDataSetAsync(data.Moves, cancellationToken),
            MoveDamageCategories = await GenerateDataSetAsync(data.MoveDamageCategories, cancellationToken),
            Natures = await GenerateDataSetAsync(data.Natures, cancellationToken),
            PokemonAbilities = await GenerateDataSetAsync(data.PokemonAbilities, cancellationToken),
            PokemonEggGroups = await GenerateDataSetAsync(data.PokemonEggGroups, cancellationToken),
            PokemonForms = await GenerateDataSetAsync(data.PokemonForms, cancellationToken),
            PokemonSpecies = await GenerateDataSetAsync(data.PokemonSpecies, cancellationToken),
            Stats = await GenerateDataSetAsync(data.Stats, cancellationToken),
            Types = await GenerateDataSetAsync(data.Types, cancellationToken),
            BreedablePokemonForms = await GenerateDataSetAsync(
                data.PokemonForms.Where(form => form.IsBreedable),
                cancellationToken),
            BreedablePokemonSpecies = await GenerateDataSetAsync(
                data.PokemonSpecies.Where(species => species.Forms.Any(form => form.IsBreedable)),
                cancellationToken),
            Pokeballs = await GenerateDataSetAsync(
                data.Items.Where(item => item.Identifier.Contains("ball")),
                cancellationToken),
        };
    }

    public virtual Task<IHomeBallsDataSet<TKey, TEntity>> GenerateDataSetAsync<TKey, TEntity>(
        IQueryable<TEntity> data,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity : class, HomeBalls.Entities.IKeyed<TKey>, HomeBalls.Entities.IIdentifiable, new() =>
        GenerateDataSetAsync<TKey, TEntity>(data, entities => entities, cancellationToken);

    public virtual async Task<IHomeBallsDataSet<TKey, TEntity>> GenerateDataSetAsync<TKey, TEntity>(
        IQueryable<TEntity> data,
        Func<IEnumerable<TEntity>, IEnumerable<TEntity>> sort,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity : class, HomeBalls.Entities.IKeyed<TKey>, HomeBalls.Entities.IIdentifiable, new()
    {
        var set = new HomeBallsDataSet<TKey, TEntity> { };
        var entities = sort(await data.AsNoTracking().ToListAsync(cancellationToken));
        set.AddRange(entities);
        return set;
    }

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsGameVersion>> GenerateDataSetAsync(
        IQueryable<HomeBallsGameVersion> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsGameVersion>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsGeneration>> GenerateDataSetAsync(
        IQueryable<HomeBallsGeneration> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsGeneration>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<UInt16, HomeBallsItem>> GenerateDataSetAsync(
        IQueryable<HomeBallsItem> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<UInt16, HomeBallsItem>(
            data.Include(entity => entity.Names),
            items => items.OrderBy(item => item.Id, ItemComparer),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsItemCategory>> GenerateDataSetAsync(
        IQueryable<HomeBallsItemCategory> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsItemCategory>(
            data,
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsLanguage>> GenerateDataSetAsync(
        IQueryable<HomeBallsLanguage> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsLanguage>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<HomeBalls.Entities.HomeBallsEntryKey, HomeBallsEntryLegality>> GenerateDataSetAsync(
        IQueryable<HomeBallsEntryLegality> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<HomeBalls.Entities.HomeBallsEntryKey, HomeBallsEntryLegality>(
            data,
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<UInt16, HomeBallsMove>> GenerateDataSetAsync(
        IQueryable<HomeBallsMove> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<UInt16, HomeBallsMove>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsMoveDamageCategory>> GenerateDataSetAsync(
        IQueryable<HomeBallsMoveDamageCategory> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsMoveDamageCategory>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsNature>> GenerateDataSetAsync(
        IQueryable<HomeBallsNature> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsNature>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<UInt16, HomeBallsPokemonAbility>> GenerateDataSetAsync(
        IQueryable<HomeBallsPokemonAbility> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<UInt16, HomeBallsPokemonAbility>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsPokemonEggGroup>> GenerateDataSetAsync(
        IQueryable<HomeBallsPokemonEggGroup> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsPokemonEggGroup>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm>> GenerateDataSetAsync(
        IQueryable<HomeBallsPokemonForm> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm>(
            data.Include(entity => entity.Names)
                .Include(entity => entity.Abilities)
                .Include(entity => entity.EggGroups)
                .Include(entity => entity.Types),
            forms => forms.OrderBy(form => form.Id, PokemonComparer),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<UInt16, HomeBallsPokemonSpecies>> GenerateDataSetAsync(
        IQueryable<HomeBallsPokemonSpecies> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<UInt16, HomeBallsPokemonSpecies>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsStat>> GenerateDataSetAsync(
        IQueryable<HomeBallsStat> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsStat>(
            data.Include(entity => entity.Names),
            cancellationToken);

    protected internal Task<IHomeBallsDataSet<Byte, HomeBallsType>> GenerateDataSetAsync(
        IQueryable<HomeBallsType> data,
        CancellationToken cancellationToken = default) =>
        GenerateDataSetAsync<Byte, HomeBallsType>(
            data.Include(entity => entity.Names),
            cancellationToken);
}