namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsDataSource
{
    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsGameVersion> GameVersions { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsGeneration> Generations { get; }

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsItem> Items { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsLanguage> Languages { get; }

    IHomeBallsDataSourceReadOnlyProperty<HomeBallsEntryKey, IHomeBallsEntryLegality> Legalities { get; }

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsMove> Moves { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsNature> Natures { get; }

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    IHomeBallsDataSourceReadOnlyProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> PokemonForms { get; }

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsStat> Stats { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsType> Types { get; }

    IHomeBallsDataSourceReadOnlyProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> BreedablePokemonForms { get; }

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsPokemonSpecies> BreedablePokemonSpecies { get; }

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsItem> Pokeballs { get; }
}