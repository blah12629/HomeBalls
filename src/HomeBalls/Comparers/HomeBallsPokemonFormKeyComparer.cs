namespace CEo.Pokemon.HomeBalls.Comparers;

public interface IHomeBallsPokemonFormKeyBaseComparer :
    IHomeBallsBaseClassComparer<HomeBallsPokemonFormKey> { }

public interface IHomeBallsPokemonFormKeyComparer :
    IHomeBallsPokemonFormKeyBaseComparer,
    IPredefinedInstance<IHomeBallsPokemonFormKeyComparer>
{
    IHomeBallsPokemonFormKeyComparer UseDefaultComparer();

    IHomeBallsPokemonFormKeyComparer UseAlphanumericComparer();
}

public abstract class HomeBallsPokemonFormKeyBaseComparer :
    HomeBallsBaseClassComparer<HomeBallsPokemonFormKey>,
    IHomeBallsBaseClassComparer<HomeBallsPokemonFormKey>
{
    protected internal override Boolean EqualsCore(
        HomeBallsPokemonFormKey x,
        HomeBallsPokemonFormKey y) =>
        x.SpeciesId.Equals(y.SpeciesId) &&
        x.FormId.Equals(y.FormId);

    protected internal override Int32 GetHashCodeCore(
        [DisallowNull] HomeBallsPokemonFormKey obj) =>
        obj.SpeciesId * 101 + obj.FormId;
}

public class HomeBallsPokemonFormKeyComparer :
    HomeBallsBaseSwitchableClassComparer<IHomeBallsPokemonFormKeyBaseComparer, HomeBallsPokemonFormKey>,
    IHomeBallsPokemonFormKeyComparer,
    IPredefinedInstance<HomeBallsPokemonFormKeyComparer>
{
    public static HomeBallsPokemonFormKeyComparer Instance { get; } =
        new(
            HomeBallsPokemonFormKeyDefaultComparer.Instance,
            HomeBallsPokemonFormKeyAlphanumericComparer.Instance);

    static IHomeBallsPokemonFormKeyComparer IPredefinedInstance<IHomeBallsPokemonFormKeyComparer>
        .Instance =>
        HomeBallsPokemonFormKeyComparer.Instance;

    public HomeBallsPokemonFormKeyComparer(
        IHomeBallsPokemonFormKeyBaseComparer defaultComparer,
        IHomeBallsPokemonFormKeyBaseComparer alphanumericComparer) :
        base(defaultComparer)
    {
        DefualtComparer = defaultComparer;
        AlphanumericComparer = alphanumericComparer;
    }

    protected internal IHomeBallsPokemonFormKeyBaseComparer DefualtComparer { get; }

    protected internal IHomeBallsPokemonFormKeyBaseComparer AlphanumericComparer { get; }

    public virtual HomeBallsPokemonFormKeyComparer UseAlphanumericComparer() =>
        UseComparer(AlphanumericComparer);

    public virtual HomeBallsPokemonFormKeyComparer UseDefaultComparer() =>
        UseComparer(DefualtComparer);

    protected internal override HomeBallsPokemonFormKeyComparer UseComparer(
        IHomeBallsPokemonFormKeyBaseComparer comparer)
    {
        base.UseComparer(comparer);
        return this;
    }

    IHomeBallsPokemonFormKeyComparer IHomeBallsPokemonFormKeyComparer
        .UseDefaultComparer() =>
        UseDefaultComparer();

    IHomeBallsPokemonFormKeyComparer IHomeBallsPokemonFormKeyComparer
        .UseAlphanumericComparer() =>
        UseAlphanumericComparer();
}

public class HomeBallsPokemonFormKeyDefaultComparer :
    HomeBallsPokemonFormKeyBaseComparer,
    IHomeBallsPokemonFormKeyBaseComparer,
    IPredefinedInstance<HomeBallsPokemonFormKeyDefaultComparer>
{
    public static HomeBallsPokemonFormKeyDefaultComparer Instance { get; } =
        new(HomeBallsPokemonFormKeyAlphanumericComparer.Instance);

    public HomeBallsPokemonFormKeyDefaultComparer(
        IHomeBallsPokemonFormKeyBaseComparer alphanumericComparer) =>
        AlphanumericComparer = alphanumericComparer;

    protected internal IHomeBallsPokemonFormKeyBaseComparer AlphanumericComparer { get; }

    protected internal IReadOnlyList<Int32> PumpkabooIndices { get; } =
        new List<Int32> { 0, 2, 1, 3, 4 }.AsReadOnly();

    protected internal virtual Boolean AreBothSpecies(
        HomeBallsPokemonFormKey x,
        HomeBallsPokemonFormKey y,
        UInt16 speciesId) =>
        x.SpeciesId.Equals(speciesId) && y.SpeciesId.Equals(speciesId);

    protected internal override Int32 CompareCore(
        HomeBallsPokemonFormKey x,
        HomeBallsPokemonFormKey y)
    {
        if (AreBothSpecies(x, y, 710) || AreBothSpecies(x, y, 711))
            return ComparePumpkabooForms(x.FormId, y.FormId);

        return AlphanumericComparer.Compare(x, y);
    }

    protected internal virtual Int32 ComparePumpkabooForms(Byte x, Byte y) =>
        PumpkabooIndices[x].CompareTo(PumpkabooIndices[y]);
}

public class HomeBallsPokemonFormKeyAlphanumericComparer :
    HomeBallsPokemonFormKeyBaseComparer,
    IHomeBallsPokemonFormKeyBaseComparer,
    IPredefinedInstance<HomeBallsPokemonFormKeyAlphanumericComparer>
{
    public static HomeBallsPokemonFormKeyAlphanumericComparer Instance { get; } = new();

    protected internal override Int32 CompareCore(
        HomeBallsPokemonFormKey x,
        HomeBallsPokemonFormKey y)
    {
        var species = x.SpeciesId.CompareTo(y.SpeciesId);
        return species == 0 ? x.FormId.CompareTo(y.FormId) : species;
    }
}