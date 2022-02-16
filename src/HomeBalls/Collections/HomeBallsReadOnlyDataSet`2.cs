namespace CEo.Pokemon.HomeBalls.Collections;

public interface IHomeBallsReadOnlyDataSet<TKey, out TRecord> :
    IHomeBallsReadOnlyCollection<TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    TRecord this[TKey key] { get; }

    TRecord this[String identifier] { get; }

    IReadOnlyCollection<TKey> Keys { get; }

    IReadOnlyCollection<String> Identifiers { get; }

    IReadOnlyCollection<TRecord> Values { get; }

    Boolean ContainsKey(TKey key);

    Boolean ContainsIdentifier(String identifier);
}

public class HomeBallsReadOnlyDataSet<TKey, TRecord> :
    IHomeBallsReadOnlyDataSet<TKey, TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    public HomeBallsReadOnlyDataSet(
        IHomeBallsDataSet<TKey, TRecord> dataSet) =>
        DataSet = dataSet;

    public virtual TRecord this[TKey key] => DataSet[key];

    public virtual TRecord this[String identifier] => DataSet[identifier];

    public virtual IReadOnlyCollection<TKey> Keys => DataSet.Keys;

    public virtual IReadOnlyCollection<String> Identifiers => DataSet.Identifiers;

    public virtual IReadOnlyCollection<TRecord> Values => DataSet.Values;

    public virtual Int32 Count => DataSet.Count;

    public virtual Type ElementType => DataSet.ElementType;

    protected internal IHomeBallsDataSet<TKey, TRecord> DataSet { get; }

    public virtual Boolean ContainsIdentifier(String identifier) =>
        DataSet.ContainsIdentifier(identifier);

    public virtual Boolean ContainsKey(TKey key) =>
        DataSet.ContainsKey(key);

    public virtual IEnumerator<TRecord> GetEnumerator() => 
        DataSet.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
