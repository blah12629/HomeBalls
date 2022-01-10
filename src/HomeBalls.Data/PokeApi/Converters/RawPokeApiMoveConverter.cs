namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiMoveConverter
{
    EFCoreMove Convert(RawPokeApiMove move);

    IReadOnlyList<EFCoreMove> Convert(
        IEnumerable<RawPokeApiMove> moves);

    IReadOnlyList<EFCoreMove> Convert(
        IEnumerable<RawPokeApiMove> moves,
        IEnumerable<RawPokeApiMoveName> names);
}

public class RawPokeApiMoveConverter :
    RawPokeApiRecordConverter<EFCoreMove, UInt16, RawPokeApiMove, RawPokeApiMoveName>,
    IRawPokeApiMoveConverter
{
    public RawPokeApiMoveConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiMoveName, UInt16> NameForeignKeySelector =>
        name => name.MoveId;

    protected internal override Func<EFCoreMove, IReadOnlyList<EFCoreString>, EFCoreMove> RecordNamesSetter =>
        (move, names) => move with { Names = names };

    public override EFCoreMove Convert(
        RawPokeApiMove rawRecord) =>
        new EFCoreMove
        {
            DamageCategoryId = rawRecord.DamageClassId,
            Id = rawRecord.Id,
            Identifier = rawRecord.Identifier,
            TypeId = rawRecord.TypeId.HasValue ?
                ToTypeId(rawRecord.TypeId.Value) :
                default(Byte?)
        };
}