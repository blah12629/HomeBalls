namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IPokeApiDataDbContextMigrator
{
    Task<IPokeApiDataDbContextMigrator> MigratePokeApiDataAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default);
}

public class PokeApiDataDbContextMigrator :
    IPokeApiDataDbContextMigrator
{
    public PokeApiDataDbContextMigrator(
        IRawPokeApiDataDbContextMigrator rawMigrator,
        IRawPokeApiConverter converter,
        IHomeBallsDataRepository repository,
        IProjectPokemonHomeSpriteIdService spriteIdService,
        IHomeBallsPokeBallService pokeBallService,
        ILogger? logger = default)
    {
        (RawMigrator, Repository, Converter) = (rawMigrator, repository, converter);
        (SpriteIdService, PokeBallService) = (spriteIdService, pokeBallService);
        Logger = logger;
    }

    protected internal IPokeApiDataDbContextMigrator RawMigrator { get; }

    protected internal IRawPokeApiConverter Converter { get; }

    protected internal IHomeBallsDataRepository Repository { get; }

    protected internal IProjectPokemonHomeSpriteIdService SpriteIdService { get; }

    protected internal IHomeBallsPokeBallService PokeBallService { get; }

    protected internal ILogger? Logger { get; }

    public virtual async Task<PokeApiDataDbContextMigrator> MigratePokeApiDataAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
    {
        await RawMigrator.MigratePokeApiDataAsync(getDataContext, cancellationToken);
        await RemoveUnnecessaryLanguagesAsync(getDataContext, cancellationToken);
        await TrimItemsAsync(getDataContext, cancellationToken);
        await FixFormIdentifiersAsync(getDataContext, cancellationToken);
        await LabelBreedablesAsync(getDataContext, cancellationToken);
        await AddSpriteIdsAsync(getDataContext, cancellationToken);
        await MarkPokeBallsAsync(getDataContext, cancellationToken);
        await VacuumAsync(getDataContext, cancellationToken);
        return this;
    }

    protected internal virtual Task<PokeApiDataDbContextMigrator> RemoveUnnecessaryLanguagesAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default) =>
        RemoveUnnecessaryLanguagesAsync(
            getDataContext,
            new Byte[] { PokeApiEnglishLanguageId },
            cancellationToken);

    protected internal virtual async Task<PokeApiDataDbContextMigrator> RemoveUnnecessaryLanguagesAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        IEnumerable<Byte> keptLanguageIds,
        CancellationToken cancellationToken = default)
    {
        await using var dbContext = getDataContext();

        Logger?.LogInformation(
            $"Removing `{nameof(HomeBallsString)}` whose " +
            $"`{nameof(HomeBallsString.LanguageId)}` " +
            $"is not within the set of [ {String.Join(", ", keptLanguageIds)} ] " +
            $"from `{dbContext.GetType()}`");

        await Repository.RemoveEntitiesAsync(
            dbContext.Strings,
            @string => !keptLanguageIds.Contains(@string.LanguageId),
            dbContext.SaveChangesAsync,
            cancellationToken);
        return this;
    }

    protected internal virtual async Task<PokeApiDataDbContextMigrator> TrimItemsAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
    {
        await using var dbContext = getDataContext();

        Logger?.LogInformation(
            $"Removing `{nameof(HomeBallsItem)}` whose " +
            $"`{nameof(HomeBallsItem.CategoryId)}` " +
            "is not within the set of [ 33, 34, 39 ] " +
            "or whose identifier does not contain `incense` " +
            $"from `{dbContext.GetType()}`.");

        await Repository.RemoveEntitiesAsync(
            dbContext.Items,
            dbContext.Items.Include(item => item.Names),
            item =>
                item.CategoryId != 33 && item.CategoryId != 34 && item.CategoryId != 39 &&
                !item.Identifier.Contains("incense"),
            dbContext.SaveChangesAsync,
            cancellationToken);
        return this;
    }

    protected internal virtual async Task<PokeApiDataDbContextMigrator> FixFormIdentifiersAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
    {
        await using var data = getDataContext();
        var tasks = new Task<PokeApiDataDbContextMigrator>[]
        {
            ReplaceFormIdentifierAsync(data, form => form.SpeciesId == 105 && form.FormId == 3, "totem-alola", cancellationToken)
        };

        await Task.WhenAll(tasks);
        await data.SaveChangesAsync(cancellationToken);
        return this;
    }

    protected internal virtual async Task<PokeApiDataDbContextMigrator> ReplaceFormIdentifierAsync(
        IHomeBallsBaseDataDbContext data,
        Expression<Func<HomeBallsPokemonForm, Boolean>> condition,
        String identifier,
        CancellationToken cancellationToken = default)
    {
        var form = await data.PokemonForms
            .AsNoTracking()
            .SingleAsync(condition, cancellationToken);

        data.Update(form with { FormIdentifier = identifier });
        return this;
    }

    protected internal virtual async Task<PokeApiDataDbContextMigrator> LabelBreedablesAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
    {
        var breedableIds = new HashSet<HomeBalls.Entities.HomeBallsPokemonFormKey>
        {
            (001, 1), (004, 1), (007, 1), (010, 1), (013, 1), (016, 1), (019, 1),
                (019, 2), (021, 1), (023, 1), (027, 1), (027, 2), (029, 1), (032, 1),
                (037, 1), (037, 2), (041, 1), (043, 1), (046, 1), (048, 1), (050, 1),
                (050, 2), (052, 1), (052, 2), (052, 3), (054, 1), (056, 1), (058, 1),
                (060, 1), (063, 1), (066, 1), (069, 1), (072, 1), (074, 1), (074, 2),
                (077, 1), (077, 2), (079, 1), (079, 2), (081, 1), (083, 1), (083, 2),
                (084, 1), (086, 1), (088, 1), (088, 2), (090, 1), (092, 1), (095, 1),
                (096, 1), (098, 1), (100, 1), (102, 1), (104, 1), (108, 1), (109, 1),
                (111, 1), (114, 1), (115, 1), (116, 1), (118, 1), (120, 1), (123, 1),
                (127, 1), (128, 1), (129, 1), (131, 1), (133, 1), (137, 1), (138, 1),
                (140, 1), (142, 1), (147, 1),
            (152, 1), (155, 1), (158, 1), (161, 1), (163, 1), (165, 1), (167, 1),
                (170, 1), (172, 1), (173, 1), (174, 1), (175, 1), (177, 1), (179, 1),
                (187, 1), (190, 1), (191, 1), (193, 1), (194, 1), (198, 1), (200, 1),
                (203, 1), (204, 1), (206, 1), (207, 1), (209, 1), (211, 1), (213, 1),
                (214, 1), (215, 1), (216, 1), (218, 1), (220, 1), (222, 1), (222, 2),
                (223, 1), (225, 1), (227, 1), (228, 1), (231, 1), (234, 1), (235, 1),
                (236, 1), (238, 1), (239, 1), (240, 1), (241, 1), (246, 1),
            (252, 1), (255, 1), (258, 1), (261, 1), (263, 1), (263, 2), (265, 1),
                (270, 1), (273, 1), (276, 1), (278, 1), (280, 1), (283, 1), (285, 1),
                (287, 1), (290, 1), (293, 1), (296, 1), (183, 1), (299, 1), (300, 1),
                (302, 1), (303, 1), (304, 1), (307, 1), (309, 1), (311, 1), (312, 1),
                (313, 1), (314, 1), (316, 1), (318, 1), (320, 1), (322, 1), (324, 1),
                (325, 1), (327, 1), (328, 1), (331, 1), (333, 1), (335, 1), (336, 1),
                (337, 1), (338, 1), (339, 1), (341, 1), (343, 1), (345, 1), (347, 1),
                (349, 1), (351, 1), (352, 1), (353, 1), (355, 1), (357, 1), (359, 1),
                (202, 1), (361, 1), (363, 1), (366, 1), (369, 1), (370, 1), (371, 1),
                (374, 1),
            (387, 1), (390, 1), (393, 1), (396, 1), (399, 1), (401, 1), (403, 1),
                (315, 1), (408, 1), (410, 1), (412, 1), (415, 1), (417, 1), (418, 1),
                (420, 1), (422, 1), (422, 2), (425, 1), (427, 1), (431, 1), (358, 1),
                (434, 1), (436, 1), (185, 1), (122, 1), (122, 2), (113, 1), (441, 1),
                (442, 1), (443, 1), (143, 1), (447, 1), (449, 1), (451, 1), (453, 1),
                (455, 1), (456, 1), (226, 1), (459, 1), (479, 1), (489, 1),
            (495, 1), (498, 1), (501, 1), (504, 1), (506, 1), (509, 1), (511, 1),
                (513, 1), (515, 1), (517, 1), (519, 1), (522, 1), (524, 1), (527, 1),
                (529, 1), (531, 1), (532, 1), (535, 1), (538, 1), (539, 1), (540, 1),
                (543, 1), (546, 1), (548, 1), (550, 1), (550, 2), (551, 1), (554, 1),
                (554, 2), (556, 1), (557, 1), (559, 1), (561, 1), (562, 1), (562, 2),
                (564, 1), (566, 1), (568, 1), (570, 1), (572, 1), (574, 1), (577, 1),
                (580, 1), (582, 1), (585, 1), (587, 1), (588, 1), (590, 1), (592, 1),
                (594, 1), (595, 1), (597, 1), (599, 1), (602, 1), (605, 1), (607, 1),
                (610, 1), (613, 1), (615, 1), (616, 1), (618, 1), (618, 2), (619, 1),
                (621, 1), (622, 1), (624, 1), (626, 1), (627, 1), (629, 1), (631, 1),
                (632, 1), (633, 1), (636, 1),
            (650, 1), (653, 1), (656, 1), (659, 1), (661, 1), (664, 1), (667, 1),
                (669, 1), (669, 2), (669, 3), (669, 4), (669, 5), (672, 1), (674, 1),
                (676, 1), (674, 1), (676, 1), (677, 1), (679, 1), (682, 1), (684, 1),
                (686, 1), (688, 1), (690, 1), (692, 1), (694, 1), (696, 1), (698, 1),
                (701, 1), (702, 1), (703, 1), (704, 1), (707, 1), (708, 1), (710, 1),
                (710, 2), (710, 3), (710, 4), (712, 1), (714, 1),
            (722, 1), (725, 1), (728, 1), (731, 1), (734, 1), (736, 1), (739, 1),
                (741, 1), (742, 1), (744, 1), (744, 2), (746, 1), (747, 1), (749, 1),
                (751, 1), (753, 1), (755, 1), (757, 1), (759, 1), (761, 1), (764, 1),
                (765, 1), (766, 1), (767, 1), (769, 1), (771, 1), (774, 8), (774, 9),
                (774, 10), (774, 11), (774, 12), (774, 13), (774, 14), (775, 1),
                (776, 1), (777, 1), (778, 1), (779, 1), (780, 1), (781, 1), (782, 1),
            (810, 1), (813, 1), (816, 1), (819, 1), (821, 1), (824, 1), (827, 1),
                (829, 1), (831, 1), (833, 1), (835, 1), (837, 1), (840, 1), (843, 1),
                (845, 1), (846, 1), (848, 1), (850, 1), (852, 1), (854, 1), (856, 1),
                (859, 1), (868, 1), (870, 1), (871, 1), (872, 1), (874, 1), (875, 1),
                (876, 1), (876, 2), (877, 1), (878, 1), (884, 1), (885, 1)
        };
        var breedableIdsImmutable = breedableIds.ToList().AsReadOnly();

        Logger?.LogInformation(
            $"Setting the `{nameof(HomeBallsPokemonForm.IsBreedable)}` property of " +
            $"{breedableIdsImmutable.Count} entities to `true`.");

        var forms = await Repository.GetPokemonFormsAsync(getDataContext, false, cancellationToken);
        var breedables = forms
            .Where(pokemon => breedableIds.Remove(pokemon.Id))
            .ToList().AsReadOnly();

        await using var dbContext = getDataContext();
        dbContext.PokemonForms.UpdateRange(breedables.Select(breedable =>
            breedable with { IsBreedable = true }));
        var count = await dbContext.SaveChangesAsync(cancellationToken);

        Logger?.LogInformation(
            $"{count} / {breedableIdsImmutable.Count} " +
            $"`{nameof(HomeBalls.Entities.IHomeBallsPokemonForm)}." +
            $"{nameof(HomeBallsPokemonForm.IsBreedable)}` set to `true`.");

        if (breedableIds.Count > 0) Logger?.LogWarning(
            $"`{nameof(HomeBallsPokemonForm)}` " +
            $"[ {String.Join(", ", breedableIds)} ] " +
            "could not be labelled as breedables.");

        return this;
    }

    protected internal virtual async Task<PokeApiDataDbContextMigrator> AddSpriteIdsAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
    {
        var forms = await Repository.GetPokemonFormsAsync(getDataContext, true, cancellationToken);
        await using var dbContext = getDataContext();

        dbContext.PokemonForms.UpdateRange(forms.Select(form =>
            form with
        {
            Species = default!,
            ProjectPokemonHomeSpriteId = SpriteIdService.GenerateId(form)
        }));

        await dbContext.SaveChangesAsync(cancellationToken);
        return this;
    }

    protected internal virtual async Task<PokeApiDataDbContextMigrator> MarkPokeBallsAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
    {
        var items = await Repository.GetItemsAsync(getDataContext, cancellationToken);
        await using var data = getDataContext();
        data.Items.UpdateRange(items.Select(item => PokeBallService.MarkItem(item)));
        await data.SaveChangesAsync(cancellationToken);
        return this;
    }

    protected internal virtual async Task<PokeApiDataDbContextMigrator> VacuumAsync(
        Func<IHomeBallsBaseDataDbContext> getDataContext,
        CancellationToken cancellationToken = default)
    {
        await using var dbContext = getDataContext();
        await dbContext.Database.VacuumAsync(cancellationToken);
        return this;
    }

    async Task<IPokeApiDataDbContextMigrator> IPokeApiDataDbContextMigrator
        .MigratePokeApiDataAsync(
            Func<IHomeBallsBaseDataDbContext> getDataContext,
            CancellationToken cancellationToken) =>
        await MigratePokeApiDataAsync(getDataContext, cancellationToken);
}