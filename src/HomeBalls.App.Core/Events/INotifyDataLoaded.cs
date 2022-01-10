namespace CEo.Pokemon.HomeBalls.App.Core;

public interface INotifyDataLoaded
{
    event EventHandler<TimedActionEndedEventArgs>? DataLoaded;
}

public interface INotifyDataLoading : INotifyDataLoaded
{
    event EventHandler<TimedActionStartingEventArgs>? DataLoading;
}