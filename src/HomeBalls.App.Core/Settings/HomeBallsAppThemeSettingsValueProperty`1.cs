namespace CEo.Pokemon.HomeBalls.App.Settings;

public class HomeBallsAppThemeSettingsValueProperty<TValue> :
    HomeBallsAppSettingsValueProperty<TValue>,
    IAsyncLoadable<HomeBallsAppThemeSettingsValueProperty<TValue>>
{
    public HomeBallsAppThemeSettingsValueProperty(
        IMutableNotifyingProperty<TValue> property,
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(property, propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger) { }

    new public virtual async ValueTask<HomeBallsAppThemeSettingsValueProperty<TValue>> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    protected internal override async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveAsync(cancellationToken);
        await UpdateAppTheme(cancellationToken);
    }

    protected internal virtual ValueTask UpdateAppTheme(CancellationToken cancellationToken = default) =>
        JSRuntime.InvokeVoidAsync(UpdateThemeFunctionId, new Object?[] { default, default }!);
}