namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IHomeBallsEntryLegalityCollectionBuilder :
    HomeBalls.Entities.IKeyed<HomeBalls.Entities.HomeBallsPokemonFormKey>
{
    IHomeBallsEntryLegalityCollectionBallsBuilder ObtainableIn { get; }

    IReadOnlyList<HomeBallsEntryLegality> BuildLegalities();
}

public interface IHomeBallsEntryLegalityCollectionBallsBuilder :
    IHomeBallsEntryLegalityCollectionBuilder
{
    IHomeBallsEntryLegalityCollectionBallsBuilder Usum(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder Swsh(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder Bdsp(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder Ball(
        UInt16 ballId,
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder ShopBalls(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder ApricornBalls(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder PokeBall(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder DreamBall(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder BeastBall(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder SafariBall(
        Boolean hasHiddenAbility = true);

    IHomeBallsEntryLegalityCollectionBallsBuilder SportBall(
        Boolean hasHiddenAbility = true);
}

public class HomeBallsEntryLegalityCollectionBuilder :
    IHomeBallsEntryLegalityCollectionBuilder,
    IHomeBallsEntryLegalityCollectionBallsBuilder
{
    public HomeBallsEntryLegalityCollectionBuilder(
        HomeBalls.Entities.HomeBallsPokemonFormKey key,
        IReadOnlyCollection<UInt16> shopBallIds,
        IReadOnlyCollection<UInt16> apricornBallIds,
        ILogger? logger = default)
    {
        Id = key;
        Legalities = new List<HomeBallsEntryLegality> { };
        (ShopBallIds, ApricornBallIds) = (shopBallIds, apricornBallIds);
    }

    protected internal ICollection<HomeBallsEntryLegality> Legalities { get; }

    protected internal IReadOnlyCollection<UInt16> ShopBallIds { get; }

    protected internal IReadOnlyCollection<UInt16> ApricornBallIds { get; }

    public virtual IHomeBallsEntryLegalityCollectionBallsBuilder ObtainableIn => this;

    public virtual HomeBalls.Entities.HomeBallsPokemonFormKey Id { get; }

    public virtual IReadOnlyList<HomeBallsEntryLegality> BuildLegalities() =>
        Legalities.GroupBy(legality => legality.Id)
            .Select(group => new HomeBallsEntryLegality
            {
                Id = group.Key,
                IsObtainable = true,
                IsObtainableWithHiddenAbility = group
                    .Any(legality => legality.IsObtainableWithHiddenAbility)
            })
            .ToList().AsReadOnly();

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .Usum(Boolean hasHiddenAbility) =>
        ObtainableIn.ShopBalls(hasHiddenAbility)
            .ApricornBalls(hasHiddenAbility)
            .BeastBall(hasHiddenAbility);

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .Swsh(Boolean hasHiddenAbility) =>
        ObtainableIn.ShopBalls(hasHiddenAbility)
            .ApricornBalls(hasHiddenAbility)
            .DreamBall(hasHiddenAbility)
            .SportBall(hasHiddenAbility)
            .SafariBall(hasHiddenAbility)
            .BeastBall(hasHiddenAbility);

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .Bdsp(Boolean hasHiddenAbility) =>
        ObtainableIn.ShopBalls(hasHiddenAbility)
            .ApricornBalls(hasHiddenAbility);

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .Ball(UInt16 ballId, Boolean hasHiddenAbility)
    {
        Legalities.Add(new HomeBallsEntryLegality
        {
            SpeciesId = Id.SpeciesId,
            FormId = Id.FormId,
            BallId = ballId,
            IsObtainable = true,
            IsObtainableWithHiddenAbility = hasHiddenAbility
        });
        return this;
    }

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .ShopBalls(Boolean hasHiddenAbility)
    {
        foreach (var id in ShopBallIds) ObtainableIn.Ball(id, hasHiddenAbility);
        return this;
    }

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .ApricornBalls(Boolean hasHiddenAbility)
    {
        foreach (var id in ApricornBallIds) ObtainableIn.Ball(id, hasHiddenAbility);
        return this;
    }

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .PokeBall(Boolean hasHiddenAbility) =>
        ObtainableIn.Ball(4, hasHiddenAbility);

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .DreamBall(Boolean hasHiddenAbility) =>
        ObtainableIn.Ball(617, hasHiddenAbility);

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .BeastBall(Boolean hasHiddenAbility) =>
        ObtainableIn.Ball(887, hasHiddenAbility);

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .SafariBall(Boolean hasHiddenAbility) =>
        ObtainableIn.Ball(5, hasHiddenAbility);

    IHomeBallsEntryLegalityCollectionBallsBuilder IHomeBallsEntryLegalityCollectionBallsBuilder
        .SportBall(Boolean hasHiddenAbility) =>
        ObtainableIn.Ball(457, hasHiddenAbility);
}

// public interface IHomeBallsEntryLegalityCollectionFactory
// {
//     IHomeBallsEntryLegalityCollectionFactory Pokemon(UInt16 speciesId, Byte formId);

//     IHomeBallsEntryLegalityCollectionFactory ObtainableInShopBalls(
//         Boolean withHiddenAbility = true);

//     IHomeBallsEntryLegalityCollectionFactory ObtainableInApricornBalls(
//         Boolean withHiddenAbility = true);

//     IHomeBallsEntryLegalityCollectionFactory ObtainableInDreamBall(
//         Boolean withHiddenAbility = true);

//     IHomeBallsEntryLegalityCollectionFactory ObtainableInBeastBall(
//         Boolean withHiddenAbility = true);

//     IHomeBallsEntryLegalityCollectionFactory ObtainableInSafariBall(
//         Boolean withHiddenAbility = true);

//     IHomeBallsEntryLegalityCollectionFactory ObtainableInSportBall(
//         Boolean withHiddenAbility = true);

//     IHomeBallsEntryLegalityCollectionFactory ObtainableInSwSh(
//         Boolean withHiddenAbility = true);

//     IHomeBallsEntryLegalityCollectionFactory ObtainableIn(
//         UInt16 ballId,
//         Boolean withHiddenAbility = true);

//     IReadOnlyCollection<EFCoreEntryLegality> CreateLegalities();
// }

// public class HomeBallsEntryLegalityCollectionFactory :
//     IHomeBallsEntryLegalityCollectionFactory
// {
//     IReadOnlyCollection<UInt16>? _apricornBallIds, _shopBallIds;

//     public HomeBallsEntryLegalityCollectionFactory(
//         IHomeBallsDataSource data,
//         ILogger? logger)
//     {
//         Data = data;
//         Logger = logger;
//         Legalities = new List<(UInt16, Boolean)> { };
//     }

//     protected internal IHomeBallsDataSource Data { get; }

//     protected internal ILogger? Logger { get; }

//     protected internal UInt16? SpeciesId { get; set; }

//     protected internal Byte? FormId { get; set; }

//     protected internal ICollection<(UInt16 BallId, Boolean HasHiddenAbility)> Legalities { get; }

//     protected internal IReadOnlyCollection<UInt16> ApricornBallIds =>
//         _apricornBallIds ??= Data.Items
//             .Where(item => item.CategoryId == 39)
//             .Select(item => item.Id)
//             .ToList().AsReadOnly();

//     protected internal IReadOnlyCollection<UInt16> ShopBallIds =>
//         _shopBallIds ??= new List<UInt16>
//         {
//             1, 2, 3, 4,
//             6, 7, 8, 9, 10, 11, 12, 13, 14, 15
//         }.AsReadOnly();

//     public virtual IReadOnlyCollection<EFCoreEntryLegality> CreateLegalities()
//     {
//         var legalities = Legalities
//             .GroupBy(entry => entry.BallId)
//             .Select(CreateLegality)
//             .ToList().AsReadOnly();

//         InitializeValues();
//         return legalities;
//     }

//     protected internal virtual EFCoreEntryLegality CreateLegality(
//         IGrouping<UInt16, (UInt16 BallId, Boolean HasHiddenAbility)> group) =>
//         new EFCoreEntryLegality
//         {
//             SpeciesId = SpeciesId ?? throw new ArgumentNullException(),
//             FormId = FormId ?? throw new ArgumentNullException(),
//             BallId = group.Key,
//             IsObtainable = true,
//             IsObtainableWithHiddenAbility = group.Last().HasHiddenAbility
//         };

//     protected internal virtual void InitializeValues()
//     {
//         (SpeciesId, FormId) = (default(UInt16?), default(Byte?));

//         Legalities.Clear();
//         Legalities.Add((4, false));
//     }

//     public virtual HomeBallsEntryLegalityCollectionFactory ObtainableIn(
//         UInt16 ballId,
//         Boolean withHiddenAbility = true)
//     {
//         Legalities.Add((ballId, withHiddenAbility));
//         return this;
//     }

//     protected internal virtual HomeBallsEntryLegalityCollectionFactory ObtainableIn(
//         IEnumerable<UInt16> ballIds,
//         Boolean withHiddenAbility)
//     {
//         foreach (var id in ballIds) ObtainableIn(id, withHiddenAbility);
//         return this;
//     }

//     public virtual HomeBallsEntryLegalityCollectionFactory ObtainableInApricornBalls(
//         Boolean withHiddenAbility = true) =>
//         ObtainableIn(ApricornBallIds, withHiddenAbility);

//     public virtual HomeBallsEntryLegalityCollectionFactory ObtainableInBeastBall(
//         Boolean withHiddenAbility = true) =>
//         ObtainableIn(887, withHiddenAbility);

//     public virtual HomeBallsEntryLegalityCollectionFactory ObtainableInDreamBall(
//         Boolean withHiddenAbility = true) =>
//         ObtainableIn(617, withHiddenAbility);

//     public virtual HomeBallsEntryLegalityCollectionFactory ObtainableInSafariBall(
//         Boolean withHiddenAbility = true) =>
//         ObtainableIn(5, withHiddenAbility);

//     public virtual HomeBallsEntryLegalityCollectionFactory ObtainableInShopBalls(
//         Boolean withHiddenAbility = true) =>
//         ObtainableIn(ShopBallIds, withHiddenAbility);

//     public virtual HomeBallsEntryLegalityCollectionFactory ObtainableInSportBall(
//         Boolean withHiddenAbility = true) =>
//         ObtainableIn(457, withHiddenAbility);

//     public virtual HomeBallsEntryLegalityCollectionFactory ObtainableInSwSh(
//         Boolean withHiddenAbility = true) =>
//         ObtainableInShopBalls(withHiddenAbility)
//             .ObtainableInApricornBalls(withHiddenAbility)
//             .ObtainableInDreamBall(withHiddenAbility)
//             .ObtainableInBeastBall(withHiddenAbility)
//             .ObtainableInSafariBall(withHiddenAbility)
//             .ObtainableInSportBall(withHiddenAbility);

//     public virtual HomeBallsEntryLegalityCollectionFactory Pokemon(
//         UInt16 speciesId,
//         Byte formId)
//     {
//         (SpeciesId, FormId) = (speciesId, formId);
//         return this;
//     }

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .Pokemon(UInt16 speciesId, Byte formId) =>
//         Pokemon(speciesId, formId);

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .ObtainableInShopBalls(Boolean withHiddenAbility) =>
//         ObtainableInShopBalls(withHiddenAbility);

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .ObtainableInApricornBalls(Boolean withHiddenAbility) =>
//         ObtainableInApricornBalls(withHiddenAbility);

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .ObtainableInDreamBall(Boolean withHiddenAbility) =>
//         ObtainableInDreamBall(withHiddenAbility);

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .ObtainableInBeastBall(Boolean withHiddenAbility) =>
//         ObtainableInBeastBall(withHiddenAbility);

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .ObtainableInSafariBall(Boolean withHiddenAbility) =>
//         ObtainableInSafariBall(withHiddenAbility);

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .ObtainableInSportBall(Boolean withHiddenAbility) =>
//         ObtainableInSportBall(withHiddenAbility);

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .ObtainableInSwSh(Boolean withHiddenAbility) =>
//         ObtainableInSwSh(withHiddenAbility);

//     IHomeBallsEntryLegalityCollectionFactory IHomeBallsEntryLegalityCollectionFactory
//         .ObtainableIn(UInt16 ballId, Boolean withHiddenAbility) =>
//         ObtainableIn(ballId, withHiddenAbility);
// }
