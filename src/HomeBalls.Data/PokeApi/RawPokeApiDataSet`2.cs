namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiDataSet<TKey, TRecord> :
    IRawPokeApiDataLoadable<IRawPokeApiDataSet<TKey, TRecord>>,
    IHomeBallsDataSet<TKey, TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, RawPokeApiRecord, IKeyed<TKey>, IIdentifiable
{
    new IRawPokeApiDataSet<TKey, TRecord> Add(TRecord record);

    new IRawPokeApiDataSet<TKey, TRecord> AddRange(IEnumerable<TRecord> records);
}

public class RawPokeApiDataSet<TKey, TRecord> :
    RawPokeApiDataLoadable,
    IRawPokeApiDataSet<TKey, TRecord>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, RawPokeApiRecord, IKeyed<TKey>, IIdentifiable
{
    public RawPokeApiDataSet(
        IFileSystem fileSystem,
        HttpClient rawPokeApiGithubClient,
        IRawPokeApiFileNameService fileNameService,
        ICsvHelperFactory csvHelperFactory,
        ILogger? logger = default) :
        base(fileSystem, rawPokeApiGithubClient, fileNameService, csvHelperFactory, logger) =>
        Records = new HomeBallsDataSet<TKey, TRecord> { };

    public virtual TRecord this[TKey key] { get => Records[key]; set => Records[key] = value; }

    public virtual TRecord this[String identifier] { get => Records[identifier]; set => Records[identifier] = value; }

    public virtual IReadOnlyCollection<TKey> Keys => Records.Keys;

    public virtual IReadOnlyCollection<String> Identifiers => Records.Identifiers;

    public virtual IReadOnlyCollection<TRecord> Values => Records.Values;

    public virtual Int32 Count => Records.Count;

    public virtual Boolean IsReadOnly => Records.IsReadOnly;

    public virtual Type ElementType => Records.ElementType;

    protected internal HomeBallsDataSet<TKey, TRecord> Records { get; }

    protected internal override String FileName =>
        FileNameService.GetFileName<TRecord>(_Values.DefaultCsvExtension);

    public virtual RawPokeApiDataSet<TKey, TRecord> Add(TRecord record)
    {
        Records.Add(record);
        return this;
    }

    public virtual RawPokeApiDataSet<TKey, TRecord> AddRange(IEnumerable<TRecord> records)
    {
        Records.AddRange(records);
        return this;
    }

    public virtual IHomeBallsReadOnlyDataSet<TKey, TRecord> AsReadOnly() => Records.AsReadOnly();

    public virtual RawPokeApiDataSet<TKey, TRecord> Clear()
    {
        Records.Clear();
        IsLoaded = false;
        return this;
    }

    public virtual Boolean Contains(TRecord item) => Records.Contains(item);

    public virtual Boolean ContainsIdentifier(String identifier) => Records.ContainsIdentifier(identifier);

    public virtual Boolean ContainsKey(TKey key) => Records.ContainsKey(key);

    new public virtual ValueTask<RawPokeApiDataSet<TKey, TRecord>> EnsureLoadedAsync(
        CancellationToken cancellationToken = default) =>
        EnsureLoadedAsync(this, cancellationToken);

    new public virtual RawPokeApiDataSet<TKey, TRecord> InDirectory(
        String directory) =>
        InDirectory(this, directory);

    public virtual IEnumerator<TRecord> GetEnumerator() => Records.GetEnumerator();

    public virtual Boolean Remove(TKey key) => Records.Remove(key);

    public virtual Boolean Remove(String identifier) => Records.Remove(identifier);

    public virtual Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TRecord value) => Records.TryGetValue(key, out value);

    public virtual Boolean TryGetValue(String identifier, [MaybeNullWhen(false)] out TRecord value) => Records.TryGetValue(identifier, out value);

    protected internal override Task LoadRecordsAsync(
        IReader reader,
        CancellationToken cancellationToken = default)
    {
        Clear().AddRange(reader.GetRecords<TRecord>());
        return Task.CompletedTask;
    }

    IHomeBallsDataSet<TKey, TRecord> IHomeBallsDataSet<TKey, TRecord>
        .Add(TRecord record)  =>
        Add(record);

    void ICollection<TRecord>.Add(TRecord item) => Add(item);

    IRawPokeApiDataSet<TKey, TRecord> IRawPokeApiDataSet<TKey, TRecord>
        .Add(TRecord record) =>
        Add(record);

    IHomeBallsDataSet<TKey, TRecord> IHomeBallsDataSet<TKey, TRecord>
        .AddRange(IEnumerable<TRecord> records) =>
        AddRange(records);

    IRawPokeApiDataSet<TKey, TRecord> IRawPokeApiDataSet<TKey, TRecord>
        .AddRange(IEnumerable<TRecord> records) =>
        AddRange(records);

    IHomeBallsDataSet<TKey, TRecord> IHomeBallsDataSet<TKey, TRecord>
        .Clear() => Clear();

    void ICollection<TRecord>.Clear() => Clear();

    IRawPokeApiDataSet<TKey, TRecord> IRawPokeApiDataLoadable<IRawPokeApiDataSet<TKey, TRecord>>
        .Clear() => Clear();

    async ValueTask<IRawPokeApiDataSet<TKey, TRecord>> IAsyncLoadable<IRawPokeApiDataSet<TKey, TRecord>>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    IRawPokeApiDataSet<TKey, TRecord> IFileLoadable<IRawPokeApiDataSet<TKey, TRecord>>
        .InDirectory(String directory) =>
        InDirectory(directory);

    void ICollection<TRecord>.CopyTo(TRecord[] array, Int32 arrayIndex) =>
        ((ICollection<TRecord>)Records).CopyTo(array, arrayIndex);

    bool ICollection<TRecord>.Remove(TRecord item) =>
        ((ICollection<TRecord>)Records).Remove(item);
}