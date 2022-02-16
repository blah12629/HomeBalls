namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IHomeBallsEntryCollectionInitializer
{
    Task<ICollection<HomeBallsEntry>> InitializeAsync(
        Func<IHomeBallsBaseDataDbContext> getData,
        CancellationToken cancellationToken = default);

    Task<ICollection<HomeBallsEntry>> InitializeAsync(
        IHomeBallsReadOnlyDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> breedables,
        CancellationToken cancellationToken = default);
}

public class HomeBallsEntryCollectionInitializer :
    IHomeBallsEntryCollectionInitializer
{
    UInt16[]? _rareBallIds;

    public HomeBallsEntryCollectionInitializer(
        ILogger? logger = default) =>
        Logger = logger;

    protected internal ILogger? Logger { get; }

    protected internal IReadOnlyCollection<UInt16> SwshBreedables { get; } = new UInt16[]
    {
        001, 004, 007, 010, 027, 029, 032, 037, 041, 043, 050, 052, 054, 058, 060, 063, 066, 072, 077, 079, 081, 083, 090, 092, 095, 098, 102, 104, 108, 109, 111, 113, 114, 115, 116, 118, 120, 122, 123, 127, 128, 129, 131, 133, 137, 138, 140, 142, 143, 147,
        163, 170, 172, 173, 174, 175, 177, 183, 185, 194, 202, 206, 211, 213, 214, 215, 220, 222, 223, 225, 226, 227, 236, 238, 239, 240, 241, 246,
        252, 255, 258, 263, 270, 273, 278, 280, 290, 293, 302, 303, 304, 309, 315, 318, 320, 324, 328, 333, 337, 338, 339, 341, 343, 345, 347, 349, 355, 359, 361, 363, 369, 371, 374,
        403, 415, 420, 422, 425, 427, 434, 436, 442, 443, 447, 449, 451, 453, 459, 479, 
        506, 509, 517, 519, 524, 527, 529, 531, 532, 535, 538, 539, 543, 546, 548, 550, 551, 554, 556, 557, 559, 561, 562, 564, 566, 568, 570, 572, 574, 577, 582, 587, 588, 590, 592, 595, 597, 599, 605, 607, 610, 613, 615, 616, 618, 619, 621, 622, 624, 626, 627, 629, 631, 632, 633, 636,
        659, 661, 674, 677, 679, 682, 684, 686, 688, 690, 692, 694, 696, 698, 701, 702, 703, 704, 707, 708, 710, 712, 714,
        722, 725, 728, 736, 742, 744, 746, 747, 749, 751, 753, 755, 757, 759, 761, 764, 765, 766, 767, 769, 771, 776, 777, 778, 780, 781, 782,
        819, 821, 824, 827, 829, 831, 833, 835, 837, 840, 843, 845, 846, 848, 850, 852, 854, 856, 859, 868, 870, 871, 872, 874, 875, 876, 877, 878, 884, 885
    }.AsReadOnly();

    protected internal IReadOnlyCollection<UInt16> BdspBreedables { get; } = new UInt16[]
    {
        013, 016, 019, 021, 023, 046, 048, 056, 069, 074, 084, 086, 088, 096, 100,
        152, 155, 158, 161, 165, 167, 179, 187, 190, 191, 193, 198, 200, 203, 204, 207, 209, 216, 218, 228, 231, 234, 235,
        261, 265, 276, 283, 285, 287, 296, 299, 300, 307, 311, 312, 313, 314, 316, 322, 325, 327, 331, 335, 336, 351, 352, 353, 357, 358, 366, 370,
        387, 390, 393, 396, 399, 401, 408, 410, 412, 417, 418, 431, 441, 455, 456
    }.AsReadOnly();

    protected internal IReadOnlyCollection<UInt16> BdspSafariBreedables { get; } = new UInt16[]
    {
        013, 016, 019, 021, 023, 046, 048, 056, 069, 074, 084, 086, 088, 096, 100,
        152, 155, 158, 161, 165, 167, 179, 187, 190, 191, 193, 198, 200, 204, 207, 209, 216, 218, 228, 231,
        261, 265, 276, 283, 285, 287, 296, 299, 300, 307, 316, 322, 325, 331, 353, 358,
        387, 390, 393, 396, 399, 401, 408, 410, 412, 418, 431, 455, 456
    }.AsReadOnly();

    protected internal UInt16[] ApriballIds { get; } =
        new UInt16[] { 453, 454, 449, 450, 452, 455, 451 };

    protected internal UInt16[] RareBallIds => _rareBallIds ??=
        ApriballIds.Concat(new UInt16[] { 617, 457, 5, 887 }).ToArray();

    public virtual async Task<ICollection<HomeBallsEntry>> InitializeAsync(
        Func<IHomeBallsBaseDataDbContext> getData,
        CancellationToken cancellationToken = default)
    {
        var data = getData();
        var breedables = await data.PokemonForms
            .AsNoTracking()
            .Where(form => form.IsBreedable)
            .Include(form => form.Abilities)
            .ToListAsync(cancellationToken);
        await data.DisposeAsync();

        return await InitializeAsync(
            new HomeBallsDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> { }
                .AddRange(breedables).AsReadOnly(),
            cancellationToken);
    }

    public virtual Task<ICollection<HomeBallsEntry>> InitializeAsync(
        IHomeBallsReadOnlyDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> breedables,
        CancellationToken cancellationToken = default)
    {
        var entries = new List<HomeBallsEntry>();
        var addedOn = DateTime.Now;

        foreach (var entry in breedables
            .Where(form => SwshBreedables.Contains(form.Id.SpeciesId))
            .Select(form => CreateEntry(form, addedOn))
            .SelectMany(entry => AddBallIds(entry, RareBallIds)))
            entries.Add(entry);

        foreach (var entry in BdspBreedables
            .Select(id => CreateEntry(breedables, id, addedOn))
            .SelectMany(entry => AddBallIds(entry, ApriballIds)))
            entries.Add(entry);

        foreach (var entry in BdspSafariBreedables
            .Select(id => CreateEntry(breedables, id, addedOn))
            .SelectMany(entry => AddBallIds(entry, 5)))
            entries.Add(entry);

        return Task.FromResult(SortEntryCollection(entries));
    }

    protected internal virtual HomeBallsEntry CreateEntry(
        IHomeBallsReadOnlyDataSet<HomeBalls.Entities.HomeBallsPokemonFormKey, HomeBallsPokemonForm> breedables,
        UInt16 speciesId,
        DateTime addedOn) =>
        CreateEntry(breedables[(speciesId, 1)], addedOn);

    protected internal virtual HomeBallsEntry CreateEntry(
        HomeBallsPokemonForm form,
        DateTime addedOn) =>
        new HomeBallsEntry(form.Abilities.Any(ability => ability.IsHidden))
        {
            Id = (form.Id.SpeciesId, form.Id.FormId, 0),
            AddedOn = addedOn
        };

    protected internal virtual IEnumerable<HomeBallsEntry> AddBallIds(
        HomeBallsEntry entry,
        params UInt16[] ballIds) =>
        ballIds.Select(id => entry with { Id = entry.Id with { BallId = id } });

    protected internal virtual ICollection<HomeBallsEntry> SortEntryCollection(
        IEnumerable<HomeBallsEntry> entries) =>
        entries.OrderBy(entry => entry.Id).ToList();
}