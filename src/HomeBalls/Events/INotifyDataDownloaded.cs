namespace CEo.Pokemon.HomeBalls;

public interface INotifyDataDownloaded
{
    event EventHandler<TimedActionEndedEventArgs>? DataDownloaded;
}

public interface INotifyDataDownloading : INotifyDataDownloaded
{
    event EventHandler<TimedActionStartingEventArgs>? DataDownloading;
}