namespace CEo.Pokemon.HomeBalls.App.Components.Settings.Theme;

public abstract class HomeBallsThemeSettingsComponentBase :
    HomeBallsAppSettingsItemComponentBase<IHomeBallsAppThemeSettings>
{
    IHomeBallsBreedablesSpriteService? _sprites;

    [Inject]
    protected internal IHomeBallsBreedablesSpriteService Sprites
    {
        get => _sprites ?? throw new NullReferenceException();
        init => _sprites = value;
    }
}