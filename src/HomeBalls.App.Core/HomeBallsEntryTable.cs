namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryTable
{
    ICollection<IHomeBallsEntry> Entries { get; }

    ICollection<IHomeBallsEntryLegality> Legalities { get; }

    IList<IHomeBallsEntryColumn> Columns { get; }

    // Task AddRangeByBatchAsync(
    //     IEnumerable<IHomeBallsEntry> entries,
    //     UInt16 batchSize,
    //     Func<Task>? postBatchAddTask = default,
    //     CancellationToken cancellationToken = default);
}

public class HomeBallsEntryTable :
    IHomeBallsEntryTable
{
    public HomeBallsEntryTable(
        IList<IHomeBallsEntryColumn> columns,
        IReadOnlyDictionary<HomeBallsPokemonFormKey, Int32> columnsIndexMap,
        ILoggerFactory? loggerFactory = default)
    {
        LoggerFactory = loggerFactory;
        EventRaiser = new EventRaiser().RaisedBy(this);
        Logger = LoggerFactory?.CreateLogger<HomeBallsEntryTable>();

        EntriesObservable = new HomeBallsEntryCollection(LoggerFactory?.CreateLogger<HomeBallsEntryCollection>());
        LegalitiesObservable = new HomeBallsObservableList<IHomeBallsEntryLegality>(LoggerFactory?.CreateLogger<HomeBallsObservableList<IHomeBallsEntryLegality>>());
        (Columns, ColumnsIndexMap) = (columns, columnsIndexMap);

        EntriesObservable.CollectionChanged += OnEntriesChanged;
        LegalitiesObservable.CollectionChanged += OnLegalitiesChanged;
    }

    public ICollection<IHomeBallsEntry> Entries => EntriesObservable;

    public ICollection<IHomeBallsEntryLegality> Legalities => LegalitiesObservable;

    public IList<IHomeBallsEntryColumn> Columns { get; }

    protected internal IReadOnlyDictionary<HomeBallsPokemonFormKey, Int32> ColumnsIndexMap { get; }

    protected internal IHomeBallsObservableCollection<IHomeBallsEntry> EntriesObservable { get; }

    protected internal IHomeBallsObservableCollection<IHomeBallsEntryLegality> LegalitiesObservable { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal virtual void OnEntriesChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Move) return;

        InvokeOnChanged<IHomeBallsEntry>(e.OldItems, (cell, entry) =>
            cell.ObtainedStatus = HomeBallsEntryObtainedStatus.NotObtained);

        InvokeOnChanged<IHomeBallsEntry>(e.NewItems, (cell, entry) =>
            cell.ObtainedStatus = entry.HasHiddenAbility ?
                HomeBallsEntryObtainedStatus.ObtainedWithHiddenAbility :
                HomeBallsEntryObtainedStatus.ObtainedWithoutHiddenAbility);
    }

    protected internal virtual void OnLegalitiesChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Move) return;
        
        InvokeOnChanged<IHomeBallsEntryLegality>(e.OldItems, (cell, legality) =>
            cell.LegalityStatus = HomeBallsEntryLegalityStatus.NotObtainable);

        InvokeOnChanged<IHomeBallsEntryLegality>(e.NewItems, (cell, legality) =>
            cell.LegalityStatus = legality.IsObtainableWithHiddenAbility ?
                HomeBallsEntryLegalityStatus.ObtainableWithHiddenAbility :
                HomeBallsEntryLegalityStatus.ObtainableWithoutHiddenAbility);
    }

    protected internal virtual void InvokeOnChanged<T>(
        IList? items,
        Action<IHomeBallsEntryCell, T> cellAction)
        where T : notnull, IKeyed<HomeBallsEntryKey>
    {
        if (items != default) foreach (var entry in items.Cast<T>())
        {
            var index = ColumnsIndexMap[new (entry.Id.SpeciesId, entry.Id.FormId)];
            cellAction(Columns[index][entry.Id.BallId], entry);
        }
    }
}

