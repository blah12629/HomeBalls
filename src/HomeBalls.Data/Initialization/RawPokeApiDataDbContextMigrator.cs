namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IRawPokeApiDataDbContextMigrator :
    IPokeApiDataDbContextMigrator
{
    new Task<IRawPokeApiDataDbContextMigrator> MigratePokeApiDataAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default);
}

public class RawPokeApiDataDbContextMigrator :
    IRawPokeApiDataDbContextMigrator
{
    public RawPokeApiDataDbContextMigrator(
        IRawPokeApiDataSource data,
        IRawPokeApiConverter converter,
        ILogger? logger = default)
    {
        (Data, Converter) = (data, converter);
        Logger = logger;
    }

    protected internal IRawPokeApiDataSource Data { get; }

    protected internal IRawPokeApiConverter Converter { get; }

    protected internal ILogger? Logger { get; }

    public virtual async Task<RawPokeApiDataDbContextMigrator> MigratePokeApiDataAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
    {
        await Data.EnsureLoadedAsync(cancellationToken);

        var conversions = new Func<IEnumerable<Object>>[]
        {
            () => Converter.Convert(Data.Versions, Data.VersionGroups),
            () => Converter.Convert(Data.VersionNames),
            () => Converter.Convert(Data.Generations),
            () => Converter.Convert(Data.GenerationNames),
            () => Converter.Convert(Data.Items),
            () => Converter.Convert(Data.ItemNames),
            () => Converter.Convert(Data.ItemCategories),
            () => Converter.Convert(Data.Languages),
            () => Converter.Convert(Data.LanguageNames),
            () => Converter.Convert(Data.Moves),
            () => Converter.Convert(Data.MoveNames),
            () => Converter.Convert(Data.MoveDamageClasses),
            () => Converter.Convert(Data.MoveDamageClassProse),
            () => Converter.Convert(Data.Natures),
            () => Converter.Convert(Data.NatureNames),
            () => Converter.Convert(Data.Abilities),
            () => Converter.Convert(Data.AbilityNames),
            () => Converter.Convert(Data.PokemonAbilities, Data.PokemonForms, Data.Pokemon),
            () => Converter.Convert(Data.EggGroups),
            () => Converter.Convert(Data.EggGroupProse),
            () => Converter.Convert(Data.PokemonEggGroups, Data.PokemonForms, Data.Pokemon),
            () => Converter.Convert(Data.PokemonForms, Data.Pokemon, Data.PokemonSpecies, Data.EvolutionChains),
            () => Converter.Convert(Data.PokemonFormNames, Data.PokemonForms, Data.Pokemon),
            () => Converter.Convert(Data.PokemonSpecies),
            () => Converter.Convert(Data.PokemonSpeciesNames),
            () => Converter.Convert(Data.PokemonTypes, Data.PokemonForms, Data.Pokemon),
            () => Converter.Convert(Data.Stats),
            () => Converter.Convert(Data.StatNames),
            () => Converter.Convert(Data.Types),
            () => Converter.Convert(Data.TypeNames),
        };

        await using var dataContext = getDataContext();
        var tasks = conversions.Select(conversion =>
            MigratePokeApiDataSetAsync(dataContext, conversion, cancellationToken));
        await Task.WhenAll(tasks);
        await dataContext.SaveChangesAsync(cancellationToken);

        return this;
    }

    protected internal virtual async Task MigratePokeApiDataSetAsync<TResult>(
        IHomeBallsBaseDataDbContext dataContext,
        Func<IEnumerable<TResult>> conversion,
        CancellationToken cancellationToken = default)
    {
        var converted = (IEnumerable<Object>)conversion();
        await dataContext.AddRangeAsync(converted, cancellationToken);
    }

    async Task<IPokeApiDataDbContextMigrator> IPokeApiDataDbContextMigrator
        .MigratePokeApiDataAsync(
            Func<IHomeBallsBaseDataDbContext> getDataContext,
            CancellationToken cancellationToken) =>
        await MigratePokeApiDataAsync(getDataContext, cancellationToken);

    async Task<IRawPokeApiDataDbContextMigrator> IRawPokeApiDataDbContextMigrator
        .MigratePokeApiDataAsync(
            Func<IHomeBallsBaseDataDbContext> getDataContext,
            CancellationToken cancellationToken) =>
        await MigratePokeApiDataAsync(getDataContext, cancellationToken);
}