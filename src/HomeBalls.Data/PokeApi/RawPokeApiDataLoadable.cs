namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiDataLoadable :
    IAsyncLoadable,
    IFileLoadable { }

public interface IRawPokeApiDataLoadable<T> :
    IRawPokeApiDataLoadable,
    IAsyncLoadable<T>,
    IFileLoadable<T>
{
    T Clear();
}

public abstract class RawPokeApiDataLoadable :
    RawPokeApiDataDownloadable,
    IRawPokeApiDataLoadable
{
    protected RawPokeApiDataLoadable(
        IFileSystem fileSystem,
        HttpClient rawPokeApiGithubClient,
        IRawPokeApiFileNameService fileNameService,
        ICsvHelperFactory csvHelperFactory,
        ILogger? logger = default,
        String rootDirectory = _Values.DefaultDataRoot) :
        base(fileSystem, rawPokeApiGithubClient, fileNameService, logger, rootDirectory) =>
        CsvHelperFactory = csvHelperFactory;

    protected internal ICsvHelperFactory CsvHelperFactory { get; }

    protected internal Boolean IsLoaded { get; set; }

    protected internal virtual async ValueTask<T> EnsureLoadedAsync<T>(
        T returnValue,
        CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync(cancellationToken);
        return returnValue;
    }

    protected internal virtual async ValueTask EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (IsLoaded) return;

        await EnsureDownloadedAsync(cancellationToken);
        await LoadFileAsync(cancellationToken);
        IsLoaded = true;
    }

    protected internal virtual async Task LoadFileAsync(
        CancellationToken cancellationToken = default)
    {
        await using var fileStream = FileSystem.File.OpenRead(FilePath);
        using var fileReader = new StreamReader(fileStream);
        using var csvReader = CsvHelperFactory.CreateReader(fileReader);
        await LoadRecordsAsync(csvReader, cancellationToken);
    }

    protected internal abstract Task LoadRecordsAsync(
        IReader reader,
        CancellationToken cancellationToken = default);

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(new Object(), cancellationToken);

    void IFileLoadable.InDirectory(String directory) => InDirectory(directory);
}