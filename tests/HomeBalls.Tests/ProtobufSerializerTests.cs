using ProtoBuf;

namespace CEo.Pokemon.HomeBalls.ProtocolBuffers.Tests;

public class ProtobufSerializerTests : HomeBallsBaseTest
{
    [
        Theory,
        InlineData(typeof(ProtobufGameVersion)),
        InlineData(typeof(ProtobufGeneration)),
        InlineData(typeof(ProtobufItem)),
        InlineData(typeof(ProtobufItemCategory)),
        InlineData(typeof(ProtobufLanguage)),
        InlineData(typeof(ProtobufMove)),
        InlineData(typeof(ProtobufMoveDamageCategory)),
        InlineData(typeof(ProtobufNature)),
        InlineData(typeof(ProtobufPokemonAbility)),
        InlineData(typeof(ProtobufPokemonEggGroup)),
        InlineData(typeof(ProtobufPokemonForm)),
        InlineData(typeof(ProtobufPokemonSpecies)),
        InlineData(typeof(ProtobufStat)),
        InlineData(typeof(ProtobufType)),
    ]
    public async Task Serialize_ShouldKeepData_WhenDeserializingSameObject(Type type)
    {
        Object @object = Activator.CreateInstance(type), deserialized;

        await using (var memory = new MemoryStream())
        {
            Serializer.Serialize(memory, @object);
            memory.Seek(0, SeekOrigin.Begin);
            deserialized = Serializer.Deserialize(type, memory);
        }

        @object.Should().BeEquivalentTo(deserialized);
    }
}