using CEo.Pokemon.HomeBalls.Entities;

namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public abstract record RawPokeApiEntity : HomeBallsEntity { }

public abstract record RawPokeApiEntity<TKey> :
    RawPokeApiEntity,
    IKeyed<TKey>,
    IIdentifiable
    where TKey : notnull, IEquatable<TKey>
{
    String? _identifier;

    #nullable disable
    protected RawPokeApiEntity() { }
    #nullable enable

    protected internal virtual TKey Id { get; init; }

    protected internal virtual String Identifier
    {
        get => _identifier ??= Id?.ToString() ?? throw new ArgumentNullException();
        init => _identifier = value;
    }

    TKey IKeyed<TKey>.Id => Id;

    String IIdentifiable.Identifier => Identifier;
}

public abstract record RawPokeApiIdentifiableEntity<TKey> :
    RawPokeApiEntity<TKey>
    where TKey : notnull, IEquatable<TKey>
{
    #nullable disable
    protected RawPokeApiIdentifiableEntity() : base() { }
    #nullable enable

    new public virtual TKey Id { get => base.Id; init => base.Id = value; }

    new public virtual String Identifier
    {
        get => base.Identifier;
        init => base.Identifier = value;
    }
}

public abstract record RawPokeApiJoinEntity<TKey1, TKey2> :
    RawPokeApiEntity<(TKey1 Id1, TKey2 Id2)>
{
    TKey1 _id1;
    TKey2 _id2;

    #nullable disable
    protected RawPokeApiJoinEntity() : base() { }
    #nullable enable

    protected internal virtual TKey1 Id1
    {
        get => _id1;
        init { _id1 = value; Id = (Id1, Id2); }
    }

    protected internal virtual TKey2 Id2
    {
        get => _id2;
        init { _id2 = value; Id = (Id1, Id2); }
    }
}

public abstract record RawPokeApiGlobalObject<TForeignKey> :
    RawPokeApiJoinEntity<TForeignKey, Byte>
    where TForeignKey : notnull, IEquatable<TForeignKey>
{
    #nullable disable
    protected RawPokeApiGlobalObject() : base() { }
    #nullable enable

    protected internal virtual TForeignKey ForeignId { get => Id1; init => Id1 = value; }

    public virtual Byte LocalLanguageId { get => Id2; init => Id2 = value; }
}

public abstract record RawPokeApiName<TForeignKey> :
    RawPokeApiGlobalObject<TForeignKey>
    where TForeignKey : notnull, IEquatable<TForeignKey>
{
    #nullable disable
    protected RawPokeApiName() : base() { }
    #nullable enable

    public virtual String Name { get; init; }
}

public record RawPokeApiAbility : RawPokeApiIdentifiableEntity<UInt16>
{
    public virtual Byte GenerationId { get; init; }

    public virtual Boolean IsMainSeries { get; init; }
}

