namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsEntity { }

public interface IHomeBallsGameVersion :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable,
    INamed
{
    Byte GenerationId { get; }
}

public interface IHomeBallsGeneration :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable,
    INamed { }

public interface IHomeBallsItem :
    IHomeBallsEntity,
    IKeyed<UInt16>,
    IIdentifiable,
    INamed
{
    Byte CategoryId { get; }
}

public interface IHomeBallsItemCategory :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable { }

public interface IHomeBallsLanguage :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable,
    INamed { }

public interface IHomeBallsMove :
    IHomeBallsEntity,
    IKeyed<UInt16>,
    IIdentifiable,
    INamed
{
    Byte? DamageCategoryId { get; }

    Byte? TypeId { get; }
}

public interface IHomeBallsMoveDamageCategory :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable,
    INamed { }

public interface IHomeBallsNature :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable,
    INamed
{
    Byte DecreasedStatId { get; }

    Byte IncreasedStatId { get; }
}

public interface IHomeBallsPokemonAbility :
    IHomeBallsEntity,
    IKeyed<UInt16>,
    IIdentifiable,
    INamed { }

public interface IHomeBallsPokemonEggGroup :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable,
    INamed { }

public interface IHomeBallsPokemonForm :
    IHomeBallsEntity,
    IKeyed,
    IIdentifiable,
    INamed
{
    UInt16 SpeciesId { get; }

    Byte FormId { get; }

    IEnumerable<IHomeBallsPokemonTypeSlot> Types { get; }

    IEnumerable<IHomeBallsPokemonAbilitySlot> Abilities { get; }

    IEnumerable<IHomeBallsPokemonEggGroupSlot> EggGroups { get; }

    Boolean IsBreedable { get; }

    UInt16? EvolvesFromSpeciesId { get; }

    Byte? EvolvesFromFormId { get; }

    UInt16? BabyTriggerId { get; }
}

public interface IHomeBallsPokemonSpecies :
    IHomeBallsEntity,
    IKeyed<UInt16>,
    IIdentifiable,
    INamed
{
    SByte GenderRate { get; }

    Boolean IsFormSwitchable { get; }
}

public interface IHomeBallsStat :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable,
    INamed { }

public interface IHomeBallsType :
    IHomeBallsEntity,
    IKeyed<Byte>,
    IIdentifiable,
    INamed { }