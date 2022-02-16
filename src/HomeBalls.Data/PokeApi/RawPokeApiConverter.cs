namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiConverter :
    IRawPokeApiPokemonConverter
{
    IReadOnlyList<HomeBallsGameVersion> Convert(IHomeBallsReadOnlyDataSet<Byte, RawPokeApiVersion> versions, IHomeBallsReadOnlyDataSet<Byte, RawPokeApiVersionGroup> versionGroups);

    HomeBallsGameVersion Convert(RawPokeApiVersion version, IHomeBallsReadOnlyDataSet<Byte, RawPokeApiVersionGroup> versionGroups);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiVersionName> names);

    HomeBallsString Convert(RawPokeApiVersionName name);

    IReadOnlyList<HomeBallsGeneration> Convert(IHomeBallsReadOnlyDataSet<Byte, RawPokeApiGeneration> generations);

    HomeBallsGeneration Convert(RawPokeApiGeneration generation);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiGenerationName> names);

    HomeBallsString Convert(RawPokeApiGenerationName name);

    IReadOnlyList<HomeBallsItem> Convert(IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiItem> items);

    HomeBallsItem Convert(RawPokeApiItem item);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiItemName> names);

    HomeBallsString Convert(RawPokeApiItemName name);

    IReadOnlyList<HomeBallsItemCategory> Convert(IHomeBallsReadOnlyDataSet<Byte, RawPokeApiItemCategory> categories);

    HomeBallsItemCategory Convert(RawPokeApiItemCategory category);

    IReadOnlyList<HomeBallsLanguage> Convert(IHomeBallsReadOnlyDataSet<Byte, RawPokeApiLanguage> languages);

    HomeBallsLanguage Convert(RawPokeApiLanguage language);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiLanguageName> names);

    HomeBallsString Convert(RawPokeApiLanguageName name);

    IReadOnlyList<HomeBallsMove> Convert(IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiMove> moves);

    HomeBallsMove Convert(RawPokeApiMove move);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiMoveName> names);

    HomeBallsString Convert(RawPokeApiMoveName name);

    IReadOnlyList<HomeBallsMoveDamageCategory> Convert(IHomeBallsReadOnlyDataSet<Byte, RawPokeApiMoveDamageClass> classes);

    HomeBallsMoveDamageCategory Convert(RawPokeApiMoveDamageClass @class);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiMoveDamageClassProse> names);

    HomeBallsString Convert(RawPokeApiMoveDamageClassProse name);

    IReadOnlyList<HomeBallsNature> Convert(IHomeBallsReadOnlyDataSet<Byte, RawPokeApiNature> natures);

    HomeBallsNature Convert(RawPokeApiNature nature);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiNatureName> names);

    HomeBallsString Convert(RawPokeApiNatureName name);

    IReadOnlyList<HomeBallsPokemonAbility> Convert(IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiAbility> abilities);

    HomeBallsPokemonAbility Convert(RawPokeApiAbility ability);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiAbilityName> names);

    HomeBallsString Convert(RawPokeApiAbilityName name);

    IReadOnlyList<HomeBallsPokemonEggGroup> Convert(IHomeBallsReadOnlyDataSet<Byte, RawPokeApiEggGroup> groups);

    HomeBallsPokemonEggGroup Convert(RawPokeApiEggGroup group);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiEggGroupProse> names);

    HomeBallsString Convert(RawPokeApiEggGroupProse name);

    IReadOnlyList<HomeBallsStat> Convert(IHomeBallsReadOnlyDataSet<Byte, RawPokeApiStat> stats);

    HomeBallsStat Convert(RawPokeApiStat stat);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiStatName> names);

    HomeBallsString Convert(RawPokeApiStatName name);

    IReadOnlyList<HomeBallsType> Convert(IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiType> types);

    HomeBallsType Convert(RawPokeApiType type);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiTypeName> names);

    HomeBallsString Convert(RawPokeApiTypeName name);
}

