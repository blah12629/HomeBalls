namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsSettings
{
    IHomeBallsObservableCollection<UInt16> BallIdsShown { get; }

    IObservableProperty<Boolean> IsIllegalEntryShown { get; }
}

public class HomeBallsSettings :
    IHomeBallsSettings
{
    public HomeBallsSettings(
        ILoggerFactory? loggerFactory = default)
    {
        LoggerFactory = loggerFactory;
        EventRaiser = new EventRaiser(createLogger<EventRaiser>()).RaisedBy(this);
        Logger = createLogger<HomeBallsSettings>();

        BallIdsShown = new HomeBallsObservableSet<UInt16>(createLogger<HomeBallsObservableSet<UInt16>>());
        IsIllegalEntryShown = createObservable<Boolean>(false, nameof(IsIllegalEntryShown));

        ILogger<T>? createLogger<T>() => LoggerFactory?.CreateLogger<T>();
        IObservableProperty<T> createObservable<T>(
            T defaultValue,
            String propertyName) =>
            new ObservableProperty<T>(defaultValue, propertyName, EventRaiser, Logger);
    }

    public IHomeBallsObservableCollection<UInt16> BallIdsShown { get; }

    public IObservableProperty<Boolean> IsIllegalEntryShown { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }
}