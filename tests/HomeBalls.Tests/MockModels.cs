#nullable disable

namespace CEo.Pokemon.HomeBalls.Tests;

public abstract record MockRecord : HomeBallsRecord, IHomeBallsEntity { }

public abstract record MockKeyedRecord<TKey> : MockRecord, IKeyed<TKey>
    where TKey : notnull, IEquatable<TKey>
{
    public virtual TKey Id { get; init; }
}

public abstract record MockIdentifiableRecord<TKey> : MockKeyedRecord<TKey>, IIdentifiable
    where TKey : notnull, IEquatable<TKey>
{
    public String Identifier { get; init; }
}

public abstract record MockNamedRecord<TKey> : MockIdentifiableRecord<TKey>, INamed<MockString>
    where TKey : notnull, IEquatable<TKey>
{
    public IEnumerable<MockString> Names { get; init; } =
        new List<MockString> { }.AsReadOnly();

    IEnumerable<IHomeBallsString> INamed.Names => Names;
}

public record MockEntry : MockKeyedRecord<HomeBallsEntryKey>, IHomeBallsEntry
{
    UInt16 _speciesId, _ballId;
    Byte _formId;

    public UInt16 SpeciesId { get => _speciesId; init { _speciesId = value; Id = new(SpeciesId, FormId, BallId); } }

    public Byte FormId { get => _formId; init { _formId = value; Id = new(SpeciesId, FormId, BallId); } }

    public UInt16 BallId { get => _ballId; init { _ballId = value; Id = new(SpeciesId, FormId, BallId); } }

    public Boolean HasHiddenAbility { get; set; }

    public ICollection<UInt16> AvailableEggMoveIds { get; init; }

    public DateTime AddedOn { get; init; }

    public DateTime LastUpdatedOn { get; init; }

    HomeBallsEntryKey IKeyed<HomeBallsEntryKey>.Id => (HomeBallsEntryKey)Id;

    public event EventHandler<HomeBallsPropertyChangedEventArgs> PropertyChanged;

    public Boolean Equals(IHomeBallsEntry other) => throw new NotImplementedException();
}

public record MockEntryLegality : MockKeyedRecord<HomeBallsEntryKey>, IHomeBallsEntryLegality
{
    public UInt16 SpeciesId { get => Id.SpeciesId; init => Id = new(value, FormId, BallId); }

    public Byte FormId { get => Id.FormId; init => Id = new(SpeciesId, value, BallId); }

    public UInt16 BallId { get => Id.BallId; init => Id = new(SpeciesId, FormId, value); }

    public Boolean IsObtainable { get; init; }

    public Boolean IsObtainableWithHiddenAbility { get; init; }

    String IIdentifiable.Identifier => Id.ToString();
}

public record MockGameVersion : MockNamedRecord<Byte>, IHomeBallsGameVersion
{
    public Byte GenerationId { get; init; }
}

public record MockGeneration : MockNamedRecord<Byte>, IHomeBallsGeneration { }

public record MockItem : MockNamedRecord<UInt16>, IHomeBallsItem
{
    public Byte CategoryId { get; init; }
}

public record MockItemCategory : MockNamedRecord<Byte>, IHomeBallsItemCategory { }

public record MockLanguage : MockNamedRecord<Byte>, IHomeBallsLanguage { }

public record MockMove : MockNamedRecord<UInt16>, IHomeBallsMove
{
    public Byte? DamageCategoryId { get; init; }

    public Byte? TypeId { get; init; }
}

public record MockMoveDamageCategory : MockNamedRecord<Byte>, IHomeBallsMoveDamageCategory { }

public record MockNature : MockNamedRecord<Byte>, IHomeBallsNature
{
    public Byte DecreasedStatId { get; init; }

    public Byte IncreasedStatId { get; init; }
}

public record MockPokemonAbility : MockNamedRecord<UInt16>, IHomeBallsPokemonAbility { }

public record MockPokemonAbilitySlot : MockRecord, IHomeBallsPokemonAbilitySlot
{
    public virtual UInt16 AbilityId { get; init; }

    public virtual Boolean IsHidden { get; init; }

    public virtual Byte Slot { get; init; }
}

public record MockPokemonEggGroup : MockNamedRecord<Byte>, IHomeBallsPokemonEggGroup { }

public record MockPokemonEggGroupSlot : MockRecord, IHomeBallsPokemonEggGroupSlot
{
    public virtual Byte EggGroupId { get; init; }

    public virtual Byte Slot { get; init; }
}

public record MockPokemonForm : MockNamedRecord<HomeBallsPokemonFormKey>, IHomeBallsPokemonForm
{
    UInt16 _speciesId;
    Byte _formId;

    public UInt16 SpeciesId { get => _speciesId; init { _speciesId = value; Id = new(SpeciesId, FormId); } }

    public Byte FormId { get => _formId; init { _formId = value; Id = new(SpeciesId, FormId); } }

    public virtual IEnumerable<MockPokemonTypeSlot> Types { get; init; } =
        new List<MockPokemonTypeSlot> { }.AsReadOnly();

    public virtual IEnumerable<MockPokemonAbilitySlot> Abilities { get; init; } =
        new List<MockPokemonAbilitySlot> { }.AsReadOnly();

    public virtual IEnumerable<MockPokemonEggGroupSlot> EggGroups { get; init; } =
        new List<MockPokemonEggGroupSlot> { }.AsReadOnly();

    public virtual Boolean IsBreedable { get; init; }

    public virtual UInt16? EvolvesFromSpeciesId { get; init; }

    public virtual Byte? EvolvesFromFormId { get; init; }

    public virtual UInt16? BabyTriggerId { get; init; }

    IEnumerable<IHomeBallsPokemonTypeSlot> IHomeBallsPokemonForm.Types => Types;

    IEnumerable<IHomeBallsPokemonAbilitySlot> IHomeBallsPokemonForm.Abilities => Abilities;

    IEnumerable<IHomeBallsPokemonEggGroupSlot> IHomeBallsPokemonForm.EggGroups => EggGroups;

    HomeBallsPokemonFormKey IKeyed<HomeBallsPokemonFormKey>.Id => (HomeBallsPokemonFormKey)Id;
}

public record MockPokemonSpecies : MockNamedRecord<UInt16>, IHomeBallsPokemonSpecies
{
    public virtual SByte GenderRate { get; init; }

    public virtual Boolean IsFormSwitchable { get; init; }
}

public record MockPokemonTypeSlot : MockRecord, IHomeBallsPokemonTypeSlot
{
    public virtual Byte TypeId { get; init; }

    public virtual Byte Slot { get; init; }
}

public record MockStat : MockNamedRecord<Byte>, IHomeBallsStat { }

public record MockString : MockRecord, IHomeBallsString
{
    public Byte LanguageId { get; init; }

    public String Value { get; init; }
}

public record MockType : MockNamedRecord<Byte>, IHomeBallsType { }