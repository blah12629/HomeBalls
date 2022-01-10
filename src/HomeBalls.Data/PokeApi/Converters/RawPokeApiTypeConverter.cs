namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiTypeConverter
{
    EFCoreType Convert(RawPokeApiType type);

    IReadOnlyList<EFCoreType> Convert(
        IEnumerable<RawPokeApiType> types);

    IReadOnlyList<EFCoreType> Convert(
        IEnumerable<RawPokeApiType> types,
        IEnumerable<RawPokeApiTypeName> names);
}

public class RawPokeApiTypeConverter :
    RawPokeApiRecordConverter<EFCoreType, Byte, RawPokeApiType, RawPokeApiTypeName>,
    IRawPokeApiTypeConverter
{
    public RawPokeApiTypeConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiTypeName, Byte> NameForeignKeySelector =>
        name => ToTypeId(name.TypeId);

    protected internal override Func<EFCoreType, IReadOnlyList<EFCoreString>, EFCoreType> RecordNamesSetter =>
        (type, names) => type with { Names = names };

    public override EFCoreType Convert(
        RawPokeApiType rawRecord) =>
        new EFCoreType
        {
            Id = ToTypeId(rawRecord.Id),
            Identifier = rawRecord.Identifier
        };
}
