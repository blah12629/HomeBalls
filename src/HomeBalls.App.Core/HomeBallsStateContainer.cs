namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsStateContainer
{
    IObservableProperty<String?> ActiveCategoryId { get; }

    IObservableProperty<String?> LoadingMessage { get; }

    Task<IHomeBallsStateContainer> EnsurePresetsLoaded(
        CancellationToken cancellationToken = default);
}

public class HomeBallsStateContainer :
    IHomeBallsStateContainer
{
    public HomeBallsStateContainer(
        ILocalStorageService localStorage,
        ILoggerFactory? loggerFactory = default)
    {
        LocalStorage = localStorage;

        LoggerFactory = loggerFactory;
        Logger = createLogger<HomeBallsStateContainer>();
        EventRaiser = new EventRaiser(createLogger<EventRaiser>()).RaisedBy(this);

        ActiveCategoryId = createObservable<String?>(default, nameof(ActiveCategoryId));
        LoadingMessage = createObservable<String?>(default, nameof(LoadingMessage));

        ILogger<T>? createLogger<T>() => LoggerFactory?.CreateLogger<T>();
        IObservableProperty<T> createObservable<T>(
            T defaultValue,
            String propertyName) =>
            new ObservableProperty<T>(
                defaultValue,
                propertyName,
                $"appSettings.{propertyName.ToCamelCase()}",
                EventRaiser,
                Logger);
    }

    public IObservableProperty<String?> ActiveCategoryId { get; }

    public IObservableProperty<String?> LoadingMessage { get; }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    public virtual async Task<HomeBallsStateContainer> EnsurePresetsLoaded(
        CancellationToken cancellationToken = default)
    {
        await EnsurePresetLoaded(ActiveCategoryId, cancellationToken);
        return this;
    }

    protected internal virtual async Task<HomeBallsStateContainer> EnsurePresetLoaded<T>(
        IObservableProperty<T> property,
        CancellationToken cancellationToken = default)
    {
        property.Value = typeof(T) == typeof(String) ?
            await LocalStorage.GetItemAsync<T>(property.Identifier, cancellationToken) :
            (T)(Object)await LocalStorage.GetItemAsStringAsync(
                property.Identifier,
                cancellationToken);

        return this;
    }

    async Task<IHomeBallsStateContainer> IHomeBallsStateContainer
        .EnsurePresetsLoaded(CancellationToken cancellationToken) =>
        await EnsurePresetsLoaded(cancellationToken);
}