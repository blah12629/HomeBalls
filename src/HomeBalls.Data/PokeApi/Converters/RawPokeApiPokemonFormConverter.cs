namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiPokemonFormConverter
{
    EFCorePokemonForm Convert(
        RawPokeApiPokemonForm form,
        RawPokeApiPokemon variant,
        RawPokeApiPokemonSpecies species,
        Int32 formIndex);

    IReadOnlyList<EFCorePokemonForm> Convert(
        IEnumerable<RawPokeApiPokemonForm> forms,
        IEnumerable<RawPokeApiPokemon> variants,
        IEnumerable<RawPokeApiPokemonSpecies> species);

    IReadOnlyList<EFCorePokemonForm> Convert(
        IEnumerable<RawPokeApiPokemonForm> forms,
        IEnumerable<RawPokeApiPokemon> variants,
        IEnumerable<RawPokeApiPokemonSpecies> species,
        IEnumerable<RawPokeApiPokemonAbility> abilities,
        IEnumerable<RawPokeApiPokemonEggGroup> eggGroups,
        IEnumerable<RawPokeApiPokemonType> types);

    IReadOnlyList<EFCorePokemonForm> Convert(
        IEnumerable<RawPokeApiPokemonForm> forms,
        IEnumerable<RawPokeApiPokemon> variants,
        IEnumerable<RawPokeApiPokemonSpecies> species,
        IEnumerable<RawPokeApiPokemonAbility> abilities,
        IEnumerable<RawPokeApiPokemonEggGroup> eggGroups,
        IEnumerable<RawPokeApiPokemonType> types,
        IEnumerable<RawPokeApiPokemonFormName> names);

    EFCorePokemonAbilitySlot Convert(RawPokeApiPokemonAbility ability);

    IReadOnlyList<EFCorePokemonAbilitySlot> Convert(
        IEnumerable<RawPokeApiPokemonAbility> abilities);

    EFCorePokemonEggGroupSlot Convert(RawPokeApiPokemonEggGroup eggGroup, Int32 index);

    IReadOnlyList<EFCorePokemonEggGroupSlot> Convert(
        IEnumerable<RawPokeApiPokemonEggGroup> eggGroups);

    EFCorePokemonTypeSlot Convert(RawPokeApiPokemonType type);

    IReadOnlyList<EFCorePokemonTypeSlot> Convert(
        IEnumerable<RawPokeApiPokemonType> types);
}