public class RawPokeApiConverter :
    RawPokeApiBaseConverter,
    IRawPokeApiConverter
{
    public RawPokeApiConverter(ILogger? logger = default) : base(logger)
    {
        PokemonConverter = new RawPokeApiPokemonConverter(Logger);
    }

    protected internal IRawPokeApiPokemonConverter PokemonConverter { get; }

    public virtual IReadOnlyList<HomeBallsGameVersion> Convert(
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiVersion> versions,
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiVersionGroup> versionGroups) =>
        ConvertList(versions, version => Convert(version, versionGroups));

    public virtual HomeBallsGameVersion Convert(
        RawPokeApiVersion version,
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiVersionGroup> versionGroups) =>
        new HomeBallsGameVersion
        {
            Id = version.Id,
            GenerationId = versionGroups[version.VersionGroupId].GenerationId,
            Identifier = version.Identifier
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiVersionName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiVersionName name) =>
        ConvertName<RawPokeApiVersionName, Byte>(name) with
        {
            NameOfGameVersionId = name.VersionId
        };

    public virtual IReadOnlyList<HomeBallsGeneration> Convert(
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiGeneration> generations) =>
        ConvertList(generations, Convert);

    public virtual HomeBallsGeneration Convert(RawPokeApiGeneration generation) =>
        new HomeBallsGeneration
        {
            Id = generation.Id,
            Identifier = generation.Identifier
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiGenerationName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiGenerationName name) =>
        ConvertName<RawPokeApiGenerationName, Byte>(name) with
        {
            NameOfGenerationId = name.GenerationId
        };

    public virtual IReadOnlyList<HomeBallsItem> Convert(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiItem> items) =>
        ConvertList(items, Convert);

    public virtual HomeBallsItem Convert(RawPokeApiItem item) =>
        new HomeBallsItem
        {
            CategoryId = item.CategoryId,
            Id = item.Id,
            Identifier = item.Identifier
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiItemName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiItemName name) =>
        ConvertName<RawPokeApiItemName, UInt16>(name) with
        {
            NameOfItemId = name.ItemId
        };

    public virtual IReadOnlyList<HomeBallsItemCategory> Convert(
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiItemCategory> categories) =>
        ConvertList(categories, Convert);

    public virtual HomeBallsItemCategory Convert(RawPokeApiItemCategory category) =>
        new HomeBallsItemCategory
        {
            Id = category.Id,
            Identifier = category.Identifier
        };

    public virtual IReadOnlyList<HomeBallsLanguage> Convert(
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiLanguage> languages) =>
        ConvertList(languages, Convert);

    public virtual HomeBallsLanguage Convert(RawPokeApiLanguage language) =>
        new HomeBallsLanguage
        {
            Id = language.Id,
            Identifier = language.Identifier
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiLanguageName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiLanguageName name) =>
        ConvertName<RawPokeApiLanguageName, Byte>(name) with
        {
            NameOfLanguageId = name.LanguageId
        };

    public virtual IReadOnlyList<HomeBallsMove> Convert(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiMove> moves) =>
        ConvertList(moves, Convert);

    public virtual HomeBallsMove Convert(RawPokeApiMove move) =>
        new HomeBallsMove
        {
            DamageCategoryId = move.DamageClassId,
            Id = move.Id,
            Identifier = move.Identifier,
            TypeId = move.TypeId.HasValue ? ToTypeId(move.TypeId.Value) : default(Byte?)
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiMoveName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiMoveName name) =>
        ConvertName<RawPokeApiMoveName, UInt16>(name) with
        {
            NameOfMoveId = name.MoveId
        };

    public virtual IReadOnlyList<HomeBallsMoveDamageCategory> Convert(
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiMoveDamageClass> classes) =>
        ConvertList(classes, Convert);

    public virtual HomeBallsMoveDamageCategory Convert(
        RawPokeApiMoveDamageClass @class) =>
        new HomeBallsMoveDamageCategory
        {
            Id = @class.Id,
            Identifier = @class.Identifier
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiMoveDamageClassProse> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiMoveDamageClassProse name) =>
        ConvertName<RawPokeApiMoveDamageClassProse, Byte>(name) with
        {
            NameOfMoveDamageCategoryId = name.MoveDamageClassId
        };

    public virtual IReadOnlyList<HomeBallsNature> Convert(
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiNature> natures) =>
        ConvertList(natures, Convert);

    public virtual HomeBallsNature Convert(RawPokeApiNature nature) =>
        new HomeBallsNature
        {
            DecreasedStatId = nature.DecreasedStatId,
            Id = nature.Id,
            Identifier = nature.Identifier,
            IncreasedStatId = nature.IncreasedStatId
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiNatureName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiNatureName name) =>
        ConvertName<RawPokeApiNatureName, Byte>(name) with
        {
            NameOfNatureId = name.NatureId
        };

    public virtual IReadOnlyList<HomeBallsPokemonAbility> Convert(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiAbility> abilities) =>
        ConvertList(abilities, Convert);

    public virtual HomeBallsPokemonAbility Convert(RawPokeApiAbility ability) =>
        new HomeBallsPokemonAbility
        {
            Id = ability.Id,
            Identifier = ability.Identifier
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiAbilityName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiAbilityName name) =>
        ConvertName<RawPokeApiAbilityName, UInt16>(name) with
        {
            NameOfPokemonAbilityId = name.AbilityId
        };

    public virtual IReadOnlyList<HomeBallsPokemonEggGroup> Convert(
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiEggGroup> groups) =>
        ConvertList(groups, Convert);

    public virtual HomeBallsPokemonEggGroup Convert(RawPokeApiEggGroup group) =>
        new HomeBallsPokemonEggGroup
        {
            Id = group.Id,
            Identifier = group.Identifier
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiEggGroupProse> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiEggGroupProse name) =>
        ConvertName<RawPokeApiEggGroupProse, Byte>(name) with
        {
            NameOfPokemonEggGroupId = name.EggGroupId
        };

    public virtual IReadOnlyList<HomeBallsStat> Convert(
        IHomeBallsReadOnlyDataSet<Byte, RawPokeApiStat> stats) =>
        ConvertList(stats, Convert);

    public virtual HomeBallsStat Convert(RawPokeApiStat stat) =>
        new HomeBallsStat
        {
            Id = stat.Id,
            Identifier = stat.Identifier
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(Byte, Byte), RawPokeApiStatName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiStatName name) =>
        ConvertName<RawPokeApiStatName, Byte>(name) with { NameOfStatId = name.StatId };

    public virtual IReadOnlyList<HomeBallsType> Convert(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiType> types) =>
        ConvertList(types, Convert);

    public virtual HomeBallsType Convert(RawPokeApiType type) =>
        new HomeBallsType { Id = ToTypeId(type.Id), Identifier = type.Identifier };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiTypeName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiTypeName name) =>
        ConvertName<RawPokeApiTypeName, UInt16>(name) with
        {
            NameOfTypeId = ToTypeId(name.TypeId)
        };

    public virtual IReadOnlyList<HomeBallsPokemonForm> Convert(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiEvolutionChain> chains) =>
        PokemonConverter.Convert(forms, variants, species, chains);

    public virtual HomeBallsPokemonForm Convert(
        RawPokeApiPokemonForm form,
        HomeBalls.Entities.HomeBallsPokemonFormKey id,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiEvolutionChain> chains) =>
        PokemonConverter.Convert(form, id, variants, species, chains);

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonFormName> names,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        PokemonConverter.Convert(names, forms, variants);

    public virtual HomeBallsString Convert(
        RawPokeApiPokemonFormName name,
        HomeBalls.Entities.HomeBallsPokemonFormKey id) =>
        PokemonConverter.Convert(name, id);

    public virtual IReadOnlyList<HomeBallsPokemonSpecies> Convert(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species) =>
        PokemonConverter.Convert(species);

    public virtual HomeBallsPokemonSpecies Convert(
        RawPokeApiPokemonSpecies species) =>
        PokemonConverter.Convert(species);

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonSpeciesName> names) =>
        PokemonConverter.Convert(names);

    public virtual HomeBallsString Convert(RawPokeApiPokemonSpeciesName name) =>
        PokemonConverter.Convert(name);

    public virtual IReadOnlyList<HomeBallsPokemonAbilitySlot> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, UInt16), RawPokeApiPokemonAbility> pokemonAbilities,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        PokemonConverter.Convert(pokemonAbilities, forms, variants);

    public virtual HomeBallsPokemonAbilitySlot Convert(
        RawPokeApiPokemonAbility join,
        HomeBalls.Entities.HomeBallsPokemonFormKey id) =>
        PokemonConverter.Convert(join, id);

    public virtual IReadOnlyList<HomeBallsPokemonEggGroupSlot> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonEggGroup> pokemonEggGroups,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        PokemonConverter.Convert(pokemonEggGroups, forms, variants);

    public virtual HomeBallsPokemonEggGroupSlot Convert(
        RawPokeApiPokemonEggGroup join,
        HomeBalls.Entities.HomeBallsPokemonFormKey id,
        Byte slot) =>
        PokemonConverter.Convert(join, id, slot);

    public virtual IReadOnlyList<HomeBallsPokemonTypeSlot> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, UInt16), RawPokeApiPokemonType> pokemonTypes,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        PokemonConverter.Convert(pokemonTypes, forms, variants);

    public virtual HomeBallsPokemonTypeSlot Convert(
        RawPokeApiPokemonType join,
        HomeBalls.Entities.HomeBallsPokemonFormKey id) =>
        PokemonConverter.Convert(join, id);
}