namespace CEo.Pokemon.HomeBalls.Data;

public interface IHomeBallsEntryLegalityInitializer
{
    Task<IHomeBallsEntryLegalityInitializer> SaveToDataDbContextAsync(
        HomeBallsDataDbContext dbContext,
        CancellationToken cancellationToken = default);
}

public class HomeBallsEntryLegalityInitializer :
    IHomeBallsEntryLegalityInitializer
{
    public HomeBallsEntryLegalityInitializer(
        ILogger? logger = default)
    {
        Logger = Logger;
    }

    protected internal ILogger? Logger { get; }

    public virtual async Task<HomeBallsEntryLegalityInitializer> SaveToDataDbContextAsync(
        HomeBallsDataDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Legalities.AddRangeAsync(
            await GenerateLegalitiesAsync(
                await dbContext.EnsureLoadedAsync(cancellationToken),
                cancellationToken));
        await dbContext.SaveChangesAsync(cancellationToken);
        return this;
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateLegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var generationTasks = new Task<IReadOnlyCollection<EFCoreEntryLegality>>[]
        {
            GenerateGen1LegalitiesAsync(data, cancellationToken),
            GenerateGen2LegalitiesAsync(data, cancellationToken),
            GenerateGen3LegalitiesAsync(data, cancellationToken),
            GenerateGen4LegalitiesAsync(data, cancellationToken),
            GenerateGen5LegalitiesAsync(data, cancellationToken),
            GenerateGen6LegalitiesAsync(data, cancellationToken),
            GenerateGen7LegalitiesAsync(data, cancellationToken),
            GenerateGen8LegalitiesAsync(data, cancellationToken),
        };
        return (await Task.WhenAll(generationTasks))
            .SelectMany(legality => legality)
            .OrderBy(legality => legality.SpeciesId)
            .ThenBy(legality => legality.FormId)
            .ThenBy(legality => legality.BallId)
            .ToList().AsReadOnly();
    }

    protected internal virtual Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateSwShLegalities(
        IEnumerable<HomeBallsPokemonFormKey> keys,
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        return Task.FromResult<IReadOnlyCollection<EFCoreEntryLegality>>(keys
            .SelectMany(key => factory.Pokemon(key.SpeciesId, key.FormId).ObtainableInSwSh().CreateLegalities())
            .Cast<EFCoreEntryLegality>()
            .ToList().AsReadOnly());
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen1LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        legalities.AddRange(await GenerateSwShLegalities(
            new HomeBallsPokemonFormKey[]
            {
                new(001, 1), new(004, 1), new(007, 1), new(010, 1), new(027, 1), new(027, 2),
                new(029, 1), new(032, 1), new(037, 1), new(037, 2), new(041, 1), new(043, 1),
                new(050, 1), new(050, 2), new(052, 1), new(052, 2), new(052, 3), new(054, 1),
                new(058, 1), new(060, 1), new(063, 1), new(066, 1), new(072, 1), new(077, 1),
                new(077, 2), new(079, 1), new(079, 2), new(081, 1), new(083, 1), new(083, 2),
                new(090, 1), new(092, 1), new(095, 1), new(098, 1), new(102, 1), new(104, 1),
                new(108, 1), new(109, 1), new(111, 1), new(113, 1), new(114, 1), new(115, 1),
                new(116, 1), new(118, 1), new(120, 1), new(122, 1), new(122, 2), new(123, 1),
                new(127, 1), new(128, 1), new(129, 1), new(131, 1), new(133, 1), new(137, 1),
                new(138, 1), new(140, 1), new(142, 1), new(143, 1), new(147, 1)
            },
            data,
            cancellationToken));

        /* weedle          */ legalities.AddRange(factory.Pokemon(13, 1).ObtainableInApricornBalls().ObtainableInSportBall().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* pidgey          */ legalities.AddRange(factory.Pokemon(16, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* rattata         */ legalities.AddRange(factory.Pokemon(19, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* rattata-alola   */ legalities.AddRange(factory.Pokemon(19, 2).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* spearow         */ legalities.AddRange(factory.Pokemon(21, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* ekans           */ legalities.AddRange(factory.Pokemon(23, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* paras           */ legalities.AddRange(factory.Pokemon(46, 1).ObtainableInSwSh().CreateLegalities());
        /* venonat         */ legalities.AddRange(factory.Pokemon(48, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInSportBall().ObtainableInShopBalls().ObtainableInDreamBall().CreateLegalities());
        /* mankey          */ legalities.AddRange(factory.Pokemon(56, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* bellsprout      */ legalities.AddRange(factory.Pokemon(69, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* geodude         */ legalities.AddRange(factory.Pokemon(74, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* geodude-alola   */ legalities.AddRange(factory.Pokemon(74, 2).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* doduo           */ legalities.AddRange(factory.Pokemon(84, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().CreateLegalities());
        /* seel            */ legalities.AddRange(factory.Pokemon(86, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* grimer          */ legalities.AddRange(factory.Pokemon(88, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* grimer-alola    */ legalities.AddRange(factory.Pokemon(88, 2).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* drowzee         */ legalities.AddRange(factory.Pokemon(96, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* voltorb         */ legalities.AddRange(factory.Pokemon(100, 1).ObtainableInSafariBall(false).ObtainableInApricornBalls(false).ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall(false).CreateLegalities());

        return legalities.AsReadOnly();
    }

    protected internal virtual Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen2LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        // $"/* {breedable.Identifier.PadRight(15)} */ legalities.AddRange(factory.Pokemon({breedable.SpeciesId}, {breedable.FormId}).CreateLegalities());"
        return Task.FromResult<IReadOnlyCollection<EFCoreEntryLegality>>(legalities.AsReadOnly());
    }

    protected internal virtual Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen3LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        // $"/* {breedable.Identifier.PadRight(15)} */ legalities.AddRange(factory.Pokemon({breedable.SpeciesId}, {breedable.FormId}).CreateLegalities());"
        return Task.FromResult<IReadOnlyCollection<EFCoreEntryLegality>>(legalities.AsReadOnly());
    }

    protected internal virtual Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen4LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        // $"/* {breedable.Identifier.PadRight(15)} */ legalities.AddRange(factory.Pokemon({breedable.SpeciesId}, {breedable.FormId}).CreateLegalities());"
        return Task.FromResult<IReadOnlyCollection<EFCoreEntryLegality>>(legalities.AsReadOnly());
    }

    protected internal virtual Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen5LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        // $"/* {breedable.Identifier.PadRight(15)} */ legalities.AddRange(factory.Pokemon({breedable.SpeciesId}, {breedable.FormId}).CreateLegalities());"
        return Task.FromResult<IReadOnlyCollection<EFCoreEntryLegality>>(legalities.AsReadOnly());
    }

    protected internal virtual Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen6LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        // $"/* {breedable.Identifier.PadRight(15)} */ legalities.AddRange(factory.Pokemon({breedable.SpeciesId}, {breedable.FormId}).CreateLegalities());"
        return Task.FromResult<IReadOnlyCollection<EFCoreEntryLegality>>(legalities.AsReadOnly());
    }

    protected internal virtual Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen7LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        // $"/* {breedable.Identifier.PadRight(15)} */ legalities.AddRange(factory.Pokemon({breedable.SpeciesId}, {breedable.FormId}).CreateLegalities());"
        return Task.FromResult<IReadOnlyCollection<EFCoreEntryLegality>>(legalities.AsReadOnly());
    }

    protected internal virtual Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen8LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        // $"/* {breedable.Identifier.PadRight(15)} */ legalities.AddRange(factory.Pokemon({breedable.SpeciesId}, {breedable.FormId}).CreateLegalities());"
        return Task.FromResult<IReadOnlyCollection<EFCoreEntryLegality>>(legalities.AsReadOnly());
    }

    async Task<IHomeBallsEntryLegalityInitializer> IHomeBallsEntryLegalityInitializer
        .SaveToDataDbContextAsync(
            HomeBallsDataDbContext dbContext,
            CancellationToken cancellationToken) =>
        await SaveToDataDbContextAsync(dbContext, cancellationToken);
}