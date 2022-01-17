namespace CEo.Pokemon.HomeBalls;

public record struct HomeBallsPokemonFormKey :
    IEquatable<HomeBallsPokemonFormKey>
{
    Int32 _hashCode;
    Boolean _isHashCodeSet;

    public HomeBallsPokemonFormKey(UInt16 speciesId, Byte formId)
    {
        (SpeciesId, FormId) = (speciesId, formId);
        (_hashCode, _isHashCodeSet) = (0, false);
    }

    public UInt16 SpeciesId { get; init; }

    public Byte FormId { get; init; }

    public Boolean Equals(HomeBallsPokemonFormKey? other) =>
        other.HasValue ? Equals(other.Value) : false;

    public Boolean Equals(HomeBallsPokemonFormKey other) =>
        GetHashCode() == other.GetHashCode() &&
        SpeciesId == other.SpeciesId &&
        FormId == other.FormId;

    public override Int32 GetHashCode()
    {
        if (!_isHashCodeSet)
        {
            _hashCode = SpeciesId * 101 + FormId;
            _isHashCodeSet = true;
        }

        return _hashCode;
    }
}