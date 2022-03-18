namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppSettings :
    IHomeBallsAppTab,
    IAsyncLoadable<IHomeBallsAppSettings>
{
    IHomeBallsAppSettingsValueProperty<Byte> LanguageId { get; }

    IHomeBallsAppThemeSettings Theme { get; }

    IHomeBallsEntryTableSettings EntryTable { get; }
}

public class HomeBallsAppSettings :
    HomeBallsAppSettingsBase,
    IHomeBallsAppSettings,
    IAsyncLoadable<HomeBallsAppSettings>
{
    public HomeBallsAppSettings(
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        ILoggerFactory? loggerFactory = default) :
        base(localStorage, jsRuntime, default, loggerFactory)
    {
        LanguageId = CreateValueProperty(EnglishLanguageId, nameof(LanguageId));
        Theme = new HomeBallsAppThemeSettings(nameof(Theme), LocalStorage, JSRuntime, EventRaiser, LoggerFactory);
        EntryTable = new HomeBallsEntryTableSettings(nameof(EntryTable), LocalStorage, JSRuntime, EventRaiser, LoggerFactory);

        TabMetadata = new HomeBallsAppTabMetadata(CreateNames(), EventRaiser, Logger);
    }

    public IHomeBallsAppSettingsValueProperty<Byte> LanguageId { get; }

    public IHomeBallsAppThemeSettings Theme { get; }

    public IHomeBallsEntryTableSettings EntryTable { get; }

    public IMutableNotifyingProperty<Boolean> IsSelected => TabMetadata.IsSelected;

    public IEnumerable<IHomeBallsString> Names => TabMetadata.Names;

    protected internal IHomeBallsAppTabMetadata TabMetadata { get; }

    new public virtual async ValueTask<HomeBallsAppSettings> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
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

    async ValueTask<IHomeBallsAppSettings> IAsyncLoadable<IHomeBallsAppSettings>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}