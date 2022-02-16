namespace CEo.Pokemon.HomeBalls.Entities;

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsPokemonForm))]
public abstract record HomeBallsPokemonFormEntity :
    HomeBallsEntity,
    IKeyed<HomeBallsPokemonFormKey>
{
    protected HomeBallsPokemonFormEntity() => Id = new();

    [ProtoMember(2)]
    public virtual HomeBallsPokemonFormKey Id { get; init; }
}

[ProtoContract]
public record HomeBallsPokemonForm :
    HomeBallsPokemonFormEntity,
    IHomeBallsPokemonForm
{
    String? _identifier, _speciesIdentifier;

    #nullable disable
    public HomeBallsPokemonForm() : base() { }
    #nullable enable

    public virtual String Identifier => _identifier ??= FormIdentifier == default ?
        SpeciesIdentifier :
        $"{SpeciesIdentifier}-{FormIdentifier}";

    [ProtoMember(1)]
    public virtual String SpeciesIdentifier
    {
        get => _speciesIdentifier!;
        init => (_speciesIdentifier, _identifier) = (value, default);
    }

    [ProtoMember(2)]
    public virtual String? FormIdentifier { get; init; }

    [ProtoMember(3)]
    public virtual IEnumerable<HomeBallsString> Names { get; init; } =
        new List<HomeBallsString> { };

    [ProtoMember(4)]
    public virtual IEnumerable<HomeBallsPokemonTypeSlot> Types { get; init; } =
        new List<HomeBallsPokemonTypeSlot> { };

    [ProtoMember(5)]
    public virtual IEnumerable<HomeBallsPokemonAbilitySlot> Abilities { get; init; } =
        new List<HomeBallsPokemonAbilitySlot> { };

    [ProtoMember(6)]
    public virtual IEnumerable<HomeBallsPokemonEggGroupSlot> EggGroups { get; init; } =
        new List<HomeBallsPokemonEggGroupSlot> { };

    [ProtoMember(7)]
    public virtual Boolean IsBreedable { get; init; }

    [ProtoMember(8)]
    public virtual HomeBallsPokemonFormKey? EvolvesFromId { get; init; }

    [ProtoMember(9)]
    public virtual UInt16? BabyTriggerId { get; init; }

    [ProtoMember(10)]
    public virtual String ProjectPokemonHomeSpriteId { get; init; }

    IEnumerable<IHomeBallsString> INamed.Names => Names;

    IEnumerable<IHomeBallsPokemonTypeSlot> IHomeBallsPokemonForm.Types => Types;

    IEnumerable<IHomeBallsPokemonAbilitySlot> IHomeBallsPokemonForm.Abilities => Abilities;

    IEnumerable<IHomeBallsPokemonEggGroupSlot> IHomeBallsPokemonForm.EggGroups => EggGroups;
}