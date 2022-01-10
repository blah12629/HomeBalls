namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiAbilityConverter
{
    EFCorePokemonAbility Convert(RawPokeApiAbility ability);

    IReadOnlyList<EFCorePokemonAbility> Convert(
        IEnumerable<RawPokeApiAbility> abilities);

    IReadOnlyList<EFCorePokemonAbility> Convert(
        IEnumerable<RawPokeApiAbility> abilities,
        IEnumerable<RawPokeApiAbilityName> names);
}

public class RawPokeApiAbilityConverter :
    RawPokeApiRecordConverter<EFCorePokemonAbility, UInt16, RawPokeApiAbility, RawPokeApiAbilityName>,
    IRawPokeApiAbilityConverter
{
    public RawPokeApiAbilityConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiAbilityName, UInt16> NameForeignKeySelector =>
        name => name.AbilityId;

    protected internal override Func<EFCorePokemonAbility, IReadOnlyList<EFCoreString>, EFCorePokemonAbility> RecordNamesSetter =>
        (ability, names) => ability with { Names = names };

    public override EFCorePokemonAbility Convert(
        RawPokeApiAbility rawRecord) =>
        new EFCorePokemonAbility
        {
            Id = rawRecord.Id,
            Identifier = rawRecord.Identifier
        };
}