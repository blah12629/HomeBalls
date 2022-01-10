namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryTableFactory
{
    Task<IHomeBallsEntryTable> CreateTableAsync(
        IHomeBallsEntryCollection entries,
        CancellationToken cancellationToken = default);
}

public class HomeBallsEntryTableFactory :
    IHomeBallsEntryTableFactory
{
    public HomeBallsEntryTableFactory(
        IHomeBallsLocalStorageDataSource dataSource,
        ILoggerFactory? loggerFactory)
    {
        DataSource = dataSource;
        LoggerFactory = loggerFactory;
    }

    protected internal virtual IHomeBallsLocalStorageDataSource DataSource { get; }

    protected internal virtual ILoggerFactory? LoggerFactory { get; }

    protected internal virtual ILogger? Logger { get; }

    protected internal virtual IReadOnlyList<IHomeBallsEntryCell>? DefaultCells { get; set; }

    public virtual async Task<IHomeBallsEntryTable> CreateTableAsync(
        IHomeBallsEntryCollection entries,
        CancellationToken cancellationToken = default)
    {
        var columns = await CreateColumnsAsync(cancellationToken);
        var indexMap = columns
            .Select((column, index) => (Column: column, Index: index))
            .ToDictionary(
                pair => (pair.Column.SpeciesId, pair.Column.FormId),
                pair => pair.Index);

        var table = new HomeBallsEntryTable(
            columns,
            indexMap,
            new HomeBallsEntryCollection(),
            LoggerFactory.CreateLogger(typeof(HomeBallsEntryTable)));
        foreach (var entry in entries) table.Add(entry);

        return table;
    }

    protected internal virtual async Task<IReadOnlyList<IHomeBallsEntryColumn>> CreateColumnsAsync(
        CancellationToken cancellationToken = default)
    {
        await DataSource.EnsureLoadedAsync(cancellationToken);
        var loadingTask = DataSource.PokemonForms
            .Where(form => form.IsBreedable)
            .OrderBy(form => form.SpeciesId)
            .ThenBy(form => form.FormId)
            .Select(async form => new HomeBallsEntryColumn(
                await CreateCellsAsync(cancellationToken),
                LoggerFactory.CreateLogger(typeof(HomeBallsEntryColumn))));

        return (await Task.WhenAll(loadingTask)).ToList().AsReadOnly();
    }

    protected internal virtual async Task<IReadOnlyList<IHomeBallsEntryCell>> CreateCellsAsync(
        CancellationToken cancellationToken = default)
    {
        await EnsureDefaultCellsLoadedAsync(cancellationToken);
        return DefaultCells!.Select(cell => cell.Clone()).ToList().AsReadOnly();
    }

    protected internal virtual async ValueTask<HomeBallsEntryTableFactory> EnsureDefaultCellsLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (DefaultCells != default) return this;

        await DataSource.EnsureLoadedAsync(cancellationToken);
        DefaultCells = DataSource.Items
            .Where(item => item.Identifier.Contains("ball"))
            .OrderBy(item => item.Id)
            .Select(item => new HomeBallsEntryCell { BallId = item.Id })
            .ToList().AsReadOnly();

        return this;
    }
}