namespace CEo.Pokemon.HomeBalls.Data;

public interface IHomeBallsDataRepository
{
    Task<IReadOnlyList<HomeBallsItem>> GetItemsAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<HomeBallsPokemonForm>> GetPokemonFormsAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        Boolean includeSpecies = false,
        CancellationToken cancellationToken = default);

    Task RemoveEntitiesAsync<TEntity>(
        DbSet<TEntity> dbSet,
        Expression<Func<TEntity, Boolean>> condition,
        Func<CancellationToken, Task<Int32>> saveTask,
        CancellationToken cancellationToken = default)
        where TEntity : class;

    Task RemoveEntitiesAsync<TEntity>(
        DbSet<TEntity> dbSet,
        IQueryable<TEntity> entities,
        Expression<Func<TEntity, Boolean>> condition,
        Func<CancellationToken, Task<Int32>> saveTask,
        CancellationToken cancellationToken = default)
        where TEntity : class;
}

public class HomeBallsDataRepository : IHomeBallsDataRepository
{
    public HomeBallsDataRepository(
        ILogger? logger = default) =>
        Logger = logger;

    protected internal ILogger? Logger { get; }

    protected internal virtual async Task<IReadOnlyList<TEntity>> GetEntitiesAsync<TEntity>(
        Func<IHomeBallsBaseDataDbContext, IQueryable<TEntity>> queryable,
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await using var data = getDataContext();
        var entities = await queryable(data).AsNoTracking().ToListAsync(cancellationToken);
        return entities.AsReadOnly();
    }

    public virtual Task<IReadOnlyList<HomeBallsPokemonForm>> GetPokemonFormsAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        Boolean includeSpecies = false,
        CancellationToken cancellationToken = default) =>
        GetEntitiesAsync(
            data =>
            {
                IQueryable<HomeBallsPokemonForm> queryable = data.PokemonForms;
                return includeSpecies ?
                    queryable.Include(form => form.Species) :
                    queryable;
            },
            getDataContext,
            cancellationToken);

    public virtual Task<IReadOnlyList<HomeBallsItem>> GetItemsAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default) =>
        GetEntitiesAsync(data => data.Items, getDataContext, cancellationToken);

    public virtual Task RemoveEntitiesAsync<TEntity>(
        DbSet<TEntity> dbSet,
        Expression<Func<TEntity, Boolean>> condition,
        Func<CancellationToken, Task<Int32>> saveTask,
        CancellationToken cancellationToken = default)
        where TEntity : class =>
        RemoveEntitiesAsync(dbSet, dbSet, condition, saveTask, cancellationToken);

    public virtual async Task RemoveEntitiesAsync<TEntity>(
        DbSet<TEntity> dbSet,
        IQueryable<TEntity> entities,
        Expression<Func<TEntity, Boolean>> condition,
        Func<CancellationToken, Task<Int32>> saveTask,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var removed = await entities.AsNoTracking().Where(condition).ToListAsync();
        dbSet.AttachRange(removed);
        dbSet.RemoveRange(removed);

        var count = await saveTask(cancellationToken);
        Logger?.LogInformation($"{count} `{typeof(TEntity).Name}` removed.");
    }
}