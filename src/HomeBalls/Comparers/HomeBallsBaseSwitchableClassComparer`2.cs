namespace CEo.Pokemon.HomeBalls.Comparers;

public abstract class HomeBallsBaseSwitchableClassComparer<TComparer, T> :
    IHomeBallsBaseClassComparer<T>
    where TComparer : notnull, IHomeBallsBaseClassComparer<T>
    where T : class
{
    protected HomeBallsBaseSwitchableClassComparer(
        TComparer current) =>
        CurrentComparer = current;

    protected internal TComparer CurrentComparer { get; set; }

    public virtual Int32 Compare(T? x, T? y) =>
        CurrentComparer.Compare(x, y);

    public virtual Boolean Equals(T? x, T? y) =>
        CurrentComparer.Equals(x, y);

    public virtual Int32 GetHashCode([DisallowNull] T? obj) =>
        CurrentComparer.GetHashCode(obj);

    protected internal virtual HomeBallsBaseSwitchableClassComparer<TComparer, T> UseComparer(
        TComparer comparer)
    {
        CurrentComparer = comparer;
        return this;
    }
}
