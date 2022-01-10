namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiMoveDamageClassConverter
{
    EFCoreMoveDamageCategory Convert(RawPokeApiMoveDamageClass @class);

    IReadOnlyList<EFCoreMoveDamageCategory> Convert(
        IEnumerable<RawPokeApiMoveDamageClass> classes);

    IReadOnlyList<EFCoreMoveDamageCategory> Convert(
        IEnumerable<RawPokeApiMoveDamageClass> classes,
        IEnumerable<RawPokeApiMoveDamageClassProse> names);
}

public class RawPokeApiMoveDamageClassConverter :
    RawPokeApiRecordConverter<EFCoreMoveDamageCategory, Byte, RawPokeApiMoveDamageClass, RawPokeApiMoveDamageClassProse>,
    IRawPokeApiMoveDamageClassConverter
{
    public RawPokeApiMoveDamageClassConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiMoveDamageClassProse, Byte> NameForeignKeySelector =>
        name => name.MoveDamageClassId;

    protected internal override Func<EFCoreMoveDamageCategory, IReadOnlyList<EFCoreString>, EFCoreMoveDamageCategory> RecordNamesSetter =>
        (category, names) => category with { Names = names };

    public override EFCoreMoveDamageCategory Convert(
        RawPokeApiMoveDamageClass rawRecord) =>
        new EFCoreMoveDamageCategory
        {
            Id = rawRecord.Id,
            Identifier = rawRecord.Identifier,
        };
}