namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiGenerationConverter
{
    EFCoreGeneration Convert(RawPokeApiGeneration generation);

    IReadOnlyList<EFCoreGeneration> Convert(
        IEnumerable<RawPokeApiGeneration> generations);

    IReadOnlyList<EFCoreGeneration> Convert(
        IEnumerable<RawPokeApiGeneration> generations,
        IEnumerable<RawPokeApiGenerationName> names);
}

public class RawPokeApiGenerationConverter :
    RawPokeApiRecordConverter<EFCoreGeneration, Byte, RawPokeApiGeneration, RawPokeApiGenerationName>,
    IRawPokeApiGenerationConverter
{
    public RawPokeApiGenerationConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiGenerationName, Byte> NameForeignKeySelector =>
        name => name.GenerationId;

    protected internal override Func<EFCoreGeneration, IReadOnlyList<EFCoreString>, EFCoreGeneration> RecordNamesSetter =>
        (generation, names) => generation with { Names = names };

    public override EFCoreGeneration Convert(RawPokeApiGeneration rawRecord) =>
        new EFCoreGeneration { Id = rawRecord.Id, Identifier = rawRecord.Identifier };
}
