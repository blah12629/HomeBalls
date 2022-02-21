namespace CEo.Pokemon.HomeBalls.App.Categories.Settings;

public interface IHomeBallsEntryColumnIdentifierSettings :
    IHomeBallsAppBaseSettings,
    IAsyncLoadable<IHomeBallsEntryColumnIdentifierSettings>
{
    IHomeBallsAppSettingsValueProperty<Boolean> IsUsingDefaultSettings { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingNumber { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingSprite { get; }

    IHomeBallsAppSettingsValueProperty<Boolean> IsShowingName { get; }
}

public class HomeBallsEntryColumnIdentifierSettings :
    HomeBallsAppBaseSettings,
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
        IsUsingDefaultSettings = CreateValueProperty(false, nameof(IsUsingDefaultSettings));
        IsShowingNumber = CreateValueProperty(false, nameof(IsShowingNumber));
        IsShowingSprite = CreateValueProperty(false, nameof(IsShowingSprite));
        IsShowingName = CreateValueProperty(false, nameof(IsShowingName));
    }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsUsingDefaultSettings { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingNumber { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingSprite { get; }

    public IHomeBallsAppSettingsValueProperty<Boolean> IsShowingName { get; }

    protected internal override IReadOnlyCollection<IAsyncLoadable> CreateLoadables() =>
        Array.AsReadOnly(new IAsyncLoadable[]
        {
            IsUsingDefaultSettings,
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
