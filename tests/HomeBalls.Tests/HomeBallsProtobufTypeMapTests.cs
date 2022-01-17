namespace CEo.Pokemon.HomeBalls.ProtocolBuffers.Tests;

public class HomeBallsProtobufTypeMapTests
{
    public HomeBallsProtobufTypeMap Sut { get; } = new();

    [
        Theory,
        InlineData(typeof(IHomeBallsEntry), typeof(ProtobufEntry)),
        InlineData(typeof(IHomeBallsEntryLegality), typeof(ProtobufEntryLegality)),
        InlineData(typeof(IHomeBallsGameVersion), typeof(ProtobufGameVersion)),
        InlineData(typeof(IHomeBallsGeneration), typeof(ProtobufGeneration)),
        InlineData(typeof(IHomeBallsItem), typeof(ProtobufItem)),
        InlineData(typeof(IHomeBallsItemCategory), typeof(ProtobufItemCategory)),
        InlineData(typeof(IHomeBallsLanguage), typeof(ProtobufLanguage)),
        InlineData(typeof(IHomeBallsMove), typeof(ProtobufMove)),
        InlineData(typeof(IHomeBallsMoveDamageCategory), typeof(ProtobufMoveDamageCategory)),
        InlineData(typeof(IHomeBallsNature), typeof(ProtobufNature)),
        InlineData(typeof(IHomeBallsPokemonAbility), typeof(ProtobufPokemonAbility)),
        InlineData(typeof(IHomeBallsPokemonEggGroup), typeof(ProtobufPokemonEggGroup)),
        InlineData(typeof(IHomeBallsPokemonForm), typeof(ProtobufPokemonForm)),
        InlineData(typeof(IHomeBallsPokemonSpecies), typeof(ProtobufPokemonSpecies)),
        InlineData(typeof(IHomeBallsStat), typeof(ProtobufStat)),
        InlineData(typeof(IHomeBallsType), typeof(ProtobufType)),
        InlineData(typeof(IHomeBallsString), typeof(ProtobufString)),
        InlineData(typeof(IHomeBallsPokemonAbilitySlot), typeof(ProtobufPokemonFormAbilitySlot)),
        InlineData(typeof(IHomeBallsPokemonEggGroupSlot), typeof(ProtobufPokemonFormEggGroupSlot)),
        InlineData(typeof(IHomeBallsPokemonTypeSlot), typeof(ProtobufPokemonFormTypeSlot)),
    ]
    public void GetProtobufConcreteType_ShouldReturnExpectedType(Type input, Type expected) =>
        Sut.GetProtobufConcreteType(input).Should().Be(expected);

    [
        Theory,
        InlineData(typeof(IAsyncDisposable))
    ]
    public void GetProtobufConcreteType_ShouldThrowNotSupportedException_WhenTypeNotSupported(
        Type type) =>
        new Action(() => Sut.GetProtobufConcreteType(type))
            .Should().Throw<NotSupportedException>();
}