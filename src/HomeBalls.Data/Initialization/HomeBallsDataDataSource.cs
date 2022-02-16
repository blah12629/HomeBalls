using System.Runtime.CompilerServices;

namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IHomeBallsDataDataSource : IHomeBallsDataSource
{
    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsGameVersion> GameVersions { get; }

    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsGeneration> Generations { get; }

    new IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsItem> Items { get; }

    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsItemCategory> ItemCategories { get; }

    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsLanguage> Languages { get; }

    new IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsEntryKey, HomeBallsEntryLegality> Legalities { get; }

    new IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsMove> Moves { get; }

    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsNature> Natures { get; }

    new IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonAbility> PokemonAbilities { get; }

    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    new IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> PokemonForms { get; }

    new IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonSpecies> PokemonSpecies { get; }

    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsStat> Stats { get; }

    new IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsType> Types { get; }

    new IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> BreedablePokemonForms { get; }

    new IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonSpecies> BreedablePokemonSpecies { get; }

    new IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsItem> Pokeballs { get; }
}

public class HomeBallsDataDataSource : IHomeBallsDataDataSource
{
    IHomeBallsDataSet<Byte, HomeBallsGameVersion>? _gameVersions;
    IHomeBallsDataSet<Byte, HomeBallsGeneration>? _generations;
    IHomeBallsDataSet<UInt16, HomeBallsItem>? _items;
    IHomeBallsDataSet<Byte, HomeBallsItemCategory>? _itemCategories;
    IHomeBallsDataSet<Byte, HomeBallsLanguage>? _languages;
    IHomeBallsDataSet<HomeBalls.Entities.HomeBallsEntryKey, HomeBallsEntryLegality>? _legalities;
    IHomeBallsDataSet<UInt16, HomeBallsMove>? _moves;
    IHomeBallsDataSet<Byte, HomeBallsMoveDamageCategory>? _moveDamageCategories;
    IHomeBallsDataSet<Byte, HomeBallsNature>? _natures;
    IHomeBallsDataSet<UInt16, HomeBallsPokemonAbility>? _pokemonAbilities;
    IHomeBallsDataSet<Byte, HomeBallsPokemonEggGroup>? _pokemonEggGroups;
    IHomeBallsDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm>? _pokemonForms;
    IHomeBallsDataSet<UInt16, HomeBallsPokemonSpecies>? _pokemonSpecies;
    IHomeBallsDataSet<Byte, HomeBallsStat>? _stats;
    IHomeBallsDataSet<Byte, HomeBallsType>? _types;
    IHomeBallsDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm>? _breedablePokemonForms;
    IHomeBallsDataSet<UInt16, HomeBallsPokemonSpecies>? _breedablePokemonSpecies;
    IHomeBallsDataSet<UInt16, HomeBallsItem>? _pokeballs;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsGameVersion>? _gameVersionsReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsGeneration>? _generationsReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsItem>? _itemsReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsItemCategory>? _itemCategoriesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsLanguage>? _languagesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsEntryKey, HomeBallsEntryLegality>? _legalitiesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsMove>? _movesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsMoveDamageCategory>? _moveDamageCategoriesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsNature>? _naturesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonAbility>? _pokemonAbilitiesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsPokemonEggGroup>? _pokemonEggGroupsReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm>? _pokemonFormsReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonSpecies>? _pokemonSpeciesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsStat>? _statsReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsType>? _typesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm>? _breedablePokemonFormsReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonSpecies>? _breedablePokemonSpeciesReadOnly;
    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsItem>? _pokeballsReadOnly;

    public HomeBallsDataDataSource(ILogger? logger) => Logger = logger;

    public virtual IHomeBallsDataSet<Byte, HomeBallsGameVersion> GameVersions { get => _gameVersions ?? throw new NullReferenceException(); init => SetDataSet(ref _gameVersions, ref _gameVersionsReadOnly, value); }

    public virtual IHomeBallsDataSet<Byte, HomeBallsGeneration> Generations { get => _generations ?? throw new NullReferenceException(); init => SetDataSet(ref _generations, ref _generationsReadOnly, value); }

