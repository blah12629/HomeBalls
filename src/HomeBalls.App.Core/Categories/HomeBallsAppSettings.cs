namespace CEo.Pokemon.HomeBalls.App.Core.Categories;

public interface IHomeBallsAppSettings :
    IHomeBallsAppCateogry
{
    IHomeBallsObservableCollection<UInt16> BallIdsShown { get; }

    IObservableProperty<Boolean> IsIllegalEntryShown { get; }
}

public class HomeBallsAppSettings :
    HomeBallsAppCategory,
    IHomeBallsAppSettings
{
    public HomeBallsAppSettings(
        ILoggerFactory? loggerFactory = default) :
        base(default, loggerFactory?.CreateLogger(typeof(HomeBallsAppSettings)))
    {
        LoggerFactory = loggerFactory;

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

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal override void GenerateIconSvgPaths(List<String> paths) { }
}