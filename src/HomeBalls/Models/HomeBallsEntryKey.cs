namespace CEo.Pokemon.HomeBalls;

public record struct HomeBallsEntryKey :
    IEquatable<HomeBallsEntryKey>
{
    Int32 _hashCode;
    Boolean _isHashCodeSet;

    public HomeBallsEntryKey(UInt16 speciesId, Byte formId, UInt16 ballId)
    {
        (SpeciesId, FormId, BallId) = (speciesId, formId, ballId);
        (_hashCode, _isHashCodeSet) = (0, false);
    }

    public UInt16 SpeciesId { get; init; }

    public Byte FormId { get; init; }

    public UInt16 BallId { get; init; }

    public Boolean Equals(HomeBallsEntryKey? other) =>
        other.HasValue ? Equals(other.Value) : false;

    public Boolean Equals(HomeBallsEntryKey other) =>
        GetHashCode() == other.GetHashCode() &&
        SpeciesId == other.SpeciesId &&
        FormId == other.FormId &&
        BallId == other.BallId;

    public override Int32 GetHashCode()
    {
        if (!_isHashCodeSet)
        {
            _hashCode = (SpeciesId * 101 + FormId) * 1009 + BallId;
            _isHashCodeSet = true;
        }

        return _hashCode;
    }
}