namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsEntryColumnIdentifierSettings :
    IProperty,
    IIdentifiable,
    IAsyncLoadable<IHomeBallsEntryColumnIdentifierSettings>
{
    IHomeBallsAppSettingsValueProperty<Boolean> IsUsingDefault { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingNumber { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingSprite { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingName { get; }
}

public class HomeBallsEntryColumnIdentifierSettings :
    HomeBallsAppSettingsPropertyBase,
    IHomeBallsEntryColumnIdentifierSettings,
    IAsyncLoadable<HomeBallsEntryColumnIdentifierSettings>
{
    public HomeBallsEntryColumnIdentifierSettings(
        String propertyName,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        this(propertyName, propertyName, localStorage, jsRuntime, eventRaiser, loggerFactory) { }

    public HomeBallsEntryColumnIdentifierSettings(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILoggerFactory? loggerFactory = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, loggerFactory)
    {
        IsUsingDefault = CreateValueProperty(true, nameof(IsUsingDefault));
        IsShowingNumber = CreateValueProperty(false, nameof(IsShowingNumber));
        IsShowingSprite = CreateValueProperty(false, nameof(IsShowingSprite));
        IsShowingName = CreateValueProperty(false, nameof(IsShowingName));
    }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsUsingDefault { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingNumber { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingSprite { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingName { get; }

    protected internal override IReadOnlyCollection<IAsyncLoadable> CreateLoadables() =>
        Array.AsReadOnly(new IAsyncLoadable[]
        {
            IsUsingDefault,
            IsShowingNumber,
            IsShowingSprite,
            IsShowingName,
        });

    new public virtual async ValueTask<HomeBallsEntryColumnIdentifierSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    async ValueTask<IHomeBallsEntryColumnIdentifierSettings> IAsyncLoadable<IHomeBallsEntryColumnIdentifierSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}
