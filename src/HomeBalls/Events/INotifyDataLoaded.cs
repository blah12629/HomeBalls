namespace CEo.Pokemon.HomeBalls;

public interface INotifyDataLoaded
{
    event EventHandler<TimedActionEndedEventArgs>? DataLoaded;
}

public interface INotifyDataLoading : INotifyDataLoaded
{
    event EventHandler<TimedActionStartingEventArgs>? DataLoading;
}