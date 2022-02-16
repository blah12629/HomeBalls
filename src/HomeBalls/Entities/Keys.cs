namespace CEo.Pokemon.HomeBalls.Entities;

[ProtoContract]
public record HomeBallsEntryKey :
    IEquatable<HomeBallsEntryKey>,
    IComparable<HomeBallsEntryKey>
{
    public HomeBallsEntryKey() { }

    public HomeBallsEntryKey(UInt16 speciesId, Byte formId, UInt16 ballId) : this() =>
        (SpeciesId, FormId, BallId) = (speciesId, formId, ballId);

    [ProtoMember(1)]
    public UInt16 SpeciesId { get; init; }

    [ProtoMember(2)]
    public Byte FormId { get; init; }

    [ProtoMember(3)]
    public UInt16 BallId { get; init; }

    public virtual Int32 CompareTo(HomeBallsEntryKey? other) =>
        HomeBallsEntryKeyComparer.Instance.Compare(this, other);

    public override Int32 GetHashCode() =>
        HomeBallsEntryKeyComparer.Instance.GetHashCode(this);

    public virtual Boolean Equals(HomeBallsEntryKey? other) =>
        HomeBallsEntryKeyComparer.Instance.Equals(this, other);

    public virtual void Deconstruct(
        out UInt16 speciesId,
        out Byte formId,
        out UInt16 ballId) =>
        (speciesId, formId, ballId) = (SpeciesId, FormId, BallId);

    public static implicit operator HomeBallsEntryKey(
        (UInt16 SpeciesId, Byte FormId, UInt16 BallId) key) =>
        new(key.SpeciesId, key.FormId, key.BallId);

    public static implicit operator HomeBallsPokemonFormKey(
        HomeBallsEntryKey key) =>
        new(key.SpeciesId, key.FormId);
}

[ProtoContract]
public record HomeBallsPokemonFormKey :
    IEquatable<HomeBallsPokemonFormKey>,
    IComparable<HomeBallsPokemonFormKey>
{
    public HomeBallsPokemonFormKey() { }

    public HomeBallsPokemonFormKey(UInt16 speciesId, Byte formId) : this() =>
        (SpeciesId, FormId) = (speciesId, formId);

    [ProtoMember(1)]
    public UInt16 SpeciesId { get; init; }

    [ProtoMember(2)]
    public Byte FormId { get; init; }

    public virtual Int32 CompareTo(HomeBallsPokemonFormKey? other) =>
        HomeBallsPokemonFormKeyComparer.Instance.Compare(this, other);

    public override Int32 GetHashCode() =>
        HomeBallsPokemonFormKeyComparer.Instance.GetHashCode(this);

    public virtual Boolean Equals(HomeBallsPokemonFormKey? other) =>
        HomeBallsPokemonFormKeyComparer.Instance.Equals(this, other);

    public virtual void Deconstruct(
        out UInt16 speciesId,
        out Byte formId) =>
        (speciesId, formId) = (SpeciesId, FormId);

    public static implicit operator HomeBallsPokemonFormKey(
        (UInt16 SpeciesId, Byte FormId) key) =>
        new(key.SpeciesId, key.FormId);
}