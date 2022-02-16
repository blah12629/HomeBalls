#nullable disable

namespace CEo.Pokemon.HomeBalls.Entities;

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsString))]
[ProtoInclude(2, typeof(HomeBallsSlottableEntityComponent))]
public abstract record HomeBallsEntityComponent :
    HomeBallsRecord { }

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsPokemonAbilitySlot))]
[ProtoInclude(2, typeof(HomeBallsPokemonEggGroupSlot))]
[ProtoInclude(3, typeof(HomeBallsPokemonTypeSlot))]
public abstract record HomeBallsSlottableEntityComponent :
    HomeBallsEntityComponent,
    ISlottable
{
    [ProtoMember(4)]
    public virtual Byte Slot { get; init; }
}

[ProtoContract]
public record HomeBallsString :
    HomeBallsEntityComponent,
    IHomeBallsString
{
    [ProtoMember(1)]
    public virtual Byte LanguageId { get; init; }

    [ProtoMember(2)]
    public virtual String Value { get; init; }
}

[ProtoContract]
public record HomeBallsPokemonAbilitySlot :
    HomeBallsSlottableEntityComponent,
    IHomeBallsPokemonAbilitySlot
{
    [ProtoMember(1)]
    public virtual UInt16 AbilityId { get; init; }

    [ProtoMember(2)]
    public virtual Boolean IsHidden { get; init; }
}

[ProtoContract]
public record HomeBallsPokemonEggGroupSlot :
    HomeBallsSlottableEntityComponent,
    IHomeBallsPokemonEggGroupSlot
{
    [ProtoMember(1)]
    public virtual Byte EggGroupId { get; init; }
}

[ProtoContract]
public record HomeBallsPokemonTypeSlot :
    HomeBallsSlottableEntityComponent,
    IHomeBallsPokemonTypeSlot
{
    [ProtoMember(1)]
    public virtual Byte TypeId { get; init; }
}