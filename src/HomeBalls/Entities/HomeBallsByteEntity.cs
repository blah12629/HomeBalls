#nullable disable

namespace CEo.Pokemon.HomeBalls.Entities;

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsIdentifiableByteEntity))]
public abstract record HomeBallsByteEntity :
    HomeBallsEntity,
    IKeyed<Byte>
{
    [ProtoMember(2)]
    public virtual Byte Id { get; init; }
}

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsNamedByteEntity))]
[ProtoInclude(2, typeof(HomeBallsItemCategory))]
public abstract record HomeBallsIdentifiableByteEntity :
    HomeBallsByteEntity,
    IIdentifiable
{
    [ProtoMember(3)]
    public virtual String Identifier { get; init; }
}

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsGameVersion))]
[ProtoInclude(2, typeof(HomeBallsGeneration))]
[ProtoInclude(3, typeof(HomeBallsItemCategory))]
[ProtoInclude(4, typeof(HomeBallsLanguage))]
[ProtoInclude(5, typeof(HomeBallsMoveDamageCategory))]
[ProtoInclude(6, typeof(HomeBallsNature))]
[ProtoInclude(7, typeof(HomeBallsPokemonEggGroup))]
[ProtoInclude(8, typeof(HomeBallsStat))]
[ProtoInclude(9, typeof(HomeBallsType))]
public abstract record HomeBallsNamedByteEntity :
    HomeBallsIdentifiableByteEntity,
    INamed<HomeBallsString>
{
    [ProtoMember(10)]
    public virtual IEnumerable<HomeBallsString> Names { get; init; } =
        new List<HomeBallsString> { };

    IEnumerable<IHomeBallsString> INamed.Names => Names;
}

[ProtoContract]
public record HomeBallsGameVersion :
    HomeBallsNamedByteEntity,
    IHomeBallsGameVersion
{
    [ProtoMember(1)]
    public virtual Byte GenerationId { get; init; }
}

[ProtoContract]
public record HomeBallsGeneration :
    HomeBallsNamedByteEntity,
    IHomeBallsGeneration { }

[ProtoContract]
public record HomeBallsItemCategory :
    HomeBallsIdentifiableByteEntity,
    IHomeBallsItemCategory { }

[ProtoContract]
public record HomeBallsLanguage :
    HomeBallsNamedByteEntity,
    IHomeBallsLanguage { }

[ProtoContract]
public record HomeBallsMoveDamageCategory :
    HomeBallsNamedByteEntity,
    IHomeBallsMoveDamageCategory { }

[ProtoContract]
public record HomeBallsNature :
    HomeBallsNamedByteEntity,
    IHomeBallsNature
{
    [ProtoMember(1)]
    public virtual Byte DecreasedStatId { get; init; }

    [ProtoMember(2)]
    public virtual Byte IncreasedStatId { get; init; }
}

[ProtoContract]
public record HomeBallsPokemonEggGroup :
    HomeBallsNamedByteEntity,
    IHomeBallsPokemonEggGroup { }

[ProtoContract]
public record HomeBallsStat :
    HomeBallsNamedByteEntity,
    IHomeBallsStat { }

[ProtoContract]
public record HomeBallsType :
    HomeBallsNamedByteEntity,
    IHomeBallsType { }