namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppThemeSettings :
    IHomeBallsAppSettingsProperty,
    IAsyncLoadable<IHomeBallsAppThemeSettings>
{
    IHomeBallsAppSettingsValueProperty<String> Id { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsDarkMode { get; }
}

public class HomeBallsAppThemeSettings :
    HomeBallsAppSettingsPropertyRoot,
    IHomeBallsAppThemeSettings,
    IAsyncLoadable<HomeBallsAppThemeSettings>
{
    public HomeBallsAppThemeSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger)
    {
        Id = CreateValueProperty<String>(DreamThemeId, nameof(Id));
        IsDarkMode = CreateValueProperty<Boolean>(false, nameof(IsDarkMode));
    }

    public IHomeBallsAppSettingsValueProperty<String> Id { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsDarkMode { get; }

    new public virtual async ValueTask<HomeBallsAppThemeSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    protected internal override IHomeBallsAppSettingsValueProperty<TValue> CreateValueProperty<TValue>(
        TValue defaultValue,
        String propertyName) =>
        new HomeBallsAppThemeSettingsValueProperty<TValue>(
            new MutableNotifyingProperty<TValue>(defaultValue, propertyName, EventRaiser, Logger),
            propertyName, CreateSubpropertyIdentifier(propertyName),
            LocalStorage, JSRuntime, EventRaiser, Logger);

    protected internal override IReadOnlyCollection<IHomeBallsAppSettingsProperty> CreateSubpropertyCollection() =>
        new IHomeBallsAppSettingsProperty[] { Id, IsDarkMode }.AsReadOnly();

    async ValueTask<IHomeBallsAppThemeSettings> IAsyncLoadable<IHomeBallsAppThemeSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}