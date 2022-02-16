using CEo.Pokemon.HomeBalls.Entities;

namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiDownloader :
    IHomeBallsDataDownloader<IRawPokeApiDownloader> { }

public class RawPokeApiDownloader :
    HomeBallsDataDownloader<String>,
    IRawPokeApiDownloader,
    IHomeBallsDataDownloader<RawPokeApiDownloader>
{
    public RawPokeApiDownloader(
        HttpClient dataClient,
        IFileSystem fileSystem,
        String dataRootDirectory = DefaultDataRoot,
        ILogger? logger = default)
    {
        (DataClient, FileSystem) = (dataClient, fileSystem);
        DataRoot = dataRootDirectory;
    }

    protected internal String DataRoot { get; }

    protected internal HttpClient DataClient { get; }

    protected internal IFileSystem FileSystem { get; }

    new public virtual async Task<RawPokeApiDownloader> DownloadAsync(
        IIdentifiable identifiable,
        String? fileName = default,
        CancellationToken cancellationToken = default)
    {
        await base.DownloadAsync(identifiable, fileName, cancellationToken);
        return this;
    }

    new public virtual async Task<RawPokeApiDownloader> DownloadAsync(
        IEnumerable<IIdentifiable> identifiables,
        String? fileExtension = default,
        CancellationToken cancellationToken = default)
    {
        await base.DownloadAsync(identifiables, fileExtension, cancellationToken);
        return this;
    }

    new public virtual async Task<RawPokeApiDownloader> DownloadAsync(
        IEnumerable<(IIdentifiable Identifiable, String FileName)> identifiables,
        CancellationToken cancellationToken = default)
    {
        await base.DownloadAsync(identifiables, cancellationToken);
        return this;
    }

    protected override Task<String> DownloadCoreAsync(
        String fileName,
        CancellationToken cancellationToken = default) =>
        DataClient.GetStringAsync(fileName, cancellationToken);

    protected override String GetFileName(IIdentifiable identifiable) =>
        identifiable.Identifier.AddFileExtension(DefaultCsvExtension);

    protected override Task SaveDataAsync(
        IIdentifiable identifiable,
        String fileName,
        String data,
        CancellationToken cancellationToken = default) =>
        FileSystem.File.WriteAllTextAsync(
            FileSystem.Path.Join(DataRoot, fileName),
            data,
            cancellationToken);

    async Task<IRawPokeApiDownloader> IHomeBallsDataDownloader<IRawPokeApiDownloader>
        .DownloadAsync(
            IIdentifiable identifiable,
            String? fileName,
            CancellationToken cancellationToken) =>
        await DownloadAsync(identifiable, fileName, cancellationToken);

    async Task<IRawPokeApiDownloader> IHomeBallsDataDownloader<IRawPokeApiDownloader>
        .DownloadAsync(
            IEnumerable<IIdentifiable> identifiables,
            String? fileExtension,
            CancellationToken cancellationToken) =>
        await DownloadAsync(identifiables, fileExtension, cancellationToken);

    async Task<IRawPokeApiDownloader> IHomeBallsDataDownloader<IRawPokeApiDownloader>
        .DownloadAsync(
            IEnumerable<(IIdentifiable Identifiable, String FileName)> identifiables,
            CancellationToken cancellationToken) =>
        await DownloadAsync(identifiables, cancellationToken);
}