    public virtual IHomeBallsDataSet<UInt16, HomeBallsItem> Items { get => _items ?? throw new NullReferenceException(); init => SetDataSet(ref _items, ref _itemsReadOnly, value); }

    public virtual IHomeBallsDataSet<Byte, HomeBallsItemCategory> ItemCategories { get => _itemCategories ?? throw new NullReferenceException(); init => SetDataSet(ref _itemCategories, ref _itemCategoriesReadOnly, value); }

    public virtual IHomeBallsDataSet<Byte, HomeBallsLanguage> Languages { get => _languages ?? throw new NullReferenceException(); init => SetDataSet(ref _languages, ref _languagesReadOnly, value); }

    public virtual IHomeBallsDataSet<HomeBalls.Entities.HomeBallsEntryKey, HomeBallsEntryLegality> Legalities { get => _legalities ?? throw new NullReferenceException(); init => SetDataSet(ref _legalities, ref _legalitiesReadOnly, value); }

    public virtual IHomeBallsDataSet<UInt16, HomeBallsMove> Moves { get => _moves ?? throw new NullReferenceException(); init => SetDataSet(ref _moves, ref _movesReadOnly, value); }

    public virtual IHomeBallsDataSet<Byte, HomeBallsMoveDamageCategory> MoveDamageCategories { get => _moveDamageCategories ?? throw new NullReferenceException(); init => SetDataSet(ref _moveDamageCategories, ref _moveDamageCategoriesReadOnly, value); }

    public virtual IHomeBallsDataSet<Byte, HomeBallsNature> Natures { get => _natures ?? throw new NullReferenceException(); init => SetDataSet(ref _natures, ref _naturesReadOnly, value); }

    public virtual IHomeBallsDataSet<UInt16, HomeBallsPokemonAbility> PokemonAbilities { get => _pokemonAbilities ?? throw new NullReferenceException(); init => SetDataSet(ref _pokemonAbilities, ref _pokemonAbilitiesReadOnly, value); }

    public virtual IHomeBallsDataSet<Byte, HomeBallsPokemonEggGroup> PokemonEggGroups { get => _pokemonEggGroups ?? throw new NullReferenceException(); init => SetDataSet(ref _pokemonEggGroups, ref _pokemonEggGroupsReadOnly, value); }

    public virtual IHomeBallsDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> PokemonForms { get => _pokemonForms ?? throw new NullReferenceException(); init => SetDataSet(ref _pokemonForms, ref _pokemonFormsReadOnly, value); }

    public virtual IHomeBallsDataSet<UInt16, HomeBallsPokemonSpecies> PokemonSpecies { get => _pokemonSpecies ?? throw new NullReferenceException(); init => SetDataSet(ref _pokemonSpecies, ref _pokemonSpeciesReadOnly, value); }

    public virtual IHomeBallsDataSet<Byte, HomeBallsStat> Stats { get => _stats ?? throw new NullReferenceException(); init => SetDataSet(ref _stats, ref _statsReadOnly, value); }

    public virtual IHomeBallsDataSet<Byte, HomeBallsType> Types { get => _types ?? throw new NullReferenceException(); init => SetDataSet(ref _types, ref _typesReadOnly, value); }

    public virtual IHomeBallsDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> BreedablePokemonForms { get => _breedablePokemonForms ?? throw new NullReferenceException(); init => SetDataSet(ref _breedablePokemonForms, ref _breedablePokemonFormsReadOnly, value); }

    public virtual IHomeBallsDataSet<UInt16, HomeBallsPokemonSpecies> BreedablePokemonSpecies { get => _breedablePokemonSpecies ?? throw new NullReferenceException(); init => SetDataSet(ref _breedablePokemonSpecies, ref _breedablePokemonSpeciesReadOnly, value); }

    public virtual IHomeBallsDataSet<UInt16, HomeBallsItem> Pokeballs { get => _pokeballs ?? throw new NullReferenceException(); init => SetDataSet(ref _pokeballs, ref _pokeballsReadOnly, value); }

