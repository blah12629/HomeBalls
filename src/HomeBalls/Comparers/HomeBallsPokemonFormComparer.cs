namespace CEo.Pokemon.HomeBalls;

public class HomeBallsPokemonFormComparer :
    IComparer<IHomeBallsPokemonForm>,
    IComparer<HomeBallsPokemonFormKey>,
    IComparer<HomeBallsPokemonFormKey?>
{
    protected IReadOnlyList<Int32> PumpkabooIndices { get; } =
        new List<Int32> { 0, 2, 1, 3, 4 }.AsReadOnly();

    public virtual Int32 Compare(
        IHomeBallsPokemonForm? x,
        IHomeBallsPokemonForm? y) =>
        Compare(x?.Id, y?.Id);

    public virtual Int32 Compare(
        HomeBallsPokemonFormKey x,
        HomeBallsPokemonFormKey y)
    {
        if (areBothSpecies(710) || areBothSpecies(711))
        {
            Byte xId = x.FormId, yId = y.FormId;
            return PumpkabooIndices[xId].CompareTo(PumpkabooIndices[yId]);
        }

        var species = x.SpeciesId.CompareTo(y.SpeciesId);
        return species == 0 ? x.FormId.CompareTo(y.FormId) : species;

        Boolean areBothSpecies(UInt16 id) => x.SpeciesId == id && y.SpeciesId == id;
    }

    public virtual Int32 Compare(
        HomeBallsPokemonFormKey? x,
        HomeBallsPokemonFormKey? y) =>
        Compare(x ?? new(0, 0), y ?? new(0, 0));
}