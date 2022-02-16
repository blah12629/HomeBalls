namespace CEo.Pokemon.HomeBalls;

public interface IPredefinedInstance<out T>
{
    static abstract T Instance { get; }
}