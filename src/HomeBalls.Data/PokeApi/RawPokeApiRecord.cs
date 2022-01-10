namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public abstract record RawPokeApiRecord : HomeBallsRecord { }

public abstract record RawPokeApiRecord<TKey> :
    RawPokeApiRecord,
    IKeyed<TKey>,
    IIdentifiable
    where TKey : struct, INumber<TKey>
{
    public virtual TKey Id { get; init; }

    public virtual String Identifier { get; init; }

    dynamic IKeyed.Id => Id;
}

public abstract record RawPokeApiGlobalObject : RawPokeApiRecord
{
    public virtual Byte LocalLanguageId { get; init; }
}

public record RawPokeApiName : RawPokeApiGlobalObject
{
    public virtual String Name { get; init; }
}

public abstract record RawPokeApiJoin : RawPokeApiRecord { }

public abstract record RawPokeApiSlottedJoin :
    RawPokeApiRecord,
    ISlottable
{
    public virtual Byte Slot { get; init; }
}

public record RawPokeApiAbility : RawPokeApiRecord<UInt16>
{
    public virtual Byte GenerationId { get; init; }

    public virtual Boolean IsMainSeries { get; init; }
}

public record RawPokeApiAbilityName : RawPokeApiName
{
    public virtual UInt16 AbilityId { get; init; }
}

public record RawPokeApiEggGroupProse : RawPokeApiName
{
    public virtual Byte EggGroupId { get; init; }
}

public record RawPokeApiEggGroup : RawPokeApiRecord<Byte> { }

public record RawPokeApiEvolutionChain : RawPokeApiRecord, IKeyed<UInt16>
{
    public virtual UInt16 Id { get; init; }

    public virtual UInt16? BabyTriggerItemId { get; init; }

    dynamic IKeyed.Id => Id;
}

public record RawPokeApiGenerationName : RawPokeApiName
{
    public virtual Byte GenerationId { get; init; }
}

public record RawPokeApiGeneration : RawPokeApiRecord<Byte>
{
    public virtual Byte MainRegionId { get; init; }
}

public record RawPokeApiItemCategory : RawPokeApiRecord<Byte>
{
    public virtual Byte PocketId { get; init; }
}

public record RawPokeApiItemName : RawPokeApiName
{
    public virtual UInt16 ItemId { get; init; }
}

public record RawPokeApiItem : RawPokeApiRecord<UInt16>
{
    public virtual Byte CategoryId { get; init; }

    public virtual UInt32 Cost { get; init; }

    public virtual Byte? FlingPower { get; init; }

    public virtual Byte? FlingEffectId { get; init; }
}

public record RawPokeApiLanguageName : RawPokeApiName
{
    public virtual Byte LanguageId { get; init; }
}

public record RawPokeApiLanguage : RawPokeApiRecord<Byte>
{
    public virtual Boolean Official { get; init; }

    public virtual Byte Order { get; init; }
}

public record RawPokeApiMoveDamageClassProse : RawPokeApiName
{
    public virtual Byte MoveDamageClassId { get; init; }

    public virtual String Description { get; init; }
}

public record RawPokeApiMoveDamageClass : RawPokeApiRecord<Byte> { }

public record RawPokeApiMoveName : RawPokeApiName
{
    public virtual UInt16 MoveId { get; init; }
}

public record RawPokeApiMove : RawPokeApiRecord<UInt16>
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

public record RawPokeApiNatureName : RawPokeApiName
{
    public virtual Byte NatureId { get; init; }
}

public record RawPokeApiNature : RawPokeApiRecord<Byte>
{
    public virtual Byte DecreasedStatId { get; init; }

    public virtual Byte IncreasedStatId { get; init; }

    public virtual Byte HatesFlavorId { get; init; }

    public virtual Byte LikesFlavorId { get; init; }

    public virtual Byte GameIndex { get; init; }
}

public record RawPokeApiPokemonAbility : RawPokeApiRecord, ISlottable
{
    public virtual UInt16 PokemonId { get; init; }

    public virtual UInt16 AbilityId { get; init; }

    public virtual Boolean IsHidden { get; init; }

    public virtual Byte Slot { get; init; }
}

public record RawPokeApiPokemonEggGroup : RawPokeApiRecord
{
    public virtual UInt16 SpeciesId { get; init; }

    public virtual Byte EggGroupId { get; init; }
}

public record RawPokeApiPokemonEvolution :
    RawPokeApiRecord,
    IKeyed<UInt16>
{
    public virtual UInt16 Id { get; init; }

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

    dynamic IKeyed.Id => Id;
}

public record RawPokeApiPokemonFormName : RawPokeApiGlobalObject
{
    public virtual UInt16 PokemonFormId { get; init; }

    public virtual String? FormName { get; init; }

    public virtual String? PokemonName { get; init; }
}

public record RawPokeApiPokemonForm : RawPokeApiRecord<UInt16>
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

public record RawPokeApiPokemonGameIndex : RawPokeApiRecord
{
    public virtual UInt16 PokemonId { get; init; }

    public virtual Byte VersionId { get; init; }

    public virtual UInt16 GameIndex { get; init; }
}

public record RawPokeApiPokemonSpeciesName : RawPokeApiName
{
    public virtual UInt16 PokemonSpeciesId { get; init; }

    public virtual String? Genus { get; init; }
}

public record RawPokeApiPokemonSpecies : RawPokeApiRecord<UInt16>
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

public record RawPokeApiPokemonType : RawPokeApiRecord, ISlottable
{
    public virtual UInt16 PokemonId { get; init; }

    public virtual UInt16 TypeId { get; init; }

    public virtual Byte Slot { get; init; }
}

public record RawPokeApiPokemon : RawPokeApiRecord<UInt16>
{
    public virtual UInt16 SpeciesId { get; init; }

    public virtual UInt16 Height { get; init; }

    public virtual UInt16 Weight { get; init; }

    public virtual UInt16 BaseExperience { get; init; }

    public virtual UInt16? Order { get; init; }

    public virtual Boolean IsDefault { get; init; }
}

public record RawPokeApiStatName : RawPokeApiName
{
    public virtual Byte StatId { get; init; }
}

public record RawPokeApiStat : RawPokeApiRecord<Byte>
{
    public virtual Byte? DamageClassId { get; init; }

    public virtual Boolean IsBattleOnly { get; init; }

    public virtual Byte? GameIndex { get; init; }
}

public record RawPokeApiTypeName : RawPokeApiName
{
    public virtual UInt16 TypeId { get; init; }
}

public record RawPokeApiType : RawPokeApiRecord<UInt16>
{
    public virtual Byte GenerationId { get; init; }

    public virtual Byte? DamageClassId { get; init; }
}

public record RawPokeApiVersionGroup : RawPokeApiRecord<Byte>
{
    public virtual Byte GenerationId { get; init; }

    public virtual Byte Order { get; init; }
}

public record RawPokeApiVersionName : RawPokeApiName
{
    public virtual Byte VersionId { get; init; }
}

public record RawPokeApiVersion : RawPokeApiRecord<Byte>
{
    public virtual Byte VersionGroupId { get; init; }
}