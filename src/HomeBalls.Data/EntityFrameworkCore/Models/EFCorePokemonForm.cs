namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCorePokemonForm :
        EFCoreNamedRecord<HomeBallsPokemonFormKey>,
        IHomeBallsPokemonForm
    {
        #nullable disable
        public EFCorePokemonForm() { }
        #nullable enable

        public virtual UInt16 SpeciesId
        {
            get => Id.SpeciesId;
            init => Id = new(value, FormId);
        }

        public virtual EFCorePokemonSpecies Species { get; init; }

        public virtual Byte FormId
        {
            get => Id.FormId;
            init => Id = new(SpeciesId, value);
        }

        public virtual Boolean IsBreedable { get; init; }

        public virtual Boolean IsBaby { get; init; }

        public virtual Boolean IsBattleOnly { get; init; }

        public virtual Boolean IsMega { get; init; }

        public virtual UInt16? EvolvesFromSpeciesId { get; init; }

        public virtual Byte? EvolvesFromFormId { get; init; }

        public virtual EFCorePokemonForm? EvolvesFrom { get; init; }

        public virtual IEnumerable<EFCorePokemonForm> EvolvesTo { get; init; } =
            new List<EFCorePokemonForm> { };

        public virtual IEnumerable<EFCorePokemonAbilitySlot> Abilities { get; init; } =
            new List<EFCorePokemonAbilitySlot> { };

        public virtual IEnumerable<EFCorePokemonTypeSlot> Types { get; init; } =
            new List<EFCorePokemonTypeSlot> { };

        public virtual IEnumerable<EFCorePokemonEggGroupSlot> EggGroups { get; init; } =
            new List<EFCorePokemonEggGroupSlot> { };

        public virtual UInt16? BabyTriggerId { get; init; }

        public virtual EFCoreItem? BabyTrigger { get; init; }

        public virtual IEnumerable<EFCoreEntryLegality> LegalOn { get; init; } =
            new List<EFCoreEntryLegality> { };

        IEnumerable<IHomeBallsPokemonTypeSlot> IHomeBallsPokemonForm.Types => Types;

        IEnumerable<IHomeBallsPokemonAbilitySlot> IHomeBallsPokemonForm.Abilities => Abilities;

        IEnumerable<IHomeBallsPokemonEggGroupSlot> IHomeBallsPokemonForm.EggGroups => EggGroups;
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCorePokemonFormConfiguration :
        EFCoreRecordConfiguration<EFCorePokemonForm>
    {
        public EFCorePokemonFormConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureId();
            ConfigureSpecies();
            ConfigureEvolvesFrom();
            ConfigureEvolvesTo();
            ConfigureAbilities();
            ConfigureTypes();
            ConfigureEggGroups();
            ConfigureBabyTrigger();
            ConfigureLegalOn();
        }

        protected internal override void ConfigureKey() =>
            ConfigureLogged(() => Builder
                .HasKey(form => new { form.SpeciesId, form.FormId }));

        protected internal virtual void ConfigureId() =>
            ConfigureLogged(() => Builder
                .Ignore(form => form.Id));

        protected internal virtual void ConfigureSpecies() =>
            ConfigureLogged(() => Builder
                .HasOne(form => form.Species)
                .WithMany(species => species.Forms)
                .HasForeignKey(form => form.SpeciesId));

        protected internal virtual void ConfigureEvolvesFrom() =>
            ConfigureLogged(() => Builder
                .HasOne(form => form.EvolvesFrom)
                .WithMany(preevolution => preevolution.EvolvesTo)
                .HasForeignKey(form => new { form.EvolvesFromSpeciesId, form.EvolvesFromFormId }));

        protected internal virtual void ConfigureEvolvesTo() =>
            ConfigureLogged(() => Builder
                .HasMany(form => form.EvolvesTo)
                .WithOne(evolution => evolution.EvolvesFrom)
                .HasForeignKey(evolution => new { evolution.EvolvesFromSpeciesId, evolution.EvolvesFromFormId }));

        protected internal virtual void ConfigureAbilities() =>
            ConfigureLogged(() => Builder
                .HasMany(form => form.Abilities)
                .WithOne(ability => ability.Form)
                .HasForeignKey(ability => new { ability.SpeciesId, ability.FormId }));

        protected internal virtual void ConfigureTypes() =>
            ConfigureLogged(() => Builder
                .HasMany(form => form.Types)
                .WithOne(group => group.Form)
                .HasForeignKey(group => new { group.SpeciesId, group.FormId }));

        protected internal virtual void ConfigureEggGroups() =>
            ConfigureLogged(() => Builder
                .HasMany(form => form.EggGroups)
                .WithOne(group => group.Form)
                .HasForeignKey(group => new { group.SpeciesId, group.FormId }));

        protected internal virtual void ConfigureBabyTrigger() =>
            ConfigureLogged(() => Builder
                .HasOne(form => form.BabyTrigger)
                .WithMany(item => item.BabyTriggerFor)
                .HasForeignKey(form => form.BabyTriggerId));

        protected internal virtual void ConfigureLegalOn() =>
            ConfigureLogged(() => Builder
                .HasMany(form => form.LegalOn)
                .WithOne(legality => legality.Form)
                .HasForeignKey(legality => new { legality.SpeciesId, legality.FormId }));
    }
}