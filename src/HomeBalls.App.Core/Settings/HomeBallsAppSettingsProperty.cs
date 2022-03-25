namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppSettingsProperty :
    IProperty,
    IIdentifiable,
    IAsyncLoadable,
    INotifyDataLoading { }

public abstract class HomeBallsAppSettingsProperty :
    IHomeBallsAppSettingsProperty
{
    protected HomeBallsAppSettingsProperty(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default)
    {
        (PropertyName, Identifier) = (propertyName, identifier);
        (LocalStorage, JSRuntime) = (localStorage, jsRuntime);
        (EventRaiser, Logger) = (eventRaiser, logger);
    }

    public String PropertyName { get; }

    public String Identifier { get; }

    public Boolean IsLoaded { get; protected internal set; }

    public Boolean IsLoading { get; protected internal set; }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IJSRuntime JSRuntime { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event EventHandler<TimedActionStartingEventArgs>? DataLoading;

    public event EventHandler<TimedActionEndedEventArgs>? DataLoaded;

    public async ValueTask EnsureLoadedAsync(CancellationToken cancellationToken = default)
    {
        if (IsLoading || IsLoaded) return;
        var start = EventRaiser.Raise(DataLoading, Identifier);
        IsLoading = true;

        try { await EnsureLoadedCoreAsync(cancellationToken); }
        catch { IsLoading = false; throw; }

        EventRaiser.Raise(DataLoaded, start.StartTime, Identifier);
        (IsLoading, IsLoaded) = (false, true);
    }

    protected internal abstract Task EnsureLoadedCoreAsync(CancellationToken cancellationToken = default);
}