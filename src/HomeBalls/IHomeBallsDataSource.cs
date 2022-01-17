namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsDataSource
{
    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGameVersion> GameVersions { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGeneration> Generations { get; }

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsItem> Items { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsLanguage> Languages { get; }

    IHomeBallsReadOnlyDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> Legalities { get; }

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsMove> Moves { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsNature> Natures { get; }

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    IHomeBallsReadOnlyDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> PokemonForms { get; }

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsStat> Stats { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsType> Types { get; }
}