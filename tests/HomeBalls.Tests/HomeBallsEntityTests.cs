namespace CEo.Pokemon.HomeBalls.Entities.Tests;

public class HomeBallsEntityTests : HomeBallsBaseTests
{
    public HomeBallsEntityTests(ITestOutputHelper output) : base(output) { }

    [
        Theory,
        InlineData(typeof(IHomeBallsEntity)),
        InlineData(typeof(IHomeBallsEntityComponent))
    ]
    public void HomeBallsEntity_ShouldHaveConcreteTypes(Type interfaceType)
    {
        var types = interfaceType.Assembly.GetTypes();
        var entitiyInterfaces = types
            .Where(type => type.IsInterface && type.IsAssignableTo(interfaceType));

        foreach (var @interface in entitiyInterfaces)
        {
            try
            {
                types.Any(type => !type.IsInterface && type.IsAssignableTo(@interface))
                    .Should().BeTrue();
            }
            catch
            {
                throw new NotImplementedException(
                    $"`{@interface.Name}` has no concrete implementation.");
            }
        }
    }

    public static IEnumerable<Object> HomeBallEntityTypes
    {
        get
        {
            yield return new Object[] { typeof(HomeBallsEntryKey) };
            yield return new Object[] { typeof(HomeBallsPokemonFormKey) };

            yield return new Object[] { typeof(HomeBallsGameVersion) };
            yield return new Object[] { typeof(HomeBallsGeneration) };
            yield return new Object[] { typeof(HomeBallsItemCategory) };
            yield return new Object[] { typeof(HomeBallsLanguage) };
            yield return new Object[] { typeof(HomeBallsMoveDamageCategory) };
            yield return new Object[] { typeof(HomeBallsNature) };
            yield return new Object[] { typeof(HomeBallsPokemonEggGroup) };
            yield return new Object[] { typeof(HomeBallsStat) };
            yield return new Object[] { typeof(HomeBallsType) };

            yield return new Object[] { typeof(HomeBallsItem) };
            yield return new Object[] { typeof(HomeBallsMove) };
            yield return new Object[] { typeof(HomeBallsPokemonAbility) };
            yield return new Object[] { typeof(HomeBallsPokemonSpecies) };

            yield return new Object[] { typeof(HomeBallsPokemonForm) };

            yield return new Object[] { typeof(HomeBallsEntry) };
            yield return new Object[] { typeof(HomeBallsEntryLegality) };

            yield return new Object[] { typeof(HomeBallsString) };
            yield return new Object[] { typeof(HomeBallsPokemonAbilitySlot) };
            yield return new Object[] { typeof(HomeBallsPokemonEggGroupSlot) };
            yield return new Object[] { typeof(HomeBallsPokemonTypeSlot) };
        }
    }

    [Theory, MemberData(nameof(HomeBallEntityTypes))]
    public Task HomeBallsEntity_ShouldNotLoseData_WhenSerializedAndDeserializedWithProtoBuf(
        Type entityType)
    {
        var method = GetType().GetMethod(
            nameof(HomeBallsEntity_ShouldNotLoseData_WhenSerializedAndDeserializedWithProtoBuf_Protected),
            BindingFlags.Instance | BindingFlags.NonPublic);

        return method?.MakeGenericMethod(entityType).Invoke(this, new Object?[] { }) as Task ??
            throw new NullReferenceException();
    }

    protected virtual async Task HomeBallsEntity_ShouldNotLoseData_WhenSerializedAndDeserializedWithProtoBuf_Protected<TEntity>()
        where TEntity : class
    {
        TEntity @object = GenerateValues<TEntity>(), deserialized;

        await using (var memory = new MemoryStream())
        {
            ProtoBuf.Serializer.Serialize(memory, @object);
            memory.Seek(0, SeekOrigin.Begin);
            deserialized = ProtoBuf.Serializer.Deserialize<TEntity>(memory);
        }

        deserialized.Should().BeEquivalentTo(@object);
    }

    [Theory, MemberData(nameof(HomeBallEntityTypes))]
    public Task HomeBallsEntity_ShouldNotLoseData_WhenSerializedAndDeserializedAsCollectionWithProtoBuf(
        Type entityType)
    {
        var method = GetType().GetMethod(
            nameof(HomeBallsEntity_ShouldNotLoseData_WhenSerializedAndDeserializedAsCollectionWithProtoBuf_Protected),
            BindingFlags.Instance | BindingFlags.NonPublic);

        return method?.MakeGenericMethod(entityType).Invoke(this, new Object?[] { }) as Task ??
            throw new NullReferenceException();
    }
    
    protected internal async Task HomeBallsEntity_ShouldNotLoseData_WhenSerializedAndDeserializedAsCollectionWithProtoBuf_Protected<TEntity>()
        where TEntity : class
    {
        IEnumerable<TEntity> @object = Enumerable.Range(0, 3)
            .Select(i => GenerateValues<TEntity>())
            .ToList(),
        deserialized;

        await using (var memory = new MemoryStream())
        {
            ProtoBuf.Serializer.Serialize(memory, @object);
            memory.Seek(0, SeekOrigin.Begin);
            deserialized = ProtoBuf.Serializer.Deserialize<IEnumerable<TEntity>>(memory);
        }

        deserialized.Should().BeEquivalentTo(@object);
    }

    protected T GenerateValues<T>()
    {
        if (typeof(T) == typeof(HomeBallsEntry)) return (T)(Object)(
            new HomeBallsEntry(false, GenerateValues<List<UInt16>>()) with
            {
                Id = GenerateValues<HomeBallsEntryKey>()
            });

        return AutoFaker.Generate<T>();
    }
}