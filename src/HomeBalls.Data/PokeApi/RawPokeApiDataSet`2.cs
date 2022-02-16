using CEo.Pokemon.HomeBalls.Entities;

namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiDataSet<TKey, out TRecord> :
    IHomeBallsLoadableDataSet<TKey, TRecord>,
    IIdentifiable,
    IFileLoadable<IRawPokeApiDataSet<TKey, TRecord>>,
    INotifyDataDownloading
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable { }

public class RawPokeApiDataSet<TKey, TRecord> :
    HomeBallsLoadableDataSet<TKey, TRecord>,
    IRawPokeApiDataSet<TKey, TRecord>,
    IFileLoadable<RawPokeApiDataSet<TKey, TRecord>>
    where TKey : notnull, IEquatable<TKey>
    where TRecord : notnull, IKeyed<TKey>, IIdentifiable
{
    String? _identifier;

    public RawPokeApiDataSet(
        IFileSystem fileSystem,
        IRawPokeApiDownloader downloader,
        ICsvHelperFactory serializer,
        IHomeBallsIdentifierService identifiers,
        String dataRootDirectory = DefaultDataRoot,
        ILogger? logger = default) :
        this(
            fileSystem, downloader, serializer, identifiers,
            new HomeBallsDataSet<TKey, TRecord> { }, dataRootDirectory, logger) { }

    public RawPokeApiDataSet(
        IFileSystem fileSystem,
        IRawPokeApiDownloader downloader,
        ICsvHelperFactory serializer,
        IHomeBallsIdentifierService identifiers,
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        String dataRootDirectory = DefaultDataRoot,
        ILogger? logger = default) :
        base(dataSet, (dataSet, cancellationToken) => Task.CompletedTask, logger)
    {
        (FileSystem, Downloader, Serializer, IdentifierService) =
            (fileSystem, downloader, serializer, identifiers);

        DataRoot = dataRootDirectory;
        LoadTask = LoadAsync;
    }

    protected internal virtual String DataRoot { get; set; }

    protected internal virtual String Identifier =>
        _identifier ??= IdentifierService.GenerateIdentifier(ElementType);

    protected internal IFileSystem FileSystem { get; }

    protected internal IRawPokeApiDownloader Downloader { get; }

    protected internal ICsvHelperFactory Serializer { get; }

    protected internal IHomeBallsIdentifierService IdentifierService { get; }

    String IIdentifiable.Identifier => Identifier;

    public event EventHandler<TimedActionStartingEventArgs>? DataDownloading
    {
        add => Downloader.DataDownloading += value;
        remove => Downloader.DataDownloading -= value;
    }

    public event EventHandler<TimedActionEndedEventArgs>? DataDownloaded
    {
        add => Downloader.DataDownloaded += value;
        remove => Downloader.DataDownloaded -= value;
    }

    protected internal virtual async Task<RawPokeApiDataSet<TKey, TRecord>> LoadAsync(
        IHomeBallsDataSet<TKey, TRecord> dataSet,
        CancellationToken cancellationToken = default)
    {
        var fileName = Identifier.AddFileExtension(DefaultCsvExtension);
        var filePath = FileSystem.Path.Join(DataRoot, fileName);

        await EnsureDownloadedAsync(fileName, filePath, cancellationToken);
        await using var fileStream = FileSystem.File.OpenRead(filePath);
        using (var reader = new StreamReader(fileStream))
        using (var deserializer = Serializer.CreateReader(reader))

        await foreach (var record in deserializer
            .GetRecordsAsync<TRecord>(cancellationToken))
            dataSet.Add(record);

        return this;
    }

    protected internal virtual async Task<RawPokeApiDataSet<TKey, TRecord>> EnsureDownloadedAsync(
        String fileName,
        String filePath,
        CancellationToken cancellationToken = default)
    {
        if (FileSystem.File.Exists(filePath)) return this;

        await Downloader.DownloadAsync(this, fileName, cancellationToken);
        return this;
    }

    public virtual RawPokeApiDataSet<TKey, TRecord> InDirectory(String directory)
    {
        DataRoot = directory;
        return this;
    }

    IRawPokeApiDataSet<TKey, TRecord> IFileLoadable<IRawPokeApiDataSet<TKey, TRecord>>
        .InDirectory(String directory) =>
        InDirectory(directory);

    void IFileLoadable.InDirectory(String directory) => InDirectory(directory);
}