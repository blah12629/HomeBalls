namespace CEo.Pokemon.HomeBalls.Comparers;

public interface IHomeBallsBaseClassComparer<TClass> :
    IComparer<TClass?>,
    IEqualityComparer<TClass?>
    where TClass : class { }

public abstract class HomeBallsBaseClassComparer<TClass> :
    IHomeBallsBaseClassComparer<TClass>
    where TClass : class
{
    public virtual Int32 Compare(TClass? x, TClass? y) => x == default(TClass?) ?
        y == default(TClass?) ? 0 : -1 :
        y == default(TClass?) ? 1 : CompareCore(x, y);

    protected internal abstract Int32 CompareCore(TClass x, TClass y);

    public virtual Boolean Equals(TClass? x, TClass? y)
    {
        Boolean isXDefault = x == default(TClass?), isYDefault = y == default(TClass?);
        if (isXDefault && isYDefault) return true;
        if (!isXDefault && !isYDefault && GetHashCode(x!) == GetHashCode(y!))
            return EqualsCore(x!, y!);

        return false;
    }

    protected internal abstract Boolean EqualsCore(TClass x, TClass y);

    public virtual Int32 GetHashCode([DisallowNull] TClass? obj) =>
        obj == default(TClass?) ? 0 : GetHashCodeCore(obj);

    protected internal abstract Int32 GetHashCodeCore([DisallowNull] TClass obj);
}