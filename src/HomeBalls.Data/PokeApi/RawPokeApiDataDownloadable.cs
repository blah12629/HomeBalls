namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiDataDownloadable :
    IAsyncDownloadable,
    IFileLoadable { }

public interface IRawPokeApiDataDownloadable<T> :
    IRawPokeApiDataDownloadable,
    IAsyncDownloadable<T>,
    IFileLoadable<T> { }

public abstract class RawPokeApiDataDownloadable :
    IAsyncDownloadable
{
    protected RawPokeApiDataDownloadable(
        IFileSystem fileSystem,
        HttpClient rawPokeApiGithubClient,
        IRawPokeApiFileNameService fileNameService,
        ILogger? logger = default,
        String rootDirectory = _Values.DefaultDataRoot)
    {
        FileSystem = fileSystem;
        RawPokeApiGithubClient = rawPokeApiGithubClient;
        FileNameService = fileNameService;
        Logger = logger;
        RootDirectory = rootDirectory;
    }

    protected internal IFileSystem FileSystem { get; }

    protected internal HttpClient RawPokeApiGithubClient { get; }

    protected internal IRawPokeApiFileNameService FileNameService { get; }

    protected internal ILogger? Logger { get; }

    protected internal virtual String FullUrl => (
        RawPokeApiGithubClient.BaseAddress?.ToString() ??
            throw new NullReferenceException())
            .TrimEnd(
                FileSystem.Path.DirectorySeparatorChar,
                FileSystem.Path.AltDirectorySeparatorChar) +
        $"/{FileName}";

    protected internal abstract String FileName { get; }

    protected internal virtual String FilePath =>
        FileSystem.Path.Join(RootDirectory, FileName);

    protected internal String RootDirectory { get; set; }

    protected internal virtual T InDirectory<T>(
        T returnValue,
        String directory)
    {
        InDirectory(directory);
        return returnValue;
    }

    protected internal virtual void InDirectory(
        String directory) =>
        RootDirectory = directory;

    protected internal virtual async ValueTask<T> EnsureDownloadedAsync<T>(
        T returnValue,
        CancellationToken cancellationToken = default)
    {
        await EnsureDownloadedAsync(cancellationToken);
        return returnValue;
    }

    protected internal virtual async ValueTask EnsureDownloadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (FileSystem.File.Exists(FilePath)) return;

        await DownloadFileAsync(cancellationToken);
        Logger?.LogDebug($"Successfully downloaded `{FullUrl}` to `{FilePath}`.");
    }

    protected internal virtual async Task DownloadFileAsync(
        CancellationToken cancellationToken = default)
    {
        await using var stream = await GetSteamAsync(cancellationToken);

        FileSystem.Directory.CreateDirectory((FileSystem.Path.GetDirectoryName(FilePath)));
        await using var fileStream = FileSystem.File.OpenWrite(FilePath);
        await stream.CopyToAsync(fileStream, cancellationToken);
        return;
    }

    protected internal virtual async Task<Stream> GetSteamAsync(
        CancellationToken cancellationToken = default)
    {
        Stream stream;
        try
        {
            stream = await RawPokeApiGithubClient
                .GetStreamAsync(FileName, cancellationToken);
        }
        catch (HttpRequestException httpRequestException)
        {
            Logger?.LogError(
                $"`{FullUrl}` responded with status code " +
                $"[{httpRequestException.StatusCode}].");
            throw;
        }

        return stream;
    }

    ValueTask IAsyncDownloadable.EnsureDownloadedAsync(
        CancellationToken cancellationToken) =>
        EnsureDownloadedAsync(cancellationToken);
}