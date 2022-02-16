#nullable disable

namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsPokemonTypeSlot :
        HomeBalls.Entities.HomeBallsPokemonTypeSlot,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsPokemonTypeSlot>
    {
        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsPokemonTypeSlot);

        public virtual HomeBallsType Type { get; init; }

        public virtual UInt16 SpeciesId { get; init; }

        public virtual HomeBallsPokemonSpecies Species { get; init; }

        public virtual Byte FormId { get; init; }

        public virtual HomeBallsPokemonForm Form { get; init; }

        public virtual HomeBalls.Entities.HomeBallsPokemonTypeSlot ToBaseType() =>
            this.Adapt<HomeBalls.Entities.HomeBallsPokemonTypeSlot>();
    }
}

#nullable enable

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsPokemonTypeSlotConfiguration :
        HomeBallsEntityConfiguration<HomeBallsPokemonTypeSlot>
    {
        public HomeBallsPokemonTypeSlotConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureType();
            ConfigureSpecies();
            ConfigureForm();
        }

        protected internal override void ConfigureKey() =>
            Builder.HasKey(slot => new { slot.TypeId, slot.SpeciesId, slot.FormId });

        protected internal virtual void ConfigureType() =>
            Builder.HasOne(slot => slot.Type)
                .WithMany(ability => ability.OnPokemon)
                .HasForeignKey(slot => slot.TypeId);

        protected internal virtual void ConfigureSpecies() =>
            Builder.HasOne(slot => slot.Species)
                .WithMany(species => species.Types)
                .HasForeignKey(slot => slot.SpeciesId);

        protected internal virtual void ConfigureForm() =>
            Builder.HasOne(slot => slot.Form)
                .WithMany(form => form.Types)
                .HasForeignKey(slot => new { slot.SpeciesId, slot.FormId });
    }
}