namespace CEo.Pokemon.HomeBalls;

public static class HomeBallsEnumerableExtensions
{
    // public static Type GetElementType<T>(this IEnumerable<T> enumerable) =>
    //     enumerable is IHomeBallsEnumerable<T> homeBallsEnumerable ?
    //         homeBallsEnumerable.ElementType :
    //         typeof(T);
}

public static class HomeBallsCollectionExtensions
{
    public static async Task<TCollection> AddRangeByBatchAsync<TCollection, TElement>(
        this TCollection collection,
        IEnumerable<TElement> elements,
        UInt32 batchSize,
        Task? postBatchTask = default,
        CancellationToken cancellationToken = default)
        where TCollection : notnull, ICollection<TElement>
    {
        var task = postBatchTask ?? Task.CompletedTask;
        var i = UInt32.MinValue;

        foreach (var element in elements)
        {
            if (i == batchSize)
            {
                i = UInt32.MinValue;
                await task;
            }

            collection.Add(element);
            i ++;
        }

        return collection;
    }

    public static IReadOnlyCollection<T> AsReadOnly<T>(
        this ICollection<T> collection) =>
        new HomeBallsReadOnlyCollection<T>(collection);
}

public static class HomeBallsDictionaryExtensions
{
    public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary)
        where TKey : notnull =>
        new ReadOnlyDictionary<TKey, TValue>(dictionary);

    public static TValue? GetOrDefault<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue> dictionary,
        TKey key) =>
        dictionary.TryGetValue(key, out var value) ? value : default(TValue);
}