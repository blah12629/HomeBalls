// namespace CEo.Pokemon.HomeBalls.Data;

// public class HomeBallsLoadableDataDbSet<TKey, TEntity, TConcrete> :
//     DbSet<TConcrete>,
//     IHomeBallsLoadableDataSet<TKey, TEntity>,
//     IAsyncLoadable<HomeBallsLoadableDataDbSet<TKey, TEntity, TConcrete>>
//     where TKey : notnull, IEquatable<TKey>
//     where TEntity : notnull, IKeyed<TKey>, IIdentifiable
//     where TConcrete : class, TEntity
// {
//     public HomeBallsLoadableDataDbSet(
//         Func<DbSet<TConcrete>> getDbSet,
//         Func<DbSet<TConcrete>, CancellationToken, Task<IEnumerable<TEntity>>> getEntitiesTask,
//         ILogger? logger = default)
//     {
//         (GetDbSet, GetEntitiesTask) = (getDbSet, getEntitiesTask);
//         Logger = logger;

//         DataSet = new HomeBallsLoadableDataSet<TKey, TEntity>(
//             new HomeBallsDataSet<TKey, TEntity>(),
//             async (dataSet, cancellationToken) =>
//                 dataSet.AddRange(await GetEntitiesTask.Invoke(DbSet, cancellationToken)),
//             Logger);
//     }

//     public virtual TEntity this[TKey key] => DataSet[key];

//     public virtual TEntity this[String identifier] => DataSet[identifier];

//     public virtual Boolean IsLoaded => DataSet.IsLoaded;

//     public virtual IReadOnlyCollection<TKey> Keys => DataSet.Keys;

//     public virtual IReadOnlyCollection<String> Identifiers => DataSet.Identifiers;

//     public virtual IReadOnlyCollection<TEntity> Values => DataSet.Values;

//     public override IEntityType EntityType => DbSet.EntityType;

//     public virtual Type ElementType => DataSet.ElementType;

//     public virtual Int32 Count => DataSet.Count;

//     protected internal Func<DbSet<TConcrete>> GetDbSet { get; }

//     protected internal DbSet<TConcrete> DbSet => GetDbSet.Invoke();

//     protected internal Func<DbSet<TConcrete>, CancellationToken, Task<IEnumerable<TEntity>>> GetEntitiesTask { get; }

//     protected internal IHomeBallsLoadableDataSet<TKey, TEntity> DataSet { get; }

//     protected internal ILogger? Logger { get; }

//     public virtual event EventHandler<TimedActionStartingEventArgs>? DataLoading
//     {
//         add => DataSet.DataLoading += value;
//         remove => DataSet.DataLoading -= value;
//     }

//     public virtual event EventHandler<TimedActionEndedEventArgs>? DataLoaded
//     {
//         add => DataSet.DataLoaded += value;
//         remove => DataSet.DataLoaded -= value;
//     }

//     public virtual IHomeBallsLoadableDataSet<TKey, TEntity> Clear() => DataSet.Clear();

//     public virtual Boolean ContainsIdentifier(String identifier) =>
//         DataSet.ContainsIdentifier(identifier);

//     public virtual Boolean ContainsKey(TKey key) =>
//         DataSet.ContainsKey(key);

//     public virtual async ValueTask<HomeBallsLoadableDataDbSet<TKey, TEntity, TConcrete>> EnsureLoadedAsync(
//         CancellationToken cancellationToken = default)
//     {
//         await DataSet.EnsureLoadedAsync(cancellationToken);
//         return this;
//     }

//     public virtual IEnumerator<TEntity> GetEnumerator() => DataSet.GetEnumerator();

//     public virtual Boolean TryGetValue(
//         TKey key,
//         [MaybeNullWhen(false)] out TEntity value) =>
//         DataSet.TryGetValue(key, out value);

//     public virtual Boolean TryGetValue(
//         String identifier,
//         [MaybeNullWhen(false)] out TEntity value) =>
//         DataSet.TryGetValue(identifier, out value);

//     async ValueTask IAsyncLoadable
//         .EnsureLoadedAsync(CancellationToken cancellationToken) =>
//         await EnsureLoadedAsync(cancellationToken);

//     async ValueTask<IHomeBallsLoadableDataSet<TKey, TEntity>> IAsyncLoadable<IHomeBallsLoadableDataSet<TKey, TEntity>>
//         .EnsureLoadedAsync(CancellationToken cancellationToken) =>
//         await EnsureLoadedAsync(cancellationToken);
// }