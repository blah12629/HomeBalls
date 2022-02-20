namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsTableGenerator
{
    Task GenerateColumnsAsync(
        IHomeBallsAppStateContainer state,
        IHomeBallsAppSettings settings,
        IHomeBallsDataSource data,
        IHomeBallsLocalStorageEntryCollection entries,
        IHomeBallsEntryColumnFactory columnFactory,
        IHomeBallsEntryTable table,
        Int32 millisecondsDelay = 1,
        CancellationToken cancellationToken = default);

    Task LoadTableDataAsync(
        IHomeBallsAppStateContainer state,
        IHomeBallsLocalStorageDataSource data,
        IHomeBallsLocalStorageEntryCollection entries,
        Int32 postLoadDelay = 50,
        CancellationToken cancellationToken = default);

    Task PostGenerationAsync(
        IHomeBallsAppStateContainer state,
        IHomeBallsLocalStorageDataSource data,
        Int32 postLoadDelay = 50,
        CancellationToken cancellationToken = default);
}

public class HomeBallsTableGenerator : IHomeBallsTableGenerator
{
    public HomeBallsTableGenerator(
        ILogger? logger = default)
    {
        Logger = logger;
    }

    protected internal ILogger? Logger { get; }

    protected internal virtual Task InvokeDelayedAsync(
        Action action,
        Int32 millisecondsDelay = 1,
        CancellationToken cancellationToken = default) =>
        InvokeDelayedAsync(new Action[] { action }, millisecondsDelay, cancellationToken);

    protected internal virtual async Task InvokeDelayedAsync(
        IEnumerable<Action> actions,
        Int32 millisecondsDelay = 1,
        CancellationToken cancellationToken = default)
    {
        foreach (var action in actions)
        {
            if (cancellationToken.IsCancellationRequested) break;
            action();
            await Task.Delay(millisecondsDelay);
        }
    }

    protected internal virtual async Task EnsureLoadedLoggedAsync(
        IHomeBallsAppStateContainer state,
        IAsyncLoadable loadable,
        String loadedItemName,
        Func<Int32> getCount,
        Int32 millisecondsDelay = 50,
        CancellationToken cancellationToken = default)
    {
        state.LoadingMessage.Value = $"Loading {loadedItemName} data...";
        var start = DateTime.UtcNow;
        await loadable.EnsureLoadedAsync(cancellationToken);
        Logger?.LogInformation(
            $"Successfully loaded {getCount()} {loadedItemName} " +
            $"data after {DateTime.UtcNow - start}.");
        await Task.Delay(millisecondsDelay);
    }

    public virtual async Task GenerateColumnsAsync(
        IHomeBallsAppStateContainer state,
        IHomeBallsAppSettings settings,
        IHomeBallsDataSource data,
        IHomeBallsLocalStorageEntryCollection entries,
        IHomeBallsEntryColumnFactory columnFactory,
        IHomeBallsEntryTable table,
        Int32 millisecondsDelay = 1,
        CancellationToken cancellationToken = default)
    {
        state.LoadingMessage.Value = "Generating entries...";

        var start = DateTime.Now;
        var legalityList = data.Legalities.ToList().AsReadOnly();
        var entryList = entries.ToList().AsReadOnly();
        var (i, j, k) = (0, 0, 0);

        foreach (var column in columnFactory
            .UsingItems(data.Pokeballs, true, true)
            .UsingPokemonForms(data.BreedablePokemonForms, true, true)
            .CreateColumns())
        {
            var key = column.Id;
            await InvokeDelayedAsync(() => table.Columns.Add(column), millisecondsDelay, cancellationToken);
            await InvokeDelayedAsync(() => AddToTable(legalityList, table.Legalities, ref j, key), millisecondsDelay, cancellationToken);
            await InvokeDelayedAsync(() => AddToTable(entryList, table.Entries, ref k, key), millisecondsDelay, cancellationToken);
            i += 1;
        }

        Logger?.LogInformation(
            $"`{nameof(IHomeBallsEntryTable)}` generated after " +
            $"{DateTime.Now - start}.");
    }

    protected internal virtual void AddToTable<TEntity>(
        IReadOnlyList<TEntity> entities,
        IAddable<TEntity> addable,
        ref Int32 index,
        HomeBallsPokemonFormKey key)
        where TEntity : notnull, IKeyed<HomeBallsEntryKey>
    {
        for (; index < entities.Count; index++)
        {
            var entity = entities[index];
            if (((HomeBallsPokemonFormKey)entity.Id).CompareTo(key) > 0) break;
            addable.Add(entity);
        }
    }

    public virtual async Task LoadTableDataAsync(
        IHomeBallsAppStateContainer state,
        IHomeBallsLocalStorageDataSource data,
        IHomeBallsLocalStorageEntryCollection entries,
        Int32 postLoadDelay = 50,
        CancellationToken cancellationToken = default)
    {
        await EnsureLoadedLoggedAsync(state, data.Pokeballs, "Poké Ball", () => data.Pokeballs.Count, postLoadDelay, cancellationToken);
        await EnsureLoadedLoggedAsync(state, data.BreedablePokemonForms, "Pokémon", () => data.BreedablePokemonForms.Count, postLoadDelay, cancellationToken);
        await EnsureLoadedLoggedAsync(state, data.Legalities, "legality", () => data.Legalities.Count, postLoadDelay, cancellationToken);
        await EnsureLoadedLoggedAsync(state, entries, "entry", () => entries.Count,  postLoadDelay, cancellationToken);
    }

    public virtual async Task PostGenerationAsync(
        IHomeBallsAppStateContainer state,
        IHomeBallsLocalStorageDataSource data,
        Int32 postLoadDelay = 50,
        CancellationToken cancellationToken = default)
    {
        await EnsureLoadedLoggedAsync(state, data.BreedablePokemonSpecies, "more Pokémon", () => data.BreedablePokemonSpecies.Count, postLoadDelay, cancellationToken);
        state.LoadingMessage.Value = default;
    }
}