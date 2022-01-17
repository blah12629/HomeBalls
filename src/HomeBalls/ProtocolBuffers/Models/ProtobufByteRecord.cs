#nullable disable

namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufIdentifiableByteRecord))]
public abstract record ProtobufByteRecord :
    ProtobufRecord,
    IKeyed<Byte>
{
    [ProtoMember(2)]
    public virtual Byte Id { get; init; }
}

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufNamedByteRecord))]
[ProtoInclude(2, typeof(ProtobufItemCategory))]
public abstract record ProtobufIdentifiableByteRecord :
    ProtobufByteRecord,
    IIdentifiable
{
    [ProtoMember(3)]
    public virtual String Identifier { get; init; }
}

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufGameVersion))]
[ProtoInclude(2, typeof(ProtobufGeneration))]
[ProtoInclude(3, typeof(ProtobufLanguage))]
[ProtoInclude(4, typeof(ProtobufMoveDamageCategory))]
[ProtoInclude(5, typeof(ProtobufNature))]
[ProtoInclude(6, typeof(ProtobufPokemonEggGroup))]
[ProtoInclude(7, typeof(ProtobufStat))]
[ProtoInclude(8, typeof(ProtobufType))]
public abstract record ProtobufNamedByteRecord :
    ProtobufIdentifiableByteRecord,
    INamed<ProtobufString>
{
    [ProtoMember(9)]
    public virtual IEnumerable<ProtobufString> Names { get; init; } =
        new List<ProtobufString> { };

    IEnumerable<IHomeBallsString> INamed.Names => Names;
}