namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppThemeSettings :
    IProperty,
    IIdentifiable,
    IAsyncLoadable<IHomeBallsAppThemeSettings>
{
    IHomeBallsAppSettingsValueProperty<String> Id { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsDarkMode { get; }
}

public class HomeBallsAppThemeSettings :
    HomeBallsAppSettingsPropertyBase,
    IHomeBallsAppThemeSettings,
    IAsyncLoadable<HomeBallsAppThemeSettings>
{
    public HomeBallsAppThemeSettings(
        String propertyName,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        this(propertyName, propertyName, localStorage, jsRuntime, eventRaiser, loggerFactory) { }

    public HomeBallsAppThemeSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, loggerFactory)
    {
        Id = CreateThemeProperty(DreamThemeId, nameof(Id));
        IsDarkMode = CreateThemeProperty(false, nameof(IsDarkMode));
    }

    public IHomeBallsAppSettingsValueProperty<String> Id { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsDarkMode { get; }

    protected internal override IReadOnlyCollection<IAsyncLoadable> CreateLoadables() =>
        Array.AsReadOnly(new IAsyncLoadable[] { Id, IsDarkMode });

    protected internal virtual HomeBallsAppThemeProperty<T> CreateThemeProperty<T>(
        T defaultValue,
        String propertyName) =>
        new HomeBallsAppThemeProperty<T>(
            defaultValue,
            CreatePropertyName(propertyName),
            LocalStorage, JSRuntime, EventRaiser, Logger);

    new public virtual async ValueTask<HomeBallsAppThemeSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    async ValueTask<IHomeBallsAppThemeSettings> IAsyncLoadable<IHomeBallsAppThemeSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}
