namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

[ProtoContract]
public record ProtobufEntry :
    ProtobufRecord,
    IHomeBallsEntry
{
    HomeBallsEntryKey? _id;

    public virtual HomeBallsEntryKey Id => _id ??= new(SpeciesId, FormId, BallId);

    [ProtoMember(1)]
    public virtual UInt16 SpeciesId { get; init; }

    [ProtoMember(2)]
    public virtual Byte FormId { get; init; }

    [ProtoMember(3)]
    public virtual UInt16 BallId { get; init; }

    [ProtoMember(4)]
    public virtual Boolean HasHiddenAbility { get; set; }

    [ProtoMember(5)]
    public virtual ICollection<UInt16> AvailableEggMoveIds { get; init; } =
        new List<UInt16> { };

    [ProtoMember(6)]
    public virtual DateTime AddedOn { get; init; }

    [ProtoMember(7)]
    public virtual DateTime LastUpdatedOn { get; init; }

    public event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged { add { } remove { } }

    public virtual Boolean Equals(IHomeBallsEntry? other) =>
        SpeciesId == other?.SpeciesId &&
        FormId == other?.FormId &&
        BallId == other?.BallId;

    public virtual HomeBallsEntry ToHomeBallsEntry()
    {
        var entry = new HomeBallsEntry
        {
            SpeciesId = SpeciesId,
            FormId = FormId,
            BallId = BallId,
            HasHiddenAbility = HasHiddenAbility,
            AddedOn = AddedOn,
            LastUpdatedOn = LastUpdatedOn,
        };

        foreach (var id in AvailableEggMoveIds) entry.AvailableEggMoveIds.Add(id);
        return entry;
    }
}