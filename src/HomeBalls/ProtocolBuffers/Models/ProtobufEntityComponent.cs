#nullable disable

namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufString))]
[ProtoInclude(2, typeof(ProtobufSlottableEntityComponent))]
public abstract record ProtobufEntityComponent : ProtobufRecord { }

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufPokemonFormAbilitySlot))]
[ProtoInclude(2, typeof(ProtobufPokemonFormEggGroupSlot))]
[ProtoInclude(3, typeof(ProtobufPokemonFormTypeSlot))]
public abstract record ProtobufSlottableEntityComponent :
    ProtobufEntityComponent,
    ISlottable
{
    [ProtoMember(4)]
    public virtual Byte Slot { get; init; }
}

[ProtoContract]
public record ProtobufString :
    ProtobufEntityComponent,
    IHomeBallsString
{
    [ProtoMember(1)]
    public virtual Byte LanguageId { get; init; }

    [ProtoMember(2)]
    public virtual String Value { get; init; }
}

[ProtoContract]
public record ProtobufPokemonFormAbilitySlot :
    ProtobufSlottableEntityComponent,
    IHomeBallsPokemonAbilitySlot
{
    [ProtoMember(1)]
    public virtual UInt16 AbilityId { get; init; }

    [ProtoMember(2)]
    public virtual Boolean IsHidden { get; init; }
}

[ProtoContract]
public record ProtobufPokemonFormEggGroupSlot :
    ProtobufSlottableEntityComponent,
    IHomeBallsPokemonEggGroupSlot
{
    [ProtoMember(1)]
    public virtual Byte EggGroupId { get; init; }
}

[ProtoContract]
public record ProtobufPokemonFormTypeSlot :
    ProtobufSlottableEntityComponent,
    IHomeBallsPokemonTypeSlot
{
    [ProtoMember(1)]
    public virtual Byte TypeId { get; init; }
}
