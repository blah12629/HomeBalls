namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsEntryBallIdsSettings :
    IProperty,
    IIdentifiable,
    IAsyncLoadable<IHomeBallsEntryBallIdsSettings>
{
    IHomeBallsAppSettingsValueProperty<Boolean> IsUsingDefault { get; }

    IHomeBallsAppSettingsCollectionProperty<UInt16> Collection { get; }

    IHomeBallsReadOnlyList<UInt16> DefaultValues { get; }
}

public class HomeBallsEntryBallIdsSettings :
    HomeBallsAppSettingsPropertyBase,
    IHomeBallsEntryBallIdsSettings,
    IAsyncLoadable<HomeBallsEntryBallIdsSettings>
{
    public HomeBallsEntryBallIdsSettings(
        String propertyName,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        this(propertyName, propertyName, localStorage, jsRuntime, eventRaiser, loggerFactory) { }

    public HomeBallsEntryBallIdsSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, loggerFactory)
    {
        IsUsingDefault = CreateValueProperty(true, nameof(IsUsingDefault));

        var ballIdsShownSilent = new List<UInt16>
        {
            453, 454, 449, 450, 452, 455, 451,
            617, 457, 5, 887
        };
        var ballIdsShown = new HomeBallsObservableSet<UInt16>(
            ballIdsShownSilent,
            logger: LoggerFactory?.CreateLogger<HomeBallsObservableSet<UInt16>>());
        Collection = new HomeBallsAppSettingsCollectionProperty<UInt16>(
            ballIdsShown, ballIdsShownSilent,
            CreatePropertyName(nameof(Collection)),
            LocalStorage, EventRaiser, Logger);
    }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsUsingDefault { get; }

    public IHomeBallsAppSettingsCollectionProperty<UInt16> Collection { get; }

    public IHomeBallsReadOnlyList<UInt16> DefaultValues => throw new NotImplementedException();

    new public virtual async ValueTask<HomeBallsEntryBallIdsSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    async ValueTask<IHomeBallsEntryBallIdsSettings> IAsyncLoadable<IHomeBallsEntryBallIdsSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}