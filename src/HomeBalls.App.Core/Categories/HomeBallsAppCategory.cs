namespace CEo.Pokemon.HomeBalls.App.Core.Categories;

public interface IHomeBallsAppCateogry :
    IIdentifiable,
    INamed
{
    IReadOnlyList<String> IconSvgPaths { get; }
}

public abstract class HomeBallsAppCategory :
    IHomeBallsAppCateogry
{
    protected HomeBallsAppCategory(
        IEventRaiser? eventRaiser = default,
        ILogger? logger = default)
    {
        var name = GenerateEnglishName();
        (Identifier, Names) = (name.Value.ToLower(), Array.AsReadOnly(new[] { name }));

        var iconPaths = new List<String> { };
        IconSvgPaths = iconPaths.AsReadOnly();
        GenerateIconSvgPaths(iconPaths);

        Logger = logger;
        EventRaiser = eventRaiser ?? new EventRaiser(Logger).RaisedBy(this);
    }

    public virtual String Identifier { get; }

    public IReadOnlyList<String> IconSvgPaths { get; }

    protected internal IReadOnlyList<ProtobufString> Names { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    IEnumerable<IHomeBallsString> INamed.Names => Names;

    protected internal abstract void GenerateIconSvgPaths(List<String> paths);

    protected internal virtual ProtobufString GenerateEnglishName()
    {
        var name = GetType().Name;
        var nameLower = name.ToLower();
        foreach (var @string in new[] { "homeballs", "app" }) trimStart(@string);
        return new ProtobufString { LanguageId = 9, Value = name };

        void trimStart(String @string)
        {
            if (!nameLower.StartsWith(@string)) return;

            var length = @string.Length;
            (name, nameLower) = (name[length ..], nameLower[length ..]);
        }
    }
}