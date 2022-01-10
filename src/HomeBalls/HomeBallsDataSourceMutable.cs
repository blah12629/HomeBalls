namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsDataSourceMutable : IHomeBallsDataSource
{
    new IHomeBallsDataSet<Byte, IHomeBallsGameVersion> GameVersions { get; }

    new IHomeBallsDataSet<Byte, IHomeBallsGeneration> Generations { get; }

    new IHomeBallsDataSet<UInt16, IHomeBallsItem> Items { get; }

    new IHomeBallsDataSet<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    new IHomeBallsDataSet<Byte, IHomeBallsLanguage> Languages { get; }

    new IHomeBallsDataSet<UInt16, IHomeBallsMove> Moves { get; }

    new IHomeBallsDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    new IHomeBallsDataSet<Byte, IHomeBallsNature> Natures { get; }

    new IHomeBallsDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    new IHomeBallsDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    new IHomeBallsDataSet<(UInt16 SpeciesId, Byte FormId), IHomeBallsPokemonForm> PokemonForms { get; }

    new IHomeBallsDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    new IHomeBallsDataSet<Byte, IHomeBallsStat> Stats { get; }

    new IHomeBallsDataSet<Byte, IHomeBallsType> Types { get; }

    IHomeBallsDataSourceMutable Clear();
}

public class HomeBallsDataSourceMutable : IHomeBallsDataSourceMutable
{
    public HomeBallsDataSourceMutable()
    {
        var entities = new List<IHomeBallsReadOnlyCollection<IHomeBallsEntity>> { };
        (GameVersions, GameVersionsReadOnly) = createList<Byte, IHomeBallsGameVersion>();
        (Generations, GenerationsReadOnly) = createList<Byte, IHomeBallsGeneration>();
        (Items, ItemsReadOnly) = createList<UInt16, IHomeBallsItem>();
        (ItemCategories, ItemCategoriesReadOnly) = createList<Byte, IHomeBallsItemCategory>();
        (Languages, LanguagesReadOnly) = createList<Byte, IHomeBallsLanguage>();
        (Moves, MovesReadOnly) = createList<UInt16, IHomeBallsMove>();
        (MoveDamageCategories, MoveDamageCategoriesReadOnly) = createList<Byte, IHomeBallsMoveDamageCategory>();
        (Natures, NaturesReadOnly) = createList<Byte, IHomeBallsNature>();
        (PokemonAbilities, PokemonAbilitiesReadOnly) = createList<UInt16, IHomeBallsPokemonAbility>();
        (PokemonEggGroups, PokemonEggGroupsReadOnly) = createList<Byte, IHomeBallsPokemonEggGroup>();
        (PokemonForms, PokemonFormsReadOnly) = createList<(UInt16, Byte), IHomeBallsPokemonForm>(form => (form.SpeciesId, form.FormId));
        (PokemonSpecies, PokemonSpeciesReadOnly) = createList<UInt16, IHomeBallsPokemonSpecies>();
        (Stats, StatsReadOnly) = createList<Byte, IHomeBallsStat>();
        (Types, TypesReadOnly) = createList<Byte, IHomeBallsType>();
        Entities = entities.AsReadOnly();

        (IHomeBallsDataSet<TKey, TRecord>, IHomeBallsReadOnlyDataSet<TKey, TRecord>) createList<TKey, TRecord>(
            Func<TRecord, TKey>? keySelector = default)
            where TKey : notnull
            where TRecord : notnull, IHomeBallsEntity, IKeyed, IIdentifiable
        {
            var dataSet =  keySelector == default ?
                new HomeBallsDataSet<TKey, TRecord>() :
                new HomeBallsDataSet<TKey, TRecord>(keySelector, record => record.Identifier);
            entities.Add((IHomeBallsReadOnlyCollection<IHomeBallsEntity>)dataSet.Values);
            return (dataSet, dataSet.AsReadOnly());
        }
    }

    public IReadOnlyCollection<IHomeBallsReadOnlyCollection<IHomeBallsEntity>> Entities { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsGameVersion> GameVersions { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsGeneration> Generations { get; }

    public IHomeBallsDataSet<UInt16, IHomeBallsItem> Items { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsItemCategory> ItemCategories { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsLanguage> Languages { get; }

    public IHomeBallsDataSet<UInt16, IHomeBallsMove> Moves { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategories { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsNature> Natures { get; }

    public IHomeBallsDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilities { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroups { get; }

    public IHomeBallsDataSet<(UInt16 SpeciesId, Byte FormId), IHomeBallsPokemonForm> PokemonForms { get; }

    public IHomeBallsDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpecies { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsStat> Stats { get; }

    public IHomeBallsDataSet<Byte, IHomeBallsType> Types { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGameVersion> GameVersionsReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGeneration> GenerationsReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsItem> ItemsReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsItemCategory> ItemCategoriesReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsLanguage> LanguagesReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsMove> MovesReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsMoveDamageCategory> MoveDamageCategoriesReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsNature> NaturesReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonAbility> PokemonAbilitiesReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsPokemonEggGroup> PokemonEggGroupsReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<(UInt16 SpeciesId, Byte FormId), IHomeBallsPokemonForm> PokemonFormsReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonSpecies> PokemonSpeciesReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsStat> StatsReadOnly { get; }

    protected internal IHomeBallsReadOnlyDataSet<Byte, IHomeBallsType> TypesReadOnly { get; }

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGameVersion> IHomeBallsDataSource.GameVersions => GameVersionsReadOnly;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsGeneration> IHomeBallsDataSource.Generations => GenerationsReadOnly;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsItem> IHomeBallsDataSource.Items => ItemsReadOnly;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsItemCategory> IHomeBallsDataSource.ItemCategories => ItemCategoriesReadOnly;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsLanguage> IHomeBallsDataSource.Languages => LanguagesReadOnly;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsMove> IHomeBallsDataSource.Moves => MovesReadOnly;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsMoveDamageCategory> IHomeBallsDataSource.MoveDamageCategories => MoveDamageCategoriesReadOnly;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsNature> IHomeBallsDataSource.Natures => NaturesReadOnly;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonAbility> IHomeBallsDataSource.PokemonAbilities => PokemonAbilitiesReadOnly;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsPokemonEggGroup> IHomeBallsDataSource.PokemonEggGroups => PokemonEggGroupsReadOnly;

    IHomeBallsReadOnlyDataSet<(UInt16 SpeciesId, Byte FormId), IHomeBallsPokemonForm> IHomeBallsDataSource.PokemonForms => PokemonFormsReadOnly;

    IHomeBallsReadOnlyDataSet<UInt16, IHomeBallsPokemonSpecies> IHomeBallsDataSource.PokemonSpecies => PokemonSpeciesReadOnly;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsStat> IHomeBallsDataSource.Stats => StatsReadOnly;

    IHomeBallsReadOnlyDataSet<Byte, IHomeBallsType> IHomeBallsDataSource.Types => TypesReadOnly;

    public virtual HomeBallsDataSourceMutable Clear()
    {
        GameVersions.Clear();
        Generations.Clear();
        Items.Clear();
        ItemCategories.Clear();
        Languages.Clear();
        Moves.Clear();
        MoveDamageCategories.Clear();
        Natures.Clear();
        PokemonAbilities.Clear();
        PokemonEggGroups.Clear();
        PokemonForms.Clear();
        PokemonSpecies.Clear();
        Stats.Clear();
        Types.Clear();
        return this;
    }

    IHomeBallsDataSourceMutable IHomeBallsDataSourceMutable.Clear() => Clear();
}