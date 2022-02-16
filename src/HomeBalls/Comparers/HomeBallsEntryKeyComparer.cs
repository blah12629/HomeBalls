namespace CEo.Pokemon.HomeBalls.Comparers;

public interface IHomeBallsEntryKeyBaseComparer :
    IHomeBallsBaseClassComparer<HomeBallsEntryKey> { }

public interface IHomeBallsEntryKeyComparer :
    IHomeBallsEntryKeyBaseComparer,
    IPredefinedInstance<IHomeBallsEntryKeyComparer>
{
    IHomeBallsEntryKeyComparer UseDefaultComparer();

    IHomeBallsEntryKeyComparer UseAlphanumericComparer();
}

public abstract class HomeBallsEntryKeyBaseComparer :
    HomeBallsBaseClassComparer<HomeBallsEntryKey>,
    IHomeBallsBaseClassComparer<HomeBallsEntryKey>
{
    protected HomeBallsEntryKeyBaseComparer(
        IHomeBallsPokemonFormKeyBaseComparer pokemonComparer,
        IHomeBallsItemIdBaseComparer itemComparer) =>
        (PokemonComparer, ItemComparer) = (pokemonComparer, itemComparer);

    protected internal IHomeBallsPokemonFormKeyBaseComparer PokemonComparer { get; }

    protected internal IHomeBallsItemIdBaseComparer ItemComparer { get; }

    protected internal override Int32 CompareCore(
        HomeBallsEntryKey x,
        HomeBallsEntryKey y)
    {
        var pokemon = PokemonComparer.Compare(x, y);
        return pokemon == 0 ? ItemComparer.Compare(x.BallId, y.BallId) : pokemon;
    }

    protected internal override Boolean EqualsCore(
        HomeBallsEntryKey x,
        HomeBallsEntryKey y) =>
        PokemonComparer.Equals(x, y) &&
        ItemComparer.Equals(x.BallId, y.BallId);

    protected internal override Int32 GetHashCodeCore(
        [DisallowNull] HomeBallsEntryKey obj) =>
        PokemonComparer.GetHashCode(obj) * 1009 + obj.BallId;
}

public class HomeBallsEntryKeyComparer :
    HomeBallsBaseSwitchableClassComparer<IHomeBallsEntryKeyBaseComparer, HomeBallsEntryKey>,
    IHomeBallsEntryKeyComparer,
    IPredefinedInstance<HomeBallsEntryKeyComparer>
{
    public static HomeBallsEntryKeyComparer Instance { get; } =
        new HomeBallsEntryKeyComparer(
            HomeBallsEntryKeyDefaultComparer.Instance,
            HomeBallsEntryKeyAlphanumericComparer.Instance);

    static IHomeBallsEntryKeyComparer IPredefinedInstance<IHomeBallsEntryKeyComparer>
        .Instance =>
        HomeBallsEntryKeyComparer.Instance;

    public HomeBallsEntryKeyComparer(
        IHomeBallsEntryKeyBaseComparer defaultComparer,
        IHomeBallsEntryKeyBaseComparer alphanumericComparer) :
        base(defaultComparer)
    {
        DefaultComparer = defaultComparer;
        AlphanumericComparer = alphanumericComparer;
    }

    protected internal IHomeBallsEntryKeyBaseComparer DefaultComparer { get; }

    protected internal IHomeBallsEntryKeyBaseComparer AlphanumericComparer { get; }

    public virtual HomeBallsEntryKeyComparer UseAlphanumericComparer() =>
        UseComparer(AlphanumericComparer);

    public virtual HomeBallsEntryKeyComparer UseDefaultComparer() =>
        UseComparer(DefaultComparer);

    protected internal override HomeBallsEntryKeyComparer UseComparer(
        IHomeBallsEntryKeyBaseComparer comparer)
    {
        base.UseComparer(comparer);
        return this;
    }

    IHomeBallsEntryKeyComparer IHomeBallsEntryKeyComparer
        .UseDefaultComparer() =>
        UseDefaultComparer();

    IHomeBallsEntryKeyComparer IHomeBallsEntryKeyComparer
        .UseAlphanumericComparer() =>
        UseAlphanumericComparer();
}

public class HomeBallsEntryKeyDefaultComparer :
    HomeBallsEntryKeyBaseComparer,
    IHomeBallsEntryKeyBaseComparer,
    IPredefinedInstance<HomeBallsEntryKeyDefaultComparer>
{
    public static HomeBallsEntryKeyDefaultComparer Instance { get; } = new();

    public HomeBallsEntryKeyDefaultComparer() : base(
        HomeBallsPokemonFormKeyDefaultComparer.Instance,
        HomeBallsItemIdGameIndexComparer.Instance) { }
}

public class HomeBallsEntryKeyAlphanumericComparer :
    HomeBallsEntryKeyBaseComparer,
    IHomeBallsEntryKeyBaseComparer,
    IPredefinedInstance<HomeBallsEntryKeyAlphanumericComparer>
{
    public static HomeBallsEntryKeyAlphanumericComparer Instance { get; } = new();

    public HomeBallsEntryKeyAlphanumericComparer() :  base(
        HomeBallsPokemonFormKeyAlphanumericComparer.Instance,
        HomeBallsItemIdAlphanumericComparer.Instance) { }
}