namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public class PokeApiIdentifierService :
    IHomeBallsIdentifierService
{
    public PokeApiIdentifierService(
        IPluralize pluralizer,
        ILogger? logger = default) =>
        (Pluralizer, Logger) = (pluralizer, logger);

    protected internal IPluralize Pluralizer { get; }

    protected internal ILogger? Logger { get; }

    public virtual String GenerateIdentifier<TEntity>()
        where TEntity : notnull =>
        GenerateIdentifier(typeof(TEntity));

    public virtual String GenerateIdentifier(Type entityType)
    {
        var segments = RemovePrefixes(entityType.Name, "raw", "poke", "api")
            .ToSnakeCase()
            .Split('_');

        return String.Join('_', segments.SkipLast(1)
            .Append(Pluralizer.Pluralize(segments[^ 1])));
    }

    protected internal virtual String RemovePrefixes(
        String @string,
        params String[] prefixes) =>
        RemovePrefixes(@string, false, prefixes);

    protected virtual String RemovePrefixes(
        String @string,
        Boolean returnLower,
        params String[] prefixes)
    {
        var stringLower = @string.ToLower();
        for (var i = 0; i < prefixes.Length; i ++) trimStart(prefixes[i]);
        return returnLower ? stringLower : @string;

        void trimStart(String prefix)
        {
            if (!stringLower.StartsWith(prefix)) return;
            var length = prefix.Length;
            @string = @string[length ..];
            stringLower = stringLower[length ..];
        }
    }
}