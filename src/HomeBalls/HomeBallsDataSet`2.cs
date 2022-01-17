namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsDataSet<TKey, TRecord> :
    IHomeBallsEnumerable<TRecord>,
    IHomeBallsReadOnlyDataSet<TKey, TRecord>,
    ICollection<TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    new TRecord this[TKey key] { get; set; }

    new TRecord this[String identifier] { get; set; }

    new Int32 Count { get; }

    new IHomeBallsDataSet<TKey, TRecord> Add(TRecord record);

    IHomeBallsDataSet<TKey, TRecord> AddRange(IEnumerable<TRecord> records);

    new IHomeBallsDataSet<TKey, TRecord> Clear();

    Boolean Remove(TKey key);

    Boolean Remove(String identifier);

    IHomeBallsReadOnlyDataSet<TKey, TRecord> AsReadOnly();
}

public class HomeBallsDataSet<TKey, TRecord> :
    IHomeBallsDataSet<TKey, TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    public HomeBallsDataSet()
    {
        KeySelector = record => record.Id;
        IdentifierSelector = record => record.Identifier;

        KeyDictionary = new Dictionary<TKey, TRecord> { };
        IdentifierDictionary = new Dictionary<String, TRecord> { };
        Keys = KeyDictionary.Keys.AsReadOnly();
        Identifiers = IdentifierDictionary.Keys.AsReadOnly();
        Values = KeyDictionary.Values.AsReadOnly();
        ElementType = typeof(TRecord);
    }

    protected internal IDictionary<TKey, TRecord> KeyDictionary { get; }

    protected internal IDictionary<String, TRecord> IdentifierDictionary { get; }

    protected internal Func<TRecord, TKey> KeySelector { get; }

    protected internal Func<TRecord, String> IdentifierSelector { get; }

    public virtual TRecord this[TKey key]
    {
        get => KeyDictionary[key];
        set => SetValue(key, IdentifierSelector(KeyDictionary[key]), value);
    }

    public virtual TRecord this[String identifier]
    {
        get => IdentifierDictionary[identifier];
        set => SetValue(KeySelector(IdentifierDictionary[identifier]), identifier, value);
    }

    public virtual IReadOnlyCollection<TKey> Keys { get; }

    public virtual IReadOnlyCollection<String> Identifiers { get; }

    public virtual IReadOnlyCollection<TRecord> Values { get; }

    public virtual Int32 Count => KeyDictionary.Count;

    public virtual Boolean IsReadOnly => false;

    public virtual Type ElementType { get; }

    public virtual HomeBallsDataSet<TKey, TRecord> Add(TRecord record)
    {
        KeyDictionary.Add(KeySelector(record), record);
        IdentifierDictionary.Add(IdentifierSelector(record), record);
        return this;
    }

    public virtual HomeBallsDataSet<TKey, TRecord> AddRange(
        IEnumerable<TRecord> records)
    {
        foreach (var record in records) Add(record);
        return this;
    }

    public virtual IHomeBallsReadOnlyDataSet<TKey, TRecord> AsReadOnly() =>
        new HomeBallsReadOnlyDataSet<TKey, TRecord>(this);

    public virtual HomeBallsDataSet<TKey, TRecord> Clear()
    {
        KeyDictionary.Clear();
        IdentifierDictionary.Clear();
        return this;
    }

    public virtual Boolean Contains(TRecord record)
    {
        var containsKey = ContainsKey(KeySelector(record));

        return containsKey == ContainsIdentifier(IdentifierSelector(record)) ?
            containsKey :
            throw new ArgumentException();
    }

    public virtual Boolean ContainsKey(TKey key) =>
        KeyDictionary.ContainsKey(key);

    public virtual Boolean ContainsIdentifier(String identifier) =>
        IdentifierDictionary.ContainsKey(identifier);

    public virtual IEnumerator<TRecord> GetEnumerator() =>
        Values.GetEnumerator();

    public virtual Boolean Remove(TKey key) =>
        Remove(key, IdentifierSelector(KeyDictionary[key]));

    public virtual Boolean Remove(String identifier) =>
        Remove(KeySelector(IdentifierDictionary[identifier]), identifier);

    protected internal virtual Boolean Remove(TKey key, String identifier) =>
        KeyDictionary.Remove(key) != IdentifierDictionary.Remove(identifier) ?
            throw new ArgumentException() :
            true;

    protected internal virtual HomeBallsDataSet<TKey, TRecord> SetValue(
        TKey key,
        String identifier,
        TRecord newValue)
    {
        KeyDictionary[key] = newValue;
        IdentifierDictionary[identifier] = newValue;
        return this;
    }

    public virtual Boolean TryGetValue(
        TKey key,
        [MaybeNullWhen(false)] out TRecord value) =>
        KeyDictionary.TryGetValue(key, out value);

    public virtual Boolean TryGetValue(
        String identifier,
        [MaybeNullWhen(false)] out TRecord value) =>
        IdentifierDictionary.TryGetValue(identifier, out value);

    void ICollection<TRecord>.Add(TRecord item) => Add(item);

    void ICollection<TRecord>.Clear() => Clear();

    void ICollection<TRecord>.CopyTo(TRecord[] array, Int32 arrayIndex) =>
        KeyDictionary.Values.CopyTo(array, arrayIndex);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    Boolean ICollection<TRecord>.Remove(TRecord item) =>
        Remove(KeySelector(item), IdentifierSelector(item));

    IHomeBallsDataSet<TKey, TRecord> IHomeBallsDataSet<TKey, TRecord>
        .Add(TRecord record) =>
        Add(record);

    IHomeBallsDataSet<TKey, TRecord> IHomeBallsDataSet<TKey, TRecord>
        .AddRange(IEnumerable<TRecord> records) =>
        AddRange(records);

    IHomeBallsDataSet<TKey, TRecord> IHomeBallsDataSet<TKey, TRecord>.Clear() => Clear();
}