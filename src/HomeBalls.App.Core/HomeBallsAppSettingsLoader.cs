namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppSettingsLoader
{
    Task LoadSettingsAsync(
        IHomeBallsAppSettings settings,
        CancellationToken cancellationToken = default);
}

public class HomeBallsAppSettingsLoader : IHomeBallsAppSettingsLoader
{
    public HomeBallsAppSettingsLoader(
        ILogger? logger = default) =>
        Logger = logger;

    protected internal virtual ILogger? Logger { get; }

    public virtual async Task LoadSettingsAsync(
        IHomeBallsAppSettings settings,
        CancellationToken cancellationToken = default) =>
        await settings.EnsureLoadedAsync(cancellationToken);
}