public class RawPokeApiPokemonFormConverter :
    RawPokeApiBaseNamedConverter,
    IRawPokeApiPokemonFormConverter
{
    public RawPokeApiPokemonFormConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    public virtual IReadOnlyList<EFCorePokemonForm> Convert(
        IEnumerable<RawPokeApiPokemonForm> forms,
        IEnumerable<RawPokeApiPokemon> variants,
        IEnumerable<RawPokeApiPokemonSpecies> species) =>
        Convert(forms, variants, species, out var lookup1, out var lookup2);

    public virtual IReadOnlyList<EFCorePokemonForm> Convert(
        IEnumerable<RawPokeApiPokemonForm> forms,
        IEnumerable<RawPokeApiPokemon> variants,
        IEnumerable<RawPokeApiPokemonSpecies> species,
        IEnumerable<RawPokeApiPokemonAbility> abilities,
        IEnumerable<RawPokeApiPokemonEggGroup> eggGroups,
        IEnumerable<RawPokeApiPokemonType> types) =>
        Convert(forms, variants, species, abilities, eggGroups, types, out var lookup);

    public virtual IReadOnlyList<EFCorePokemonForm> Convert(
        IEnumerable<RawPokeApiPokemonForm> forms,
        IEnumerable<RawPokeApiPokemon> variants,
        IEnumerable<RawPokeApiPokemonSpecies> species,
        IEnumerable<RawPokeApiPokemonAbility> abilities,
        IEnumerable<RawPokeApiPokemonEggGroup> eggGroups,
        IEnumerable<RawPokeApiPokemonType> types,
        IEnumerable<RawPokeApiPokemonFormName> names)
    {
        var converted = Convert(
            forms, variants, species,
            abilities, eggGroups, types,
            out var formsLookup);

        return AddNames(
            converted, names,
            name => formsLookup[name.PokemonFormId],
            record => (record.SpeciesId, record.FormId),
            names => NameConverter.Convert(names),
            (record, names) => record with { Names = names });
    }

    protected internal virtual IReadOnlyList<EFCorePokemonForm> Convert(
        IEnumerable<RawPokeApiPokemonForm> forms,
        IEnumerable<RawPokeApiPokemon> variants,
        IEnumerable<RawPokeApiPokemonSpecies> species,
        IEnumerable<RawPokeApiPokemonAbility> abilities,
        IEnumerable<RawPokeApiPokemonEggGroup> eggGroups,
        IEnumerable<RawPokeApiPokemonType> types,
        out IReadOnlyDictionary<UInt16, (UInt16 SpeciesId, Byte FormId)> formsLookup)
    {
        // NOTE: Each variant will always have 1 form
        // EXAMPLE: There is 1 form entry for Rotom-Wash pointing to
        //   1 variant entry for Rotom-Wash, pointing to the 
        //   main species entry for Rotom.
        // ASSUMPTION: IsDefault is always Form.Id==1

        var baseConverted = Convert(
            forms, variants, species,
            out formsLookup, out var variantsLookup);

        var (abilitiesLookup, eggGroupsLookup, typesLookup) = (
            ToDictionary(abilities, slot => slot.PokemonId, Convert),
            ToDictionary(eggGroups, slot => slot.SpeciesId, Convert),
            ToDictionary(types, slot => slot.PokemonId, Convert));

        return baseConverted
            .Select(form =>
            {
                var key = variantsLookup[(form.SpeciesId, form.FormId)];
                return form with
                {
                    Abilities = getOrEmpty(abilitiesLookup, key),
                    EggGroups = getOrEmpty(eggGroupsLookup, form.SpeciesId),
                    Types = getOrEmpty(typesLookup, key)
                };
            })
            .ToList();

        static IReadOnlyList<TValue> getOrEmpty<TKey, TValue>(
            IReadOnlyDictionary<TKey, IReadOnlyList<TValue>> dictionary,
            TKey key) =>
            dictionary.GetOrDefault(key) ?? new List<TValue> { };
    }

    protected internal virtual IReadOnlyList<EFCorePokemonForm> Convert(
        IEnumerable<RawPokeApiPokemonForm> forms,
        IEnumerable<RawPokeApiPokemon> variants,
        IEnumerable<RawPokeApiPokemonSpecies> species,
        out IReadOnlyDictionary<UInt16, (UInt16 SpeciesId, Byte FormId)> formIdToKeyPairLookup,
        out IReadOnlyDictionary<(UInt16 SpeciesId, Byte FormId), UInt16> variantIdToKeyPairLookup)
    {
        var variantLookup = variants.ToDictionary(variant => variant.Id, variant => variant);
        var speciesLookup = species.ToDictionary(species => species.Id, species => species);
        var formsDictionary = new Dictionary<UInt16, (UInt16, Byte)> { };
        var variantsDictionary = new Dictionary<(UInt16, Byte), UInt16> { };
        formIdToKeyPairLookup = formsDictionary.AsReadOnly();
        variantIdToKeyPairLookup = variantsDictionary.AsReadOnly();

        return forms
            .GroupBy(form => speciesLookup[variantLookup[form.PokemonId].SpeciesId].Id)
            .SelectMany(group => group
                .OrderBy(form => form.Order)
                    .ThenBy(form => form.FormOrder)
                    .ThenBy(form => form.Id)
                .Select((form, index) => Convert(
                    form, index,
                    variantLookup, speciesLookup,
                    formsDictionary, variantsDictionary)))
            .ToList();
    }

    protected internal virtual EFCorePokemonForm Convert(
        RawPokeApiPokemonForm form,
        Int32 groupIndex,
        IReadOnlyDictionary<UInt16, RawPokeApiPokemon> variantLookup,
        IReadOnlyDictionary<UInt16, RawPokeApiPokemonSpecies> speciesLookup,
        IDictionary<UInt16, (UInt16 SpeciesId, Byte FormId)> keyPairToFormIdLookup,
        IDictionary<(UInt16 SpeciesId, Byte FormId), UInt16> keyPairToVariantIdLookup)
    {
        var variant = variantLookup[form.PokemonId];
        var species = speciesLookup[variant.SpeciesId];
        var converted = Convert(form, variant, species, groupIndex);
        var key = (converted.SpeciesId, converted.FormId);
        keyPairToFormIdLookup.Add(form.Id, key);
        keyPairToVariantIdLookup.Add(key, variant.Id);
        return converted;
    }

    public virtual EFCorePokemonForm Convert(
        RawPokeApiPokemonForm form,
        RawPokeApiPokemon variant,
        RawPokeApiPokemonSpecies species,
        Int32 formIndex) =>
        new EFCorePokemonForm
        {
            FormId = Convert<Byte>(formIndex + 1),
            SpeciesId = species.Id,
            Identifier = form.Identifier,
            IsBaby = species.IsBaby,
            IsBattleOnly = form.IsBattleOnly,
            IsMega = form.IsMega,
            EvolvesFromSpeciesId = species.EvolvesFromSpeciesId,
            EvolvesFromFormId = species.EvolvesFromSpeciesId.HasValue ? 1 : default(Byte?),
        };

    public virtual EFCorePokemonAbilitySlot Convert(
        RawPokeApiPokemonAbility ability) =>
        new EFCorePokemonAbilitySlot
        {
            AbilityId = ability.AbilityId,
            Slot = ability.Slot,
            IsHidden = ability.IsHidden
        };

    public virtual IReadOnlyList<EFCorePokemonAbilitySlot> Convert(
        IEnumerable<RawPokeApiPokemonAbility> abilities) =>
        ConvertCollection(abilities, Convert);

    public virtual EFCorePokemonEggGroupSlot Convert(
        RawPokeApiPokemonEggGroup eggGroup,
        Int32 index) =>
        new EFCorePokemonEggGroupSlot
        {
            EggGroupId = eggGroup.EggGroupId,
            Slot = Convert<Byte>(index + 1)
        };

    public virtual IReadOnlyList<EFCorePokemonEggGroupSlot> Convert(
        IEnumerable<RawPokeApiPokemonEggGroup> eggGroups) =>
        ConvertCollection(eggGroups, Convert);

    public virtual EFCorePokemonTypeSlot Convert(
        RawPokeApiPokemonType type) =>
        new EFCorePokemonTypeSlot
        {
            TypeId = ToTypeId(type.TypeId),
            Slot = type.Slot
        };

    public virtual IReadOnlyList<EFCorePokemonTypeSlot> Convert(
        IEnumerable<RawPokeApiPokemonType> types) =>
        ConvertCollection(types, Convert);
}