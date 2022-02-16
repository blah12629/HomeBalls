namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsPokemonEggGroup :
        HomeBalls.Entities.HomeBallsPokemonEggGroup,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsPokemonEggGroup>
    {
        public HomeBallsPokemonEggGroup()
        {
            base.Names = new List<HomeBallsString> { };
            OnPokemon = new List<HomeBallsPokemonEggGroupSlot> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsPokemonEggGroup);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual IEnumerable<HomeBallsPokemonEggGroupSlot> OnPokemon { get; init; }

        public virtual HomeBalls.Entities.HomeBallsPokemonEggGroup ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsPokemonEggGroup>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsPokemonEggGroupConfiguration :
        HomeBallsEntityConfiguration<HomeBallsPokemonEggGroup>
    {
        public HomeBallsPokemonEggGroupConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureOnPokemon();
        }

        protected internal virtual void ConfigureOnPokemon() =>
            Builder.HasMany(group => group.OnPokemon)
                .WithOne(slot => slot.EggGroup)
                .HasForeignKey(slot => slot.EggGroupId);
    }
}