namespace CEo.Pokemon.HomeBalls.App.Core;

public interface INotifyDataDownloaded
{
    event EventHandler<TimedActionEndedEventArgs>? DataDownloaded;
}

public interface INotifyDataDownloading : INotifyDataDownloaded
{
    event EventHandler<TimedActionStartingEventArgs>? DataDownloading;
}