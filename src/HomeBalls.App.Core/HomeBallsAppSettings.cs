namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppSettings :
    IHomeBallsAppTab,
    IAsyncLoadable<IHomeBallsAppSettings>
{
    IHomeBallsAppSettingsValueProperty<Byte> LanguageId { get; }

    IHomeBallsAppThemeSettings Theme { get; }

    IHomeBallsAppEntriesSettings Entries { get; }
}

public class HomeBallsAppSettings :
    HomeBallsAppSettingsPropertyRoot,
    IHomeBallsAppSettings,
    IAsyncLoadable<HomeBallsAppSettings>
{
    public HomeBallsAppSettings(
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser? eventRaiser = default,
        ILogger? logger = default) :
        this(SettingsTabEnglishName, localStorage, jsRuntime, eventRaiser ?? new EventRaiser(logger), logger) { }

    public HomeBallsAppSettings(
        String propertyName,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser? eventRaiser = default,
        ILogger? logger = default) :
        base(propertyName, propertyName, localStorage, jsRuntime, eventRaiser ?? new EventRaiser(logger), logger)
    {
        EventRaiser.RaisedBy(this);

        LanguageId = CreateValueProperty<Byte>(EnglishLanguageId, nameof(LanguageId));
        Theme = new HomeBallsAppThemeSettings(
            nameof(Theme), CreateSubpropertyIdentifier(nameof(Theme)),
            LocalStorage, JSRuntime, EventRaiser, Logger);
        Entries = new HomeBallsAppEntriesSettings(
            nameof(Entries), CreateSubpropertyIdentifier(nameof(Entries)),
            LocalStorage, JSRuntime, EventRaiser, Logger);

        TabMetadata = new HomeBallsAppTabMetadata(CreateNames(), EventRaiser, Logger)
        {
            IsDisabled = false
        };
    }

    public IHomeBallsAppSettingsValueProperty<Byte> LanguageId { get; }

    public IHomeBallsAppThemeSettings Theme { get; }

    public IHomeBallsAppEntriesSettings Entries { get; }

    public IMutableNotifyingProperty<Boolean> IsSelected => TabMetadata.IsSelected;

    public Boolean IsDisabled => TabMetadata.IsDisabled;

    public IEnumerable<IHomeBallsString> Names => TabMetadata.Names;

    protected internal IHomeBallsAppTabMetadata TabMetadata { get; }

    new public virtual async ValueTask<HomeBallsAppSettings> EnsureLoadedAsync(CancellationToken cancellationToken)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    protected internal virtual IReadOnlyCollection<IHomeBallsString> CreateNames() =>
        new List<IHomeBallsString>
        {
            new HomeBallsString
            {
                LanguageId = EnglishLanguageId,
                Value = SettingsTabEnglishName
            }
        }.AsReadOnly();

    protected internal override IReadOnlyCollection<IHomeBallsAppSettingsProperty> CreateSubpropertyCollection() =>
        new IHomeBallsAppSettingsProperty[] { LanguageId, Theme, Entries }.AsReadOnly();

    async ValueTask<IHomeBallsAppSettings> IAsyncLoadable<IHomeBallsAppSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}