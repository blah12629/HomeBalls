namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppStateContainer
{
    IMutableNotifyingProperty<String?> ActiveCategoryId { get; }

    IMutableNotifyingProperty<String?> LoadingMessage { get; }
}

public class HomeBallsAppStateContainer :
    IHomeBallsAppStateContainer
{
    public HomeBallsAppStateContainer(
        ILogger? logger = default)
    {
        EventRaiser = new EventRaiser().RaisedBy(this);
        Logger = logger;

        ActiveCategoryId = new MutableNotifyingProperty<String?>(default, nameof(ActiveCategoryId), EventRaiser, Logger);
        LoadingMessage = new MutableNotifyingProperty<String?>(default, nameof(LoadingMessage), EventRaiser, Logger);
    }

    public IMutableNotifyingProperty<String?> ActiveCategoryId { get; }

    public IMutableNotifyingProperty<String?> LoadingMessage { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }
}

// public class HomeBallsAppStateContainer :
//     IHomeBallsAppStateContainer
// {
//     public HomeBallsAppStateContainer(
//         ILocalStorageService localStorage,
//         ILoggerFactory? loggerFactory = default)
//     {
//         LocalStorage = localStorage;

//         LoggerFactory = loggerFactory;
//         Logger = createLogger<HomeBallsAppStateContainer>();
//         EventRaiser = new EventRaiser(createLogger<EventRaiser>()).RaisedBy(this);

//         ActiveCategoryId = createObservable<String?>(default, nameof(ActiveCategoryId));
//         LoadingMessage = createObservable<String?>(default, nameof(LoadingMessage));

//         ILogger<T>? createLogger<T>() => LoggerFactory?.CreateLogger<T>();
//         IObservableProperty<T> createObservable<T>(
//             T defaultValue,
//             String propertyName) =>
//             new ObservableProperty<T>(
//                 defaultValue,
//                 propertyName,
//                 $"appSettings.{propertyName.ToCamelCase()}",
//                 EventRaiser,
//                 Logger);
//     }

//     public IObservableProperty<String?> ActiveCategoryId { get; }

//     public IObservableProperty<String?> LoadingMessage { get; }

//     protected internal ILocalStorageService LocalStorage { get; }

//     protected internal IEventRaiser EventRaiser { get; }

//     protected internal ILogger? Logger { get; }

//     protected internal ILoggerFactory? LoggerFactory { get; }

//     public virtual async Task<HomeBallsAppStateContainer> EnsureLoadedAsync(
//         CancellationToken cancellationToken = default)
//     {
//         await EnsureLoadedAsync(ActiveCategoryId, cancellationToken);
//         return this;
//     }

//     public virtual async Task<HomeBallsAppStateContainer> EnsureLoadedAsync<T>(
//         IObservableProperty<T> property,
//         CancellationToken cancellationToken = default)
//     {
//         property.Value = typeof(T) == typeof(String) ?
//             await LocalStorage.GetItemAsync<T>(property.Identifier, cancellationToken) :
//             (T)(Object)await LocalStorage.GetItemAsStringAsync(
//                 property.Identifier,
//                 cancellationToken);

//         return this;
//     }
// }