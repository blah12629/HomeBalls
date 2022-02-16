namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsPokemonSpecies :
        HomeBalls.Entities.HomeBallsPokemonSpecies,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsPokemonSpecies>
    {
        public HomeBallsPokemonSpecies()
        {
            base.Names = new List<HomeBallsString> { };
            Forms = new List<HomeBallsPokemonForm> { };
            Types = new List<HomeBallsPokemonTypeSlot> { };
            Abilities = new List<HomeBallsPokemonAbilitySlot> { };
            EggGroups = new List<HomeBallsPokemonEggGroupSlot> { };
            EvolvesInto = new List<HomeBallsPokemonForm> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsPokemonSpecies);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual IEnumerable<HomeBallsPokemonForm> Forms { get; init; }

        public virtual IEnumerable<HomeBallsPokemonTypeSlot> Types { get; init; }

        public virtual IEnumerable<HomeBallsPokemonAbilitySlot> Abilities { get; init; }

        public virtual IEnumerable<HomeBallsPokemonEggGroupSlot> EggGroups { get; init; }

        public virtual IEnumerable<HomeBallsPokemonForm> EvolvesInto { get; init; }

        public virtual HomeBalls.Entities.HomeBallsPokemonSpecies ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsPokemonSpecies>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsPokemonSpeciesConfiguration :
        HomeBallsEntityConfiguration<HomeBallsPokemonSpecies>
    {
        public HomeBallsPokemonSpeciesConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureForms();
            ConfigureTypes();
            ConfigureAbilities();
            ConfigureEggGroups();
            ConfigureEvolvesInto();
        }

        protected internal virtual void ConfigureForms() =>
            Builder.HasMany(species => species.Forms)
                .WithOne(form => form.Species)
                .HasForeignKey(fomr => fomr.SpeciesId);

        protected internal virtual void ConfigureTypes() =>
            Builder.HasMany(species => species.Types)
                .WithOne(type => type.Species)
                .HasForeignKey(type => type.SpeciesId);

        protected internal virtual void ConfigureAbilities() =>
            Builder.HasMany(species => species.Abilities)
                .WithOne(ability => ability.Species)
                .HasForeignKey(ability => ability.SpeciesId);

        protected internal virtual void ConfigureEggGroups() =>
            Builder.HasMany(species => species.EggGroups)
                .WithOne(group => group.Species)
                .HasForeignKey(group => group.SpeciesId);

        protected internal virtual void ConfigureEvolvesInto()  =>
            Builder.HasMany(species => species.EvolvesInto)
                .WithOne(evolution => evolution.EvolvesFromSpecies)
                .HasForeignKey(evolution => evolution.EvolvesFromSpeciesId);
    }
}