namespace CEo.Pokemon.HomeBalls.Tests;

public class HomeBallsTestsActivator
{
    public virtual T CreateInstance<T>(Byte listCount = 3) =>
        (T)CreateInstance(typeof(T), listCount);

    public virtual Object CreateInstance(Type type, Byte listCount = 3)
    {
        SByte i = 1;
        return CreateInstance(type, ref i, listCount);
    }

    public virtual Object CreateInstance(Type type, ref SByte i, Byte listCount = 3)
    {
        var result = CreateNumber(type, i) ?? (
            type == typeof(String) ? $"{type.FullName ?? type.Name} {i}" :
            type.IsValueType ? System.Activator.CreateInstance(type) :
            CreateEnumerable(type, ref i, listCount));

        if (result != default) return adapt(result);
        result = System.Activator.CreateInstance(type);

        var properties = type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(property => (Property: property, Setter: property.GetSetMethod()))
            .Where(property => property.Setter != default)
            .Select(property => (Property: property.Property, Setter: property.Setter!))
            .ToList().AsReadOnly();

        for (SByte k = 0; k < properties.Count; k ++, i = nextI(i))
        {
            var (property, setter) = properties[k];
            if (property.PropertyType == typeof(HomeBallsPokemonFormKey)) continue;
            else if (property.PropertyType == typeof(HomeBallsEntryKey)) continue;

            var value = CreateInstance(property.PropertyType, ref i, listCount);
            setter.Invoke(result, new Object?[] { value });
        }

        return adapt(result ?? throw new ArgumentException());

        static SByte nextI(SByte i) => Convert.ToSByte(i == SByte.MaxValue ? 1 : i + 1);

        static T adapt<T>(T item) where T : notnull => item.Adapt<T>();
    }

    public virtual Object? CreateNumber(Type type, SByte i)
    {
        if (TryConvertNumber<SByte>(type, i, out var a)) return a;
        else if (TryConvertNumber<Byte>(type, i, out var b)) return b;
        else if (TryConvertNumber<UInt16>(type, i, out var c)) return c;
        else if (TryConvertNumber<Int16>(type, i, out var d)) return c;
        else if (TryConvertNumber<UInt32>(type, i, out var e)) return d;
        else if (TryConvertNumber<Int32>(type, i, out var f)) return d;
        else if (TryConvertNumber<UInt64>(type, i, out var g)) return d;
        return default;
    }

    public virtual Boolean TryConvertNumber<TNumber>(
        Type type,
        SByte i,
        [MaybeNullWhen(false)] out TNumber number)
        where TNumber : struct, INumber<TNumber>
    {
        var isNumber = type == typeof(TNumber) || type == typeof(TNumber?);
        number = TNumber.Create(i);
        return isNumber;
    }

    public virtual Object? CreateEnumerable(Type type, ref SByte i, Byte listCount)
    {
        var enumerableType = type.GetInterfaces().Append(type)
            .SingleOrDefault(type =>
                type.IsInterface &&
                type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>))?
            .GetGenericArguments()
            .SingleOrDefault();

        if (enumerableType == default) return default;
        var list = new List<Object> { };
        for (Byte k = 0; k < listCount; k ++)
            list.Add(CreateInstance(enumerableType, ref i, listCount));

        if (list.Count > 0) i -= 1;

        var castMethod = getEnumerableMethod(nameof(Enumerable.Cast));
        var toListMethod = getEnumerableMethod(nameof(Enumerable.ToList));
        var cast = castMethod.Invoke(default, new Object?[] { list });
        return toListMethod.Invoke(default, new Object?[] { cast })!;

        MethodInfo getEnumerableMethod(String methodName) =>
            typeof(Enumerable).GetMethod(methodName)!.MakeGenericMethod(enumerableType);
    }
}