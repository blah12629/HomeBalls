namespace CEo.Pokemon.HomeBalls.Comparers;

public abstract class HomeBallsBaseSwitchableStructComparer<TComparer, T> :
    IHomeBallsBaseStructComparer<T>
    where TComparer : notnull, IHomeBallsBaseStructComparer<T>
    where T : struct
{
    protected HomeBallsBaseSwitchableStructComparer(
        TComparer current) =>
        CurrentComparer = current;

    protected internal TComparer CurrentComparer { get; set; }

    public virtual Int32 Compare(T x, T y) => CurrentComparer.Compare(x, y);

    public virtual Int32 Compare(T? x, T? y) => CurrentComparer.Compare(x, y);

    public virtual Boolean Equals(T x, T y) => CurrentComparer.Equals(x, y);

    public virtual Boolean Equals(T? x, T? y) => CurrentComparer.Equals(x, y);

    public virtual Int32 GetHashCode([DisallowNull] T obj) =>
        CurrentComparer.GetHashCode(obj);

    public virtual Int32 GetHashCode([DisallowNull] T? obj) =>
        CurrentComparer.GetHashCode(obj);

    protected internal virtual HomeBallsBaseSwitchableStructComparer<TComparer, T> UseComparer(
        TComparer comparer)
    {
        CurrentComparer = comparer;
        return this;
    }
}
