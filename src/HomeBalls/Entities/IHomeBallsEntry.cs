namespace CEo.Pokemon.HomeBalls.Entities;

public interface IHomeBallsEntry :
    IHomeBallsEntity,
    IKeyed<HomeBallsEntryKey>
{
    IMutableNotifyingProperty<Boolean> HasHiddenAbility { get; }

    IHomeBallsObservableList<UInt16> AvailableEggMoveIds { get; }

    DateTime AddedOn { get; }

    DateTime LastUpdatedOn { get; }
}

public interface IHomeBallsEntryLegality :
    IHomeBallsEntity,
    IKeyed<HomeBallsEntryKey>,
    IIdentifiable
{
    Boolean IsObtainable { get; }

    Boolean IsObtainableWithHiddenAbility { get; }
}