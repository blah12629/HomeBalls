namespace CEo.Pokemon.HomeBalls.App.Categories.Settings;

public interface IHomeBallsEntryTableSettings :
    IHomeBallsAppBaseSettings,
    IAsyncLoadable<IHomeBallsEntryTableSettings>
{
    IHomeBallsAppSettingsCollectionProperty<UInt16> BallIdsShown { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingIllegalEntries { get; }

    IHomeBallsEntryColumnIdentifierSettings ColumnIdentifier { get; }
}

public class HomeBallsEntryTableSettings :
    HomeBallsAppBaseSettings,
    IHomeBallsEntryTableSettings,
    IAsyncLoadable<HomeBallsEntryTableSettings>
{
    public HomeBallsEntryTableSettings(
        String propertyName,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        this(propertyName, propertyName, localStorage, jsRuntime, eventRaiser, loggerFactory) { }

    public HomeBallsEntryTableSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, loggerFactory)
    {
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

        IsShowingIllegalEntries = CreateValueProperty(false, nameof(IsShowingIllegalEntries));
        ColumnIdentifier = new HomeBallsEntryColumnIdentifierSettings(
            CreatePropertyName(nameof(ColumnIdentifier)),
            LocalStorage, JSRuntime, EventRaiser, LoggerFactory);
    }

    public IHomeBallsAppSettingsCollectionProperty<UInt16> BallIdsShown { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingIllegalEntries { get; }

    public IHomeBallsEntryColumnIdentifierSettings ColumnIdentifier { get; }

    protected internal override IReadOnlyCollection<IAsyncLoadable> CreateLoadables() =>
        Array.AsReadOnly(new IAsyncLoadable[]
        {
            BallIdsShown,
            IsShowingIllegalEntries,
            ColumnIdentifier
        });

    new public virtual async ValueTask<HomeBallsEntryTableSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    async ValueTask<IHomeBallsEntryTableSettings> IAsyncLoadable<IHomeBallsEntryTableSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}
