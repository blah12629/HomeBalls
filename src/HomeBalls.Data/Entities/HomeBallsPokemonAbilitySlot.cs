#nullable disable

namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsPokemonAbilitySlot :
        HomeBalls.Entities.HomeBallsPokemonAbilitySlot,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsPokemonAbilitySlot>
    {
        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsPokemonAbilitySlot);

        public virtual HomeBallsPokemonAbility Ability { get; init; }

        public virtual UInt16 SpeciesId { get; init; }

        public virtual HomeBallsPokemonSpecies Species { get; init; }

        public virtual Byte FormId { get; init; }

        public virtual HomeBallsPokemonForm Form { get; init; }

        public virtual HomeBalls.Entities.HomeBallsPokemonAbilitySlot ToBaseType() =>
            this.Adapt<HomeBalls.Entities.HomeBallsPokemonAbilitySlot>();
    }
}

#nullable enable

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsPokemonAbilitySlotConfiguration :
        HomeBallsEntityConfiguration<HomeBallsPokemonAbilitySlot>
    {
        public HomeBallsPokemonAbilitySlotConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureAbility();
            ConfigureSpecies();
            ConfigureForm();
        }

        protected internal override void ConfigureKey() =>
            Builder.HasKey(slot => new { slot.AbilityId, slot.SpeciesId, slot.FormId });

        protected internal virtual void ConfigureAbility() =>
            Builder.HasOne(slot => slot.Ability)
                .WithMany(ability => ability.OnPokemon)
                .HasForeignKey(slot => slot.AbilityId);

        protected internal virtual void ConfigureSpecies() =>
            Builder.HasOne(slot => slot.Species)
                .WithMany(species => species.Abilities)
                .HasForeignKey(slot => slot.SpeciesId);

        protected internal virtual void ConfigureForm() =>
            Builder.HasOne(slot => slot.Form)
                .WithMany(form => form.Abilities)
                .HasForeignKey(slot => new { slot.SpeciesId, slot.FormId });
    }
}