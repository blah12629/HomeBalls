namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IHomeBallsEntryLegalitySeeder
{
    Task<IHomeBallsEntryLegalitySeeder> SeedEntriesAsync(
        Func<IHomeBallsBaseDataDbContext> getData,
        CancellationToken cancellationToken = default);
}

public class HomeBallsEntryLegalitySeeder :
    IHomeBallsEntryLegalitySeeder
{
    public HomeBallsEntryLegalitySeeder(
        IHomeBallsEntryLegalityCollectionFactory legalities,
        ILogger? logger = default)
    {
        Breedables = new Dictionary<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> { };
        BreedableConfigurations = new Dictionary<HomeBalls.Entities.HomeBallsPokemonFormKey, IHomeBallsEntryLegalityCollectionBuilder> { };
        IsBreedableConfigured = new Dictionary<HomeBalls.Entities.HomeBallsPokemonFormKey, Boolean> { };

        Legalities = legalities;
        Logger = logger;
    }

    protected internal ILogger? Logger { get; }

    protected internal IHomeBallsEntryLegalityCollectionFactory Legalities { get; }

    protected internal IDictionary<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> Breedables { get; }

    protected internal IDictionary<HomeBalls.Entities.HomeBallsPokemonFormKey, IHomeBallsEntryLegalityCollectionBuilder> BreedableConfigurations { get; }

    protected internal IDictionary<HomeBalls.Entities.HomeBallsPokemonFormKey, Boolean> IsBreedableConfigured { get; }
    
    public virtual async Task<HomeBallsEntryLegalitySeeder> SeedEntriesAsync(
        Func<IHomeBallsBaseDataDbContext> getData,
        CancellationToken cancellationToken = default)
    {
        await PopulateBreedablesAsync(getData, cancellationToken);
        SeedPokeBallEntries();
        SeedSwshEntries();
        SeedUsumEntries();
        SeedDreamBallEntries();
        SeedSafariBallEntries();
        SeedSportBallEntries();
        SeedApricornBallEntries();
        SeedShopBallEntries();
        SeedBdspEntries();
        await SaveLegalitiesAsync(getData, cancellationToken);

        var unconfiguredIds = IsBreedableConfigured
            .Where(pair => !pair.Value)
            .Select(pair => pair.Key)
            .ToList().AsReadOnly();

        if (unconfiguredIds.Count > 0) Logger?.LogWarning(
            $"No legality entries were added for these {unconfiguredIds.Count} " +
            $"`{nameof(HomeBalls.Entities.IHomeBallsPokemonForm)}`:\n\t" +
            $@"{String.Join("\n\t", unconfiguredIds
                .Select(id => Breedables[id])
                .Select(form => $"{form.Id}\t{form.Identifier}"))}");

        return this;
    }

    protected internal virtual async Task<HomeBallsEntryLegalitySeeder> PopulateBreedablesAsync(
        Func<IHomeBallsBaseDataDbContext> getData,
        CancellationToken cancellationToken = default)
    {
        var data = getData();
        var forms = await data.PokemonForms
            .Where(form => form.IsBreedable)
            .ToListAsync(cancellationToken);
        await data.DisposeAsync();

        Breedables.Clear();
        foreach (var form in forms.OrderBy(form => form.Id))
        {
            Breedables.Add(form.Id, form);
            BreedableConfigurations.Add(form.Id, Legalities.ForPokemon(form.Id));
            IsBreedableConfigured.Add(form.Id, false);
        }
        return this;
    }

    protected internal virtual HomeBallsEntryLegalitySeeder UpdateBreedables(
        IEnumerable<IEnumerable<HomeBalls.Entities.HomeBallsPokemonFormKey>> keys,
        Func<IHomeBallsEntryLegalityCollectionBuilder, IHomeBallsEntryLegalityCollectionBuilder> configuration,
        Boolean setWhenConfigured = true) =>
        UpdateBreedables(keys.SelectMany(keys => keys), configuration, setWhenConfigured);

    protected internal virtual HomeBallsEntryLegalitySeeder UpdateBreedables(
        IEnumerable<HomeBalls.Entities.HomeBallsPokemonFormKey> keys,
        Func<IHomeBallsEntryLegalityCollectionBuilder, IHomeBallsEntryLegalityCollectionBuilder> configuration,
        Boolean setWhenConfigured = true)
    {
        foreach (var key in keys)
        {
            configuration(BreedableConfigurations[key]);
            if (setWhenConfigured) IsBreedableConfigured[key] = true;
        }
        return this;
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedPokeBallEntries()
    {
        var keys = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (489, 1), (810, 1), (813, 1), (816, 1)
        };

        return UpdateBreedables(keys, pokemon => pokemon.ObtainableIn.PokeBall());
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedSwshEntries()
    {
        var gen1 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (001, 1), (004, 1), (007, 1), (010, 1), (027, 1), (027, 2),
            (029, 1), (032, 1), (037, 1), (037, 2), (041, 1), (043, 1),
            (050, 1), (050, 2), (052, 1), (052, 2), (052, 3), (054, 1),
            (058, 1), (060, 1), (063, 1), (066, 1), (072, 1), (077, 1),
            (077, 2), (079, 1), (079, 2), (081, 1), (083, 1), (083, 2),
            (090, 1), (092, 1), (095, 1), (098, 1), (102, 1), (104, 1),
            (108, 1), (109, 1), (111, 1), (113, 1), (114, 1), (115, 1),
            (116, 1), (118, 1), (120, 1), (122, 1), (122, 2), (123, 1),
            (127, 1), (128, 1), (129, 1), (131, 1), (133, 1), (137, 1),
            (138, 1), (140, 1), (142, 1), (143, 1), (147, 1)
        };
        var gen2 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (163, 1), (170, 1), (172, 1), (173, 1), (174, 1), (175, 1),
            (177, 1), (183, 1), (185, 1), (194, 1), (202, 1), (206, 1),
            (211, 1), (213, 1), (214, 1), (215, 1), (220, 1), (222, 1),
            (222, 2), (223, 1), (225, 1), (226, 1), (227, 1), (236, 1),
            (238, 1), (239, 1), (240, 1), (241, 1), (246, 1), 
        };
        var gen3 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (252, 1), (255, 1), (258, 1), (263, 1), (263, 2), (270, 1),
            (273, 1), (278, 1), (280, 1), (290, 1), (293, 1), (302, 1),
            (303, 1), (304, 1), (309, 1), (315, 1), (318, 1), (320, 1),
            (324, 1), (328, 1), (333, 1), (337, 1), (338, 1), (339, 1),
            (341, 1), (343, 1), (345, 1), (347, 1), (349, 1), (355, 1),
            (359, 1), (361, 1), (363, 1), (369, 1), (371, 1), (374, 1)
        };
        var gen4 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (403, 1), (415, 1), (420, 1), (422, 1), (422, 2), (425, 1),
            (427, 1), (434, 1), (436, 1), (442, 1), (443, 1), (447, 1),
            (449, 1), (451, 1), (453, 1), (459, 1), (479, 1), 
        };
        var gen5 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (506, 1), (509, 1), (517, 1), (519, 1), (524, 1), (527, 1),
            (529, 1), (531, 1), (532, 1), (535, 1), (538, 1), (539, 1),
            (543, 1), (546, 1), (548, 1), (550, 1), (550, 2), (551, 1),
            (554, 1), (554, 2), (556, 1), (557, 1), (559, 1), (561, 1),
            (562, 1), (562, 2), (564, 1), (566, 1), (568, 1), (570, 1),
            (572, 1), (574, 1), (577, 1), (582, 1), (587, 1), (588, 1),
            (590, 1), (592, 1), (595, 1), (597, 1), (599, 1), (605, 1),
            (607, 1), (610, 1), (613, 1), (615, 1), (616, 1), (618, 1),
            (618, 2), (619, 1), (621, 1), (622, 1), (624, 1), (626, 1),
            (627, 1), (629, 1), (631, 1), (632, 1), (633, 1), (636, 1),
        };
        var gen6 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (659, 1), (661, 1), (674, 1), (677, 1), (679, 1), (682, 1), 
            (684, 1), (686, 1), (688, 1), (690, 1), (692, 1), (694, 1), 
            (696, 1), (698, 1), (701, 1), (702, 1), (703, 1), (704, 1), 
            (707, 1), (708, 1), (710, 1), (710, 2), (710, 3), (710, 4), 
            (712, 1), (714, 1),
        };
        var gen7 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (722, 1), (725, 1), (728, 1), (736, 1), (742, 1), (744, 1), 
            (744, 2), (746, 1), (747, 1), (749, 1), (751, 1), (753, 1), 
            (755, 1), (757, 1), (759, 1), (761, 1), (764, 1), (765, 1), 
            (766, 1), (767, 1), (769, 1), (771, 1), (776, 1), (777, 1), 
            (778, 1), (780, 1), (781, 1), (782, 1),
        };
        var gen8 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (819, 1), (821, 1), (824, 1), (827, 1), (829, 1), (831, 1), 
            (833, 1), (835, 1), (837, 1), (840, 1), (843, 1), (845, 1), 
            (846, 1), (848, 1), (850, 1), (852, 1), (854, 1), (856, 1), 
            (859, 1), (868, 1), (870, 1), (871, 1), (872, 1), (874, 1), 
            (875, 1), (876, 1), (876, 2), (877, 1), (878, 1), (884, 1), 
            (885, 1),
        };

        return UpdateBreedables(
            new[] { gen1, gen2, gen3, gen4, gen5, gen6, gen7, gen8 },
            pokemon => pokemon.ObtainableIn.Swsh());
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedUsumEntries()
    {
        var gen1 = Breedables.Keys
            .Where(id =>
                id.SpeciesId.IsInBetweenOf(000, 028) ||
                id.SpeciesId.IsInBetweenOf(035, 042) ||
                id.SpeciesId.IsInBetweenOf(046, 047) ||
                id.SpeciesId.IsInBetweenOf(050, 076) ||
                id.SpeciesId.IsInBetweenOf(079, 082) ||
                id.SpeciesId.IsInBetweenOf(086, 095) ||
                id.SpeciesId.IsInBetweenOf(102, 105) ||
                id.SpeciesId == 108 ||
                id.SpeciesId.IsInBetweenOf(111, 113) ||
                id.SpeciesId.IsInBetweenOf(115, 136) ||
                id.SpeciesId.IsInBetweenOf(143, 149))
            .ToArray();
        var gen2 = Breedables.Keys
            .Where(id =>
                id.SpeciesId.IsInBetweenOf(152, 160) ||
                id.SpeciesId.IsInBetweenOf(163, 185) ||
                id.SpeciesId == 190 ||
                id.SpeciesId.IsInBetweenOf(193, 200) ||
                id.SpeciesId.IsInBetweenOf(204, 206) ||
                id.SpeciesId.IsInBetweenOf(209, 210) ||
                id.SpeciesId.IsInBetweenOf(214, 215) ||
                id.SpeciesId.IsInBetweenOf(218, 229) ||
                id.SpeciesId.IsInBetweenOf(235, 248))
            .ToArray();
        var gen3 = Breedables.Keys
            .Where(id =>
                id.SpeciesId.IsInBetweenOf(252, 376) &&
                !id.SpeciesId.IsInBetweenOf(261, 269) &&
                !id.SpeciesId.IsInBetweenOf(285, 286) &&
                !id.SpeciesId.IsInBetweenOf(290, 295) &&
                !id.SpeciesId.IsInBetweenOf(300, 301) &&
                !id.SpeciesId.IsInBetweenOf(311, 314) &&
                !id.SpeciesId.IsInBetweenOf(316, 317) &&
                !id.SpeciesId.IsInBetweenOf(322, 323) &&
                !id.SpeciesId.IsInBetweenOf(331, 332) &&
                !id.SpeciesId.IsInBetweenOf(335, 338) &&
                !id.SpeciesId.IsInBetweenOf(345, 348) &&
                !id.SpeciesId.IsInBetweenOf(335, 336) &&
                id.SpeciesId != 358)
            .ToArray();
        var gen4 = Breedables.Keys
            .Where(id =>
                id.SpeciesId.IsInBetweenOf(387, 398) ||
                id.SpeciesId.IsInBetweenOf(403, 405) ||
                id.SpeciesId.IsInBetweenOf(418, 419) ||
                id.SpeciesId.IsInBetweenOf(422, 428) ||
                id.SpeciesId.IsInBetweenOf(443, 452) ||
                id.SpeciesId.IsInBetweenOf(456, 479))
            .ToArray();
        var gen5 = Breedables.Keys
            .Where(id =>
                id.SpeciesId.IsInBetweenOf(495, 503) ||
                id.SpeciesId.IsInBetweenOf(506, 507) ||
                id.SpeciesId.IsInBetweenOf(524, 526) ||
                id.SpeciesId.IsInBetweenOf(531, 534) ||
                id.SpeciesId.IsInBetweenOf(540, 558) ||
                id.SpeciesId.IsInBetweenOf(559, 561) ||
                id.SpeciesId.IsInBetweenOf(568, 584) ||
                id.SpeciesId.IsInBetweenOf(587, 587) ||
                id.SpeciesId.IsInBetweenOf(592, 594) ||
                id.SpeciesId.IsInBetweenOf(602, 612) ||
                id.SpeciesId.IsInBetweenOf(618, 625) ||
                id.SpeciesId.IsInBetweenOf(627, 630) ||
                id.SpeciesId.IsInBetweenOf(633, 637))
            .ToArray();
        var gen6 = Breedables.Keys
            .Where(id =>
                id.SpeciesId.IsInBetweenOf(650, 658) ||
                id.SpeciesId.IsInBetweenOf(661, 668) ||
                id.SpeciesId.IsInBetweenOf(674, 676) ||
                id.SpeciesId.IsInBetweenOf(679, 681) ||
                id.SpeciesId.IsInBetweenOf(686, 695) ||
                id.SpeciesId.IsInBetweenOf(701, 709) ||
                id.SpeciesId.IsInBetweenOf(714, 715))
            .ToArray();
        var gen7 = Breedables.Keys.Where(id => id.SpeciesId >= 731).ToArray();

        UpdateBreedables(
            new[] { gen1, gen2, gen3, gen4, gen5, gen6, gen7 },
            pokemon => pokemon.ObtainableIn.Usum());

        UpdateBreedables(
            new HomeBalls.Entities.HomeBallsPokemonFormKey[] { (100, 1), (599, 1) },
            pokemon => pokemon.ObtainableIn.Usum(false));

        UpdateBreedables(
            new Byte[] { 1, 2, 3, 5 }.Select(id =>
                (HomeBalls.Entities.HomeBallsPokemonFormKey)(669, id)),
            pokemon => pokemon.ObtainableIn.Usum());

        return this;
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedDreamBallEntries()
    {
        var preGen6 = Breedables.Keys
            .Where(id =>
                id.SpeciesId < 650 &&
                !id.SpeciesId.IsInBetweenOf(489, 489) &&
                !id.SpeciesId.IsInBetweenOf(495, 510) &&
                !id.SpeciesId.IsInBetweenOf(522, 523) &&
                !id.SpeciesId.IsInBetweenOf(528, 529) &&
                !id.SpeciesId.IsInBetweenOf(540, 542) &&
                !id.SpeciesId.IsInBetweenOf(554, 556) &&
                !id.SpeciesId.IsInBetweenOf(562, 563) &&
                !id.SpeciesId.IsInBetweenOf(566, 573) &&
                !id.SpeciesId.IsInBetweenOf(585, 586) &&
                !id.SpeciesId.IsInBetweenOf(590, 593) &&
                !id.SpeciesId.IsInBetweenOf(597, 598) &&
                !id.SpeciesId.IsInBetweenOf(602, 604) &&
                !id.SpeciesId.IsInBetweenOf(607, 609) &&
                !id.SpeciesId.IsInBetweenOf(613, 615) &&
                !id.SpeciesId.IsInBetweenOf(619, 620) &&
                !id.SpeciesId.IsInBetweenOf(626, 630) &&
                !id.SpeciesId.IsInBetweenOf(633, 637))
            .ToArray();

        return UpdateBreedables(preGen6, pokemon => pokemon.ObtainableIn.DreamBall(), false);
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedSafariBallEntries()
    {
        var rangesWithHiddenAbility = new (UInt16 Start, UInt16 End)[]
        {
            (016, 028), (035, 036), (039, 051), (054, 055), (060, 071), (074, 079),
            (083, 085), (088, 089), (093, 099), (102, 105), (108, 112), (114, 114),
            (118, 119), (122, 123), (125, 127), (129, 131), (147, 149),
            (161, 168), (177, 184), (187, 205), (207, 210), (213, 214), (216, 217),
            (223, 224), (228, 235), (246, 248),
            (263, 264), (270, 275), (283, 289), (299, 299), (304, 310), (315, 319),
            (324, 324), (327, 332), (335, 342), (352, 358), (363, 365), (371, 373),
            (396, 400), (403, 405), (417, 419), (443, 455)
        };
        var rangesWithoutHiddenAbility = new (UInt16 Start, UInt16 End)[]
        {
            (029, 034), (081, 082), (100, 101), (113, 113), (115, 115), (128, 128),
            (241, 241), (313, 314), (374, 374), (436, 436)
        };

        UpdateBreedables(
            Breedables.Keys
                .Where(id => isWithinRanges(id.SpeciesId, rangesWithHiddenAbility))
                .ToArray(),
            pokemon => pokemon.ObtainableIn.SafariBall(),
            false);

        UpdateBreedables(
            Breedables.Keys
                .Where(id => isWithinRanges(id.SpeciesId, rangesWithoutHiddenAbility))
                .ToArray(),
            pokemon => pokemon.ObtainableIn.SafariBall(false),
            false);

        return this;

        Boolean isWithinRanges(UInt16 id, (UInt16 Start, UInt16 End)[] ranges) =>
            ranges.Any(range => id.IsInBetweenOf(range.Start, range.End));
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedSportBallEntries()
    {
        var hasHiddenAbility = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (010, 1), (013, 1), (046, 1), (048, 1), (123, 1), (127, 1),
            (265, 1), (290, 1), (401, 1), (415, 1)
        };
        var hasNoHiddenAbility = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
        {
            (313, 1), (314, 1)
        };

        UpdateBreedables(hasHiddenAbility, pokemon => pokemon.ObtainableIn.SportBall(), false);
        UpdateBreedables(hasNoHiddenAbility, pokemon => pokemon.ObtainableIn.SportBall(false), false);
        return this;
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedApricornBallEntries()
    {
        var rangesWithHiddenAbility = new (UInt16 Start, UInt16 End)[]
        {
            (001, 028), (035, 099), (102, 105), (108, 136), (143, 149),
            (152, 248),
            (252, 289), (293, 299), (302, 312), (315, 328), (333, 334), (339, 344),
            (349, 354), (357, 376),
            (387, 405), (412, 415), (418, 428), (441, 441), (443, 452), (455, 479)
        };
        var rangesWithoutHiddenAbility = new (UInt16 Start, UInt16 End)[]
        {
            (029, 034), (100, 101), (436, 437)
        };

        UpdateBreedables(
            Breedables.Keys
                .Where(id => isWithinRanges(id.SpeciesId, rangesWithHiddenAbility))
                .ToArray(),
            pokemon => pokemon.ObtainableIn.SafariBall());

        UpdateBreedables(
            Breedables.Keys
                .Where(id => isWithinRanges(id.SpeciesId, rangesWithoutHiddenAbility))
                .ToArray(),
            pokemon => pokemon.ObtainableIn.SafariBall(false));

        return this;

        Boolean isWithinRanges(UInt16 id, (UInt16 Start, UInt16 End)[] ranges) =>
            ranges.Any(range => id.IsInBetweenOf(range.Start, range.End));
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedShopBallEntries()
    {
        var excludedSpeciesIds = new UInt16[]
        {
            489, 566, 696, 698, 722, 725, 728, 810, 813, 816
        };

        return UpdateBreedables(
            Breedables.Keys.Where(id => !excludedSpeciesIds.Contains(id.SpeciesId)).ToArray(),
            pokemon => pokemon.ObtainableIn.ShopBalls());
    }

    protected internal virtual HomeBallsEntryLegalitySeeder SeedBdspEntries()
    {
        var keys = Breedables.Keys
            .Where(id => id.SpeciesId < 495 && id.SpeciesId != 408 && id.SpeciesId != 410)
            .ToArray();

        return UpdateBreedables(keys, pokemon => pokemon.ObtainableIn.Bdsp());
    }

    protected internal virtual async Task<HomeBallsEntryLegalitySeeder> SaveLegalitiesAsync(
        Func<IHomeBallsBaseDataDbContext> getData,
        CancellationToken cancellationToken = default)
    {
        var legalities = BreedableConfigurations.Values
            .SelectMany(pokemon => pokemon.BuildLegalities())
            .OrderBy(legality => legality.Id)
            .ToList().AsReadOnly();

        await using var data = getData();
        await data.Legalities.AddRangeAsync(legalities, cancellationToken);
        await data.SaveChangesAsync(cancellationToken);
        await data.Database.VacuumAsync(cancellationToken);
        return this;
    }

    // public virtual async Task<HomeBallsEntryLegalitySeeder> SeedEntriesAsync(
    //     Func<IHomeBallsBaseDataDbContext> getData,
    //     CancellationToken cancellationToken = default)
    // {
    //     IReadOnlyList<HomeBallsPokemonForm> forms;

    //     await PopulateBreedableIdsAsync(getData, cancellationToken);
    //     await SeedPokeBallEntriesAsync(getData, cancellationToken);
    //     await SeedSwshEntriesAsync(getData, cancellationToken);
    //     await SeedUsumEntriesAsync(getData, cancellationToken);
    //     // await SeedDreamBallEntriesAsync(getData, cancellationToken);
    //     // await SeedSafariBallEntriesAsync(getData, cancellationToken);
    //     // await SeedSportBallEntriesAsync(getData, cancellationToken);

    //     await using (var data = getData())
    //     {
    //         await data.SaveChangesAsync(cancellationToken);
    //         await data.Database.VacuumAsync(cancellationToken);
    //         forms = (await Task.WhenAll(BreedableIds
    //             .Select(id => data.PokemonForms.SingleAsync(
    //                 form =>
    //                     form.SpeciesId == id.SpeciesId &&
    //                     form.FormId == id.FormId,
    //                 cancellationToken))))
    //             .AsReadOnly();
    //     }

    //     if (forms.Count > 0) Logger?.LogWarning(
    //         $"No legality entries were added for these {forms.Count} " +
    //         $"`{nameof(HomeBalls.Entities.IHomeBallsPokemonForm)}`:\n\t" +
    //         $@"{String.Join("\n\t", forms
    //             .Select(form => $"{form.Id}\t{form.Identifier}"))}");

    //     return this;
    // }

    // protected internal virtual async Task<HomeBallsEntryLegalitySeeder> PopulateBreedableIdsAsync(
    //     Func<IHomeBallsBaseDataDbContext> getData,
    //     CancellationToken cancellationToken = default)
    // {
    //     var data = getData();
    //     var ids = (await data.PokemonForms.AsNoTracking()
    //         .Where(form => form.IsBreedable)
    //         .Select(form => form.Id)
    //         .ToListAsync())
    //         .OrderBy(id => id)
    //         .ToList().AsReadOnly();
    //     await data.DisposeAsync();

    //     BreedableIds.Clear();
    //     BreedableIds.AddRange(ids);
    //     return this;
    // }

    // protected internal virtual async Task<HomeBallsEntryLegalitySeeder> SeedKeysAsync(
    //     Func<IHomeBallsBaseDataDbContext> getData,
    //     IEnumerable<HomeBalls.Entities.HomeBallsPokemonFormKey> keys,
    //     Func<IHomeBallsEntryLegalityCollectionBuilder, IHomeBallsEntryLegalityCollectionBuilder> configuration,
    //     CancellationToken cancellationToken = default)
    // {
    //     await using var data = getData();

    //     foreach (var key in keys) if (BreedableIds.Contains(key))
    //     {
    //         var builder = Legalities.ForPokemon(key);
    //         var legalities = configuration(builder).BuildLegalities();
    //         BreedableIds.Remove(key);
    //         await data.Legalities.AddRangeAsync(legalities, cancellationToken);
    //     }
    //     else Logger?.LogWarning(
    //         $"`{nameof(HomeBallsPokemonForm)}` " +
    //         $"`{key}` is not breedable.");

    //     await data.SaveChangesAsync(cancellationToken);
    //     return this;
    // }

    // protected internal virtual Task<HomeBallsEntryLegalitySeeder> SeedPokeBallEntriesAsync(
    //     Func<IHomeBallsBaseDataDbContext> getData,
    //     CancellationToken cancellationToken = default)
    // {
    //     var keys = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (489, 1), (810, 1), (813, 1), (816, 1)
    //     };

    //     return SeedKeysAsync(
    //         getData,
    //         keys,
    //         builder => builder.ObtainableIn.PokeBall(),
    //         cancellationToken);
    // }

    // protected internal virtual Task<HomeBallsEntryLegalitySeeder> SeedSwshEntriesAsync(
    //     Func<IHomeBallsBaseDataDbContext> getData,
    //     CancellationToken cancellationToken = default)
    // {
    //     var gen1 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (001, 1), (004, 1), (007, 1), (010, 1), (027, 1), (027, 2),
    //         (029, 1), (032, 1), (037, 1), (037, 2), (041, 1), (043, 1),
    //         (050, 1), (050, 2), (052, 1), (052, 2), (052, 3), (054, 1),
    //         (058, 1), (060, 1), (063, 1), (066, 1), (072, 1), (077, 1),
    //         (077, 2), (079, 1), (079, 2), (081, 1), (083, 1), (083, 2),
    //         (090, 1), (092, 1), (095, 1), (098, 1), (102, 1), (104, 1),
    //         (108, 1), (109, 1), (111, 1), (113, 1), (114, 1), (115, 1),
    //         (116, 1), (118, 1), (120, 1), (122, 1), (122, 2), (123, 1),
    //         (127, 1), (128, 1), (129, 1), (131, 1), (133, 1), (137, 1),
    //         (138, 1), (140, 1), (142, 1), (143, 1), (147, 1)
    //     };
    //     var gen2 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (163, 1), (170, 1), (172, 1), (173, 1), (174, 1), (175, 1),
    //         (177, 1), (183, 1), (185, 1), (194, 1), (202, 1), (206, 1),
    //         (211, 1), (213, 1), (214, 1), (215, 1), (220, 1), (222, 1),
    //         (222, 2), (223, 1), (225, 1), (226, 1), (227, 1), (236, 1),
    //         (238, 1), (239, 1), (240, 1), (241, 1), (246, 1), 
    //     };
    //     var gen3 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (252, 1), (255, 1), (258, 1), (263, 1), (263, 2), (270, 1),
    //         (273, 1), (278, 1), (280, 1), (290, 1), (293, 1), (302, 1),
    //         (303, 1), (304, 1), (309, 1), (315, 1), (318, 1), (320, 1),
    //         (324, 1), (328, 1), (333, 1), (337, 1), (338, 1), (339, 1),
    //         (341, 1), (343, 1), (345, 1), (347, 1), (349, 1), (355, 1),
    //         (359, 1), (361, 1), (363, 1), (369, 1), (371, 1), (374, 1)
    //     };
    //     var gen4 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (403, 1), (415, 1), (420, 1), (422, 1), (422, 2), (425, 1),
    //         (427, 1), (434, 1), (436, 1), (442, 1), (443, 1), (447, 1),
    //         (449, 1), (451, 1), (453, 1), (459, 1), (479, 1), 
    //     };
    //     var gen5 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (506, 1), (509, 1), (517, 1), (519, 1), (524, 1), (527, 1),
    //         (529, 1), (531, 1), (532, 1), (535, 1), (538, 1), (539, 1),
    //         (543, 1), (546, 1), (548, 1), (550, 1), (550, 2), (551, 1),
    //         (554, 1), (554, 2), (556, 1), (557, 1), (559, 1), (561, 1),
    //         (562, 1), (562, 2), (564, 1), (566, 1), (568, 1), (570, 1),
    //         (572, 1), (574, 1), (577, 1), (582, 1), (587, 1), (588, 1),
    //         (590, 1), (592, 1), (595, 1), (597, 1), (599, 1), (605, 1),
    //         (607, 1), (610, 1), (613, 1), (615, 1), (616, 1), (618, 1),
    //         (618, 2), (619, 1), (621, 1), (622, 1), (624, 1), (626, 1),
    //         (627, 1), (629, 1), (631, 1), (632, 1), (633, 1), (636, 1),
    //     };
    //     var gen6 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (659, 1), (661, 1), (674, 1), (677, 1), (679, 1), (682, 1), 
    //         (684, 1), (686, 1), (688, 1), (690, 1), (692, 1), (694, 1), 
    //         (696, 1), (698, 1), (701, 1), (702, 1), (703, 1), (704, 1), 
    //         (707, 1), (708, 1), (710, 1), (710, 2), (710, 3), (710, 4), 
    //         (712, 1), (714, 1),
    //     };
    //     var gen7 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (722, 1), (725, 1), (728, 1), (736, 1), (742, 1), (744, 1), 
    //         (744, 2), (746, 1), (747, 1), (749, 1), (751, 1), (753, 1), 
    //         (755, 1), (757, 1), (759, 1), (761, 1), (764, 1), (765, 1), 
    //         (766, 1), (767, 1), (769, 1), (771, 1), (776, 1), (777, 1), 
    //         (778, 1), (780, 1), (781, 1), (782, 1),
    //     };
    //     var gen8 = new HomeBalls.Entities.HomeBallsPokemonFormKey[]
    //     {
    //         (819, 1), (821, 1), (824, 1), (827, 1), (829, 1), (831, 1), 
    //         (833, 1), (835, 1), (837, 1), (840, 1), (843, 1), (845, 1), 
    //         (846, 1), (848, 1), (850, 1), (852, 1), (854, 1), (856, 1), 
    //         (859, 1), (868, 1), (870, 1), (871, 1), (872, 1), (874, 1), 
    //         (875, 1), (876, 1), (876, 2), (877, 1), (878, 1), (884, 1), 
    //         (885, 1),
    //     };

    //     return SeedKeysAsync(
    //         getData,
    //         new[]
    //         {
    //             gen1, gen2, gen3, gen4, gen5,
    //             gen6, gen7, gen8,
    //         }.SelectMany(keys => keys).ToList().AsReadOnly(),
    //         builder => builder.ObtainableIn.Swsh(),
    //         cancellationToken);
    // }

    // protected internal virtual Task<HomeBallsEntryLegalitySeeder> SeedUsumEntriesAsync(
    //     Func<IHomeBallsBaseDataDbContext> getData,
    //     CancellationToken cancellationToken = default)
    // {
    //     // voltorb
    //     var gen1 = BreedableIds.Where(id =>
    //         id.SpeciesId.IsInBetweenOf(00, 028) ||
    //         id.SpeciesId.IsInBetweenOf(035, 042) ||
    //         id.SpeciesId.IsInBetweenOf(046, 047) ||
    //         id.SpeciesId.IsInBetweenOf(050, 076) ||
    //         id.SpeciesId.IsInBetweenOf(079, 082) ||
    //         id.SpeciesId.IsInBetweenOf(086, 095) ||
    //         id.SpeciesId.IsInBetweenOf(102, 105) ||
    //         id.SpeciesId == 108 ||
    //         id.SpeciesId.IsInBetweenOf(111, 113) ||
    //         id.SpeciesId.IsInBetweenOf(115, 136) ||
    //         id.SpeciesId.IsInBetweenOf(143, 149));
    //     var gen7 = BreedableIds.Where(id => id.SpeciesId > 721).ToArray();
    //     return Task.FromResult(this);
    // }

    async Task<IHomeBallsEntryLegalitySeeder> IHomeBallsEntryLegalitySeeder
        .SeedEntriesAsync(
            Func<IHomeBallsBaseDataDbContext> getData,
            CancellationToken cancellationToken) =>
        await SeedEntriesAsync(getData, cancellationToken);
}