namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsPokemonForm :
        HomeBalls.Entities.HomeBallsPokemonForm,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsPokemonForm>
    {
        #nullable disable
        UInt16? _evolvesFromSpeciesId;
        Byte? _evolvesFromFormId;

        public HomeBallsPokemonForm()
        {
            base.Names = new List<HomeBallsString> { };
            base.Abilities = new List<HomeBallsPokemonAbilitySlot> { };
            base.EggGroups = new List<HomeBallsPokemonEggGroupSlot> { };
            base.Types = new List<HomeBallsPokemonTypeSlot> { };
            EvolvesInto = new List<HomeBallsPokemonForm> { };
            LegalOn = new List<HomeBallsEntryLegality> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsPokemonForm);

        public virtual UInt16 SpeciesId
        {
            get => Id.SpeciesId;
            init => Id = (value, FormId);
        }

        public virtual HomeBallsPokemonSpecies Species { get; init; }

        public virtual Byte FormId
        {
            get => Id.FormId;
            init => Id = (SpeciesId, value);
        }

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        new public virtual IEnumerable<HomeBallsPokemonTypeSlot> Types
        {
            get => (IEnumerable<HomeBallsPokemonTypeSlot>)base.Types;
            init => base.Types = value;
        }

        new public virtual IEnumerable<HomeBallsPokemonAbilitySlot> Abilities
        {
            get => (IEnumerable<HomeBallsPokemonAbilitySlot>)base.Abilities;
            init => base.Abilities = value;
        }

        new public virtual IEnumerable<HomeBallsPokemonEggGroupSlot> EggGroups
        {
            get => (IEnumerable<HomeBallsPokemonEggGroupSlot>)base.EggGroups;
            init => base.EggGroups = value;
        }
            
        public virtual UInt16? EvolvesFromSpeciesId
        {
            get => EvolvesFromId?.SpeciesId;
            init => EvolvesFromId = SetThenCreateEvolvesFromId(ref _evolvesFromSpeciesId, value);
        }

        #nullable enable

        public virtual HomeBallsPokemonSpecies? EvolvesFromSpecies { get; init; }

        public virtual Byte? EvolvesFromFormId
        {
            get => EvolvesFromId?.FormId;
            init => EvolvesFromId = SetThenCreateEvolvesFromId(ref _evolvesFromFormId, value);
        }

        public virtual HomeBallsPokemonForm? EvolvesFrom { get; init; }

        public virtual IEnumerable<HomeBallsPokemonForm> EvolvesInto { get; init; }

        public virtual Boolean IsBaby { get; init; }

        public virtual Boolean IsBattleOnly { get; init; }

        public virtual Boolean IsMega { get; init; }

        public virtual HomeBallsItem? BabyTrigger { get; init; }

        public virtual IEnumerable<HomeBallsEntryLegality> LegalOn { get; init; }

        public virtual HomeBalls.Entities.HomeBallsPokemonForm ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsPokemonForm>() with
            {
                Abilities = Abilities.Select(ability => ability.ToBaseType()).ToList().AsReadOnly(),
                EggGroups = EggGroups.Select(group => group.ToBaseType()).ToList().AsReadOnly(),
                Types = Types.Select(type => type.ToBaseType()).ToList().AsReadOnly()
            };

        protected virtual HomeBalls.Entities.HomeBallsPokemonFormKey? SetThenCreateEvolvesFromId<TId>(
            ref TId? field,
            TId? value)
            where TId : struct, INumber<TId>
        {
            field = value;
            return EvolvesFromSpeciesId.HasValue && EvolvesFromFormId.HasValue ?
                (EvolvesFromSpeciesId.Value, EvolvesFromFormId.Value) :
                default(HomeBalls.Entities.HomeBallsPokemonFormKey);
        }
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsPokemonFormConfiguration :
        HomeBallsEntityConfiguration<HomeBallsPokemonForm>
    {
        public HomeBallsPokemonFormConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureId();
            ConfigureSpecies();
            ConfigureTypes();
            ConfigureAbilities();
            ConfigureEggGroups();
            ConfigureEvolvesFromSpecies();
            ConfigureEvolvesFrom();
            ConfigureEvolvesFromId();
            ConfigureEvolvesInto();
            ConfigureBabyTrigger();
            ConfigureLegalOn();
            ConfigureProjectPokemonHomeSpriteId();
        }

        protected internal override void ConfigureKey() =>
            Builder.HasKey(form => new { form.SpeciesId, form.FormId });

        protected internal override void ConfiugreIdentifier() { }

        protected internal virtual void ConfigureId() =>
            Builder.Ignore(form => form.Id);

        protected internal virtual void ConfigureSpecies() =>
            Builder.HasOne(form => form.Species)
                .WithMany(species => species.Forms)
                .HasForeignKey(form => form.SpeciesId);

        protected internal virtual void ConfigureTypes() =>
            Builder.HasMany(form => form.Types)
                .WithOne(type => type.Form)
                .HasForeignKey(type => new { type.SpeciesId, type.FormId });

        protected internal virtual void ConfigureAbilities() =>
            Builder.HasMany(form => form.Abilities)
                .WithOne(ability => ability.Form)
                .HasForeignKey(ability => new { ability.SpeciesId, ability.FormId });

        protected internal virtual void ConfigureEggGroups() =>
            Builder.HasMany(form => form.EggGroups)
                .WithOne(group => group.Form)
                .HasForeignKey(group => new { group.SpeciesId, group.FormId });

        protected internal virtual void ConfigureEvolvesFromSpecies() =>
            Builder.HasOne(form => form.EvolvesFromSpecies)
                .WithMany(species => species.EvolvesInto)
                .HasForeignKey(form => form.EvolvesFromSpeciesId);

        protected internal virtual void ConfigureEvolvesFrom() =>
            Builder.HasOne(form => form.EvolvesFrom)
                .WithMany(form => form.EvolvesInto)
                .HasForeignKey(form => new
                {
                    form.EvolvesFromSpeciesId,
                    form.EvolvesFromFormId
                });

        protected internal virtual void ConfigureEvolvesFromId() =>
            Builder.Ignore(form => form.EvolvesFromId);

        protected internal virtual void ConfigureEvolvesInto() =>
            Builder.HasMany(form => form.EvolvesInto)
                .WithOne(form => form.EvolvesFrom)
                .HasForeignKey(form => new
                {
                    form.EvolvesFromSpeciesId,
                    form.EvolvesFromFormId
                });

        protected internal virtual void ConfigureBabyTrigger() =>
            Builder.HasOne(form => form.BabyTrigger)
                .WithMany(item => item.BabyTriggerFor)
                .HasForeignKey(form => form.BabyTriggerId);

        protected internal virtual void ConfigureLegalOn() =>
            Builder.HasMany(form => form.LegalOn)
                .WithOne(legality => legality.PokemonForm)
                .HasForeignKey(legality => new
                {
                    legality.SpeciesId,
                    legality.FormId
                });

        protected internal virtual void ConfigureProjectPokemonHomeSpriteId() =>
            Builder.Property(form => form.ProjectPokemonHomeSpriteId)
                .IsRequired(false);
    }
}