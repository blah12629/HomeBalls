namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess;

public interface IHomeBallsLocalStorageDataDownloader :
    INotifyDataDownloading
{
    Task<IHomeBallsLocalStorageDataDownloader> DownloadAsync(
        String identifier,
        String? fileName = default,
        CancellationToken cancellationToken = default);

    Task<IHomeBallsLocalStorageDataDownloader> DownloadAsync(
        IEnumerable<String> identifiers,
        CancellationToken cancellationToken = default);

    Task<IHomeBallsLocalStorageDataDownloader> DownloadAsync(
        IEnumerable<(String Identifier, String FileName)> identifiers,
        CancellationToken cancellationToken = default);
}

public class HomeBallsLocalStorageDataDownloader :
    IHomeBallsLocalStorageDataDownloader
{
    public HomeBallsLocalStorageDataDownloader(
        HttpClient rawDataClient,
        ILocalStorageService localStorage,
        ILogger? logger = default)
    {
        RawDataClient = rawDataClient;
        LocalStorage = localStorage;
        EventRaiser = new EventRaiser().RaisedBy(this);
        Logger = logger;
    }

    protected internal HttpClient RawDataClient { get; }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event EventHandler<TimedActionStartingEventArgs>? DataDownloading;

    public event EventHandler<TimedActionEndedEventArgs>? DataDownloaded;

    public virtual Task<HomeBallsLocalStorageDataDownloader> DownloadAsync(
        IEnumerable<String> identifiers,
        CancellationToken cancellationToken = default) =>
        DownloadAsync(
            identifiers.Select(id => (id, id.AddFileExtension(_Values.DefaultProtobufExtension))),
            cancellationToken);

    public virtual async Task<HomeBallsLocalStorageDataDownloader> DownloadAsync(
        IEnumerable<(String Identifier, String FileName)> identifiers,
        CancellationToken cancellationToken = default)
    {
        var startEventArgs = EventRaiser.Raise(DataDownloading);
        var downloadTasks = identifiers.Select(pair =>
            DownloadAsync(pair.Identifier, pair.FileName , cancellationToken)); 

        await Task.WhenAll(downloadTasks);

        EventRaiser.Raise(DataDownloaded, startEventArgs.StartTime);
        return this;
    }

    public virtual async Task<HomeBallsLocalStorageDataDownloader> DownloadAsync(
        String identifier,
        String? fileName = default,
        CancellationToken cancellationToken = default)
    {
        fileName = fileName ?? identifier.AddFileExtension(_Values.DefaultProtobufExtension);
        var data = await RawDataClient.GetByteArrayAsync(fileName, cancellationToken);
        var dataString = Convert.ToBase64String(data);
        await LocalStorage.SetItemAsync(identifier, dataString, cancellationToken);
        return this;
    }

    async Task<IHomeBallsLocalStorageDataDownloader> IHomeBallsLocalStorageDataDownloader.DownloadAsync(
        IEnumerable<String> identifiers,
        CancellationToken cancellationToken) =>
        await DownloadAsync(identifiers, cancellationToken);

    async Task<IHomeBallsLocalStorageDataDownloader> IHomeBallsLocalStorageDataDownloader.DownloadAsync(
        IEnumerable<(String Identifier, String FileName)> identifiers,
        CancellationToken cancellationToken) =>
        await DownloadAsync(identifiers, cancellationToken);

    async Task<IHomeBallsLocalStorageDataDownloader> IHomeBallsLocalStorageDataDownloader
        .DownloadAsync(
            String identifier,
            String? fileName,
            CancellationToken cancellationToken) =>
        await DownloadAsync(identifier, fileName, cancellationToken);
}