namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiPokemonSpeciesConverter
{
    EFCorePokemonSpecies Convert(RawPokeApiPokemonSpecies species);

    IReadOnlyList<EFCorePokemonSpecies> Convert(
        IEnumerable<RawPokeApiPokemonSpecies> species);

    IReadOnlyList<EFCorePokemonSpecies> Convert(
        IEnumerable<RawPokeApiPokemonSpecies> species,
        IEnumerable<RawPokeApiPokemonSpeciesName> names);
}

public class RawPokeApiPokemonSpeciesConverter :
    RawPokeApiRecordConverter<EFCorePokemonSpecies, UInt16, RawPokeApiPokemonSpecies, RawPokeApiPokemonSpeciesName>,
    IRawPokeApiPokemonSpeciesConverter
{
    public RawPokeApiPokemonSpeciesConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiPokemonSpeciesName, UInt16> NameForeignKeySelector =>
        name => name.PokemonSpeciesId;

    protected internal override Func<EFCorePokemonSpecies, IReadOnlyList<EFCoreString>, EFCorePokemonSpecies> RecordNamesSetter =>
        (species, names) => species with { Names = names };

    public override EFCorePokemonSpecies Convert(
        RawPokeApiPokemonSpecies species) =>
        new EFCorePokemonSpecies
        {
            GenderRate = species.GenderRate,
            Id = species.Id,
            Identifier = species.Identifier,
            IsFormSwitchable = species.FormsSwitchable,
        };

    // public virtual IReadOnlyList<EFCorePokemonSpecies> Convert(
    //     IEnumerable<RawPokeApiPokemonSpecies> species) =>
    //     ConvertCollection(species, Convert);

    // public virtual IReadOnlyList<EFCorePokemonSpecies> Convert(
    //     IEnumerable<RawPokeApiPokemonSpecies> species,
    //     IEnumerable<RawPokeApiPokemonSpeciesName> names) =>
    //     AddNames(
    //         Convert(species),
    //         names,
    //         name => name.PokemonSpeciesId,
    //         (species, names) => species with { Names = names });
}