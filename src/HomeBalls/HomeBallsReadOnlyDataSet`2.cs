namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsReadOnlyDataSet<TKey, TRecord> :
    IHomeBallsEnumerable<TRecord>,
    IReadOnlyCollection<TRecord>
    where TKey : notnull
    where TRecord : notnull, IKeyed, IIdentifiable
{
    TRecord this[TKey key] { get; }

    TRecord this[String identifier] { get; }

    IReadOnlyCollection<TKey> Keys { get; }

    IReadOnlyCollection<String> Identifiers { get; }

    IReadOnlyCollection<TRecord> Values { get; }

    Boolean ContainsKey(TKey key);

    Boolean ContainsIdentifier(String identifier);

    Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TRecord value);

    Boolean TryGetValue(String identifier, [MaybeNullWhen(false)] out TRecord value);
}

public class HomeBallsReadOnlyDataSet<TKey, TRecord> :
    IHomeBallsReadOnlyDataSet<TKey, TRecord>
    where TKey : notnull
    where TRecord : notnull, IKeyed, IIdentifiable
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

    public virtual Boolean TryGetValue(
        TKey key,
        [MaybeNullWhen(false)] out TRecord value) =>
        DataSet.TryGetValue(key, out value);

    public virtual Boolean TryGetValue(
        String identifier,
        [MaybeNullWhen(false)] out TRecord value) =>
        DataSet.TryGetValue(identifier, out value);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
