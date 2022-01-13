namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryTableFactory
{
    Task<IHomeBallsEntryTable> CreateTableAsync(
        CancellationToken cancellationToken = default);

    Task<IHomeBallsEntryTable> CreateTableAsync(
        IHomeBallsEntryCollection entries,
        CancellationToken cancellationToken = default);
}

public class HomeBallsEntryTableFactory :
    IHomeBallsEntryTableFactory
{
    public HomeBallsEntryTableFactory(
        IHomeBallsDataSource dataSource,
        ILoggerFactory? loggerFactory)
    {
        DataSource = dataSource;
        LoggerFactory = loggerFactory;
    }

    protected internal virtual IHomeBallsDataSource DataSource { get; }

    protected internal virtual ILoggerFactory? LoggerFactory { get; }

    protected internal virtual ILogger? Logger { get; }

    protected internal virtual IReadOnlyList<IHomeBallsEntryCell>? DefaultCells { get; set; }

    public virtual async Task<IHomeBallsEntryTable> CreateTableAsync(
        CancellationToken cancellationToken = default)
    {
        var columns = await CreateColumnsAsync(cancellationToken);
        var indexMap = columns
            .Select((column, index) => (Column: column, Index: index))
            .ToDictionary(
                pair => (pair.Column.SpeciesId, pair.Column.FormId),
                pair => pair.Index);

        return new HomeBallsEntryTable(
            columns,
            indexMap,
            new HomeBallsEntryCollection(),
            LoggerFactory?.CreateLogger(typeof(HomeBallsEntryTable)));;
    }

    public virtual async Task<IHomeBallsEntryTable> CreateTableAsync(
        IHomeBallsEntryCollection entries,
        CancellationToken cancellationToken = default)
    {
        var table = await CreateTableAsync(cancellationToken);
        foreach (var entry in entries) table.Add(entry);
        return table;
    }

    protected internal virtual async Task<List<IHomeBallsEntryColumn>> CreateColumnsAsync(
        CancellationToken cancellationToken = default)
    {
        var loadingTask = DataSource.PokemonForms
            .Where(form => form.IsBreedable)
            .OrderBy(form => form.SpeciesId)
            .ThenBy(form => form.FormId)
            .Select(form => CreateColumnAsync(form, cancellationToken));

        return (await Task.WhenAll(loadingTask)).ToList();
    }

    protected internal virtual async Task<IHomeBallsEntryColumn> CreateColumnAsync(
        IHomeBallsPokemonForm form,
        CancellationToken cancellationToken = default)
    {
        var column = new HomeBallsEntryColumn(
            await CreateCellsAsync(cancellationToken),
            LoggerFactory?.CreateLogger(typeof(HomeBallsEntryColumn)))
        {
            SpeciesId = form.SpeciesId,
            FormId = form.FormId
        };

        return column;
    }

    protected internal virtual async Task<IReadOnlyList<IHomeBallsEntryCell>> CreateCellsAsync(
        CancellationToken cancellationToken = default)
    {
        await EnsureDefaultCellsLoadedAsync(cancellationToken);
        return DefaultCells!.Select(cell => cell.Clone()).ToList().AsReadOnly();
    }

    protected internal virtual ValueTask<HomeBallsEntryTableFactory> EnsureDefaultCellsLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (DefaultCells == default) DefaultCells = DataSource.Items
            .Where(item => item.Identifier.Contains("ball"))
            .OrderBy(item => item, new HomeBallsPokeballComparer().UseGameIndexComparison())
            .Select(item => new HomeBallsEntryCell { BallId = item.Id })
            .ToList().AsReadOnly();

        return ValueTask.FromResult(this);
    }
}