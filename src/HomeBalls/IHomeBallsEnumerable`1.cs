namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsEnumerable<out T> :
    IEnumerable<T>
{
    Type ElementType { get; }
}