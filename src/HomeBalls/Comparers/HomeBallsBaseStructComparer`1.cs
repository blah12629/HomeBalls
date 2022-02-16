namespace CEo.Pokemon.HomeBalls.Comparers;

public interface IHomeBallsBaseStructComparer<TStruct> :
    IComparer<TStruct>,
    IComparer<TStruct?>,
    IEqualityComparer<TStruct>,
    IEqualityComparer<TStruct?>
    where TStruct : struct { }

public abstract class HomeBallsBaseStructComparer<TStruct> :
    IHomeBallsBaseStructComparer<TStruct>
    where TStruct : struct
{
    public virtual Int32 Compare(TStruct x, TStruct y) =>
        Compare((TStruct?)x, y);

    public abstract Int32 Compare(TStruct? x, TStruct? y);

    public virtual Boolean Equals(TStruct x, TStruct y) =>
        GetHashCode(x).Equals(GetHashCode(y)) && EqualsCore(x, y);

    public virtual Boolean Equals(TStruct? x, TStruct? y) =>
        x.HasValue ? y.HasValue && Equals(x.Value, y.Value) : !y.HasValue;

    protected internal abstract Boolean EqualsCore(TStruct x, TStruct y);

    public abstract Int32 GetHashCode([DisallowNull] TStruct obj);

    public virtual Int32 GetHashCode([DisallowNull] TStruct? obj) =>
        obj.HasValue ? GetHashCode(obj.Value) : 0;
}