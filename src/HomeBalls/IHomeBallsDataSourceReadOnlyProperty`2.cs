namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsDataSourceReadOnlyProperty<TKey, out TEntity> :
    IHomeBallsReadOnlyDataSet<TKey, TEntity>,
    IProperty
    where TKey : notnull, IEquatable<TKey>
    where TEntity : notnull, IKeyed<TKey>, IIdentifiable { }

public class HomeBallsDataSourceReadOnlyProperty<TKey, TEntity> :
    HomeBallsReadOnlyDataSet<TKey, TEntity>,
    IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity>
    where TKey : notnull, IEquatable<TKey>
    where TEntity : notnull, IKeyed<TKey>, IIdentifiable
{
    public HomeBallsDataSourceReadOnlyProperty(
        IHomeBallsDataSet<TKey, TEntity> dataSet,
        String propertyName) :
        base(dataSet) =>
        PropertyName = propertyName;

    public String PropertyName { get; }
}