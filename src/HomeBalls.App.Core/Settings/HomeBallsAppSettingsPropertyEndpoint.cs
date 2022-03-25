namespace CEo.Pokemon.HomeBalls.App.Settings;

public abstract class HomeBallsAppSettingsPropertyEndpoint :
    HomeBallsAppSettingsProperty
{
    protected HomeBallsAppSettingsPropertyEndpoint(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger) { }

    protected internal override async Task EnsureLoadedCoreAsync(CancellationToken cancellationToken = default)
    {
        if (!await LocalStorage.ContainKeyAsync(Identifier, cancellationToken))
            await SaveAsync(cancellationToken);

        await LoadAsync(cancellationToken);
    }

    protected internal abstract Task LoadAsync(CancellationToken cancellationToken = default);

    protected internal abstract Task SaveAsync(CancellationToken cancellationToken = default);
}