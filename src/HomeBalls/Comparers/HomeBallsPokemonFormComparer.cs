namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsPokemonFormComparer :
    IComparer<IHomeBallsPokemonForm> { }

public class HomeBallsPokemonFormComparer :
    IHomeBallsPokemonFormComparer
{
    protected IReadOnlyList<Int32> PumpkabooIndices { get; } =
        new List<Int32> { 0, 2, 1, 3, 4 }.AsReadOnly();

    public virtual Int32 Compare(
        IHomeBallsPokemonForm? x,
        IHomeBallsPokemonForm? y)
    {
        if (areBothSpecies(710) || areBothSpecies(711))
        {
            Byte xId = x?.FormId ?? 0, yId = y?.FormId ?? 0;
            return PumpkabooIndices[xId].CompareTo(PumpkabooIndices[yId]);
        }

        var species = Nullable.Compare(x?.SpeciesId, y?.SpeciesId);
        return species == 0 ? Nullable.Compare(x?.FormId, y?.FormId) : species;

        Boolean areBothSpecies(UInt16 id) => x?.SpeciesId == id && y?.SpeciesId == id;
    }
}