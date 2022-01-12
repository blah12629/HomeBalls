namespace CEo.Pokemon.HomeBalls;

public class NumberEnumerableEqualityComparerTests
{
    protected NumberEnumerableEqualityComparer<UInt16> Sut { get; } = new();

    [Fact]
    public void Equals_ShouldBeTrue_WhenUnsortedListsHaveSameValues() =>
        Sut.Equals(new UInt16[] { 12, 62, 9 }, new UInt16[] { 62, 9, 12 })
            .Should().BeTrue();
}