public record RawPokeApiAbilityName :
    RawPokeApiName<UInt16>
{
    public virtual UInt16 AbilityId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiEggGroupProse :
    RawPokeApiName<Byte>
{
    public virtual Byte EggGroupId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiEggGroup : RawPokeApiIdentifiableEntity<Byte> { }

public record RawPokeApiEvolutionChain : RawPokeApiEntity<UInt16>
{
    new public virtual UInt16 Id { get => base.Id; init => base.Id = value; }

    public virtual UInt16? BabyTriggerItemId { get; init; }
}

public record RawPokeApiGenerationName :
    RawPokeApiName<Byte>
{
    public virtual Byte GenerationId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiGeneration : RawPokeApiIdentifiableEntity<Byte>
{
    public virtual Byte MainRegionId { get; init; }
}

public record RawPokeApiItemCategory : RawPokeApiIdentifiableEntity<Byte>
{
    public virtual Byte PocketId { get; init; }
}

public record RawPokeApiItemName :
    RawPokeApiName<UInt16>
{
    public virtual UInt16 ItemId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiItem : RawPokeApiIdentifiableEntity<UInt16>
{
    public virtual Byte CategoryId { get; init; }

    public virtual UInt32 Cost { get; init; }

    public virtual Byte? FlingPower { get; init; }

    public virtual Byte? FlingEffectId { get; init; }
}

public record RawPokeApiLanguageName :
    RawPokeApiName<Byte>
{
    public virtual Byte LanguageId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiLanguage :
    RawPokeApiIdentifiableEntity<Byte>
{
    public virtual Boolean Official { get; init; }

    public virtual Byte Order { get; init; }
}

public record RawPokeApiMoveDamageClassProse :
    RawPokeApiName<Byte>
{
    #nullable disable
    public RawPokeApiMoveDamageClassProse() { }
    #nullable enable

    public virtual Byte MoveDamageClassId { get => ForeignId; init => ForeignId = value; }

    public virtual String Description { get; init; }
}

public record RawPokeApiMoveDamageClass : RawPokeApiIdentifiableEntity<Byte> { }

public record RawPokeApiMoveName :
    RawPokeApiName<UInt16>
{
    public virtual UInt16 MoveId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiMove : RawPokeApiIdentifiableEntity<UInt16>
{
    public virtual Byte GenerationId { get; init; }

    public virtual UInt16? TypeId { get; init; }

    public virtual Byte? Power { get; init; }

    // public virtual Byte? PP { get; init; }

    public virtual Byte? Accuracy { get; init; }

    public virtual SByte Priority { get; init; }

    public virtual Byte? TargetId { get; init; }

    public virtual Byte DamageClassId { get; init; }

    public virtual UInt16 EffectId { get; init; }

    public virtual Byte? EffectChance { get; init; }

    public virtual Byte? ContestTypeId { get; init; }

    public virtual Byte? ContestEffectId { get; init; }

    public virtual Byte? SuperContestEffectId { get; init; }
}

public record RawPokeApiNatureName :
    RawPokeApiName<Byte>
{
    public virtual Byte NatureId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiNature : RawPokeApiIdentifiableEntity<Byte>
{
    public virtual Byte DecreasedStatId { get; init; }

    public virtual Byte IncreasedStatId { get; init; }

    public virtual Byte HatesFlavorId { get; init; }

    public virtual Byte LikesFlavorId { get; init; }

    public virtual Byte GameIndex { get; init; }
}

public record RawPokeApiPokemonAbility :
    RawPokeApiJoinEntity<UInt16, UInt16>,
    ISlottable
{
    public virtual UInt16 PokemonId { get => Id1; init => Id1 = value; }

    public virtual UInt16 AbilityId { get => Id2; init => Id2 = value; }

    public virtual Boolean IsHidden { get; init; }

    public virtual Byte Slot { get; init; }
}

public record RawPokeApiPokemonEggGroup :
    RawPokeApiJoinEntity<UInt16, Byte>
{
    public virtual UInt16 SpeciesId  { get => Id1; init => Id1 = value; }

    public virtual Byte EggGroupId  { get => Id2; init => Id2 = value; }
}

public record RawPokeApiPokemonEvolution :
    RawPokeApiEntity<UInt16>
{
    new public virtual UInt16 Id { get => base.Id; init => base.Id = value; }

    public virtual UInt16 EvolvedSpeciesId { get; init; }

    public virtual Byte EvolutionTriggerId { get; init; }

    public virtual UInt16? TriggerItemId { get; init; }

    public virtual Byte? MinimumLevel { get; init; }

    public virtual Byte? GenderId { get; init; }

    public virtual UInt16? LocationId { get; init; }

    public virtual UInt16? HeldItemId { get; init; }

    public virtual String? TimeOfDay { get; init; }

    public virtual UInt16? KnownMoveId { get; init; }

    public virtual UInt16? KnownMoveTypeId { get; init; }

    public virtual Byte? MinimumHappiness { get; init; }

    public virtual Byte? MinimumBeauty  { get; init; }

    public virtual Byte? MinimumAffection { get; init; }

    public virtual SByte? RelativePhysicalStats { get; init; }

    public virtual UInt16? PartySpeciesId { get; init; }

    public virtual UInt16? PartyTypeId { get; init; }

    public virtual UInt16? TradeSpeciesId { get; init; }

    public virtual Boolean NeedsOverworldRain { get; init; }

    public virtual Boolean TurnUpsideDown { get; init; }
}

public record RawPokeApiPokemonFormName :
    RawPokeApiGlobalObject<UInt16>
{
    public virtual UInt16 PokemonFormId { get => ForeignId; init => ForeignId = value; }

    public virtual String? FormName { get; init; }

    public virtual String? PokemonName { get; init; }
}

public record RawPokeApiPokemonForm : RawPokeApiIdentifiableEntity<UInt16>
{
    public virtual String? FormIdentifier { get; init; }

    public virtual UInt16 PokemonId { get; init; }

    public virtual Byte IntroducedInVersionGroupId { get; init; }

    public virtual Boolean IsDefault { get; init; }

    public virtual Boolean IsBattleOnly { get; init; }

    public virtual Boolean IsMega { get; init; }

    public virtual UInt16 FormOrder { get; init; }

    public virtual UInt16 Order { get; init; }
}

public record RawPokeApiPokemonGameIndex :
    RawPokeApiJoinEntity<UInt16, Byte>
{
    public virtual UInt16 PokemonId  { get => Id1; init => Id1 = value; }

    public virtual Byte VersionId  { get => Id2; init => Id2 = value; }

    public virtual UInt16 GameIndex { get; init; }
}

public record RawPokeApiPokemonSpeciesName :
    RawPokeApiName<UInt16>
{
    public virtual UInt16 PokemonSpeciesId { get => ForeignId; init => ForeignId = value; }

    public virtual String? Genus { get; init; }
}

public record RawPokeApiPokemonSpecies : RawPokeApiIdentifiableEntity<UInt16>
{
    public virtual Byte GenerationId { get; init; }

    public virtual UInt16? EvolvesFromSpeciesId { get; init; }

    public virtual UInt16? EvolutionChainId { get; init; }

    public virtual Byte ColorId { get; init; }

    public virtual Byte ShapeId { get; init; }

    public virtual Byte? HabitatId { get; init; }

    public virtual SByte GenderRate { get; init; }

    public virtual Byte CaptureRate { get; init; }

    public virtual Byte BaseHappiness { get; init; }

    public virtual Boolean IsBaby { get; init; }

    public virtual Byte HatchCounter { get; init; }

    public virtual Boolean HasGenderDifferences { get; init; }

    public virtual Byte GrowthRateId { get; init; }

    public virtual Boolean FormsSwitchable { get; init; }

    public virtual Boolean IsLegendary { get; init; }

    public virtual Boolean IsMythical { get; init; }

    public virtual UInt16 Order { get; init; }

    public virtual UInt16? ConquestOrder { get; init; }
}

public record RawPokeApiPokemonType :
    RawPokeApiJoinEntity<UInt16, UInt16>,
    ISlottable
{
    public virtual UInt16 PokemonId { get => Id1; init => Id1 = value; }

    public virtual UInt16 TypeId { get => Id2; init => Id2 = value; }

    public virtual Byte Slot { get; init; }
}

public record RawPokeApiPokemon : RawPokeApiIdentifiableEntity<UInt16>
{
    public virtual UInt16 SpeciesId { get; init; }

    public virtual UInt16 Height { get; init; }

    public virtual UInt16 Weight { get; init; }

    public virtual UInt16 BaseExperience { get; init; }

    public virtual UInt16? Order { get; init; }

    public virtual Boolean IsDefault { get; init; }
}

public record RawPokeApiStatName :
    RawPokeApiName<Byte>
{
    public virtual Byte StatId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiStat : RawPokeApiIdentifiableEntity<Byte>
{
    public virtual Byte? DamageClassId { get; init; }

    public virtual Boolean IsBattleOnly { get; init; }

    public virtual Byte? GameIndex { get; init; }
}

public record RawPokeApiTypeName :
    RawPokeApiName<UInt16>
{
    public virtual UInt16 TypeId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiType : RawPokeApiIdentifiableEntity<UInt16>
{
    public virtual Byte GenerationId { get; init; }

    public virtual Byte? DamageClassId { get; init; }
}

public record RawPokeApiVersionGroup :
    RawPokeApiIdentifiableEntity<Byte>
{
    public virtual Byte GenerationId { get; init; }

    public virtual Byte Order { get; init; }
}

public record RawPokeApiVersionName :
    RawPokeApiName<Byte>
{
    public virtual Byte VersionId { get => ForeignId; init => ForeignId = value; }
}

public record RawPokeApiVersion : RawPokeApiIdentifiableEntity<Byte>
{
    public virtual Byte VersionGroupId { get; init; }
}