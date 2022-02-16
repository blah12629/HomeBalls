#nullable disable

namespace CEo.Pokemon.HomeBalls.Entities;

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsIdentifiableUInt16Entity))]
public abstract record HomeBallsUInt16Entity :
    HomeBallsEntity,
    IKeyed<UInt16>
{
    [ProtoMember(2)]
    public virtual UInt16 Id { get; init; }
}

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsNamedUInt16Entity))]
public abstract record HomeBallsIdentifiableUInt16Entity :
    HomeBallsUInt16Entity,
    IIdentifiable
{
    [ProtoMember(2)]
    public virtual String Identifier { get; init; }
}

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsItem))]
[ProtoInclude(2, typeof(HomeBallsMove))]
[ProtoInclude(3, typeof(HomeBallsPokemonAbility))]
[ProtoInclude(4, typeof(HomeBallsPokemonSpecies))]
public abstract record HomeBallsNamedUInt16Entity :
    HomeBallsIdentifiableUInt16Entity,
    INamed<HomeBallsString>
{
    [ProtoMember(5)]
    public virtual IEnumerable<HomeBallsString> Names { get; init; } =
        new List<HomeBallsString> { };

    IEnumerable<IHomeBallsString> INamed.Names => Names;
}

[ProtoContract]
public record HomeBallsItem :
    HomeBallsNamedUInt16Entity,
    IHomeBallsItem
{
    [ProtoMember(1)]
    public virtual Byte CategoryId { get; init; }
}

[ProtoContract]
public record HomeBallsMove :
    HomeBallsNamedUInt16Entity,
    IHomeBallsMove
{
    [ProtoMember(1)]
    public virtual Byte? DamageCategoryId { get; init; }

    [ProtoMember(2)]
    public virtual Byte? TypeId { get; init; }
}

[ProtoContract]
public record HomeBallsPokemonAbility :
    HomeBallsNamedUInt16Entity,
    IHomeBallsPokemonAbility { }

[ProtoContract]
public record HomeBallsPokemonSpecies :
    HomeBallsNamedUInt16Entity,
    IHomeBallsPokemonSpecies
{
    [ProtoMember(1)]
    public virtual SByte GenderRate { get; init; }

    [ProtoMember(2)]
    public virtual Boolean IsFormSwitchable { get; init; }
}