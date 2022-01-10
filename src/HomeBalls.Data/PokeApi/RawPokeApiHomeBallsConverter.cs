using CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IRawPokeApiHomeBallsConverter :
    IRawPokeApiAbilityConverter,
    IRawPokeApiEggGroupConverter,
    IRawPokeApiGenerationConverter,
    IRawPokeApiItemConverter,
    IRawPokeApiItemCategoryConverter,
    IRawPokeApiLanguageConverter,
    IRawPokeApiMoveConverter,
    IRawPokeApiMoveDamageClassConverter,
    IRawPokeApiNameConverter,
    IRawPokeApiNatureConverter,
    IRawPokeApiPokemonFormConverter,
    IRawPokeApiPokemonSpeciesConverter,
    IRawPokeApiStatConverter,
    IRawPokeApiTypeConverter,
    IRawPokeApiVersionConverter { }

public class RawPokeApiHomeBallsConverter :
    IRawPokeApiHomeBallsConverter
{
    public RawPokeApiHomeBallsConverter(
        IRawPokeApiAbilityConverter? abilityConverter = default,
        IRawPokeApiEggGroupConverter? eggGroupConverter = default,
        IRawPokeApiGenerationConverter? generationConverter = default,
        IRawPokeApiItemConverter? itemConverter = default,
        IRawPokeApiItemCategoryConverter? itemCategoryConverter = default,
        IRawPokeApiLanguageConverter? languageConverter = default,
        IRawPokeApiMoveConverter? moveConverter = default,
        IRawPokeApiMoveDamageClassConverter? moveDamageClassConverter = default,
        IRawPokeApiNameConverter? nameConverter = default,
        IRawPokeApiNatureConverter? natureConverter = default,
        IRawPokeApiPokemonFormConverter? pokemonFormConverter = default,
        IRawPokeApiPokemonSpeciesConverter? pokemonSpeciesConverter = default,
        IRawPokeApiStatConverter? statConverter = default,
        IRawPokeApiTypeConverter? typeConverter = default,
        IRawPokeApiVersionConverter? versionConverter = default,
        ILogger? logger = default)
    {
        Logger = logger;

        NameConverter = nameConverter ?? new RawPokeApiNameConverter(Logger);
        AbilityConverter = abilityConverter ?? new RawPokeApiAbilityConverter(NameConverter, Logger);
        EggGroupConverter = eggGroupConverter ?? new RawPokeApiEggGroupConverter(NameConverter, Logger);
        GenerationConverter = generationConverter ?? new RawPokeApiGenerationConverter(NameConverter, Logger);
        ItemConverter = itemConverter ?? new RawPokeApiItemConverter(NameConverter, Logger);
        ItemCategoryConverter = itemCategoryConverter ?? new RawPokeApiItemCategoryConverter(Logger);
        LanguageConverter = languageConverter ?? new RawPokeApiLanguageConverter(NameConverter, Logger);
        MoveConverter = moveConverter ?? new RawPokeApiMoveConverter(NameConverter, Logger);
        MoveDamageClassConverter = moveDamageClassConverter ?? new RawPokeApiMoveDamageClassConverter(NameConverter, Logger);
        NatureConverter = natureConverter ?? new RawPokeApiNatureConverter(NameConverter, Logger);
        PokemonFormConverter = pokemonFormConverter ?? new RawPokeApiPokemonFormConverter(NameConverter, Logger);
        PokemonSpeciesConverter = pokemonSpeciesConverter ?? new RawPokeApiPokemonSpeciesConverter(NameConverter, Logger);
        StatConverter = statConverter ?? new RawPokeApiStatConverter(NameConverter, Logger);
        TypeConverter = typeConverter ?? new RawPokeApiTypeConverter(NameConverter, Logger);
        VersionConverter = versionConverter ?? new RawPokeApiVersionConverter(NameConverter, Logger);
    }

    protected internal ILogger? Logger { get; }

    protected internal IRawPokeApiNameConverter NameConverter { get; }

    protected internal IRawPokeApiAbilityConverter AbilityConverter { get; }

    protected internal IRawPokeApiEggGroupConverter EggGroupConverter { get; }

    protected internal IRawPokeApiGenerationConverter GenerationConverter { get; }

    protected internal IRawPokeApiItemConverter ItemConverter { get; }

    protected internal IRawPokeApiItemCategoryConverter ItemCategoryConverter { get; }

    protected internal IRawPokeApiLanguageConverter LanguageConverter { get; }

    protected internal IRawPokeApiMoveConverter MoveConverter { get; }

    protected internal IRawPokeApiMoveDamageClassConverter MoveDamageClassConverter { get; }

    protected internal IRawPokeApiNatureConverter NatureConverter { get; }

    protected internal IRawPokeApiPokemonFormConverter PokemonFormConverter { get; }

    protected internal IRawPokeApiPokemonSpeciesConverter PokemonSpeciesConverter { get; }

    protected internal IRawPokeApiStatConverter StatConverter { get; }

    protected internal IRawPokeApiTypeConverter TypeConverter { get; }

    protected internal IRawPokeApiVersionConverter VersionConverter { get; }

    public virtual EFCorePokemonForm Convert(RawPokeApiPokemonForm form, RawPokeApiPokemon variant, RawPokeApiPokemonSpecies species, Int32 formIndex) => PokemonFormConverter.Convert(form, variant, species, formIndex);

    public virtual IReadOnlyList<EFCorePokemonForm> Convert(IEnumerable<RawPokeApiPokemonForm> forms, IEnumerable<RawPokeApiPokemon> variants, IEnumerable<RawPokeApiPokemonSpecies> species) => PokemonFormConverter.Convert(forms, variants, species);

    public virtual IReadOnlyList<EFCorePokemonForm> Convert(IEnumerable<RawPokeApiPokemonForm> forms, IEnumerable<RawPokeApiPokemon> variants, IEnumerable<RawPokeApiPokemonSpecies> species, IEnumerable<RawPokeApiPokemonAbility> abilities, IEnumerable<RawPokeApiPokemonEggGroup> eggGroups, IEnumerable<RawPokeApiPokemonType> types) => PokemonFormConverter.Convert(forms, variants, species, abilities, eggGroups, types);

    public virtual IReadOnlyList<EFCorePokemonForm> Convert(IEnumerable<RawPokeApiPokemonForm> forms, IEnumerable<RawPokeApiPokemon> variants, IEnumerable<RawPokeApiPokemonSpecies> species, IEnumerable<RawPokeApiPokemonAbility> abilities, IEnumerable<RawPokeApiPokemonEggGroup> eggGroups, IEnumerable<RawPokeApiPokemonType> types, IEnumerable<RawPokeApiPokemonFormName> names) => PokemonFormConverter.Convert(forms, variants, species, abilities, eggGroups, types, names);

    public virtual EFCorePokemonAbilitySlot Convert(RawPokeApiPokemonAbility ability) => PokemonFormConverter.Convert(ability);

    public virtual IReadOnlyList<EFCorePokemonAbilitySlot> Convert(IEnumerable<RawPokeApiPokemonAbility> abilities) => PokemonFormConverter.Convert(abilities);

    public virtual EFCorePokemonEggGroupSlot Convert(RawPokeApiPokemonEggGroup eggGroup, Int32 index) => PokemonFormConverter.Convert(eggGroup, index);

    public virtual IReadOnlyList<EFCorePokemonEggGroupSlot> Convert(IEnumerable<RawPokeApiPokemonEggGroup> eggGroups) => PokemonFormConverter.Convert(eggGroups);

    public virtual EFCorePokemonTypeSlot Convert(RawPokeApiPokemonType type) => PokemonFormConverter.Convert(type);

    public virtual IReadOnlyList<EFCorePokemonTypeSlot> Convert(IEnumerable<RawPokeApiPokemonType> types) => PokemonFormConverter.Convert(types);

    public virtual EFCorePokemonSpecies Convert(RawPokeApiPokemonSpecies species) => PokemonSpeciesConverter.Convert(species);

    public virtual IReadOnlyList<EFCorePokemonSpecies> Convert(IEnumerable<RawPokeApiPokemonSpecies> species) => PokemonSpeciesConverter.Convert(species);

    public virtual IReadOnlyList<EFCorePokemonSpecies> Convert(IEnumerable<RawPokeApiPokemonSpecies> species, IEnumerable<RawPokeApiPokemonSpeciesName> names) => PokemonSpeciesConverter.Convert(species, names);

    public virtual EFCorePokemonAbility Convert(RawPokeApiAbility ability) => AbilityConverter.Convert(ability);

    public virtual IReadOnlyList<EFCorePokemonAbility> Convert(IEnumerable<RawPokeApiAbility> abilities) => AbilityConverter.Convert(abilities);

    public virtual IReadOnlyList<EFCorePokemonAbility> Convert(IEnumerable<RawPokeApiAbility> abilities, IEnumerable<RawPokeApiAbilityName> names) => AbilityConverter.Convert(abilities, names);

    public virtual EFCorePokemonEggGroup Convert(RawPokeApiEggGroup eggGroup) => EggGroupConverter.Convert(eggGroup);

    public virtual IReadOnlyList<EFCorePokemonEggGroup> Convert(IEnumerable<RawPokeApiEggGroup> eggGroups) => EggGroupConverter.Convert(eggGroups);

    public virtual IReadOnlyList<EFCorePokemonEggGroup> Convert(IEnumerable<RawPokeApiEggGroup> eggGroups, IEnumerable<RawPokeApiEggGroupProse> names) => EggGroupConverter.Convert(eggGroups, names);

    public virtual EFCoreLanguage Convert(RawPokeApiLanguage language) => LanguageConverter.Convert(language);

    public virtual IReadOnlyList<EFCoreLanguage> Convert(IEnumerable<RawPokeApiLanguage> languages) => LanguageConverter.Convert(languages);

    public virtual IReadOnlyList<EFCoreLanguage> Convert(IEnumerable<RawPokeApiLanguage> languages, IEnumerable<RawPokeApiLanguageName> names) => LanguageConverter.Convert(languages, names);

    public virtual EFCoreString Convert(RawPokeApiName name) => NameConverter.Convert(name);

    public virtual IReadOnlyList<EFCoreString> Convert(IEnumerable<RawPokeApiName> name) => NameConverter.Convert(name);

    public virtual EFCoreString? Convert(RawPokeApiPokemonFormName name) => NameConverter.Convert(name);

    public virtual IReadOnlyList<EFCoreString> Convert(IEnumerable<RawPokeApiPokemonFormName> names) => NameConverter.Convert(names);

    public virtual EFCoreType Convert(RawPokeApiType type) => TypeConverter.Convert(type);

    public virtual IReadOnlyList<EFCoreType> Convert(IEnumerable<RawPokeApiType> types) => TypeConverter.Convert(types); 

    public virtual IReadOnlyList<EFCoreType> Convert(IEnumerable<RawPokeApiType> types, IEnumerable<RawPokeApiTypeName> names) => TypeConverter.Convert(types, names);

    public virtual EFCoreGeneration Convert(RawPokeApiGeneration generation) => GenerationConverter.Convert(generation);

    public virtual IReadOnlyList<EFCoreGeneration> Convert(IEnumerable<RawPokeApiGeneration> generations) => GenerationConverter.Convert(generations);

    public virtual IReadOnlyList<EFCoreGeneration> Convert(IEnumerable<RawPokeApiGeneration> generations, IEnumerable<RawPokeApiGenerationName> names) => GenerationConverter.Convert(generations, names);

    public virtual EFCoreGameVersion Convert(RawPokeApiVersion version, RawPokeApiVersionGroup group) => VersionConverter.Convert(version, group);

    public virtual IReadOnlyList<EFCoreGameVersion> Convert(IEnumerable<RawPokeApiVersion> versions, IEnumerable<RawPokeApiVersionGroup> groups) => VersionConverter.Convert(versions, groups);

    public virtual IReadOnlyList<EFCoreGameVersion> Convert(IEnumerable<RawPokeApiVersion> versions, IEnumerable<RawPokeApiVersionGroup> groups, IEnumerable<RawPokeApiVersionName> names) => VersionConverter.Convert(versions, groups, names);

    public virtual EFCoreItem Convert(RawPokeApiItem item) => ItemConverter.Convert(item);

    public virtual IReadOnlyList<EFCoreItem> Convert(IEnumerable<RawPokeApiItem> items) => ItemConverter.Convert(items);

    public virtual IReadOnlyList<EFCoreItem> Convert(IEnumerable<RawPokeApiItem> items, IEnumerable<RawPokeApiItemName> names) => ItemConverter.Convert(items, names);

    public virtual EFCoreItemCategory Convert(RawPokeApiItemCategory category) => ItemCategoryConverter.Convert(category);

    public virtual IReadOnlyList<EFCoreItemCategory> Convert(IEnumerable<RawPokeApiItemCategory> categories) => ItemCategoryConverter.Convert(categories);

    public virtual EFCoreMove Convert(RawPokeApiMove move) => MoveConverter.Convert(move);

    public virtual IReadOnlyList<EFCoreMove> Convert(IEnumerable<RawPokeApiMove> moves) => MoveConverter.Convert(moves);

    public virtual IReadOnlyList<EFCoreMove> Convert(IEnumerable<RawPokeApiMove> moves, IEnumerable<RawPokeApiMoveName> names) => MoveConverter.Convert(moves, names);

    public virtual EFCoreMoveDamageCategory Convert(RawPokeApiMoveDamageClass @class) => MoveDamageClassConverter.Convert(@class);

    public virtual IReadOnlyList<EFCoreMoveDamageCategory> Convert(IEnumerable<RawPokeApiMoveDamageClass> classes) => MoveDamageClassConverter.Convert(classes);

    public virtual IReadOnlyList<EFCoreMoveDamageCategory> Convert(IEnumerable<RawPokeApiMoveDamageClass> classes, IEnumerable<RawPokeApiMoveDamageClassProse> names) => MoveDamageClassConverter.Convert(classes, names);

    public virtual EFCoreNature Convert(RawPokeApiNature nature) => NatureConverter.Convert(nature);

    public virtual IReadOnlyList<EFCoreNature> Convert(IEnumerable<RawPokeApiNature> natures) => NatureConverter.Convert(natures);

    public virtual IReadOnlyList<EFCoreNature> Convert(IEnumerable<RawPokeApiNature> natures, IEnumerable<RawPokeApiNatureName> names) => NatureConverter.Convert(natures, names);

    public virtual EFCoreStat Convert(RawPokeApiStat stat) => StatConverter.Convert(stat);

    public virtual IReadOnlyList<EFCoreStat> Convert(IEnumerable<RawPokeApiStat> stats) => StatConverter.Convert(stats);

    public virtual IReadOnlyList<EFCoreStat> Convert(IEnumerable<RawPokeApiStat> stats, IEnumerable<RawPokeApiStatName> names) => StatConverter.Convert(stats, names);
}