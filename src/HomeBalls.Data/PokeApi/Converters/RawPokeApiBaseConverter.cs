namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public abstract class RawPokeApiBaseConverter
{
    protected RawPokeApiBaseConverter(
        ILogger? logger = default) =>
        Logger = logger;

    protected internal ILogger? Logger { get; }

    protected internal virtual Byte ToTypeId(UInt16 id) => ToByteId(id);

    protected internal virtual Byte ToFormId(UInt16 id) => ToByteId(id);

    protected internal virtual Byte ToByteId(UInt16 id) =>
        (Byte)(id > Byte.MaxValue ? Byte.MaxValue - id % 10_000 : id);

    protected internal virtual IReadOnlyList<TResult> ConvertList<TSource, TResult>(
        IEnumerable<TSource> sources,
        Func<TSource, TResult> conversion) =>
        sources.Select(conversion).ToList().AsReadOnly<TResult>();

    protected internal virtual HomeBallsString ConvertName<TSource, TForeignKey>(
        TSource name)
        where TSource : notnull, RawPokeApiName<TForeignKey>
        where TForeignKey : notnull, IEquatable<TForeignKey> =>
        new HomeBallsString
        {
            LanguageId = name.LocalLanguageId,
            Value = name.Name
        };
}