    protected internal ILogger? Logger { get; }

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsGameVersion> IHomeBallsDataDataSource.GameVersions => _gameVersionsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsGameVersion> IHomeBallsDataSource.GameVersions => _gameVersionsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsGeneration> IHomeBallsDataDataSource.Generations => _generationsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsGeneration> IHomeBallsDataSource.Generations => _generationsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsItem> IHomeBallsDataDataSource.Items => _itemsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBalls.Entities.IHomeBallsItem> IHomeBallsDataSource.Items => _itemsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsItemCategory> IHomeBallsDataDataSource.ItemCategories => _itemCategoriesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsItemCategory> IHomeBallsDataSource.ItemCategories => _itemCategoriesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsLanguage> IHomeBallsDataDataSource.Languages => _languagesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsLanguage> IHomeBallsDataSource.Languages => _languagesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsEntryKey, HomeBallsEntryLegality> IHomeBallsDataDataSource.Legalities => _legalitiesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsEntryKey, HomeBalls.Entities.IHomeBallsEntryLegality> IHomeBallsDataSource.Legalities => _legalitiesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsMove> IHomeBallsDataDataSource.Moves => _movesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBalls.Entities.IHomeBallsMove> IHomeBallsDataSource.Moves => _movesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsMoveDamageCategory> IHomeBallsDataDataSource.MoveDamageCategories => _moveDamageCategoriesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsMoveDamageCategory> IHomeBallsDataSource.MoveDamageCategories => _moveDamageCategoriesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsNature> IHomeBallsDataDataSource.Natures => _naturesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsNature> IHomeBallsDataSource.Natures => _naturesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonAbility> IHomeBallsDataDataSource.PokemonAbilities => _pokemonAbilitiesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBalls.Entities.IHomeBallsPokemonAbility> IHomeBallsDataSource.PokemonAbilities => _pokemonAbilitiesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsPokemonEggGroup> IHomeBallsDataDataSource.PokemonEggGroups => _pokemonEggGroupsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsPokemonEggGroup> IHomeBallsDataSource.PokemonEggGroups => _pokemonEggGroupsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> IHomeBallsDataDataSource.PokemonForms => _pokemonFormsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBalls.Entities.IHomeBallsPokemonForm> IHomeBallsDataSource.PokemonForms => _pokemonFormsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonSpecies> IHomeBallsDataDataSource.PokemonSpecies => _pokemonSpeciesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBalls.Entities.IHomeBallsPokemonSpecies> IHomeBallsDataSource.PokemonSpecies => _pokemonSpeciesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsStat> IHomeBallsDataDataSource.Stats => _statsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsStat> IHomeBallsDataSource.Stats => _statsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBallsType> IHomeBallsDataDataSource.Types => _typesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<Byte, HomeBalls.Entities.IHomeBallsType> IHomeBallsDataSource.Types => _typesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> IHomeBallsDataDataSource.BreedablePokemonForms => _breedablePokemonFormsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBalls.Entities.IHomeBallsPokemonForm> IHomeBallsDataSource.BreedablePokemonForms => _breedablePokemonFormsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsPokemonSpecies> IHomeBallsDataDataSource.BreedablePokemonSpecies => _breedablePokemonSpeciesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBalls.Entities.IHomeBallsPokemonSpecies> IHomeBallsDataSource.BreedablePokemonSpecies => _breedablePokemonSpeciesReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBallsItem> IHomeBallsDataDataSource.Pokeballs => _pokeballsReadOnly ?? throw new NullReferenceException();

    IHomeBallsDataSourceReadOnlyProperty<UInt16, HomeBalls.Entities.IHomeBallsItem> IHomeBallsDataSource.Pokeballs => _pokeballsReadOnly ?? throw new NullReferenceException();

    protected internal HomeBallsDataDataSource SetDataSet<TKey, TEntity>(
        ref IHomeBallsDataSet<TKey, TEntity>? dataSet,
        ref IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity>? dataSetReadOnly,
        IHomeBallsDataSet<TKey, TEntity> value,
        [CallerMemberName] String? propertyName = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity :
            class,
            HomeBalls.Entities.IKeyed<TKey>,
            HomeBalls.Entities.IIdentifiable,
            IHomeBallsDataType
    {
        dataSet = value;
        dataSetReadOnly = new HomeBallsDataSourceReadOnlyProperty<TKey, TEntity>(
            value,
            propertyName ?? throw new NullReferenceException());
        return this;
    }
}