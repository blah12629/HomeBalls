namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppSettingsLoader
{
    Task LoadSettingsAsync(
        IHomeBallsAppSettings settings,
        CancellationToken cancellationToken = default);

    event EventHandler<TimedActionStartingEventArgs>? SettingsLoading;

    event EventHandler<TimedActionEndedEventArgs>? SettingsLoaded;
}

public class HomeBallsAppSettingsLoader : IHomeBallsAppSettingsLoader
{
    public HomeBallsAppSettingsLoader(
        ILogger? logger = default)
    {
        Logger = logger;
        EventRaiser = new EventRaiser(Logger).RaisedBy(this);
    }

    protected internal virtual IEventRaiser EventRaiser { get; }

    protected internal virtual ILogger? Logger { get; }

    public event EventHandler<TimedActionStartingEventArgs>? SettingsLoading;

    public event EventHandler<TimedActionEndedEventArgs>? SettingsLoaded;

    public virtual async Task LoadSettingsAsync(
        IHomeBallsAppSettings settings,
        CancellationToken cancellationToken = default)
    {
        var start = EventRaiser.Raise(SettingsLoading);
        await settings.EnsureLoadedAsync(cancellationToken);
        EventRaiser.Raise(SettingsLoaded, start.StartTime);
    }
}