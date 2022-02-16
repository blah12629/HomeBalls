namespace CEo.Pokemon.HomeBalls.App.Categories;

public interface IHomeBallsAppSettings :
    IHomeBallsAppCateogry,
    IAsyncLoadable<IHomeBallsAppSettings>
{
    IHomeBallsAppSettingsCollectionProperty<UInt16> BallIdsShown { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsIllegalEntryShown { get; }

    IHomeBallsAppSettingsValueProperty<String> ThemeId { get; }
}

public class HomeBallsAppSettings :
    HomeBallsAppCategory,
    IHomeBallsAppSettings,
    IAsyncLoadable<HomeBallsAppSettings>
{
    public HomeBallsAppSettings(
        ILocalStorageService localStorage,
        ILoggerFactory? loggerFactory = default) :
        base(default, loggerFactory?.CreateLogger(typeof(HomeBallsAppSettings)))
    {
        LocalStorage = localStorage;
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

        ThemeId = new HomeBallsAppSettingsValueProperty<String>(
            DefaultThemeId,
            nameof(ThemeId),
            LocalStorage, EventRaiser, Logger);

        Loadables = new List<IAsyncLoadable>
        {
            BallIdsShown,
            IsIllegalEntryShown,
            ThemeId
        }.AsReadOnly();
    }

    public IHomeBallsAppSettingsCollectionProperty<UInt16> BallIdsShown { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsIllegalEntryShown { get; }

    public IHomeBallsAppSettingsValueProperty<String> ThemeId { get; }

    protected internal IReadOnlyCollection<IAsyncLoadable> Loadables { get; }

    protected internal ILocalStorageService LocalStorage { get; }

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

    protected internal override void GenerateIconSvgPaths(List<String> paths) { }

    async ValueTask<IHomeBallsAppSettings> IAsyncLoadable<IHomeBallsAppSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}