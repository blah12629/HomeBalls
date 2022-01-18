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
    public static TCollection AddRange<TCollection, TElement>(
        this TCollection collection,
        IEnumerable<TElement> elements)
        where TCollection : notnull, ICollection<TElement>
    {
        foreach (var element in elements) collection.Add(element);
        return collection;
    }

    public static async Task<TCollection> AddRangeByBatchAsync<TCollection, TElement>(
        this TCollection collection,
        IEnumerable<TElement> elements,
        UInt32 batchSize,
        Task? postBatchTask = default,
        CancellationToken cancellationToken = default)
        where TCollection : notnull, ICollection<TElement>
    {
        var task = postBatchTask ?? Task.CompletedTask;

        foreach (var chunk in elements.Chunk((Int32)batchSize))
        {
            foreach (var element in chunk) collection.Add(element);
            await task;
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