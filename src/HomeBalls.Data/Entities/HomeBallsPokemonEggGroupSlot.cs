#nullable disable

namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsPokemonEggGroupSlot :
        HomeBalls.Entities.HomeBallsPokemonEggGroupSlot,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsPokemonEggGroupSlot>
    {
        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsPokemonEggGroupSlot);

        public virtual HomeBallsPokemonEggGroup EggGroup { get; init; }

        public virtual UInt16 SpeciesId { get; init; }

        public virtual HomeBallsPokemonSpecies Species { get; init; }

        public virtual Byte FormId { get; init; }

        public virtual HomeBallsPokemonForm Form { get; init; }

        public virtual HomeBalls.Entities.HomeBallsPokemonEggGroupSlot ToBaseType() =>
            this.Adapt<HomeBalls.Entities.HomeBallsPokemonEggGroupSlot>();
    }
}

#nullable enable

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsPokemonEggGroupSlotConfiguration :
        HomeBallsEntityConfiguration<HomeBallsPokemonEggGroupSlot>
    {
        public HomeBallsPokemonEggGroupSlotConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureEggGroup();
            ConfigureSpecies();
            ConfigureForm();
        }

        protected internal override void ConfigureKey() =>
            Builder.HasKey(slot => new { slot.EggGroupId, slot.SpeciesId, slot.FormId });

        protected internal virtual void ConfigureEggGroup() =>
            Builder.HasOne(slot => slot.EggGroup)
                .WithMany(ability => ability.OnPokemon)
                .HasForeignKey(slot => slot.EggGroupId);

        protected internal virtual void ConfigureSpecies() =>
            Builder.HasOne(slot => slot.Species)
                .WithMany(species => species.EggGroups)
                .HasForeignKey(slot => slot.SpeciesId);

        protected internal virtual void ConfigureForm() =>
            Builder.HasOne(slot => slot.Form)
                .WithMany(form => form.EggGroups)
                .HasForeignKey(slot => new { slot.SpeciesId, slot.FormId });
    }
}