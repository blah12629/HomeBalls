namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsDataDownloader :
    INotifyDataDownloading
{
    Task DownloadAsync(
        IIdentifiable identifiable,
        String? fileName = default,
        CancellationToken cancellationToken = default);

    Task DownloadAsync(
        IEnumerable<IIdentifiable> identifiables,
        String? fileExtension = default,
        CancellationToken cancellationToken = default);

    Task DownloadAsync(
        IEnumerable<(IIdentifiable Identifiable, String FileName)> identifiables,
        CancellationToken cancellationToken = default);
}

public interface IHomeBallsDataDownloader<TDownloader> :
    IHomeBallsDataDownloader
    where TDownloader : notnull, IHomeBallsDataDownloader<TDownloader>
{
    new Task<TDownloader> DownloadAsync(
        IIdentifiable identifiable,
        String? fileName = default,
        CancellationToken cancellationToken = default);

    new Task<TDownloader> DownloadAsync(
        IEnumerable<IIdentifiable> identifiables,
        String? fileExtension = default,
        CancellationToken cancellationToken = default);

    new Task<TDownloader> DownloadAsync(
        IEnumerable<(IIdentifiable Identifiable, String FileName)> identifiables,
        CancellationToken cancellationToken = default);
}

public abstract class HomeBallsDataDownloader<TData> :
    IHomeBallsDataDownloader
{
   protected HomeBallsDataDownloader(ILogger? logger = default)
   {
       Logger = logger;
       EventRaiser = new EventRaiser(Logger).RaisedBy(this);
   }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event EventHandler<TimedActionStartingEventArgs>? DataDownloading;

    public event EventHandler<TimedActionEndedEventArgs>? DataDownloaded;

    public virtual async Task DownloadAsync(
        IIdentifiable identifiable,
        String? fileName = default,
        CancellationToken cancellationToken = default)
    {
        fileName = fileName ??= GetFileName(identifiable);
        var start = EventRaiser.Raise(DataDownloading, fileName);

        await SaveDataAsync(
            identifiable,
            fileName,
            await DownloadCoreAsync(fileName, cancellationToken),
            cancellationToken);

        EventRaiser.Raise(DataDownloaded, start.StartTime, fileName);
    }

    public virtual Task DownloadAsync(
        IEnumerable<IIdentifiable> identifiables,
        String? fileExtension = default,
        CancellationToken cancellationToken = default) =>
        DownloadAsync(
            identifiables.Select(identifiable => AddFileExtension(identifiable, fileExtension)),
            cancellationToken);

    public virtual async Task DownloadAsync(
        IEnumerable<(IIdentifiable Identifiable, String FileName)> identifiables,
        CancellationToken cancellationToken = default)
    {
        var downloadTasks = identifiables
            .Select(pair => DownloadAsync(
                pair.Identifiable,
                pair.FileName,
                cancellationToken));

        await Task.WhenAll(downloadTasks);
    }

    protected internal virtual (IIdentifiable Identifiable, String FileName) AddFileExtension(
        IIdentifiable identifiable,
        String? fileExtension = default)
    {
        var identifier = identifiable.Identifier;
        return (identifiable, fileExtension == default ?
            identifier :
            identifier.AddFileExtension(fileExtension));
    }

    protected internal abstract String GetFileName(IIdentifiable identifiable);

    protected internal abstract Task<TData> DownloadCoreAsync(
        String fileName,
        CancellationToken cancellationToken = default);

    protected internal abstract Task SaveDataAsync(
        IIdentifiable identifiable,
        String fileName,
        TData data,
        CancellationToken cancellationToken = default);
}