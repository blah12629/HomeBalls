namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IHomeBallsEntryLegalityCollectionFactory
{
    Task<IHomeBallsEntryLegalityCollectionFactory> EnsureBallsLoadedAsync(
        Func<IHomeBallsBaseDataDbContext> getData,
        CancellationToken cancellationToken = default);

    IHomeBallsEntryLegalityCollectionBuilder ForPokemon(
        HomeBalls.Entities.HomeBallsPokemonFormKey id);

    IHomeBallsEntryLegalityCollectionBuilder ForPokemon(
        UInt16 speciesId,
        Byte formId);
}

public class HomeBallsEntryLegalityCollectionFactory :
    IHomeBallsEntryLegalityCollectionFactory
{
    public HomeBallsEntryLegalityCollectionFactory(
        ILoggerFactory? loggerFactory = default)
    {
        LoggerFactory = loggerFactory;
        (ShopBallIds, ApricornBallIds) = (new HashSet<UInt16>(), new HashSet<UInt16>());
    }

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal ICollection<UInt16> ShopBallIds { get; }

    protected internal ICollection<UInt16> ApricornBallIds { get; }

    public virtual async Task<HomeBallsEntryLegalityCollectionFactory> EnsureBallsLoadedAsync(
        Func<IHomeBallsBaseDataDbContext> getData,
        CancellationToken cancellationToken = default)
    {
        await using var data = getData();

        ShopBallIds.AddRange(new UInt16[]
        {
            1, 2, 3, 4,
            6, 7, 8, 9, 10, 11, 12, 13, 14, 15
        });

        ApricornBallIds.AddRange(await data.Items
            .Where(item => item.CategoryId == 39)
            .Select(item => item.Id)
            .ToListAsync());

        return this;
    }

    public virtual IHomeBallsEntryLegalityCollectionBuilder ForPokemon(
        HomeBalls.Entities.HomeBallsPokemonFormKey id) =>
        new HomeBallsEntryLegalityCollectionBuilder(
            id,
            ShopBallIds.AsReadOnly(),
            ApricornBallIds.AsReadOnly(),
            LoggerFactory?.CreateLogger<HomeBallsEntryLegalityCollectionBuilder>());

    public virtual IHomeBallsEntryLegalityCollectionBuilder ForPokemon(
        UInt16 speciesId,
        Byte formId) =>
        ForPokemon((speciesId, formId));

    async Task<IHomeBallsEntryLegalityCollectionFactory> IHomeBallsEntryLegalityCollectionFactory
        .EnsureBallsLoadedAsync(
            Func<IHomeBallsBaseDataDbContext> getData,
            CancellationToken cancellationToken) =>
        await EnsureBallsLoadedAsync(getData, cancellationToken);
}