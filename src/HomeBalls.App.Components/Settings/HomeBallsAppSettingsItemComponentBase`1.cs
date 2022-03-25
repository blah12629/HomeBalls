namespace CEo.Pokemon.HomeBalls.App.Components.Settings.Entries;

public abstract class HomeBallsAppSettingsItemComponentBase<TSettings> :
    HomeBallsLoggingComponent
    where TSettings : notnull, IHomeBallsAppSettingsProperty
{
    TSettings? _settings;

    [Inject]
    protected internal TSettings Settings
    {
        get => _settings ?? throw new NullReferenceException();
        init => _settings = value;
    }
}