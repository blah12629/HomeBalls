namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IHomeBallsDataCacheCommiter
{
    Task<IHomeBallsDataCacheCommiter> CommitEntitiesAsync(
        Func<HomeBallsDataDbContext> getData,
        Func<HomeBallsDataDbContextCache> getCache,
        Boolean overwriteData = true,
        CancellationToken cancellationToken = default);

    Task<IHomeBallsDataCacheCommiter> CommitEntitiesAsync<TEntity>(
        IQueryable<TEntity> cachedSet,
        IHomeBallsBaseDataDbContext data,
        CancellationToken cancellationToken = default)
        where TEntity : class;

    Task<IHomeBallsDataCacheCommiter> CommitEntitiesAsync(
        IQueryable<HomeBallsString> cachedStrings,
        IHomeBallsBaseDataDbContext data,
        CancellationToken cancellationToken = default);
}

public class HomeBallsDataCacheCommiter : IHomeBallsDataCacheCommiter
{
    public HomeBallsDataCacheCommiter(
        ILogger? logger = default)
    {
        Logger = logger;
    }

    protected internal ILogger? Logger { get; }

    public async Task<HomeBallsDataCacheCommiter> CommitEntitiesAsync(
        Func<HomeBallsDataDbContext> getData,
        Func<HomeBallsDataDbContextCache> getCache,
        Boolean overwriteData = true,
        CancellationToken cancellationToken = default)
    {
        await using var data = getData();
        await using var cache = getCache();
    
        if (overwriteData) await data.Database.EnsureDeletedAsync(cancellationToken);
        await data.Database.EnsureCreatedAsync(cancellationToken);
        await cache.Database.EnsureCreatedAsync(cancellationToken);

        var tasks = new Task<HomeBallsDataCacheCommiter>[]
        {
            CommitEntitiesAsync(cache.Legalities.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.GameVersions.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.Generations.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.Items.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.ItemCategories.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.Languages.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.Moves.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.MoveDamageCategories.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.Natures.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.PokemonAbilities.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.PokemonAbilitySlots.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.PokemonEggGroups.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.PokemonEggGroupSlots.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.PokemonForms.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.PokemonSpecies.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.PokemonTypeSlots.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.Stats.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.Strings.AsNoTracking(), data, cancellationToken),
            CommitEntitiesAsync(cache.Types.AsNoTracking(), data, cancellationToken),
        };
        await Task.WhenAll(tasks);
        await data.SaveChangesAsync(cancellationToken);
        await data.Database.VacuumAsync(cancellationToken);
        return this;
    }

    public virtual async Task<HomeBallsDataCacheCommiter> CommitEntitiesAsync<TEntity>(
        IQueryable<TEntity> cachedSet,
        IHomeBallsBaseDataDbContext data,
        CancellationToken cancellationToken = default)
        where TEntity : class =>
        await CommitEntitiesAsync(
            await cachedSet.ToListAsync(cancellationToken),
            data,
            cancellationToken);

    public virtual async Task<HomeBallsDataCacheCommiter> CommitEntitiesAsync(
        IQueryable<HomeBallsString> cachedStrings,
        IHomeBallsBaseDataDbContext data,
        CancellationToken cancellationToken = default) =>
        await CommitEntitiesAsync<HomeBallsString>(
            (await cachedStrings.ToListAsync(cancellationToken))
                .Select(@string => @string with { Id = 0 })
                .ToList(),
            data,
            cancellationToken);

    protected internal virtual async Task<HomeBallsDataCacheCommiter> CommitEntitiesAsync<TEntity>(
        IReadOnlyList<TEntity> cachedEntities,
        IHomeBallsBaseDataDbContext data,
        CancellationToken cancellationToken = default)
    {
        var entities = (IEnumerable<Object>)cachedEntities;
        await data.AddRangeAsync(entities, cancellationToken);
        return this;
    }

    async Task<IHomeBallsDataCacheCommiter> IHomeBallsDataCacheCommiter
        .CommitEntitiesAsync(
            Func<HomeBallsDataDbContext> getData,
            Func<HomeBallsDataDbContextCache> getCache,
            Boolean overwriteData,
            CancellationToken cancellationToken) =>
        await CommitEntitiesAsync(getData, getCache, overwriteData, cancellationToken);

    async Task<IHomeBallsDataCacheCommiter> IHomeBallsDataCacheCommiter
        .CommitEntitiesAsync<TEntity>(
            IQueryable<TEntity> cachedSet,
            IHomeBallsBaseDataDbContext data,
            CancellationToken cancellationToken) =>
        await CommitEntitiesAsync(cachedSet.AsNoTracking(), data, cancellationToken);

    async Task<IHomeBallsDataCacheCommiter> IHomeBallsDataCacheCommiter
        .CommitEntitiesAsync(
            IQueryable<HomeBallsString> cachedStrings,
            IHomeBallsBaseDataDbContext data,
            CancellationToken cancellationToken) =>
        await CommitEntitiesAsync(cachedStrings.AsNoTracking(), data, cancellationToken);
}