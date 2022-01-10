namespace CEo.Pokemon.HomeBalls;

public interface IAsyncDownloadable
{
    ValueTask EnsureDownloadedAsync(CancellationToken cancellationToken = default);
}

public interface IAsyncDownloadable<T> : IAsyncDownloadable
{
    new ValueTask<T> EnsureDownloadedAsync(CancellationToken cancellationToken = default);
}