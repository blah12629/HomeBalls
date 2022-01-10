namespace CEo.Pokemon.HomeBalls;

public interface IAsyncLoadable
{
    ValueTask EnsureLoadedAsync(CancellationToken cancellationToken = default);
}

public interface IAsyncLoadable<T> : IAsyncLoadable
{
    new ValueTask<T> EnsureLoadedAsync(CancellationToken cancellationToken = default);
}