namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsPokemonAbility :
        HomeBalls.Entities.HomeBallsPokemonAbility,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsPokemonAbility>
    {
        public HomeBallsPokemonAbility()
        {
            base.Names = new List<HomeBallsString> { };
            OnPokemon = new List<HomeBallsPokemonAbilitySlot>();
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsPokemonAbility);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual IEnumerable<HomeBallsPokemonAbilitySlot> OnPokemon { get; init; }

        public virtual HomeBalls.Entities.HomeBallsPokemonAbility ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsPokemonAbility>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsPokemonAbilityConfiguration :
        HomeBallsEntityConfiguration<HomeBallsPokemonAbility>
    {
        public HomeBallsPokemonAbilityConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureOnPokemon();
        }

        protected internal virtual void ConfigureOnPokemon() =>
            Builder.HasMany(ability => ability.OnPokemon)
                .WithOne(slot => slot.Ability)
                .HasForeignKey(slot => slot.AbilityId);
    }
}