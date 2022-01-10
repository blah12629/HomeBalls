namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiVersionConverter
{
    EFCoreGameVersion Convert(
        RawPokeApiVersion version,
        RawPokeApiVersionGroup group);

    IReadOnlyList<EFCoreGameVersion> Convert(
        IEnumerable<RawPokeApiVersion> versions,
        IEnumerable<RawPokeApiVersionGroup> groups);

    IReadOnlyList<EFCoreGameVersion> Convert(
        IEnumerable<RawPokeApiVersion> versions,
        IEnumerable<RawPokeApiVersionGroup> groups,
        IEnumerable<RawPokeApiVersionName> names);
}

public class RawPokeApiVersionConverter :
    RawPokeApiBaseNamedConverter,
    IRawPokeApiVersionConverter
{
    public RawPokeApiVersionConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    public virtual EFCoreGameVersion Convert(
        RawPokeApiVersion version,
        RawPokeApiVersionGroup group) =>
        new EFCoreGameVersion
        {
            Id = version.Id,
            GenerationId = group.GenerationId,
            Identifier = version.Identifier,
        };

    public virtual IReadOnlyList<EFCoreGameVersion> Convert(
        IEnumerable<RawPokeApiVersion> versions,
        IEnumerable<RawPokeApiVersionGroup> groups)
    {
        var groupLookup = groups.ToDictionary(group => group.Id, group => group);
        return ConvertCollection(versions, version =>
            Convert(version, groupLookup[version.VersionGroupId]));
    }

    public virtual IReadOnlyList<EFCoreGameVersion> Convert(
        IEnumerable<RawPokeApiVersion> versions,
        IEnumerable<RawPokeApiVersionGroup> groups,
        IEnumerable<RawPokeApiVersionName> names) =>
        AddNames(
            Convert(versions, groups),
            names,
            name => name.VersionId,
            (game, names) => game with { Names = names });
}