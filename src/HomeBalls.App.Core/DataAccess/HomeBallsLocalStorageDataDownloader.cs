namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess;

public interface IHomeBallsLocalStorageDataDownloader :
    INotifyDataDownloading
{
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

        await Task.WhenAll(identifiers.Select(pair =>
            DownloadAsync(pair.Identifier, pair.FileName , cancellationToken)));

        EventRaiser.Raise(DataDownloaded, startEventArgs.StartTime);
        return this;
    }

    protected internal virtual async Task<HomeBallsLocalStorageDataDownloader> DownloadAsync(
        String identifier,
        String fileName,
        CancellationToken cancellationToken = default)
    {
        var data = await RawDataClient.GetByteArrayAsync(fileName, cancellationToken);

        await LocalStorage.SetItemAsync(
            identifier,
            Convert.ToBase64String(data),
            cancellationToken);

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
}