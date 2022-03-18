namespace CEo.Pokemon.HomeBalls.App;

internal static class _Values
{
    public const Byte EnglishLanguageId = 9;

    public const String DreamThemeId = "dream";

    public const String MoonThemeId = "moon";

    public const String LureThemeId = "lure";

    public static IReadOnlyList<String> ThemeIds { get; } =
        new List<String> { DreamThemeId, MoonThemeId, LureThemeId }.AsReadOnly();

    public const String UpdateThemeFunctionId = "updateTheme";

    public const String LocalStorageSettingsKey = "settings";

    public const String DataClientKey = "dataClient";

    public const String DefaultProtobufExtension = ".bin";

    public const String HomeSpriteClientKey = "homeSpriteClient";

    public const String HomeSpriteBaseAddress =
        "https://projectpokemon.org/images/sprites-models/homeimg/";

    public const String AboutTabEnglishName = "About";

    public const String TradeTabEnglishName = "Trade";

    public const String EditTabEnglishName = "Edit";

    public const String SettingsTabEnglishName = "Settings";
}