namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiStatConverter
{
    EFCoreStat Convert(RawPokeApiStat stat);

    IReadOnlyList<EFCoreStat> Convert(
        IEnumerable<RawPokeApiStat> stats);

    IReadOnlyList<EFCoreStat> Convert(
        IEnumerable<RawPokeApiStat> stats,
        IEnumerable<RawPokeApiStatName> statNames);
}

public class RawPokeApiStatConverter :
    RawPokeApiRecordConverter<EFCoreStat, Byte, RawPokeApiStat, RawPokeApiStatName>,
    IRawPokeApiStatConverter
{
    public RawPokeApiStatConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiStatName, Byte> NameForeignKeySelector =>
        name => name.StatId;

    protected internal override Func<EFCoreStat, IReadOnlyList<EFCoreString>, EFCoreStat> RecordNamesSetter =>
        (stat, names) => stat with { Names = names };

    public override EFCoreStat Convert(
        RawPokeApiStat rawRecord) =>
        new EFCoreStat
        {
            Id = rawRecord.GameIndex ?? rawRecord.Id,
            Identifier = rawRecord.Identifier
        };
}
