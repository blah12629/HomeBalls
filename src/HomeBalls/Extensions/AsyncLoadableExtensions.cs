namespace CEo.Pokemon.HomeBalls;

public static class AsyncLoadableExtensions
{
    public static Boolean IsAllLoaded(
        this IEnumerable<IAsyncLoadable> loadables) =>
        !loadables.Any(loadable => !loadable.IsLoaded);
}