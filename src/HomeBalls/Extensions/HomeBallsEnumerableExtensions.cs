namespace CEo.Pokemon.HomeBalls.Collections;

public static class HomeBallsEnumerableExtensions
{
    internal static MethodInfo EnumerableCastMethod { get; } =
        typeof(Enumerable).GetMethod(
            nameof(Enumerable.Cast),
            BindingFlags.Static | BindingFlags.Public) ??
            throw new MissingMethodException();

    internal static MethodInfo EnumerableToListMethod { get; } =
        typeof(Enumerable).GetMethod(
            nameof(Enumerable.ToList),
            BindingFlags.Static | BindingFlags.Public) ??
            throw new MissingMethodException();

    public static Type GetElementType<T>(this IEnumerable<T> enumerable) =>
        enumerable is IHomeBallsEnumerable<T> homeBallsEnumerable ?
            homeBallsEnumerable.ElementType :
            typeof(T);

    public static IEnumerable<Object> Cast(
        this IEnumerable elements,
        Type targetType) =>
        EnumerableCastMethod
            .MakeGenericMethod(targetType)
            .Invoke(default, new Object?[] { elements }) as dynamic ??
            throw new InvalidCastException();

    public static IEnumerable<Object> ToList(
        this IEnumerable elements,
        Type targetType) =>
        EnumerableToListMethod
            .MakeGenericMethod(targetType)
            .Invoke(default, new Object?[] { elements }) as dynamic ??
            throw new InvalidCastException();
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

    public static IHomeBallsReadOnlyCollection<T> AsReadOnly<T>(
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

public static class HomeBallsListExtensions
{
    public static IHomeBallsReadOnlyList<T> AsReadOnly<T>(
        this IList<T> collection) =>
        new HomeBallsReadOnlyList<T>(collection);
}