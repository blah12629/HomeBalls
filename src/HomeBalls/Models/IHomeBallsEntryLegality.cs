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