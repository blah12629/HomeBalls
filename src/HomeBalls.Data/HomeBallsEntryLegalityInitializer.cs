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
            .GroupBy(legality => (legality.SpeciesId, legality.FormId, legality.BallId))
            .Select(group => group.Last())
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

        /* weedle          */ legalities.AddRange(factory.Pokemon(013, 1)                              .ObtainableInApricornBalls().ObtainableInSportBall().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* pidgey          */ legalities.AddRange(factory.Pokemon(016, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* rattata         */ legalities.AddRange(factory.Pokemon(019, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* rattata-alola   */ legalities.AddRange(factory.Pokemon(019, 2).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* spearow         */ legalities.AddRange(factory.Pokemon(021, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* ekans           */ legalities.AddRange(factory.Pokemon(023, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* paras           */ legalities.AddRange(factory.Pokemon(046, 1).ObtainableInSwSh().CreateLegalities());
        /* venonat         */ legalities.AddRange(factory.Pokemon(048, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls().ObtainableInSportBall().ObtainableInShopBalls().ObtainableInDreamBall()                             .CreateLegalities());
        /* mankey          */ legalities.AddRange(factory.Pokemon(056, 1)                              .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* bellsprout      */ legalities.AddRange(factory.Pokemon(069, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* geodude         */ legalities.AddRange(factory.Pokemon(074, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* geodude-alola   */ legalities.AddRange(factory.Pokemon(074, 2).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* doduo           */ legalities.AddRange(factory.Pokemon(084, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall()                             .CreateLegalities());
        /* seel            */ legalities.AddRange(factory.Pokemon(086, 1)                              .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* grimer          */ legalities.AddRange(factory.Pokemon(088, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* grimer-alola    */ legalities.AddRange(factory.Pokemon(088, 2).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* drowzee         */ legalities.AddRange(factory.Pokemon(096, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall()     .CreateLegalities());
        /* voltorb         */ legalities.AddRange(factory.Pokemon(100, 1).ObtainableInSafariBall(false).ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall(false).CreateLegalities());
        return legalities.AsReadOnly();
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen2LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        legalities.AddRange(await GenerateSwShLegalities(
            new HomeBallsPokemonFormKey[]
            {
                new(163, 1), new(170, 1), new(172, 1), new(173, 1), new(174, 1), new(175, 1),
                new(177, 1), new(183, 1), new(185, 1), new(194, 1), new(202, 1), new(206, 1),
                new(211, 1), new(213, 1), new(214, 1), new(215, 1), new(220, 1), new(222, 1),
                new(222, 2), new(223, 1), new(225, 1), new(226, 1), new(227, 1), new(236, 1),
                new(238, 1), new(239, 1), new(240, 1), new(241, 1), new(246, 1), 
            },
            data,
            cancellationToken));

        /* chikorita       */ legalities.AddRange(factory.Pokemon(152, 1)                         .ObtainableInApricornBalls().ObtainableInShopBalls()                        .ObtainableInBeastBall().CreateLegalities());
        /* cyndaquil       */ legalities.AddRange(factory.Pokemon(155, 1)                         .ObtainableInApricornBalls().ObtainableInShopBalls()                        .ObtainableInBeastBall().CreateLegalities());
        /* totodile        */ legalities.AddRange(factory.Pokemon(158, 1)                         .ObtainableInApricornBalls().ObtainableInShopBalls()                        .ObtainableInBeastBall() .CreateLegalities());
        /* sentret         */ legalities.AddRange(factory.Pokemon(161, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall()                         .CreateLegalities());
        /* ledyba          */ legalities.AddRange(factory.Pokemon(165, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* spinarak        */ legalities.AddRange(factory.Pokemon(167, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* mareep          */ legalities.AddRange(factory.Pokemon(179, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* hoppip          */ legalities.AddRange(factory.Pokemon(187, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* aipom           */ legalities.AddRange(factory.Pokemon(190, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* sunkern         */ legalities.AddRange(factory.Pokemon(191, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* yanma           */ legalities.AddRange(factory.Pokemon(193, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* murkrow         */ legalities.AddRange(factory.Pokemon(198, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* misdreavus      */ legalities.AddRange(factory.Pokemon(200, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* girafarig       */ legalities.AddRange(factory.Pokemon(203, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall()                         .CreateLegalities());
        /* pineco          */ legalities.AddRange(factory.Pokemon(204, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* gligar          */ legalities.AddRange(factory.Pokemon(207, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall()                         .CreateLegalities());
        /* snubbull        */ legalities.AddRange(factory.Pokemon(209, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* teddiursa       */ legalities.AddRange(factory.Pokemon(216, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* slugma          */ legalities.AddRange(factory.Pokemon(218, 1)                         .ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* houndour        */ legalities.AddRange(factory.Pokemon(228, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* phanpy          */ legalities.AddRange(factory.Pokemon(231, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* stantler        */ legalities.AddRange(factory.Pokemon(234, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* smeargle        */ legalities.AddRange(factory.Pokemon(235, 1).ObtainableInSafariBall().ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        return legalities.AsReadOnly();
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen3LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        legalities.AddRange(await GenerateSwShLegalities(
            new HomeBallsPokemonFormKey[]
            {
                new(252, 1), new(255, 1), new(258, 1), new(263, 1), new(263, 2), new(270, 1),
                new(273, 1), new(278, 1), new(280, 1), new(290, 1), new(293, 1), new(302, 1),
                new(303, 1), new(304, 1), new(309, 1), new(315, 1), new(318, 1), new(320, 1),
                new(324, 1), new(328, 1), new(333, 1), new(337, 1), new(338, 1), new(339, 1),
                new(341, 1), new(343, 1), new(345, 1), new(347, 1), new(349, 1), new(355, 1),
                new(359, 1), new(361, 1), new(363, 1), new(369, 1), new(371, 1), new(374, 1)
            },
            data,
            cancellationToken));

        /* poochyena       */ legalities.AddRange(factory.Pokemon(261, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* wurmple         */ legalities.AddRange(factory.Pokemon(265, 1)                              .ObtainableInApricornBalls().ObtainableInSportBall()     .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* taillow         */ legalities.AddRange(factory.Pokemon(276, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* surskit         */ legalities.AddRange(factory.Pokemon(283, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* shroomish       */ legalities.AddRange(factory.Pokemon(285, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* slakoth         */ legalities.AddRange(factory.Pokemon(287, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* makuhita        */ legalities.AddRange(factory.Pokemon(296, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* nosepass        */ legalities.AddRange(factory.Pokemon(299, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* skitty          */ legalities.AddRange(factory.Pokemon(300, 1)                                                                                       .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* meditite        */ legalities.AddRange(factory.Pokemon(307, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* plusle          */ legalities.AddRange(factory.Pokemon(311, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* minun           */ legalities.AddRange(factory.Pokemon(312, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* volbeat         */ legalities.AddRange(factory.Pokemon(313, 1).ObtainableInSafariBall(false)                            .ObtainableInSportBall(false).ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* illumise        */ legalities.AddRange(factory.Pokemon(314, 1).ObtainableInSafariBall(false)                            .ObtainableInSportBall(false).ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* gulpin          */ legalities.AddRange(factory.Pokemon(316, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* numel           */ legalities.AddRange(factory.Pokemon(322, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* spoink          */ legalities.AddRange(factory.Pokemon(325, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* spinda          */ legalities.AddRange(factory.Pokemon(327, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* cacnea          */ legalities.AddRange(factory.Pokemon(331, 1).ObtainableInSafariBall()                                                              .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* zangoose        */ legalities.AddRange(factory.Pokemon(335, 1).ObtainableInSafariBall()                                                              .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* seviper         */ legalities.AddRange(factory.Pokemon(336, 1).ObtainableInSafariBall()                                                              .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* castform        */ legalities.AddRange(factory.Pokemon(351, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* kecleon         */ legalities.AddRange(factory.Pokemon(352, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* shuppet         */ legalities.AddRange(factory.Pokemon(353, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* tropius         */ legalities.AddRange(factory.Pokemon(357, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* chimecho        */ legalities.AddRange(factory.Pokemon(358, 1).ObtainableInSafariBall()     .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* clamperl        */ legalities.AddRange(factory.Pokemon(366, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* luvdisc         */ legalities.AddRange(factory.Pokemon(370, 1)                              .ObtainableInApricornBalls()                             .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());

        legalities.AddRange(new HomeBallsPokemonFormKey[]
        {
            new(300, 1), new(313, 1), new(314, 1), new (331, 1), new(335, 1), new(336, 1)
        }.SelectMany(key => factory
            .Pokemon(key.SpeciesId, key.FormId)
            .ObtainableInApricornBalls()
            .CreateLegalities()));

        return legalities.AsReadOnly();
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen4LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        legalities.AddRange(await GenerateSwShLegalities(
            new HomeBallsPokemonFormKey[]
            {
                new(403, 1), new(415, 1), new(420, 1), new(422, 1), new(422, 2), new(425, 1),
                new(427, 1), new(434, 1), new(436, 1), new(442, 1), new(443, 1), new(447, 1),
                new(449, 1), new(451, 1), new(453, 1), new(459, 1), new(479, 1), 
            },
            data,
            cancellationToken));

        /* turtwig         */ legalities.AddRange(factory.Pokemon(387, 1)                         .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* chimchar        */ legalities.AddRange(factory.Pokemon(390, 1)                         .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* piplup          */ legalities.AddRange(factory.Pokemon(393, 1)                         .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* starly          */ legalities.AddRange(factory.Pokemon(396, 1).ObtainableInSafariBall().ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* bidoof          */ legalities.AddRange(factory.Pokemon(399, 1).ObtainableInSafariBall().ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* kricketot       */ legalities.AddRange(factory.Pokemon(401, 1)                         .ObtainableInApricornBalls().ObtainableInSportBall().ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* cranidos        */ legalities.AddRange(factory.Pokemon(408, 1)                                                                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* shieldon        */ legalities.AddRange(factory.Pokemon(410, 1)                                                                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* burmy-plant     */ legalities.AddRange(factory.Pokemon(412, 1)                         .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* pachirisu       */ legalities.AddRange(factory.Pokemon(417, 1).ObtainableInSafariBall()                                                    .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* buizel          */ legalities.AddRange(factory.Pokemon(418, 1).ObtainableInSafariBall().ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* glameow         */ legalities.AddRange(factory.Pokemon(431, 1)                                                                             .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* chatot          */ legalities.AddRange(factory.Pokemon(441, 1)                         .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* carnivine       */ legalities.AddRange(factory.Pokemon(455, 1).ObtainableInSafariBall().ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* finneon         */ legalities.AddRange(factory.Pokemon(456, 1)                         .ObtainableInApricornBalls()                        .ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* phione          */ legalities.AddRange(factory.Pokemon(489, 1).CreateLegalities());
        
        /* pachirisu       */ legalities.AddRange(factory.Pokemon(417, 1).ObtainableInApricornBalls().CreateLegalities());
        /* glameow         */ legalities.AddRange(factory.Pokemon(431, 1).ObtainableInApricornBalls().CreateLegalities());
        return legalities.AsReadOnly();
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen5LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        legalities.AddRange(await GenerateSwShLegalities(
            new HomeBallsPokemonFormKey[]
            {
                new(506, 1), new(509, 1), new(517, 1), new(519, 1), new(524, 1), new(527, 1),
                new(529, 1), new(531, 1), new(532, 1), new(535, 1), new(538, 1), new(539, 1),
                new(543, 1), new(546, 1), new(548, 1), new(550, 1), new(550, 2), new(551, 1),
                new(554, 1), new(554, 2), new(556, 1), new(557, 1), new(559, 1), new(561, 1),
                new(562, 1), new(562, 2), new(564, 1), new(566, 1), new(568, 1), new(570, 1),
                new(572, 1), new(574, 1), new(577, 1), new(582, 1), new(587, 1), new(588, 1),
                new(590, 1), new(592, 1), new(595, 1), new(597, 1), new(599, 1), new(605, 1),
                new(607, 1), new(610, 1), new(613, 1), new(615, 1), new(616, 1), new(618, 1),
                new(618, 2), new(619, 1), new(621, 1), new(622, 1), new(624, 1), new(626, 1),
                new(627, 1), new(629, 1), new(631, 1), new(632, 1), new(633, 1), new(636, 1), 
            },
            data,
            cancellationToken));
    
        /* snivy           */ legalities.AddRange(factory.Pokemon(495, 1).ObtainableInApricornBalls().ObtainableInShopBalls()                        .ObtainableInBeastBall().CreateLegalities());
        /* tepig           */ legalities.AddRange(factory.Pokemon(498, 1).ObtainableInApricornBalls().ObtainableInShopBalls()                        .ObtainableInBeastBall().CreateLegalities());
        /* oshawott        */ legalities.AddRange(factory.Pokemon(501, 1).ObtainableInApricornBalls().ObtainableInShopBalls()                        .ObtainableInBeastBall().CreateLegalities());
        /* patrat          */ legalities.AddRange(factory.Pokemon(504, 1)                            .ObtainableInShopBalls()                                                .CreateLegalities());
        /* pansage         */ legalities.AddRange(factory.Pokemon(511, 1)                            .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* pansear         */ legalities.AddRange(factory.Pokemon(513, 1)                            .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* panpour         */ legalities.AddRange(factory.Pokemon(515, 1)                            .ObtainableInShopBalls().ObtainableInDreamBall()                        .CreateLegalities());
        /* blitzle         */ legalities.AddRange(factory.Pokemon(522, 1)                            .ObtainableInShopBalls()                                                .CreateLegalities());
        /* sewaddle        */ legalities.AddRange(factory.Pokemon(540, 1).ObtainableInApricornBalls().ObtainableInShopBalls()                        .ObtainableInBeastBall().CreateLegalities());
        /* ducklett        */ legalities.AddRange(factory.Pokemon(580, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* deerling-spring */ legalities.AddRange(factory.Pokemon(585, 1)                            .ObtainableInShopBalls()                                                .CreateLegalities());
        /* alomomola       */ legalities.AddRange(factory.Pokemon(594, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInDreamBall().ObtainableInBeastBall().CreateLegalities());
        /* tynamo          */ legalities.AddRange(factory.Pokemon(602, 1).ObtainableInApricornBalls().ObtainableInShopBalls()                        .ObtainableInBeastBall().CreateLegalities());
        return legalities.AsReadOnly();
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen6LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        legalities.AddRange(await GenerateSwShLegalities(
            new HomeBallsPokemonFormKey[]
            {
                new(659, 1), new(661, 1), new(674, 1), new(677, 1), new(679, 1), new(682, 1), 
                new(684, 1), new(686, 1), new(688, 1), new(690, 1), new(692, 1), new(694, 1), 
                new(696, 1), new(698, 1), new(701, 1), new(702, 1), new(703, 1), new(704, 1), 
                new(707, 1), new(708, 1), new(710, 1), new(710, 2), new(710, 3), new(710, 4), 
                new(712, 1), new(714, 1), 
            },
            data,
            cancellationToken));

        /* chespin         */ legalities.AddRange(factory.Pokemon(650, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* fennekin        */ legalities.AddRange(factory.Pokemon(653, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* froakie         */ legalities.AddRange(factory.Pokemon(656, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* scatterbug      */ legalities.AddRange(factory.Pokemon(664, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* litleo          */ legalities.AddRange(factory.Pokemon(667, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* flabebe-red     */ legalities.AddRange(factory.Pokemon(669, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* flabebe-yellow  */ legalities.AddRange(factory.Pokemon(669, 2).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* flabebe-orange  */ legalities.AddRange(factory.Pokemon(669, 3).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* flabebe-blue    */ legalities.AddRange(factory.Pokemon(669, 4).ObtainableInApricornBalls(false).ObtainableInShopBalls().ObtainableInBeastBall(false).CreateLegalities());
        /* flabebe-white   */ legalities.AddRange(factory.Pokemon(669, 5).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        /* skiddo          */ legalities.AddRange(factory.Pokemon(672, 1)                            .ObtainableInShopBalls()                        .CreateLegalities());
        /* furfrou-natural */ legalities.AddRange(factory.Pokemon(676, 1).ObtainableInApricornBalls().ObtainableInShopBalls().ObtainableInBeastBall().CreateLegalities());
        return legalities.AsReadOnly();
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen7LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        legalities.AddRange(await GenerateSwShLegalities(
            new HomeBallsPokemonFormKey[]
            {
                new(722, 1), new(725, 1), new(728, 1), new(736, 1), new(742, 1), new(744, 1), 
                new(744, 2), new(746, 1), new(747, 1), new(749, 1), new(751, 1), new(753, 1), 
                new(755, 1), new(757, 1), new(759, 1), new(761, 1), new(764, 1), new(765, 1), 
                new(766, 1), new(767, 1), new(769, 1), new(771, 1), new(776, 1), new(777, 1), 
                new(778, 1), new(780, 1), new(781, 1), new(782, 1), 
            },
            data,
            cancellationToken));

        legalities.AddRange(new HomeBallsPokemonFormKey[]
        {
            new(731, 1), new(734, 1), new(739, 1), new(741, 1), new(774, 8), new(774, 9), 
            new(774, 10), new(774, 11), new(774, 12), new(774, 13), new(774, 14), new(775, 1), 
            new(779, 1), 
        }
            .SelectMany(key => factory.Pokemon(key.SpeciesId, key.FormId)
                .ObtainableInApricornBalls()
                .ObtainableInShopBalls()
                .ObtainableInBeastBall()
                .CreateLegalities()));
        return legalities.AsReadOnly();
    }

    protected internal virtual async Task<IReadOnlyCollection<EFCoreEntryLegality>> GenerateGen8LegalitiesAsync(
        IHomeBallsDataSource data,
        CancellationToken cancellationToken = default)
    {
        var legalities = new List<EFCoreEntryLegality> { };
        var factory = new HomeBallsEntryLegalityCollectionFactory(data, Logger);
        legalities.AddRange(await GenerateSwShLegalities(
            new HomeBallsPokemonFormKey[]
            {
                new(819, 1), new(821, 1), new(824, 1), new(827, 1), new(829, 1), new(831, 1), 
                new(833, 1), new(835, 1), new(837, 1), new(840, 1), new(843, 1), new(845, 1), 
                new(846, 1), new(848, 1), new(850, 1), new(852, 1), new(854, 1), new(856, 1), 
                new(859, 1), new(868, 1), new(870, 1), new(871, 1), new(872, 1), new(874, 1), 
                new(875, 1), new(876, 1), new(876, 2), new(877, 1), new(878, 1), new(884, 1), 
                new(885, 1), 
            },
            data,
            cancellationToken));

        /* grookey         */ legalities.AddRange(factory.Pokemon(810, 1).CreateLegalities());
        /* scorbunny       */ legalities.AddRange(factory.Pokemon(813, 1).CreateLegalities());
        /* sobble          */ legalities.AddRange(factory.Pokemon(816, 1).CreateLegalities());
        return legalities.AsReadOnly();
    }

    async Task<IHomeBallsEntryLegalityInitializer> IHomeBallsEntryLegalityInitializer
        .SaveToDataDbContextAsync(
            HomeBallsDataDbContext dbContext,
            CancellationToken cancellationToken) =>
        await SaveToDataDbContextAsync(dbContext, cancellationToken);
}