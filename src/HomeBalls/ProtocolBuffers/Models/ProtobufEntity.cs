#nullable disable

namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufByteRecord))]
[ProtoInclude(2, typeof(ProtobufUInt16Record))]
[ProtoInclude(3, typeof(ProtobufIdentifiableRecord))]
[ProtoInclude(4, typeof(ProtobufEntryLegality))]
public abstract record ProtobufEntity : ProtobufRecord, IHomeBallsEntity { }

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufNamedRecord))]
public abstract record ProtobufIdentifiableRecord :
    ProtobufRecord,
    IIdentifiable
{
    [ProtoMember(2)]
    public virtual String Identifier { get; init; }
}

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufPokemonForm))]
public abstract record ProtobufNamedRecord :
    ProtobufIdentifiableRecord,
    INamed<ProtobufString>
{
    [ProtoMember(2)]
    public virtual IEnumerable<ProtobufString> Names { get; init; } =
        new List<ProtobufString> { };

    IEnumerable<IHomeBallsString> INamed.Names => Names;
}

[ProtoContract]
public record ProtobufEntryLegality :
    ProtobufRecord,
    IHomeBallsEntryLegality
{
    public virtual HomeBallsEntryKey Id { get; init; }

    [ProtoMember(1)]
    public virtual UInt16 SpeciesId { get => Id.SpeciesId; init => Id = new(value, FormId, BallId); }

    [ProtoMember(2)]
    public virtual Byte FormId { get => Id.FormId; init => Id = new(SpeciesId, value, BallId); }

    [ProtoMember(3)]
    public virtual UInt16 BallId { get => Id.BallId; init => Id = new(SpeciesId, FormId, value); }

    [ProtoMember(4)]
    public virtual Boolean IsObtainable { get; init; }

    [ProtoMember(5)]
    public virtual Boolean IsObtainableWithHiddenAbility { get; init; }

    String IIdentifiable.Identifier => Id.ToString();
}


[ProtoContract]
public record ProtobufGameVersion :
    ProtobufNamedByteRecord,
    IHomeBallsGameVersion
{
    [ProtoMember(1)]
    public virtual Byte GenerationId { get; init; }
}

[ProtoContract]
public record ProtobufGeneration :
    ProtobufNamedByteRecord,
    IHomeBallsGeneration { }

[ProtoContract]
public record ProtobufItem :
    ProtobufNamedUInt16Record,
    IHomeBallsItem
{
    [ProtoMember(1)]
    public virtual Byte CategoryId { get; init; }
}

[ProtoContract]
public record ProtobufItemCategory :
    ProtobufIdentifiableByteRecord,
    IHomeBallsItemCategory { }

[ProtoContract]
public record ProtobufLanguage :
    ProtobufNamedByteRecord,
    IHomeBallsLanguage { }

[ProtoContract]
public record ProtobufMove :
    ProtobufNamedUInt16Record,
    IHomeBallsMove
{
    [ProtoMember(1)]
    public virtual Byte? DamageCategoryId { get; init; }

    [ProtoMember(2)]
    public virtual Byte? TypeId { get; init; }
}

[ProtoContract]
public record ProtobufMoveDamageCategory :
    ProtobufNamedByteRecord,
    IHomeBallsMoveDamageCategory { }

[ProtoContract]
public record ProtobufNature :
    ProtobufNamedByteRecord,
    IHomeBallsNature
{
    [ProtoMember(1)]
    public virtual Byte DecreasedStatId { get; init; }

    [ProtoMember(2)]
    public virtual Byte IncreasedStatId { get; init; }
}

[ProtoContract]
public record ProtobufPokemonAbility :
    ProtobufNamedUInt16Record,
    IHomeBallsPokemonAbility { }

[ProtoContract]
public record ProtobufPokemonEggGroup :
    ProtobufNamedByteRecord,
    IHomeBallsPokemonEggGroup { }

[ProtoContract]
public record ProtobufPokemonForm :
    ProtobufNamedRecord,
    IHomeBallsPokemonForm
{
    public virtual HomeBallsPokemonFormKey Id { get; init; }

    [ProtoMember(1)]
    public virtual UInt16 SpeciesId { get => Id.SpeciesId; init => Id = new(value, FormId); }

    [ProtoMember(2)]
    public virtual Byte FormId { get => Id.FormId; init => Id = new(SpeciesId, value); }

    [ProtoMember(3)]
    public virtual IEnumerable<ProtobufPokemonFormTypeSlot> Types { get; init; } =
        new List<ProtobufPokemonFormTypeSlot> { };

    [ProtoMember(4)]
    public virtual IEnumerable<ProtobufPokemonFormAbilitySlot> Abilities { get; init; } =
        new List<ProtobufPokemonFormAbilitySlot> { };

    [ProtoMember(5)]
    public virtual IEnumerable<ProtobufPokemonFormEggGroupSlot> EggGroups { get; init; } =
        new List<ProtobufPokemonFormEggGroupSlot> { };

    [ProtoMember(6)]
    public virtual Boolean IsBreedable { get; init; }

    [ProtoMember(7)]
    public virtual UInt16? EvolvesFromSpeciesId { get; init; }

    [ProtoMember(8)]
    public virtual Byte? EvolvesFromFormId { get; init; }

    [ProtoMember(9)]
    public virtual UInt16? BabyTriggerId { get; init; }

    IEnumerable<IHomeBallsPokemonTypeSlot> IHomeBallsPokemonForm.Types => Types;

    IEnumerable<IHomeBallsPokemonAbilitySlot> IHomeBallsPokemonForm.Abilities => Abilities;

    IEnumerable<IHomeBallsPokemonEggGroupSlot> IHomeBallsPokemonForm.EggGroups => EggGroups;
}

[ProtoContract]
public record ProtobufPokemonSpecies :
    ProtobufNamedUInt16Record,
    IHomeBallsPokemonSpecies
{
    [ProtoMember(1)]
    public virtual SByte GenderRate { get; init; }

    [ProtoMember(2)]
    public virtual Boolean IsFormSwitchable { get; init; }
}

[ProtoContract]
public record ProtobufStat :
    ProtobufNamedByteRecord,
    IHomeBallsStat { }

[ProtoContract]
public record ProtobufType :
    ProtobufNamedByteRecord,
    IHomeBallsType { }