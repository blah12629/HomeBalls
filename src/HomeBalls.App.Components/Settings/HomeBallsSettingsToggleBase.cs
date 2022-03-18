namespace CEo.Pokemon.HomeBalls.App.Components.Settings;

public abstract class HomeBallsSettingsToggleBase :
    HomeBallsLoggingComponent
{
    IHomeBallsAppSettings? _settings;
    IHomeBallsAppSettingsLoader? _settingsLoader;

    [Inject]
    protected internal IHomeBallsAppSettings Settings
    {
        get => _settings ?? throw new NullReferenceException();
        init => _settings = value;
    }

    [Inject]
    protected internal IHomeBallsAppSettingsLoader SettingsLoader
    {
        get => _settingsLoader ?? throw new NullReferenceException();
        init => _settingsLoader = value;
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SettingsLoader.SettingsLoaded += OnSettingsLoaded;
    }

    protected internal virtual void OnSettingsLoaded(
        Object? sender,
        TimedActionEndedEventArgs e) =>
        StateHasChanged();
}