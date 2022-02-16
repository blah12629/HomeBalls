using ProtoBuf.Meta;

namespace CEo.Pokemon.HomeBalls.Data.Entities.Test;

public class HomeBallsEntityTests : HomeBallsBaseTests
{
    public HomeBallsEntityTests(ITestOutputHelper output) : base(output)
    {
        Serializer = new ProtoBufSerializer();
    }

    protected IProtoBufSerializer Serializer { get; }

    public static IEnumerable<Object[]> GetEntityTypePairs()
    {
        yield return new Object[]
        {
            typeof(HomeBalls.Data.Entities.HomeBallsGameVersion),
            typeof(HomeBalls.Entities.HomeBallsGameVersion),
            new HomeBallsGameVersion
            {
                GenerationId = 1,
                Id = 1,
                Identifier = "identifier",
                Names = new List<HomeBallsString>
                {
                    new HomeBallsString { Id = 1, LanguageId = 1, NameOfGameVersionId = 1, Value = "name 1" }
                }
            }
        };
    }

    [Theory, MemberData(nameof(GetEntityTypePairs))]
    public void DataHomeBallsEntity_ShouldBeDeserializableAsHomeBallsEntities(
        Type dataEntityType,
        Type baseEntityType,
        Object instance)
    {
        var method = GetType().GetMethod(
            nameof(DataHomeBallsEntity_ShouldBeDeserializableAsHomeBallsEntities_Protected),
            BindingFlags.Instance | BindingFlags.NonPublic) ??
            throw new NullReferenceException();

        method.MakeGenericMethod(dataEntityType, baseEntityType)
            .Invoke(this, new Object?[] { instance });
    }

    protected void DataHomeBallsEntity_ShouldBeDeserializableAsHomeBallsEntities_Protected<TDataEntity, TBaseEntity>(
        TDataEntity instance)
        where TDataEntity : class, TBaseEntity, IHomeBallsDataType<TBaseEntity>
        where TBaseEntity : class, HomeBalls.Entities.IHomeBallsEntity
    {
        var serialized = Serializer.ForGenericTypes.Serialize(instance.ToBaseType()).AsMemory();
        var deserialized = Serializer.ForGenericTypes.Deserialize<TBaseEntity>(serialized);
        deserialized.Should().BeEquivalentTo(
            (TBaseEntity)instance,
            options => options.Excluding(entity => entity.EntityType));
    }

    [Theory, MemberData(nameof(GetEntityTypePairs))]
    public void DataHomeBallsEntity_ShouldHaveHomeBallsEntitiesBaseType(
        Type dataEntityType,
        Type baseEntityType,
        Object instanec) =>
        dataEntityType.BaseType.Should().Be(baseEntityType);
}