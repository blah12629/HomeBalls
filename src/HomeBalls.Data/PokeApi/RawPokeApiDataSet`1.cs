namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiDataSet<TRecord> :
    IHomeBallsEnumerable<TRecord>,
    IRawPokeApiDataLoadable<IRawPokeApiDataSet<TRecord>>,
    ICollection<TRecord>
    where TRecord : notnull, RawPokeApiRecord
{
    new IRawPokeApiDataSet<TRecord> Add(TRecord record);

    IRawPokeApiDataSet<TRecord> AddRange(IEnumerable<TRecord> records);
}

public class RawPokeApiDataSet<TRecord> :
    RawPokeApiDataLoadable,
    IRawPokeApiDataSet<TRecord>
    where TRecord : notnull, RawPokeApiRecord
{
    public RawPokeApiDataSet(
        IFileSystem fileSystem,
        HttpClient rawPokeApiGithubClient,
        IRawPokeApiFileNameService fileNameService,
        ICsvHelperFactory csvHelperFactory,
        ILogger? logger = default,
        String rootDirectory = _Values.DefaultDataRoot) :
        base(fileSystem, rawPokeApiGithubClient, fileNameService, csvHelperFactory, logger, rootDirectory) =>
        Records = new List<TRecord> { };

    public virtual Int32 Count => Records.Count;

    public virtual Boolean IsReadOnly => Records.IsReadOnly;

    protected internal ICollection<TRecord> Records { get; }

    protected internal override String FileName =>
        FileNameService.GetFileName<TRecord>(_Values.DefaultCsvExtension);

    public virtual Type ElementType => typeof(TRecord);

    public virtual RawPokeApiDataSet<TRecord> Add(TRecord item)
    {
        Records.Add(item);
        return this;
    }

    public virtual RawPokeApiDataSet<TRecord> AddRange(IEnumerable<TRecord> records)
    {
        foreach (var record in records) Add(record);
        return this;
    }

    public virtual RawPokeApiDataSet<TRecord> Clear()
    {
        Records.Clear();
        IsLoaded = false;
        return this;
    }

    public virtual Boolean Contains(TRecord item) =>
        Records.Contains(item);

    public void CopyTo(TRecord[] array, Int32 arrayIndex) =>
        Records.CopyTo(array, arrayIndex);

    new public virtual ValueTask<RawPokeApiDataSet<TRecord>> EnsureLoadedAsync(
        CancellationToken cancellationToken = default) =>
        EnsureLoadedAsync(this, cancellationToken);

    new public virtual RawPokeApiDataSet<TRecord> InDirectory(
        String directory) =>
        InDirectory(this, directory);

    public virtual IEnumerator<TRecord> GetEnumerator() =>
        Records.GetEnumerator();

    public virtual Boolean Remove(TRecord item) =>
        Remove(item);

    protected internal override Task LoadRecordsAsync(
        IReader reader,
        CancellationToken cancellationToken = default)
    {
        Clear().AddRange(reader.GetRecords<TRecord>());
        return Task.CompletedTask;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    IRawPokeApiDataSet<TRecord> IRawPokeApiDataSet<TRecord>
        .Add(TRecord record) =>
        Add(record);

    IRawPokeApiDataSet<TRecord> IRawPokeApiDataSet<TRecord>
        .AddRange(IEnumerable<TRecord> records) =>
        AddRange(records);

    IRawPokeApiDataSet<TRecord> IRawPokeApiDataLoadable<IRawPokeApiDataSet<TRecord>>
        .Clear() =>
        Clear();

    IRawPokeApiDataSet<TRecord> IFileLoadable<IRawPokeApiDataSet<TRecord>>
        .InDirectory(String directory) =>
        InDirectory(directory);

    async ValueTask<IRawPokeApiDataSet<TRecord>> IAsyncLoadable<IRawPokeApiDataSet<TRecord>>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    void ICollection<TRecord>.Add(TRecord item) => Add(item);

    void ICollection<TRecord>.Clear() => Clear();
}