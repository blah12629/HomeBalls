namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsEntryTableSettings :
    IProperty,
    IIdentifiable,
    IAsyncLoadable<IHomeBallsEntryTableSettings>
{
    IHomeBallsEntryBallIdsSettings BallIdsShown { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingIllegalEntries { get; }

    IHomeBallsEntryColumnIdentifierSettings ColumnIdentifier { get; }
}

public class HomeBallsEntryTableSettings :
    HomeBallsAppSettingsPropertyBase,
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
        BallIdsShown = new HomeBallsEntryBallIdsSettings(
            CreatePropertyName(nameof(BallIdsShown)),
            LocalStorage, JSRuntime, EventRaiser, LoggerFactory);
        IsShowingIllegalEntries = CreateValueProperty(false, nameof(IsShowingIllegalEntries));
        ColumnIdentifier = new HomeBallsEntryColumnIdentifierSettings(
            CreatePropertyName(nameof(ColumnIdentifier)),
            LocalStorage, JSRuntime, EventRaiser, LoggerFactory);
    }

    public IHomeBallsEntryBallIdsSettings BallIdsShown { get; }

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
