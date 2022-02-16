namespace CEo.Pokemon.HomeBalls.Collections;

public interface IHomeBallsEnumerable<out T> :
    IEnumerable<T>
{
    Type ElementType { get; }
}