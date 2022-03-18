namespace CEo.Pokemon.HomeBalls.App.Settings;

public class HomeBallsAppThemeProperty<T> :
    HomeBallsAppSettingsValueProperty<T>
{
    public HomeBallsAppThemeProperty(
        T defaultValue,
        String propertyName,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default,
        IEqualityComparer<T>? comparer = default) :
        base(defaultValue, propertyName, localStorage, eventRaiser, logger, comparer) =>
        JSRuntime = jsRuntime;

    public HomeBallsAppThemeProperty(
        T defaultValue,
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default,
        IEqualityComparer<T>? comparer = default) :
        base(defaultValue, propertyName, identifier, localStorage, eventRaiser, logger, comparer) =>
        JSRuntime = jsRuntime;

    protected internal IJSRuntime JSRuntime { get; }

    public override async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveAsync(cancellationToken);
        await UpdateTheme(cancellationToken);
    }

    protected internal virtual Task UpdateTheme(
        CancellationToken cancellationToken = default) =>
        JSRuntime
            .InvokeVoidAsync(UpdateThemeFunctionId, new Object?[] { default, default }!)
            .AsTask();
}