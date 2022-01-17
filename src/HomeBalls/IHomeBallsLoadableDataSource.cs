namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsLoadableDataSource :
    IHomeBallsDataSource,
    IAsyncLoadable<IHomeBallsLoadableDataSource>
{
    new IHomeBallsLoadableDataSet<Byte, IHomeBallsGameVersion> GameVersions { get; }

    new IHomeBallsLoadableDataSet<Byte, IHomeBallsGeneration> Generations { get; }

    new IHomeBallsLoadableDataSet<UInt16, IHomeBallsItem> Items { get; }

    new IHomeBallsLoadableDataSet<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    new IHomeBallsLoadableDataSet<Byte, IHomeBallsLanguage> Languages { get; }

    new IHomeBallsLoadableDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality> Legalities { get; }

    new IHomeBallsLoadableDataSet<UInt16, IHomeBallsMove> Moves { get; }

    new IHomeBallsLoadableDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    new IHomeBallsLoadableDataSet<Byte, IHomeBallsNature> Natures { get; }

    new IHomeBallsLoadableDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    new IHomeBallsLoadableDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    new IHomeBallsLoadableDataSet<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> PokemonForms { get; }

    new IHomeBallsLoadableDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    new IHomeBallsLoadableDataSet<Byte, IHomeBallsStat> Stats { get; }

    new IHomeBallsLoadableDataSet<Byte, IHomeBallsType> Types { get; }
}