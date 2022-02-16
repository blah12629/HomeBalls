namespace CEo.Pokemon.HomeBalls.Data.Entities;

public record HomeBallsEntry :
    HomeBalls.Entities.HomeBallsEntry,
    IHomeBallsDataType<HomeBalls.Entities.HomeBallsEntry>,
    HomeBalls.Entities.IIdentifiable
{
    public HomeBallsEntry() : base() { }

    public HomeBallsEntry(Boolean hasHiddenAbility) : base(hasHiddenAbility) { }

    public static Type BaseEntityType { get; } =
        typeof(HomeBalls.Entities.HomeBallsEntry);

    public virtual String Identifier => Id.ToString();

    public virtual HomeBalls.Entities.HomeBallsEntry ToBaseType() =>
        new HomeBalls.Entities.HomeBallsEntry(
            HasHiddenAbilityValue,
            AvailableEggMoveIdsCollection)
        {
            Id = Id,
            AddedOn = AddedOn,
            LastUpdatedOn = LastUpdatedOn
        };
}