namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsLoadableDataSource :
    IHomeBallsDataSource,
    IAsyncLoadable<IHomeBallsLoadableDataSource>,
    INotifyDataLoading
{
    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsGameVersion> GameVersions { get; }

    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsGeneration> Generations { get; }

    new IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsItem> Items { get; }

    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsLanguage> Languages { get; }

    new IHomeBallsDataSourceLoadableProperty<HomeBallsEntryKey, IHomeBallsEntryLegality> Legalities { get; }

    new IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsMove> Moves { get; }

    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsNature> Natures { get; }

    new IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    new IHomeBallsDataSourceLoadableProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> PokemonForms { get; }

    new IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsStat> Stats { get; }

    new IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsType> Types { get; }

    new IHomeBallsDataSourceLoadableProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> BreedablePokemonForms { get; }

    new IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsPokemonSpecies> BreedablePokemonSpecies { get; }

    new IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsItem> Pokeballs { get; }
}