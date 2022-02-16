namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiPokemonConverter
{
    IReadOnlyList<HomeBallsPokemonForm> Convert(IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiEvolutionChain> chains);

    HomeBallsPokemonForm Convert(RawPokeApiPokemonForm form, HomeBalls.Entities.HomeBallsPokemonFormKey id, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiEvolutionChain> chains);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonFormName> names, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants);

    HomeBallsString Convert(RawPokeApiPokemonFormName name, HomeBalls.Entities.HomeBallsPokemonFormKey id);

    IReadOnlyList<HomeBallsPokemonSpecies> Convert(IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species);

    HomeBallsPokemonSpecies Convert(RawPokeApiPokemonSpecies species);

    IReadOnlyList<HomeBallsString> Convert(IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonSpeciesName> names);

    HomeBallsString Convert(RawPokeApiPokemonSpeciesName name);

    IReadOnlyList<HomeBallsPokemonAbilitySlot> Convert(IHomeBallsReadOnlyDataSet<(UInt16, UInt16), RawPokeApiPokemonAbility> pokemonAbilities, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants);

    HomeBallsPokemonAbilitySlot Convert(RawPokeApiPokemonAbility join, HomeBalls.Entities.HomeBallsPokemonFormKey id);

    IReadOnlyList<HomeBallsPokemonEggGroupSlot> Convert(IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonEggGroup> pokemonEggGroups, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants);

    HomeBallsPokemonEggGroupSlot Convert(RawPokeApiPokemonEggGroup join, HomeBalls.Entities.HomeBallsPokemonFormKey id, Byte slot);

    IReadOnlyList<HomeBallsPokemonTypeSlot> Convert(IHomeBallsReadOnlyDataSet<(UInt16, UInt16), RawPokeApiPokemonType> pokemonTypes, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms, IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants);

    HomeBallsPokemonTypeSlot Convert(RawPokeApiPokemonType join, HomeBalls.Entities.HomeBallsPokemonFormKey id);
}

