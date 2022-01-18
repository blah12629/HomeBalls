namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryTableFactory :
    INotifyDataDownloading,
    INotifyDataLoading,
    INotifyTableCreating
{
    Task<IHomeBallsEntryTable> CreateTableAsync(
        CancellationToken cancellationToken = default);
}

public class HomeBallsEntryTableFactory :
    IHomeBallsEntryTableFactory
{
    IReadOnlyList<IHomeBallsEntryCell>? _defaultCells;
    IReadOnlyDictionary<UInt16, Int32>? _defaultIndexMap;

    public HomeBallsEntryTableFactory(
        IHomeBallsLocalStorageDataSource dataSource,
        ILoggerFactory? loggerFactory)
    {
        DataSource = dataSource;
        LoggerFactory = loggerFactory;

        EventRaiser = new EventRaiser();
        Logger = LoggerFactory?.CreateLogger<HomeBallsEntryTableFactory>();
    }

    protected internal virtual IHomeBallsLocalStorageDataSource DataSource { get; }

    protected internal virtual ILoggerFactory? LoggerFactory { get; }

    protected internal virtual IEventRaiser EventRaiser { get; }

    protected internal virtual ILogger? Logger { get; }

    protected internal virtual IReadOnlyList<IHomeBallsEntryCell>? DefaultCells
    {
        get => _defaultCells;
        set
        {
            if (_defaultCells == value) return;
            _defaultCells = value;
            _defaultIndexMap = default;
        }
    }

    protected internal virtual IReadOnlyDictionary<UInt16, Int32> DefaultIndexMap =>
        _defaultIndexMap ??= DefaultCells?
            .Select((cell, index) => (Id: cell.BallId, Index: index))
            .ToDictionary(pair => pair.Id, pair => pair.Index).AsReadOnly() ??
            throw new ArgumentException($"Parameter not set.", nameof(DefaultCells));

    public event EventHandler<TimedActionStartingEventArgs>? DataDownloading
    {
        add => DataSource.DataDownloading += value;
        remove => DataSource.DataDownloading -= value;
    }

    public event EventHandler<TimedActionEndedEventArgs>? DataDownloaded
    {
        add => DataSource.DataDownloaded += value;
        remove => DataSource.DataDownloaded -= value;
    }

    public event EventHandler<TimedActionStartingEventArgs>? DataLoading
    {
        add => DataSource.DataLoading += value;
        remove => DataSource.DataLoading -= value;
    }

    public event EventHandler<TimedActionEndedEventArgs>? DataLoaded
    {
        add => DataSource.DataLoaded += value;
        remove => DataSource.DataLoaded -= value;
    }

    public event EventHandler<TimedActionStartingEventArgs>? TableCreating;

    public event EventHandler<TimedActionEndedEventArgs>? TableCreated;

    public virtual async Task<IHomeBallsEntryTable> CreateTableAsync(
        CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync(cancellationToken);

        var start = EventRaiser.Raise(TableCreating);
        var columns = await CreateColumnsAsync(cancellationToken);
        var indexMap = columns
            .Select((column, index) => (Column: column, Index: index))
            .ToDictionary(pair => pair.Column.Id, pair => pair.Index);

        var table = new HomeBallsEntryTable(columns, indexMap, LoggerFactory);
        EventRaiser.Raise(TableCreated, start.StartTime);
        return table;
    }

    protected internal virtual async Task<List<IHomeBallsEntryColumn>> CreateColumnsAsync(
        CancellationToken cancellationToken = default)
    {
        var loadingTask = DataSource.PokemonForms
            .Where(form => form.IsBreedable)
            .OrderBy(form => form, new HomeBallsPokemonFormComparer())
            .Select(form => CreateColumnAsync(form, cancellationToken));

        return (await Task.WhenAll(loadingTask)).ToList();
    }

    protected internal virtual async Task<IHomeBallsEntryColumn> CreateColumnAsync(
        IHomeBallsPokemonForm form,
        CancellationToken cancellationToken = default)
    {
        var cells = await CreateCellsAsync(cancellationToken);
        var indexMap = DefaultIndexMap
            .ToDictionary(pair => pair.Key, pair => pair.Value)
            .AsReadOnly();
        var logger = LoggerFactory?.CreateLogger(typeof(HomeBallsEntryColumn));

        return new HomeBallsEntryColumn(cells, indexMap, logger) { Id = form.Id, };
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

    protected internal virtual async Task<HomeBallsEntryTableFactory> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        var delay = 100;
        await executeAsync(DataSource.Items.EnsureLoadedAsync(cancellationToken));
        await executeAsync(DataSource.PokemonForms.EnsureLoadedAsync(cancellationToken));
        return this;

        async Task executeAsync<TKey, TRecord>(
            ValueTask<IHomeBallsLoadableDataSet<TKey, TRecord>> navigation)
            where TKey : notnull, IEquatable<TKey>
            where TRecord : notnull, IKeyed<TKey>, IIdentifiable
        {
            await navigation;
            await Task.Delay(delay);
        }
    }
}