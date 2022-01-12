namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsPokeballComparer :
    IComparer<IHomeBallsItem>
{
    IHomeBallsPokeballComparer UseDefaultComparison();

    IHomeBallsPokeballComparer UseGameIndexComparison();
}

public class HomeBallsPokeballComparer : IHomeBallsPokeballComparer
{
    public HomeBallsPokeballComparer()
    {
        DefaultComparer = new HomeBallsPokeballDefaultComparer();
        GameIndexComparer = new HomeBallsPokeballGameIndexComparer();
        CurrentComparer = GameIndexComparer;
    }

    protected internal IComparer<IHomeBallsItem> CurrentComparer { get; set; }

    protected internal IComparer<IHomeBallsItem> DefaultComparer { get; }

    protected internal IComparer<IHomeBallsItem> GameIndexComparer { get; }

    public virtual Int32 Compare(IHomeBallsItem? x, IHomeBallsItem? y) =>
        CurrentComparer.Compare(x, y);

    public virtual HomeBallsPokeballComparer UseDefaultComparison() =>
        UseComparison(DefaultComparer);

    public virtual HomeBallsPokeballComparer UseGameIndexComparison() =>
        UseComparison(GameIndexComparer);

    protected internal virtual HomeBallsPokeballComparer UseComparison(
        IComparer<IHomeBallsItem> comparer)
    {
        CurrentComparer = comparer;
        return this;
    }

    IHomeBallsPokeballComparer IHomeBallsPokeballComparer
        .UseDefaultComparison() =>
        UseDefaultComparison();

    IHomeBallsPokeballComparer IHomeBallsPokeballComparer
        .UseGameIndexComparison() =>
        UseGameIndexComparison();
}

public abstract class HomeBallsBasePokeballComparer : IComparer<IHomeBallsItem>
{
    IReadOnlyDictionary<UInt16, Int32>? _idIndeces;

    protected internal abstract IReadOnlyList<UInt16> BallsSortedId { get; }

    protected internal IReadOnlyDictionary<UInt16, Int32> IdIndeces =>
        _idIndeces ??= BallsSortedId
            .GroupBy(id => id)
            .Select(group =>
            {
                if (group.Count() > 1)
                    throw new ArgumentException(message: group.Key.ToString());

                return group.Key;
            })
            .Select((id, index) => (Id: id, Index: index))
            .ToDictionary(pair => pair.Id, pair => pair.Index).AsReadOnly();

    public virtual Int32 Compare(IHomeBallsItem? x, IHomeBallsItem? y)
    {
        UInt16 xId = x?.Id ?? 0, yId = y?.Id ?? 0;
        Int32 xIndex, yIndex;
        if (!IdIndeces.TryGetValue(xId, out xIndex)) xIndex = UInt16.MaxValue + 1;
        if (!IdIndeces.TryGetValue(yId, out yIndex)) yIndex = UInt16.MaxValue + 1;

        var comparison = xIndex.CompareTo(yIndex);
        return comparison == 0 ? xId.CompareTo(yId) : comparison;
    }
}

public class HomeBallsPokeballDefaultComparer : HomeBallsBasePokeballComparer
{
    protected internal override IReadOnlyList<UInt16> BallsSortedId =>
        Array.AsReadOnly(new UInt16[]
        {
            4, 3, 2, 1, 5, 16,
            7, 13, 14, 11, 8, 6, 12, 15, 9, 10,
            453, 454, 452, 450, 455, 449, 451,
            456, 457, 617, 887
        });
}

public class HomeBallsPokeballGameIndexComparer : HomeBallsBasePokeballComparer
{
    protected internal override IReadOnlyList<UInt16> BallsSortedId =>
        Array.AsReadOnly(new UInt16[]
        {
            4, 3, 2, 1, 12,
            14, 6, 8, 7, 13, 10, 15, 9, 11,
            453, 454, 449, 450, 452, 455, 451,
            16, 617, 457, 456, 5, 887
        });
}