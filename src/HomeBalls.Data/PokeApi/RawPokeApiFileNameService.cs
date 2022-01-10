namespace CEo.Pokemon.HomeBalls.Data;

public interface IRawPokeApiFileNameService
{
    String GetFileName(Type rawPokeApiType, String? extension = default);

    String GetFileName<TRecord>(String? extension = default)
        where TRecord : notnull, RawPokeApiRecord;
}

public class RawPokeApiFileNameService :
    IRawPokeApiFileNameService
{
    public RawPokeApiFileNameService(
        IPluralize pluralizer,
        ILogger? logger = default)
    {
        Pluralizer = pluralizer;
        Logger = logger;
    }

    protected internal IPluralize Pluralizer { get; }

    protected internal ILogger? Logger { get; }

    public virtual String GetFileName(
        Type rawPokeApiType,
        String? extension = default) =>
        rawPokeApiType.IsAssignableTo(typeof(RawPokeApiRecord)) ?
            GetFileNameUnvalidated(rawPokeApiType, extension) :
            throw new ArgumentException(default, nameof(rawPokeApiType));

    public virtual String GetFileName<TRecord>(
        String? extension = default)
        where TRecord : notnull, RawPokeApiRecord =>
        GetFileNameUnvalidated(typeof(TRecord), extension);

    protected internal virtual String GetFileNameUnvalidated(
        Type type,
        String? extension)
    {
        var name = GetFileNameUnvalidated(type);
        if (String.IsNullOrWhiteSpace(extension)) return name;

        extension = extension.StartsWith('.') ? extension[1 ..] : extension;
        return $"{name}.{extension}";
    }

    protected internal virtual String GetFileNameUnvalidated(
        Type type)
    {
        var (name, nameLowered) = (type.Name, type.Name.ToLower());
        foreach (var segment in new[] { "raw", "poke", "api" }) trimStart(segment);

        var segments = name.ToSnakeCase().Split('_');
        return String.Join('_', segments[.. ^1]
            .Append(Pluralizer.Pluralize(segments[^1])));

        void trimStart(String trim)
        {
            if (!nameLowered.StartsWith(trim)) return;
            var length = trim.Length;
            (name, nameLowered) = (name[length ..], nameLowered[length ..]);
        }
    }
}