// public class HomeBallsEntryTable :
//     IHomeBallsEntryTable
// {
//     public HomeBallsEntryTable(
//         List<IHomeBallsEntryColumn> loadedColumns,
//         IReadOnlyDictionary<(UInt16 SpeciesId, Byte FormId), Int32> columnsIndexMap,
//         IHomeBallsEntryCollection entries,
//         ILogger? logger = default)
//     {
//         (Columns, ColumnsMutable) = (loadedColumns.AsReadOnly(), loadedColumns);
//         ColumnsIndexMap = columnsIndexMap;
//         Entries = entries;
//         EventRaiser = new EventRaiser();
//         Logger = logger;

//         Entries.CollectionChanged += OnEntriesChanged;
//     }

//     public IReadOnlyList<IHomeBallsEntryColumn> Columns { get; }

//     protected internal IList<IHomeBallsEntryColumn> ColumnsMutable { get; }

//     protected internal IReadOnlyDictionary<(UInt16 SpeciesId, Byte FormId), Int32> ColumnsIndexMap { get; }

//     public virtual Type ElementType => Entries.ElementType;

//     public virtual Int32 Count => Entries.Count;

//     public virtual Boolean IsReadOnly => Entries.IsReadOnly;

//     protected internal IHomeBallsEntryCollection Entries { get; }

//     protected internal IEventRaiser EventRaiser { get; }

//     protected internal ILogger? Logger { get; }

//     IList<IHomeBallsEntryColumn> IHomeBallsEntryTable.Columns => ColumnsMutable;

//     public event NotifyCollectionChangedEventHandler? CollectionChanged;

//     public virtual void Add(IHomeBallsEntry item) => Entries.Add(item);

//     public virtual async Task AddRangeByBatchAsync(
//         IEnumerable<IHomeBallsEntry> entries,
//         UInt16 batchSize,
//         Func<Task>? postBatchAddTask = null,
//         CancellationToken cancellationToken = default)
//     {
//         var count = UInt16.MinValue;
//         var task = postBatchAddTask?.Invoke();
//         var isTaskDefault = task == default;

//         foreach (var entry in entries)
//         {
//             if (count == batchSize) await resetCountAsync();
//             Add(entry);
//             count ++;
//         }

//         async Task resetCountAsync()
//         {
//             if (!isTaskDefault) await task!;
//             await Task.Delay(5);
//             count = Byte.MinValue;
//         }
//     }

//     public virtual void Clear() => Entries.Clear();

//     public virtual Boolean Contains(IHomeBallsEntry item) => Entries.Contains(item);

//     public virtual void CopyTo(IHomeBallsEntry[] array, Int32 arrayIndex) =>
//         Entries.CopyTo(array, arrayIndex);

//     public virtual IEnumerator<IHomeBallsEntry> GetEnumerator() =>
//         Entries.GetEnumerator();

//     public virtual bool Remove(IHomeBallsEntry item) => Entries.Remove(item);

//     protected virtual void OnEntriesChanged(
//         Object? sender,
//         NotifyCollectionChangedEventArgs e)
//     {
//         if (e.Action == NotifyCollectionChangedAction.Move) return;

//         InvokeOnEntries(e.OldItems, (entry, cell) =>
//             cell.ObtainedStatus = HomeBallsEntryObtainedStatus.NotObtained);

//         InvokeOnEntries(e.NewItems, (entry, cell) =>
//             cell.ObtainedStatus = entry.HasHiddenAbility ?
//                 HomeBallsEntryObtainedStatus.ObtainedWithHiddenAbility :
//                 HomeBallsEntryObtainedStatus.ObtainedWithoutHiddenAbility);

//         CollectionChanged?.Invoke(this, e);
//     }

//     protected internal virtual void InvokeOnEntries(
//         IList? items,
//         Action<IHomeBallsEntry, IHomeBallsEntryCell> cellAction)
//     {
//         if (items != default) foreach (var entry in items.Cast<IHomeBallsEntry>())
//         {
//             var index = ColumnsIndexMap[(entry.SpeciesId, entry.FormId)];
//             cellAction(entry, Columns[index][entry.BallId]);
//         }
//     }

//     IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
// }