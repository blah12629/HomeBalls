namespace CEo.Pokemon.HomeBalls.App.DataAccess;

public interface IHomeBallsLocalStorageDownloader :
    IHomeBallsDataDownloader<IHomeBallsLocalStorageDownloader> { }

public class HomeBallsLocalStorageDownloader :
    HomeBallsDataDownloader<Byte[]>,
    IHomeBallsLocalStorageDownloader,
    IHomeBallsDataDownloader<HomeBallsLocalStorageDownloader>
{
    public HomeBallsLocalStorageDownloader(
        HttpClient dataClient,
        ILocalStorageService localStorage,
        ILogger? logger = default) :
        base(logger)
    {
        DataClient = dataClient;
        LocalStorage = localStorage;
    }

    protected internal HttpClient DataClient { get; }

    protected internal ILocalStorageService LocalStorage { get; }

    new public async Task<HomeBallsLocalStorageDownloader> DownloadAsync(
        IIdentifiable identifiable,
        String? fileName = default,
        CancellationToken cancellationToken = default)
    {
        await base.DownloadAsync(identifiable, fileName, cancellationToken);
        return this;
    }

    new public async Task<HomeBallsLocalStorageDownloader> DownloadAsync(
        IEnumerable<IIdentifiable> identifiables,
        String? fileExtension = default,
        CancellationToken cancellationToken = default)
    {
        await base.DownloadAsync(identifiables, fileExtension, cancellationToken);
        return this;
    }

    new public async Task<HomeBallsLocalStorageDownloader> DownloadAsync(
        IEnumerable<(IIdentifiable Identifiable, String FileName)> identifiables,
        CancellationToken cancellationToken = default)
    {
        await base.DownloadAsync(identifiables, cancellationToken);
        return this;
    }

    protected override Task<Byte[]> DownloadCoreAsync(
        String fileName,
        CancellationToken cancellationToken = default) =>
        DataClient.GetByteArrayAsync(fileName, cancellationToken);

    protected override String GetFileName(IIdentifiable identifiable) =>
        identifiable.Identifier.AddFileExtension(DefaultProtobufExtension);

    protected override async Task SaveDataAsync(
        IIdentifiable identifiable,
        String fileName,
        Byte[] data,
        CancellationToken cancellationToken = default) =>
        await LocalStorage.SetItemAsync(
            identifiable.Identifier,
            data,
            cancellationToken);

    async Task<IHomeBallsLocalStorageDownloader> IHomeBallsDataDownloader<IHomeBallsLocalStorageDownloader>
        .DownloadAsync(
            IIdentifiable identifiable,
            String? fileName,
            CancellationToken cancellationToken) =>
        await DownloadAsync(identifiable, fileName, cancellationToken);

    async Task<IHomeBallsLocalStorageDownloader> IHomeBallsDataDownloader<IHomeBallsLocalStorageDownloader>
        .DownloadAsync(
            IEnumerable<IIdentifiable> identifiables,
            String? fileExtension,
            CancellationToken cancellationToken) =>
        await DownloadAsync(identifiables, fileExtension, cancellationToken);

    async Task<IHomeBallsLocalStorageDownloader> IHomeBallsDataDownloader<IHomeBallsLocalStorageDownloader>
        .DownloadAsync(
            IEnumerable<(IIdentifiable Identifiable, String FileName)> identifiables,
            CancellationToken cancellationToken) =>
        await DownloadAsync(identifiables, cancellationToken);
}