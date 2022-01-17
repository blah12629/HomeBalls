namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public interface IHomeBallsProtobufTypeMap
{
    Type GetProtobufConcreteType(Type type);
}

public class HomeBallsProtobufTypeMap :
    IHomeBallsProtobufTypeMap
{
    protected internal IReadOnlyDictionary<Type, Type> InterfaceToProtobufLookup { get; } =
        new Dictionary<Type, Type>
        {
            [typeof(IHomeBallsEntry)] = typeof(ProtobufEntry),
            [typeof(IHomeBallsEntryLegality)] = typeof(ProtobufEntryLegality),
            [typeof(IHomeBallsGameVersion)] = typeof(ProtobufGameVersion),
            [typeof(IHomeBallsGeneration)] = typeof(ProtobufGeneration),
            [typeof(IHomeBallsItem)] = typeof(ProtobufItem),
            [typeof(IHomeBallsItemCategory)] = typeof(ProtobufItemCategory),
            [typeof(IHomeBallsLanguage)] = typeof(ProtobufLanguage),
            [typeof(IHomeBallsMove)] = typeof(ProtobufMove),
            [typeof(IHomeBallsMoveDamageCategory)] = typeof(ProtobufMoveDamageCategory),
            [typeof(IHomeBallsNature)] = typeof(ProtobufNature),
            [typeof(IHomeBallsPokemonAbility)] = typeof(ProtobufPokemonAbility),
            [typeof(IHomeBallsPokemonAbilitySlot)] = typeof(ProtobufPokemonFormAbilitySlot),
            [typeof(IHomeBallsPokemonEggGroup)] = typeof(ProtobufPokemonEggGroup),
            [typeof(IHomeBallsPokemonEggGroupSlot)] = typeof(ProtobufPokemonFormEggGroupSlot),
            [typeof(IHomeBallsPokemonForm)] = typeof(ProtobufPokemonForm),
            [typeof(IHomeBallsPokemonSpecies)] = typeof(ProtobufPokemonSpecies),
            [typeof(IHomeBallsPokemonTypeSlot)] = typeof(ProtobufPokemonFormTypeSlot),
            [typeof(IHomeBallsStat)] = typeof(ProtobufStat),
            [typeof(IHomeBallsType)] = typeof(ProtobufType),
            [typeof(IHomeBallsPokemonTypeSlot)] = typeof(ProtobufPokemonFormTypeSlot),
            [typeof(IHomeBallsString)] = typeof(ProtobufString),
        }.AsReadOnly();

    public virtual Type GetProtobufConcreteType(Type type)
    {
        var concreteType = InterfaceToProtobufLookup.GetOrDefault(type);
        return concreteType == default ?
            throw new NotSupportedException() :
            concreteType;
    }
}