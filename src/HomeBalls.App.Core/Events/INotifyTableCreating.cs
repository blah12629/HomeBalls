namespace CEo.Pokemon.HomeBalls.App.Core;

public interface INotifyTableCreated
{
    event EventHandler<TimedActionEndedEventArgs>? TableCreated;
}

public interface INotifyTableCreating : INotifyTableCreated
{
    event EventHandler<TimedActionStartingEventArgs>? TableCreating;
}