namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsEntryLegality :
    IHomeBallsEntity,
    IKeyed<HomeBallsEntryKey>,
    IIdentifiable
{
    UInt16 SpeciesId { get; }

    Byte FormId { get; }

    UInt16 BallId { get; }

    Boolean IsObtainable { get; }

    Boolean IsObtainableWithHiddenAbility { get; }
}

public interface IHomeBallsEntryLegalityCollection :
    IReadOnlyCollection<IHomeBallsEntryLegality>
{
    Boolean IsLegal(UInt16 SpeciesId, Byte FormId, UInt16 BallId);

    Boolean IsObtainableWithHiddenAbility(UInt16 SpeciesId, Byte FormId, UInt16 BallId);
}

public interface IHomeBallsEntryLegalityManager :
    IHomeBallsEntryLegalityCollection
{
    IHomeBallsEntryLegalityManager AddBallLegality(
        UInt16 SpeciesId,
        Byte FormId,
        UInt16 BallId);

    IHomeBallsEntryLegalityManager AddBallLegalities(
        IEnumerable<IHomeBallsEntryLegality> legalities);

    IHomeBallsEntryLegalityManager AddApricornBallLegality(
        UInt16 SpeciesId,
        Byte FormId);
}