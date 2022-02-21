namespace CEo.Pokemon.HomeBalls.App.Categories.Settings;

public interface IHomeBallsAppBaseSettings : IProperty, IIdentifiable, IAsyncLoadable { }

public abstract class HomeBallsAppBaseSettings : IHomeBallsAppBaseSettings
{
    IReadOnlyCollection<IAsyncLoadable>? _loadables;

    protected HomeBallsAppBaseSettings(
        String propertyName,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        this(propertyName, propertyName, localStorage, jsRuntime, eventRaiser ,loggerFactory) { }

    protected HomeBallsAppBaseSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default)
    {
        (PropertyName, Identifier) = (propertyName, identifier);
        (LocalStorage, JSRuntime) = (localStorage, jsRuntime);
        (EventRaiser, LoggerFactory) = (eventRaiser, loggerFactory);
        Logger = LoggerFactory?.CreateLogger(GetType());
    }

    public String PropertyName { get; }

    public String Identifier { get; }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IJSRuntime JSRuntime { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal ILogger? Logger { get; }

    protected internal IReadOnlyCollection<IAsyncLoadable> Loadables => _loadables ??= CreateLoadables();

    protected internal virtual IReadOnlyCollection<IAsyncLoadable> CreateLoadables() =>
        GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(property => property.PropertyType.IsAssignableTo(typeof(IAsyncLoadable)))
            .Select(property => property.GetValue(this) as IAsyncLoadable ??
                throw new InvalidCastException())
            .ToList().AsReadOnly();

    protected internal virtual String CreatePropertyName(String propertyName) =>
        String.Join('.', new[] { PropertyName, propertyName });

    protected internal virtual IHomeBallsAppSettingsValueProperty<T> CreateValueProperty<T>(
        T defaultValue,
        String propertyName) =>
        new HomeBallsAppSettingsValueProperty<T>(
            defaultValue,
            CreatePropertyName(propertyName),
            LocalStorage, EventRaiser, Logger);

    public virtual async ValueTask EnsureLoadedAsync(
        CancellationToken cancellationToken = default) =>
        await Task.WhenAll(Loadables.Select(loadable => loadable
            .EnsureLoadedAsync(cancellationToken)
            .AsTask()));
}
