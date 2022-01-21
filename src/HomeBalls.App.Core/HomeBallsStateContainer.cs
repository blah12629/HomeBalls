namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsStateContainer
{
    IObservableProperty<String?> LoadingMessage { get; }
}

public class HomeBallsStateContainer :
    IHomeBallsStateContainer
{
    public HomeBallsStateContainer(
        ILoggerFactory? loggerFactory = default)
    {
        LoggerFactory = loggerFactory;
        Logger = createLogger<HomeBallsStateContainer>();
        EventRaiser = new EventRaiser(createLogger<EventRaiser>()).RaisedBy(this);

        LoadingMessage = createObservable<String?>(default, nameof(LoadingMessage));

        ILogger<T>? createLogger<T>() => LoggerFactory?.CreateLogger<T>();
        IObservableProperty<T> createObservable<T>(
            T defaultValue,
            String propertyName) =>
            new ObservableProperty<T>(defaultValue, propertyName, EventRaiser, Logger);
    }
    
    public IObservableProperty<String?> LoadingMessage { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }
}