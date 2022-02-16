namespace CEo.Pokemon.HomeBalls.Collections;

public interface IHomeBallsReadOnlyList<out T> :
    IHomeBallsReadOnlyCollection<T>,
    IReadOnlyList<T> { }

public class HomeBallsReadOnlyList<T> :
    HomeBallsReadOnlyCollection<T>,
    IHomeBallsReadOnlyList<T>
{
    public HomeBallsReadOnlyList(
        IList<T> list) :
        base(list) { }

    protected internal IList<T> List => (IList<T>)Collection;

    public virtual T this[Int32 index] => List[index];
}