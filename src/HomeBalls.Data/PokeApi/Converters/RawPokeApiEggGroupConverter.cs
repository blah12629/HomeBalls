namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiEggGroupConverter
{
    EFCorePokemonEggGroup Convert(RawPokeApiEggGroup eggGroup);

    IReadOnlyList<EFCorePokemonEggGroup> Convert(
        IEnumerable<RawPokeApiEggGroup> eggGroups);

    IReadOnlyList<EFCorePokemonEggGroup> Convert(
        IEnumerable<RawPokeApiEggGroup> eggGroups,
        IEnumerable<RawPokeApiEggGroupProse> names);
}

public class RawPokeApiEggGroupConverter :
    RawPokeApiRecordConverter<EFCorePokemonEggGroup, Byte, RawPokeApiEggGroup, RawPokeApiEggGroupProse>,
    IRawPokeApiEggGroupConverter
{
    public RawPokeApiEggGroupConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiEggGroupProse, Byte> NameForeignKeySelector =>
        name => name.EggGroupId;

    protected internal override Func<EFCorePokemonEggGroup, IReadOnlyList<EFCoreString>, EFCorePokemonEggGroup> RecordNamesSetter =>
        (group, names) => group with { Names = names };

    public override EFCorePokemonEggGroup Convert(
        RawPokeApiEggGroup rawRecord) =>
        new EFCorePokemonEggGroup
        {
            Id = rawRecord.Id,
            Identifier = rawRecord.Identifier
        };
}
