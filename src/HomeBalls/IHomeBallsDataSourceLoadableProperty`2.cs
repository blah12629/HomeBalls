namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsDataSourceLoadableProperty<TKey, out TEntity> :
    IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity>,
    IHomeBallsLoadableDataSet<TKey, TEntity>
    where TKey : notnull, IEquatable<TKey>
    where TEntity : notnull, IKeyed<TKey>, IIdentifiable { }