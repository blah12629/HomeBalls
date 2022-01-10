namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiNatureConverter
{
    EFCoreNature Convert(RawPokeApiNature nature);

    IReadOnlyList<EFCoreNature> Convert(
        IEnumerable<RawPokeApiNature> natures);

    IReadOnlyList<EFCoreNature> Convert(
        IEnumerable<RawPokeApiNature> natures,
        IEnumerable<RawPokeApiNatureName> names);
}

public class RawPokeApiNatureConverter :
    RawPokeApiRecordConverter<EFCoreNature, Byte, RawPokeApiNature, RawPokeApiNatureName>,
    IRawPokeApiNatureConverter
{
    public RawPokeApiNatureConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiNatureName, Byte> NameForeignKeySelector =>
        name => name.NatureId;

    protected internal override Func<EFCoreNature, IReadOnlyList<EFCoreString>, EFCoreNature> RecordNamesSetter =>
        (nature, names) => nature with { Names = names };

    public override EFCoreNature Convert(
        RawPokeApiNature rawRecord) =>
        new EFCoreNature
        {
            DecreasedStatId = rawRecord.DecreasedStatId,
            Id = Convert<Byte>(rawRecord.GameIndex + 1),
            Identifier = rawRecord.Identifier,
            IncreasedStatId = rawRecord.IncreasedStatId
        };
}