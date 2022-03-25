namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppEntriesRowIdentifierSettings :
    IHomeBallsAppSettingsProperty,
    IHomeBallsAppSettingsDefaultUsable,
    IAsyncLoadable<IHomeBallsAppEntriesRowIdentifierSettings>
{
    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingNumber { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingSprite { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingName { get; }
}

public class HomeBallsAppEntriesRowIdentifierSettings :
    HomeBallsAppSettingsPropertyRoot,
    IHomeBallsAppEntriesRowIdentifierSettings,
    IAsyncLoadable<HomeBallsAppEntriesRowIdentifierSettings>
{
    public HomeBallsAppEntriesRowIdentifierSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger)
    {
        IsShowingNumber = CreateValueProperty<Boolean>(true, nameof(IsShowingNumber));
        IsShowingSprite = CreateValueProperty<Boolean>(false, nameof(IsShowingSprite));
        IsShowingName = CreateValueProperty<Boolean>(true, nameof(IsShowingName));
        IsUsingDefault = CreateValueProperty<Boolean>(true, nameof(IsUsingDefault));
    }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingNumber { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingSprite { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingName { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsUsingDefault { get; }

    new public virtual async ValueTask<HomeBallsAppEntriesRowIdentifierSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    protected internal override IReadOnlyCollection<IHomeBallsAppSettingsProperty> CreateSubpropertyCollection() =>
        new IHomeBallsAppSettingsProperty[] { IsShowingNumber, IsShowingSprite, IsShowingName, IsUsingDefault }.AsReadOnly();

    async ValueTask<IHomeBallsAppEntriesRowIdentifierSettings> IAsyncLoadable<IHomeBallsAppEntriesRowIdentifierSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}