namespace CEo.Pokemon.HomeBalls.App.Views;

public interface IHomeBallsStringService
{
    String GetInCurrentLanguage(INamed named);

    String GetInLanguage(INamed named, Byte languageId);
}

public class HomeBallsStringService : IHomeBallsStringService
{
    public HomeBallsStringService(
        IHomeBallsAppSettings settings,
        ILogger? logger = default)
    {
        Settings = settings;
        Logger = logger;
    }

    protected internal IHomeBallsAppSettings Settings { get; }

    protected internal ILogger? Logger { get; }

    public virtual String GetInLanguage(INamed named, Byte languageId) =>
        named.Names.Single(name => name.LanguageId == languageId).Value;

    public virtual String GetInCurrentLanguage(INamed named) =>
        GetInLanguage(named, Settings.LanguageId.Value);
}