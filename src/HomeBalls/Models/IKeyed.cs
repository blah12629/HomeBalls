namespace CEo.Pokemon.HomeBalls;

public interface IKeyed
{
    dynamic Id { get; }
}

public interface IKeyed<TKey> : IKeyed
    where TKey : notnull, IEquatable<TKey>
{
    new TKey Id { get; }
}