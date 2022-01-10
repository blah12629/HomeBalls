namespace CEo.Pokemon.HomeBalls.ProtocolBuffers.Tests;

public class HomeBallsProtobufConverterTests : HomeBallsBaseTest
{
    protected HomeBallsProtobufConverter Sut { get; } = new();

    protected void Convert_ShouldMatchConvertedProperties_NonTest<TSource, TResult>(
        TSource source)
        where TSource : notnull, IHomeBallsEntity
        where TResult : notnull, ProtobufRecord, TSource
    {
        TSource converted = Sut.Convert<TSource, TResult>(source);
        source.Should().BeEquivalentTo(converted);
    }

    [
        Theory,
        InlineData(typeof(IHomeBallsGameVersion), typeof(MockGameVersion), typeof(ProtobufGameVersion)),
        InlineData(typeof(IHomeBallsGeneration), typeof(MockGeneration), typeof(ProtobufGeneration)),
        InlineData(typeof(IHomeBallsItem), typeof(MockItem), typeof(ProtobufItem)),
        InlineData(typeof(IHomeBallsItemCategory), typeof(MockItemCategory), typeof(ProtobufItemCategory)),
        InlineData(typeof(IHomeBallsLanguage), typeof(MockLanguage), typeof(ProtobufLanguage)),
        InlineData(typeof(IHomeBallsMove), typeof(MockMove), typeof(ProtobufMove)),
        InlineData(typeof(IHomeBallsMoveDamageCategory), typeof(MockMoveDamageCategory), typeof(ProtobufMoveDamageCategory)),
        InlineData(typeof(IHomeBallsNature), typeof(MockNature), typeof(ProtobufNature)),
        InlineData(typeof(IHomeBallsPokemonAbility), typeof(MockPokemonAbility), typeof(ProtobufPokemonAbility)),
        InlineData(typeof(IHomeBallsPokemonEggGroup), typeof(MockPokemonEggGroup), typeof(ProtobufPokemonEggGroup)),
        InlineData(typeof(IHomeBallsPokemonForm), typeof(MockPokemonForm), typeof(ProtobufPokemonForm)),
        InlineData(typeof(IHomeBallsPokemonSpecies), typeof(MockPokemonSpecies), typeof(ProtobufPokemonSpecies)),
        InlineData(typeof(IHomeBallsStat), typeof(MockStat), typeof(ProtobufStat)),
        InlineData(typeof(IHomeBallsType), typeof(MockType), typeof(ProtobufType)),
    ]
    public void Convert_ShouldMatchConvertedProperties(
        Type sourceInterface,
        Type sourceMock,
        Type result)
    {
        var thisType = this.GetType();
        var mockObject = Activator.CreateInstance(sourceMock);

        this.GetType()
            .GetMethod(
                nameof(Convert_ShouldMatchConvertedProperties_NonTest),
                BindingFlags.Instance | BindingFlags.NonPublic)!
            .MakeGenericMethod(sourceInterface, result)
            .Invoke(this, new Object?[] { mockObject });
    }

    [
        Theory,
        InlineData(typeof(IHomeBallsGameVersion), typeof(MockGameVersion)),
        InlineData(typeof(IHomeBallsGeneration), typeof(MockGeneration)),
        InlineData(typeof(IHomeBallsItem), typeof(MockItem)),
        InlineData(typeof(IHomeBallsItemCategory), typeof(MockItemCategory)),
        InlineData(typeof(IHomeBallsLanguage), typeof(MockLanguage)),
        InlineData(typeof(IHomeBallsMove), typeof(MockMove)),
        InlineData(typeof(IHomeBallsMoveDamageCategory), typeof(MockMoveDamageCategory)),
        InlineData(typeof(IHomeBallsNature), typeof(MockNature)),
        InlineData(typeof(IHomeBallsPokemonAbility), typeof(MockPokemonAbility)),
        InlineData(typeof(IHomeBallsPokemonEggGroup), typeof(MockPokemonEggGroup)),
        InlineData(typeof(IHomeBallsPokemonForm), typeof(MockPokemonForm)),
        InlineData(typeof(IHomeBallsPokemonSpecies), typeof(MockPokemonSpecies)),
        InlineData(typeof(IHomeBallsStat), typeof(MockStat)),
        InlineData(typeof(IHomeBallsType), typeof(MockType)),
    ]
    public void Convert_ShouldChangeEnumerablePropertyTypesToProtobufRecord(
        Type interfaceType,
        Type mockType)
    {
        var mock = Activator.CreateInstance(mockType);

        var converted = Sut.GetType()
            .GetMethod(
                nameof(Sut.Convert),
                BindingFlags.Instance | BindingFlags.Public,
                new Type[] { interfaceType })!
            .Invoke(Sut, new Object?[] { mock });

        (converted as INamed)?.Names.Should().ContainItemsAssignableTo<ProtobufString>();

        if (converted is IHomeBallsPokemonForm form)
        {
            form.Types.Should().ContainItemsAssignableTo<ProtobufPokemonFormTypeSlot>();
            form.Abilities.Should().ContainItemsAssignableTo<ProtobufPokemonFormAbilitySlot>();
            form.EggGroups.Should().ContainItemsAssignableTo<ProtobufPokemonFormEggGroupSlot>();
        }
    }
}