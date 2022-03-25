namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppEntriesSettings :
    IHomeBallsAppSettingsProperty,
    IAsyncLoadable<IHomeBallsAppEntriesSettings>
{
    IHomeBallsAppEntriesBallIdsSettings BallIds { get; }

    IHomeBallsAppEntriesRowIdentifierSettings RowIdentifier { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingIllegalEntries { get; }
}

public class HomeBallsAppEntriesSettings :
    HomeBallsAppSettingsPropertyRoot,
    IHomeBallsAppEntriesSettings,
    IAsyncLoadable<HomeBallsAppEntriesSettings>
{
    public HomeBallsAppEntriesSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger)
    {
        BallIds = new HomeBallsAppEntriesBallIdsSettings(
            nameof(BallIds), CreateSubpropertyIdentifier(nameof(BallIds)),
            LocalStorage, JSRuntime, EventRaiser, Logger);
        RowIdentifier = new HomeBallsAppEntriesRowIdentifierSettings(
            nameof(RowIdentifier), CreateSubpropertyIdentifier(nameof(RowIdentifier)),
            LocalStorage, JSRuntime, EventRaiser, Logger);
        IsShowingIllegalEntries = CreateValueProperty(false, nameof(IsShowingIllegalEntries));
    }

    public IHomeBallsAppEntriesBallIdsSettings BallIds { get; }

    public IHomeBallsAppEntriesRowIdentifierSettings RowIdentifier { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingIllegalEntries { get; }

    new public virtual async ValueTask<HomeBallsAppEntriesSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    protected internal override IReadOnlyCollection<IHomeBallsAppSettingsProperty> CreateSubpropertyCollection() =>
        new IHomeBallsAppSettingsProperty[] { BallIds, RowIdentifier, IsShowingIllegalEntries }.AsReadOnly();

    async ValueTask<IHomeBallsAppEntriesSettings> IAsyncLoadable<IHomeBallsAppEntriesSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}