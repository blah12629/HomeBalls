namespace CEo.Pokemon.HomeBalls;

public interface IAsyncLoadable
{
    Boolean IsLoaded { get; }

    ValueTask EnsureLoadedAsync(CancellationToken cancellationToken = default);
}

public interface IAsyncLoadable<T> : IAsyncLoadable
{
    new ValueTask<T> EnsureLoadedAsync(CancellationToken cancellationToken = default);
}