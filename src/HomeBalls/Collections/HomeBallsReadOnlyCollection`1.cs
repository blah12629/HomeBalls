namespace CEo.Pokemon.HomeBalls.Collections;

public interface IHomeBallsReadOnlyCollection<out T> :
    IHomeBallsEnumerable<T>,
    IReadOnlyCollection<T> { }

public class HomeBallsReadOnlyCollection<T> : IHomeBallsReadOnlyCollection<T>
{
    public HomeBallsReadOnlyCollection(
        ICollection<T> collection)
    {
        Collection = collection;
        ElementType = typeof(T);
    }

    public virtual Int32 Count => Collection.Count;

    public virtual Type ElementType { get; }

    protected internal ICollection<T> Collection { get; }

    public virtual IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}