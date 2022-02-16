namespace CEo.Pokemon.HomeBalls.Comparers.Tests;

public class HomeBallsItemIdComparerTests<TComparer>
    where TComparer : notnull, IHomeBallsItemIdBaseComparer, new()
{
    protected TComparer Sut { get; } = new();

    protected virtual void CompareTo_ShouldBeSortedAs(
        IReadOnlyList<UInt16> sortedIds)
    {
        var ids = new UInt16[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            449, 450, 451, 452, 453, 454, 455, 456, 457,
            617, 887
        };

        var sorted = ids.OrderBy(id => id, Sut).ToList().AsReadOnly();
        sorted.Count.Should().Be(sortedIds.Count);
        for (var i = 0; i < sorted.Count; i ++) sorted[i].Should().Be(sortedIds[i]);
    }
}

public class HomeBallsItemIdAlphanumericComparerTests :
    HomeBallsItemIdComparerTests<HomeBallsItemIdAlphanumericComparer>
{
    [Fact]
    public void CompareTo_ShouldBeSorted() => CompareTo_ShouldBeSortedAs(new UInt16[]
    {
        4, 3, 2, 1, 5, 16,
        7, 13, 14, 11, 8, 6, 12, 15, 9, 10,
        453, 454, 452, 450, 455, 449, 451,
        456, 457, 617, 887
    });
}

public class HomeBallsItemIdGameIndexComparerTests :
    HomeBallsItemIdComparerTests<HomeBallsItemIdGameIndexComparer>
{
    [Fact]
    public void CompareTo_ShouldBeSorted() => CompareTo_ShouldBeSortedAs(new UInt16[]
    {
        4, 3, 2, 1, 12,
        14, 6, 8, 7, 13, 10, 15, 9, 11,
        453, 454, 449, 450, 452, 455, 451,
        16, 617, 457, 456, 5, 887
    });
}