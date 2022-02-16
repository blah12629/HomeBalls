namespace CEo.Pokemon.HomeBalls.App.DataAccess;

public partial class HomeBallsLocalStorageDataSource :
    IHomeBallsLocalStorageDataSource,
    IAsyncLoadable<HomeBallsLocalStorageDataSource>
{
    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsGameVersion> IHomeBallsLocalStorageDataSource.GameVersions => GameVersions;

    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsGeneration> IHomeBallsLocalStorageDataSource.Generations => Generations;

    IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsItem> IHomeBallsLocalStorageDataSource.Items => Items;

    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsItemCategory> IHomeBallsLocalStorageDataSource.ItemCategories => ItemCategories;

    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsLanguage> IHomeBallsLocalStorageDataSource.Languages => Languages;

    IHomeBallsLocalStorageDataSourceProperty<HomeBallsEntryKey, IHomeBallsEntryLegality> IHomeBallsLocalStorageDataSource.Legalities => Legalities;

    IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsMove> IHomeBallsLocalStorageDataSource.Moves => Moves;

    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsMoveDamageCategory> IHomeBallsLocalStorageDataSource.MoveDamageCategories => MoveDamageCategories;

    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsNature> IHomeBallsLocalStorageDataSource.Natures => Natures;

    IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonAbility> IHomeBallsLocalStorageDataSource.PokemonAbilities => PokemonAbilities;

    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsPokemonEggGroup> IHomeBallsLocalStorageDataSource.PokemonEggGroups => PokemonEggGroups;

    IHomeBallsLocalStorageDataSourceProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsLocalStorageDataSource.PokemonForms => PokemonForms;

    IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonSpecies> IHomeBallsLocalStorageDataSource.PokemonSpecies => PokemonSpecies;

    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsStat> IHomeBallsLocalStorageDataSource.Stats => Stats;

    IHomeBallsLocalStorageDataSourceProperty<Byte, IHomeBallsType> IHomeBallsLocalStorageDataSource.Types => Types;

    IHomeBallsLocalStorageDataSourceProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsLocalStorageDataSource.BreedablePokemonForms => BreedablePokemonForms;

    IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsPokemonSpecies> IHomeBallsLocalStorageDataSource.BreedablePokemonSpecies => BreedablePokemonSpecies;

    IHomeBallsLocalStorageDataSourceProperty<UInt16, IHomeBallsItem> IHomeBallsLocalStorageDataSource.Pokeballs => Pokeballs;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsGameVersion> IHomeBallsLoadableDataSource.GameVersions => GameVersions;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsGeneration> IHomeBallsLoadableDataSource.Generations => Generations;

    IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsItem> IHomeBallsLoadableDataSource.Items => Items;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsItemCategory> IHomeBallsLoadableDataSource.ItemCategories => ItemCategories;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsLanguage> IHomeBallsLoadableDataSource.Languages => Languages;

    IHomeBallsDataSourceLoadableProperty<HomeBallsEntryKey, IHomeBallsEntryLegality> IHomeBallsLoadableDataSource.Legalities => Legalities;

    IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsMove> IHomeBallsLoadableDataSource.Moves => Moves;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsMoveDamageCategory> IHomeBallsLoadableDataSource.MoveDamageCategories => MoveDamageCategories;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsNature> IHomeBallsLoadableDataSource.Natures => Natures;

    IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsPokemonAbility> IHomeBallsLoadableDataSource.PokemonAbilities => PokemonAbilities;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsPokemonEggGroup> IHomeBallsLoadableDataSource.PokemonEggGroups => PokemonEggGroups;

    IHomeBallsDataSourceLoadableProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsLoadableDataSource.PokemonForms => PokemonForms;

    IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsPokemonSpecies> IHomeBallsLoadableDataSource.PokemonSpecies => PokemonSpecies;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsStat> IHomeBallsLoadableDataSource.Stats => Stats;

    IHomeBallsDataSourceLoadableProperty<Byte, IHomeBallsType> IHomeBallsLoadableDataSource.Types => Types;

    IHomeBallsDataSourceLoadableProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsLoadableDataSource.BreedablePokemonForms => BreedablePokemonForms;

    IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsPokemonSpecies> IHomeBallsLoadableDataSource.BreedablePokemonSpecies => BreedablePokemonSpecies;

    IHomeBallsDataSourceLoadableProperty<UInt16, IHomeBallsItem> IHomeBallsLoadableDataSource.Pokeballs => Pokeballs;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsGameVersion> IHomeBallsDataSource.GameVersions => GameVersions;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsGeneration> IHomeBallsDataSource.Generations => Generations;

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsItem> IHomeBallsDataSource.Items => Items;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsItemCategory> IHomeBallsDataSource.ItemCategories => ItemCategories;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsLanguage> IHomeBallsDataSource.Languages => Languages;

    IHomeBallsDataSourceReadOnlyProperty<HomeBallsEntryKey, IHomeBallsEntryLegality> IHomeBallsDataSource.Legalities => Legalities;

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsMove> IHomeBallsDataSource.Moves => Moves;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsMoveDamageCategory> IHomeBallsDataSource.MoveDamageCategories => MoveDamageCategories;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsNature> IHomeBallsDataSource.Natures => Natures;

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsPokemonAbility> IHomeBallsDataSource.PokemonAbilities => PokemonAbilities;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsPokemonEggGroup> IHomeBallsDataSource.PokemonEggGroups => PokemonEggGroups;

    IHomeBallsDataSourceReadOnlyProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsDataSource.PokemonForms => PokemonForms;

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsPokemonSpecies> IHomeBallsDataSource.PokemonSpecies => PokemonSpecies;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsStat> IHomeBallsDataSource.Stats => Stats;

    IHomeBallsDataSourceReadOnlyProperty<Byte, IHomeBallsType> IHomeBallsDataSource.Types => Types;

    IHomeBallsDataSourceReadOnlyProperty<HomeBallsPokemonFormKey, IHomeBallsPokemonForm> IHomeBallsDataSource.BreedablePokemonForms => BreedablePokemonForms;

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsPokemonSpecies> IHomeBallsDataSource.BreedablePokemonSpecies => BreedablePokemonSpecies;

    IHomeBallsDataSourceReadOnlyProperty<UInt16, IHomeBallsItem> IHomeBallsDataSource.Pokeballs => Pokeballs;
}