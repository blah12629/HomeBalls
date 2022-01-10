namespace CEo.Pokemon.HomeBalls;

public interface INumberEnumerableEqualityComparer<TNumber> :
    IEqualityComparer,
    IEqualityComparer<IEnumerable<TNumber>>
    where TNumber : notnull, INumber<TNumber> { }

public class NumberEnumerableEqualityComparer<TNumber> :
    INumberEnumerableEqualityComparer<TNumber>
    where TNumber : notnull, INumber<TNumber>
{
    public virtual Boolean Equals(
        IEnumerable<TNumber>? x,
        IEnumerable<TNumber>? y)
    {
        if (x == default) return y == default;
        if (y == default) return false;

        Int32 xCode = GetHashCode(x), yCode = GetHashCode(y);
        if (xCode == yCode) return true;

        return x.OrderBy(i => i).SequenceEqual(y.OrderBy(i => i));
    }

    new public virtual Boolean Equals(Object? x, Object? y) =>
        Equals(x as IEnumerable<TNumber>, y as IEnumerable<TNumber>);

    public virtual Int32 GetHashCode(
        [DisallowNull] IEnumerable<TNumber> obj) =>
        obj.GetHashCode();

    public int GetHashCode(object obj)
    {
        throw new NotImplementedException();
    }
}