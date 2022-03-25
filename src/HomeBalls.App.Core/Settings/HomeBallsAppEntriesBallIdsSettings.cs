namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppEntriesBallIdsSettings :
    IHomeBallsAppSettingsProperty,
    IHomeBallsAppSettingsDefaultUsable,
    IAsyncLoadable<IHomeBallsAppEntriesBallIdsSettings>
{
    IHomeBallsAppSettingsCollectionProperty<UInt16> Collection { get; }

    IReadOnlyCollection<UInt16> DefaultValues { get; }
}

public class HomeBallsAppEntriesBallIdsSettings :
    HomeBallsAppSettingsPropertyRoot,
    IHomeBallsAppEntriesBallIdsSettings,
    IAsyncLoadable<HomeBallsAppEntriesBallIdsSettings>
{
    public HomeBallsAppEntriesBallIdsSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger)
    {
        var collection = new HomeBallsBallIdsShownSet(EventRaiser, Logger);

        DefaultValues = DefaultBalIdsShown;
        foreach (var id in DefaultValues) collection.Add(id);

        Collection = CreateCollectionProperty(collection, nameof(Collection));
        IsUsingDefault = CreateValueProperty(true, nameof(IsUsingDefault));
    }

    public IHomeBallsAppSettingsCollectionProperty<UInt16> Collection { get; }

    public IReadOnlyCollection<UInt16> DefaultValues { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsUsingDefault { get; }

    new public virtual async ValueTask<HomeBallsAppEntriesBallIdsSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    protected internal override IReadOnlyCollection<IHomeBallsAppSettingsProperty> CreateSubpropertyCollection() =>
        new IHomeBallsAppSettingsProperty[] { Collection, IsUsingDefault }.AsReadOnly();

    async ValueTask<IHomeBallsAppEntriesBallIdsSettings> IAsyncLoadable<IHomeBallsAppEntriesBallIdsSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}