namespace CEo.Pokemon.HomeBalls.Comparers;

public interface IHomeBallsItemIdBaseComparer :
    IHomeBallsBaseStructComparer<UInt16> { }

public interface IHomeBallsItemIdComparer :
    IHomeBallsItemIdBaseComparer,
    IPredefinedInstance<IHomeBallsItemIdComparer>
{
    IHomeBallsItemIdComparer UseGameIndexComparer();

    IHomeBallsItemIdComparer UseAlphanumericComparer();
}

public abstract class HomeBallsItemIdBaseComparer :
    HomeBallsBaseStructComparer<UInt16>,
    IHomeBallsBaseStructComparer<UInt16>
{
    IReadOnlyDictionary<UInt16, Int32>? _idIndeces;

    protected internal abstract IReadOnlyList<UInt16> BallIdsSorted { get; }

    protected internal IReadOnlyDictionary<UInt16, Int32> IdIndeces =>
        _idIndeces ??= BallIdsSorted
            .GroupBy(id => id)
            .Select((group, index) => (Id: group.Key, Index: index))
            .ToDictionary(pair => pair.Id, pair => pair.Index).AsReadOnly();

    public override Int32 Compare(UInt16? x, UInt16? y)
    {
        Int32? xIndex = default(Int32?), yIndex = default(Int32?);
        if (x.HasValue && IdIndeces.TryGetValue(x.Value, out var i)) xIndex = i;
        if (y.HasValue && IdIndeces.TryGetValue(y.Value, out var j)) yIndex = j;

        var comparison = Nullable.Compare(xIndex, yIndex);
        return comparison == 0 ? Nullable.Compare(x, y) : comparison;
    }

    protected internal override Boolean EqualsCore(UInt16 x, UInt16 y) => x.Equals(y);

    public override Int32 GetHashCode([DisallowNull] UInt16 obj) => obj.GetHashCode();
}

public class HomeBallsItemIdComparer :
    HomeBallsBaseSwitchableStructComparer<IHomeBallsItemIdBaseComparer, UInt16>,
    IHomeBallsItemIdComparer,
    IPredefinedInstance<HomeBallsItemIdComparer>
{
    public static HomeBallsItemIdComparer Instance { get; } = new(
        HomeBallsItemIdGameIndexComparer.Instance,
        HomeBallsItemIdAlphanumericComparer.Instance);

    static IHomeBallsItemIdComparer IPredefinedInstance<IHomeBallsItemIdComparer>
        .Instance =>
        Instance;

    public HomeBallsItemIdComparer(
        IHomeBallsItemIdBaseComparer gameIndexComparer,
        IHomeBallsItemIdBaseComparer alphanumericComparer) :
        base(gameIndexComparer)
    {
        GameIndexComparer = gameIndexComparer;
        AlphanumericComparer = alphanumericComparer;
    }

    protected internal IHomeBallsItemIdBaseComparer GameIndexComparer { get; }

    protected internal IHomeBallsItemIdBaseComparer AlphanumericComparer { get; }

    protected internal override HomeBallsItemIdComparer UseComparer(
        IHomeBallsItemIdBaseComparer comparer)
    {
        base.UseComparer(comparer);
        return this;
    }

    public virtual IHomeBallsItemIdComparer UseGameIndexComparer() =>
        UseComparer(GameIndexComparer);

    public virtual IHomeBallsItemIdComparer UseAlphanumericComparer() =>
        UseComparer(AlphanumericComparer);
}

public class HomeBallsItemIdGameIndexComparer :
    HomeBallsItemIdBaseComparer,
    IHomeBallsItemIdBaseComparer,
    IPredefinedInstance<HomeBallsItemIdGameIndexComparer>
{
    IReadOnlyList<UInt16>? _ballIdsSorted;

    public static HomeBallsItemIdGameIndexComparer Instance { get; } = new();

    protected internal override IReadOnlyList<UInt16> BallIdsSorted =>
        _ballIdsSorted ??= Array.AsReadOnly(new UInt16[]
        {
            4, 3, 2, 1, 12,
            14, 6, 8, 7, 13, 10, 15, 9, 11,
            453, 454, 449, 450, 452, 455, 451,
            16, 617, 457, 456, 5, 887
        });
}

public class HomeBallsItemIdAlphanumericComparer :
    HomeBallsItemIdBaseComparer,
    IHomeBallsItemIdBaseComparer,
    IPredefinedInstance<HomeBallsItemIdAlphanumericComparer>
{
    IReadOnlyList<UInt16>? _ballIdsSorted;

    public static HomeBallsItemIdAlphanumericComparer Instance { get; } = new();

    protected internal override IReadOnlyList<UInt16> BallIdsSorted =>
        _ballIdsSorted ??= Array.AsReadOnly(new UInt16[]
        {
            4, 3, 2, 1, 5, 16,
            7, 13, 14, 11, 8, 6, 12, 15, 9, 10,
            453, 454, 452, 450, 455, 449, 451,
            456, 457, 617, 887
        });
}