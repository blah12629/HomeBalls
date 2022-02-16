namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryColumnFactory
{
    IHomeBallsEntryColumnFactory UsingPokemonForms(
        IEnumerable<IHomeBallsPokemonForm> pokemon,
        Boolean isBreedablesOnly = true,
        Boolean isSorted = true);

    IHomeBallsEntryColumnFactory UsingItems(
        IEnumerable<IHomeBallsItem> items,
        Boolean isBallsOnly = true,
        Boolean isSorted = true);

    IEnumerable<IHomeBallsEntryColumn> CreateColumns();
}

public class HomeBallsEntryColumnFactory :
    IHomeBallsEntryColumnFactory
{
    public HomeBallsEntryColumnFactory(
        IComparer<IHomeBallsItem> pokeballComparer,
        IComparer<IHomeBallsPokemonForm> pokemonComparer,
        ILoggerFactory? loggerFactory = default)
    {
        Items = new List<IHomeBallsItem> { }.AsReadOnly();
        Pokemon = new List<IHomeBallsPokemonForm> { }.AsReadOnly();
        (PokemonComparer, PokeballComparer) = (pokemonComparer, pokeballComparer);

        LoggerFactory = loggerFactory;
        Logger = LoggerFactory?.CreateLogger<HomeBallsEntryColumnFactory>();
    }

    protected internal IReadOnlyList<IHomeBallsItem> Items { get; set; }

    protected internal IReadOnlyList<IHomeBallsPokemonForm> Pokemon { get; set; }

    protected internal IComparer<IHomeBallsItem> PokeballComparer { get; }

    protected internal IComparer<IHomeBallsPokemonForm> PokemonComparer { get; }

    protected internal ILogger? Logger { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    public virtual IEnumerable<IHomeBallsEntryColumn> CreateColumns()
    {
        for (var i = 0; i < Pokemon.Count; i ++)
            yield return CreateColumn(Pokemon[i]);
    }

    protected internal virtual IHomeBallsEntryColumn CreateColumn(
        IHomeBallsPokemonForm form)
    {
        var (id, identifier) = (form.Id, CreateIdentifier(form.Id));
        var cells = CreateCells(form.Id, out var indexMap);
        var logger = LoggerFactory?.CreateLogger<HomeBallsEntryColumn>();
        return new HomeBallsEntryColumn(id, identifier, cells, indexMap, logger);
    }

    protected internal virtual IReadOnlyList<IHomeBallsEntryCell> CreateCells(
        HomeBallsPokemonFormKey columnId,
        out IReadOnlyDictionary<UInt16, Int32> indexMap)
    {
        var cells = new List<IHomeBallsEntryCell> { };
        var indexMapMutable = new Dictionary<UInt16, Int32> { };

        for (var i = 0; i < Items.Count; i ++)
        {
            var ballId = Items[i].Id;
            var cell = CreateCell(columnId, ballId);
            cells.Add(cell);
            indexMapMutable.Add(ballId, i);
        }

        indexMap = indexMapMutable.AsReadOnly();
        return cells.AsReadOnly();
    }

    protected internal virtual IHomeBallsEntryCell CreateCell(
        HomeBallsPokemonFormKey columnId,
        UInt16 ballId)
    {
        var id = new HomeBallsEntryKey(columnId.SpeciesId, columnId.FormId, ballId);
        var identifier = CreateIdentifier(id);
        var logger = LoggerFactory?.CreateLogger<HomeBallsEntryCell>();
        return new HomeBallsEntryCell(id, identifier, logger);
    }

    protected internal virtual String CreateIdentifier(
        HomeBallsEntryKey key) =>
        $"entry-{key.SpeciesId}-{key.FormId}-{key.BallId}";

    protected internal virtual String CreateIdentifier(
        HomeBallsPokemonFormKey key) =>
        $"entry-{key.SpeciesId}-{key.FormId}";

    public virtual HomeBallsEntryColumnFactory UsingItems(
        IEnumerable<IHomeBallsItem> items,
        Boolean isBallsOnly = true,
        Boolean isSorted = true)
    {
        if (!isBallsOnly) items = items.Where(item => item.Identifier.Contains("ball"));
        if (!isSorted) items = items.OrderBy(item => item, PokeballComparer);
        Items = items.ToList().AsReadOnly();
        return this;
    }

    public virtual HomeBallsEntryColumnFactory UsingPokemonForms(
        IEnumerable<IHomeBallsPokemonForm> pokemon,
        Boolean isBreedablesOnly = true,
        Boolean isSorted = true)
    {
        if (!isBreedablesOnly) pokemon = pokemon.Where(form => form.IsBreedable);
        if (!isSorted) pokemon = pokemon.OrderBy(form => form, PokemonComparer);
        Pokemon = pokemon.ToList().AsReadOnly();
        return this;
    }

    IHomeBallsEntryColumnFactory IHomeBallsEntryColumnFactory
        .UsingItems(
            IEnumerable<IHomeBallsItem> items,
            Boolean isBallsOnly,
            Boolean isSorted) =>
        UsingItems(items, isBallsOnly, isSorted);

    IHomeBallsEntryColumnFactory IHomeBallsEntryColumnFactory
        .UsingPokemonForms(
            IEnumerable<IHomeBallsPokemonForm> pokemon,
            Boolean isBreedablesOnly,
            Boolean isSorted) =>
        UsingPokemonForms(pokemon, isBreedablesOnly, isSorted);
}