public class RawPokeApiPokemonConverter :
    RawPokeApiBaseConverter, IRawPokeApiPokemonConverter
{
    public RawPokeApiPokemonConverter(
        ILogger? logger = default) :
        base(logger) { }

    protected internal virtual IReadOnlyDictionary<UInt16, HomeBalls.Entities.HomeBallsPokemonFormKey> CreateFormKeyFormMap(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        forms.GroupBy(form => (UInt16)variants[form.PokemonId].SpeciesId)
            .SelectMany(group => group
                .OrderBy(form => form.Order)
                .Select((form, index) => (
                    RawId: form.Id,
                    Id: new HomeBalls.Entities.HomeBallsPokemonFormKey
                    {
                        SpeciesId = group.Key,
                        FormId = (Byte)(index + 1)
                    })))
            .ToDictionary(pair => pair.RawId, pair => pair.Id).AsReadOnly();

    protected internal virtual IReadOnlyDictionary<UInt16, IReadOnlyCollection<HomeBalls.Entities.HomeBallsPokemonFormKey>> CreateFormKeyVariantMap(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        CreateFormKeyVariantMap(CreateFormKeyFormMap(forms, variants), forms, variants);

    protected internal virtual IReadOnlyDictionary<UInt16, IReadOnlyCollection<HomeBalls.Entities.HomeBallsPokemonFormKey>> CreateFormKeyVariantMap(
        IReadOnlyDictionary<UInt16, HomeBalls.Entities.HomeBallsPokemonFormKey> formMap,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        formMap.Select(pair => (Id: pair.Value, RawId: forms[pair.Key].PokemonId))
            .GroupBy(pair => pair.RawId)
            .ToDictionary(
                group => group.Key,
                group => (IReadOnlyCollection<HomeBalls.Entities.HomeBallsPokemonFormKey>)group
                    .Select(pair => pair.Id).ToList().AsReadOnly())
            .AsReadOnly();

    protected internal virtual IReadOnlyDictionary<UInt16, IReadOnlyCollection<HomeBalls.Entities.HomeBallsPokemonFormKey>> CreateFormKeySpeciesMap(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        CreateFormKeySpeciesMap(CreateFormKeyFormMap(forms, variants), forms, variants);

    protected internal virtual IReadOnlyDictionary<UInt16, IReadOnlyCollection<HomeBalls.Entities.HomeBallsPokemonFormKey>> CreateFormKeySpeciesMap(
        IReadOnlyDictionary<UInt16, HomeBalls.Entities.HomeBallsPokemonFormKey> formMap,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        CreateFormKeySpeciesMap(CreateFormKeyVariantMap(formMap, forms, variants), forms, variants);

    protected internal virtual IReadOnlyDictionary<UInt16, IReadOnlyCollection<HomeBalls.Entities.HomeBallsPokemonFormKey>> CreateFormKeySpeciesMap(
        IReadOnlyDictionary<UInt16, IReadOnlyCollection<HomeBalls.Entities.HomeBallsPokemonFormKey>> variantMap,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants) =>
        variantMap.GroupBy(pair => variants[pair.Key].SpeciesId).ToDictionary(
            group => group.Key,
            group => (IReadOnlyCollection<HomeBalls.Entities.HomeBallsPokemonFormKey>)group
                .SelectMany(key => key.Value)
                .ToList().AsReadOnly());

    public virtual IReadOnlyList<HomeBallsPokemonForm> Convert(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiEvolutionChain> chains)
    {
        var indexMap = CreateFormKeyFormMap(forms, variants);
        return ConvertList(forms, form =>
            Convert(form, indexMap[form.Id], variants, species, chains));
    }

    protected internal virtual HomeBallsPokemonForm FormatFormIdentifier(
        RawPokeApiPokemonForm rawForm,
        HomeBallsPokemonForm form) =>
        rawForm.Identifier == "darmanitan-standard-galar" ?
            form with { FormIdentifier = "standard\tgalar" } :
        rawForm.Identifier == "darmanitan-zen-galar" ?
            form with { FormIdentifier = "zen\tgalar" } :
        rawForm.Identifier == "toxtricity-amped-gmax" ?
            form with { FormIdentifier = "gmax\tamped" } :
        rawForm.Identifier == "toxtricity-low-key-gmax" ?
            form with { FormIdentifier = "gmax\tlow-key" } :
        rawForm.Identifier == "urshifu-single-strike-gmax" ?
            form with { FormIdentifier = "gmax\tsingle-strike" } :
        rawForm.Identifier == "urshifu-rapid-strike-gmax" ?
            form with { FormIdentifier = "gmax\trapid-strike" } :
        form;

    public virtual HomeBallsPokemonForm Convert(
        RawPokeApiPokemonForm form,
        HomeBalls.Entities.HomeBallsPokemonFormKey id,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiEvolutionChain> chains)
    {
        var rawVariant = variants[form.PokemonId];
        var rawSpecies = species[rawVariant.SpeciesId];
        var chainId = rawSpecies.EvolutionChainId;
        var rawChain = chainId.HasValue ? chains[chainId.Value] : default;

        var preevolutionSpeciesId = rawSpecies.EvolvesFromSpeciesId;
        var formIdentifier = form.FormIdentifier;
        if (String.IsNullOrWhiteSpace(formIdentifier)) formIdentifier = default;

        var entity = new HomeBallsPokemonForm
        {
            BabyTriggerId = rawChain?.BabyTriggerItemId ?? default(UInt16?),
            EvolvesFromFormId = preevolutionSpeciesId.HasValue ? 1 : default(Byte?),
            EvolvesFromSpeciesId = preevolutionSpeciesId,
            FormId = id.FormId,
            FormIdentifier = formIdentifier,
            IsBaby = rawSpecies.IsBaby,
            IsBattleOnly = form.IsBattleOnly,
            IsMega = form.IsMega,
            SpeciesId = id.SpeciesId,
            SpeciesIdentifier = rawSpecies.Identifier,
        };
        return FormatFormIdentifier(form, entity);
    }

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonFormName> names,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants)
    {
        var indexMap = CreateFormKeyFormMap(forms, variants);
        return ConvertList(names, name => Convert(name, indexMap[name.PokemonFormId]));
    }

    public virtual HomeBallsString Convert(
        RawPokeApiPokemonFormName name,
        HomeBalls.Entities.HomeBallsPokemonFormKey id) =>
        new HomeBallsString
        {
            LanguageId = name.LocalLanguageId,
            NameOfPokemonFormFormId = id.FormId,
            NameOfPokemonFormSpeciesId = id.SpeciesId,
            Value = $"{name.PokemonName}\t{name.FormName}"
        };

    public virtual IReadOnlyList<HomeBallsPokemonSpecies> Convert(
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonSpecies> species) =>
        ConvertList(species, Convert);

    public virtual HomeBallsPokemonSpecies Convert(RawPokeApiPokemonSpecies species) =>
        new HomeBallsPokemonSpecies
        {
            GenderRate = species.GenderRate,
            Id = species.Id,
            Identifier = species.Identifier,
            IsFormSwitchable = species.FormsSwitchable
        };

    public virtual IReadOnlyList<HomeBallsString> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonSpeciesName> names) =>
        ConvertList(names, Convert);

    public virtual HomeBallsString Convert(RawPokeApiPokemonSpeciesName name) =>
        ConvertName<RawPokeApiPokemonSpeciesName, UInt16>(name) with
        {
            NameOfPokemonSpeciesId = name.PokemonSpeciesId
        };

    public virtual IReadOnlyList<HomeBallsPokemonAbilitySlot> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, UInt16), RawPokeApiPokemonAbility> pokemonAbilities,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants)
    {
        var variantMap = CreateFormKeyVariantMap(forms, variants);
        return pokemonAbilities
            .SelectMany(join => variantMap[join.PokemonId].Select(id => Convert(join, id)))
            .ToList().AsReadOnly();
    }

    public virtual HomeBallsPokemonAbilitySlot Convert(
        RawPokeApiPokemonAbility join,
        HomeBalls.Entities.HomeBallsPokemonFormKey id) =>
        new HomeBallsPokemonAbilitySlot
        {
            FormId = id.FormId,
            Slot = join.Slot,
            SpeciesId = id.SpeciesId,
            AbilityId = join.AbilityId
        };

    public virtual IReadOnlyList<HomeBallsPokemonEggGroupSlot> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, Byte), RawPokeApiPokemonEggGroup> pokemonEggGroups,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants)
    {
        var speciesMap = CreateFormKeySpeciesMap(forms, variants);
        return pokemonEggGroups
            .SelectMany(join => speciesMap[join.SpeciesId]
                .Select((id, index) => Convert(join, id, (Byte)(index + 1))))
            .ToList().AsReadOnly();
    }

    public virtual HomeBallsPokemonEggGroupSlot Convert(
        RawPokeApiPokemonEggGroup join,
        HomeBalls.Entities.HomeBallsPokemonFormKey id,
        Byte slot) =>
        new HomeBallsPokemonEggGroupSlot
        {
            FormId = id.FormId,
            EggGroupId = join.EggGroupId,
            SpeciesId = id.SpeciesId,
            Slot = slot
        };

    public virtual IReadOnlyList<HomeBallsPokemonTypeSlot> Convert(
        IHomeBallsReadOnlyDataSet<(UInt16, UInt16), RawPokeApiPokemonType> pokemonTypes,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemonForm> forms,
        IHomeBallsReadOnlyDataSet<UInt16, RawPokeApiPokemon> variants)
    {
        var variantsMap = CreateFormKeyVariantMap(forms, variants);
        return pokemonTypes
            .SelectMany(join => variantsMap[join.PokemonId].Select(id => Convert(join, id)))
            .ToList().AsReadOnly();
    }

    public virtual HomeBallsPokemonTypeSlot Convert(
        RawPokeApiPokemonType join,
        HomeBalls.Entities.HomeBallsPokemonFormKey id) =>
        new HomeBallsPokemonTypeSlot
        {
            FormId = id.FormId,
            Slot = join.Slot,
            SpeciesId = id.SpeciesId,
            TypeId = ToTypeId(join.TypeId)
        };
}