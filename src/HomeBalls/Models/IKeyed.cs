namespace CEo.Pokemon.HomeBalls;

public interface IKeyed<TKey>
    where TKey : notnull, IEquatable<TKey>
{
    TKey Id { get; }
}