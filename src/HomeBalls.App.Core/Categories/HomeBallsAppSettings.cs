namespace CEo.Pokemon.HomeBalls.App.Categories;

public interface IHomeBallsAppSettings :
    IHomeBallsAppCateogry,
    IAsyncLoadable<IHomeBallsAppSettings>
{
    IHomeBallsAppSettingsCollectionProperty<UInt16> BallIdsShown { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsIllegalEntryShown { get; }

    IHomeBallsAppSettingsValueProperty<String> ThemeId { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsDarkMode { get; }
}

public class HomeBallsAppSettings :
    HomeBallsAppCategory,
    IHomeBallsAppSettings,
    IAsyncLoadable<HomeBallsAppSettings>
{
    public HomeBallsAppSettings(
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        ILoggerFactory? loggerFactory = default) :
        base(default, loggerFactory?.CreateLogger(typeof(HomeBallsAppSettings)))
    {
        LocalStorage = localStorage;
        JSRuntime = jsRuntime;
        LoggerFactory = loggerFactory;

        var ballIdsShownSilent = new List<UInt16>
        {
            453, 454, 449, 450, 452, 455, 451,
            617, 457, 5, 887
        };
        var ballIdsShown = new HomeBallsObservableSet<UInt16>(
            ballIdsShownSilent,
            logger: LoggerFactory?.CreateLogger<HomeBallsObservableSet<UInt16>>());
        BallIdsShown = new HomeBallsAppSettingsCollectionProperty<UInt16>(
            ballIdsShown, ballIdsShownSilent,
            nameof(BallIdsShown),
            LocalStorage, EventRaiser, Logger);

        IsIllegalEntryShown = new HomeBallsAppSettingsValueProperty<Boolean>(
            false,
            nameof(IsIllegalEntryShown),
            LocalStorage, EventRaiser, Logger);

        ThemeId = new HomeBallsAppThemeProperty<String>(
            DreamThemeId,
            nameof(ThemeId),
            LocalStorage, JSRuntime, EventRaiser, Logger);

        IsDarkMode = new HomeBallsAppThemeProperty<Boolean>(
            false,
            nameof(IsDarkMode),
            LocalStorage, JSRuntime, EventRaiser, Logger);

        Loadables = new List<IAsyncLoadable>
        {
            BallIdsShown,
            IsIllegalEntryShown,
            ThemeId,
            IsDarkMode
        }.AsReadOnly();
    }

    public IHomeBallsAppSettingsCollectionProperty<UInt16> BallIdsShown { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsIllegalEntryShown { get; }

    public IHomeBallsAppSettingsValueProperty<String> ThemeId { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsDarkMode { get; }

    protected internal IReadOnlyCollection<IAsyncLoadable> Loadables { get; }

    protected internal ILocalStorageService LocalStorage { get; }

    protected internal IJSRuntime JSRuntime { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    public virtual async ValueTask<HomeBallsAppSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        var tasks = Loadables.Select(loadable => loadable
            .EnsureLoadedAsync(cancellationToken)
            .AsTask());

        await Task.WhenAll(tasks);
        return this;
    }

    protected internal override void GenerateIconSvgPaths(List<String> paths)
    {
        paths.Add("M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z");
        paths.Add("M15 12a3 3 0 11-6 0 3 3 0 016 0z");
    }

    async ValueTask<IHomeBallsAppSettings> IAsyncLoadable<IHomeBallsAppSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}