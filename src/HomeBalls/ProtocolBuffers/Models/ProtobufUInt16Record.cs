#nullable disable

namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufIdentifiableUInt16Record))]
public abstract record ProtobufUInt16Record :
    ProtobufRecord,
    IKeyed<UInt16>
{
    [ProtoMember(2)]
    public virtual UInt16 Id { get; init; }

    dynamic IKeyed.Id => Id;
}

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufNamedUInt16Record))]
public abstract record ProtobufIdentifiableUInt16Record :
    ProtobufUInt16Record,
    IIdentifiable
{
    [ProtoMember(2)]
    public virtual String Identifier { get; init; }
}

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufItem))]
[ProtoInclude(2, typeof(ProtobufMove))]
[ProtoInclude(3, typeof(ProtobufPokemonAbility))]
[ProtoInclude(4, typeof(ProtobufPokemonSpecies))]
public abstract record ProtobufNamedUInt16Record :
    ProtobufIdentifiableUInt16Record,
    INamed<ProtobufString>
{
    [ProtoMember(5)]
    public virtual IEnumerable<ProtobufString> Names { get; init; } =
        new List<ProtobufString> { };

    IEnumerable<IHomeBallsString> INamed.Names => Names;
}