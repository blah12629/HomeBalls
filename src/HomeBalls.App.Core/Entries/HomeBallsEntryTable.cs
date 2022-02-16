namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryTable
{
    IAddable<IHomeBallsEntry> Entries { get; }

    IAddable<IHomeBallsEntryLegality> Legalities { get; }

    IHomeBallsObservableList<IHomeBallsEntryColumn> Columns { get; }

    IHomeBallsEntryCell GetCell(HomeBallsEntryKey id);
}

public class HomeBallsEntryTable :
    IHomeBallsEntryTable
{
    public HomeBallsEntryTable(
        ILoggerFactory? loggerFactory = default)
    {
        LoggerFactory = loggerFactory;

        Entries = createAddable<IHomeBallsEntry>(AddEntry);
        Legalities = createAddable<IHomeBallsEntryLegality>(AddLegality);
        Columns = new HomeBallsObservableList<IHomeBallsEntryColumn>(
            logger: createLogger<HomeBallsObservableList<IHomeBallsEntryColumn>>());
        ColumnIndexMap = new ConcurrentDictionary<HomeBallsPokemonFormKey, Int32> { };

        EventRaiser = new EventRaiser(createLogger<EventRaiser>()).RaisedBy(this);
        Logger = createLogger<HomeBallsEntryTable>();

        Columns.CollectionChanged += OnColumnsChanged;

        ILogger<T>? createLogger<T>() => LoggerFactory?.CreateLogger<T>();
        IAddable<T> createAddable<T>(Action<T> addAction) =>
            new Addable<T>(addAction, createLogger<Addable<T>>());
    }

    public IAddable<IHomeBallsEntry> Entries { get; }

    public IAddable<IHomeBallsEntryLegality> Legalities { get; }

    public IHomeBallsObservableList<IHomeBallsEntryColumn> Columns { get; }

    protected internal IDictionary<HomeBallsPokemonFormKey, Int32> ColumnIndexMap { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal virtual void AddEntry(IHomeBallsEntry entry)
    {
        var cell = GetCell(entry.Id);
        cell.IsObtained.Value = true;
        cell.IsObtainedWithHiddenAbility.Value = entry.HasHiddenAbility.Value;
    }

    protected internal virtual void AddLegality(IHomeBallsEntryLegality legality)
    {
        var cell = GetCell(legality.Id);
        cell.IsLegal.Value = legality.IsObtainable;
        cell.IsLegalWithHiddenAbility.Value = legality.IsObtainableWithHiddenAbility;
    }

    public virtual IHomeBallsEntryCell GetCell(HomeBallsEntryKey key)
    {
        var index = ColumnIndexMap[new(key.SpeciesId, key.FormId)];
        return Columns[index].GetCell(key.BallId);
    }

    // public virtual Boolean TryGetCell(
    //     HomeBallsEntryKey key,
    //     [MaybeNullWhen(false)] out IHomeBallsEntryCell cell)
    // {
    //     var id = new HomeBallsPokemonFormKey(key.SpeciesId, key.FormId);
    //     var containsKey = ColumnIndexMap.TryGetValue(id, out var index);
    //     cell = containsKey ? Columns[index].GetCell(key.BallId) : default;
    //     return containsKey;
    // }

    protected internal virtual void OnColumnsChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Add)
            throw new NotSupportedException(
                $"`{GetType()}.{nameof(Columns)}` doesnt not support " +
                $"`{nameof(NotifyCollectionChangedAction)}.{e.Action.ToString()}`.");

        var item = e.NewItems?.Cast<IHomeBallsEntryColumn>().SingleOrDefault();
        if (item == default)
            throw new ArgumentNullException(
                $"Either none or more than 1 `{nameof(IHomeBallsEntryColumn)}` " +
                "were added simultaneously.");

        ColumnIndexMap.Add(item.Id, Columns.Count - 1);
    }
}