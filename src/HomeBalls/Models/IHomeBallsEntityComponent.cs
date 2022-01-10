namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsEntityComponent { }

public interface IHomeBallsString :
    IHomeBallsEntityComponent
{
    Byte LanguageId { get; }

    String Value { get; }
}

public interface IHomeBallsPokemonAbilitySlot :
    IHomeBallsEntityComponent,
    ISlottable
{
    UInt16 AbilityId { get; }

    Boolean IsHidden { get; }
}

public interface IHomeBallsPokemonEggGroupSlot :
    IHomeBallsEntityComponent,
    ISlottable
{
    Byte EggGroupId { get; }
}

public interface IHomeBallsPokemonTypeSlot :
    IHomeBallsEntityComponent,
    ISlottable
{
    Byte TypeId { get; }
}