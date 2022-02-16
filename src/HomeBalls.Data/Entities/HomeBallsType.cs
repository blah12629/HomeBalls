namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsType :
        HomeBalls.Entities.HomeBallsType,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsType>
    {
        public HomeBallsType()
        {
            base.Names = new List<HomeBallsString> { };
            OnPokemon = new List<HomeBallsPokemonTypeSlot> { };
            Moves = new List<HomeBallsMove> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsType);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual IEnumerable<HomeBallsPokemonTypeSlot> OnPokemon { get; init; }

        public virtual IEnumerable<HomeBallsMove> Moves { get; init; }

        public virtual HomeBalls.Entities.HomeBallsType ToBaseType() =>
            this.Adapt<HomeBalls.Entities.HomeBallsType>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsTypeConfiguration :
        HomeBallsEntityConfiguration<HomeBallsType>
    {
        public HomeBallsTypeConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureOnPokemon();
            ConfigureMoves();
        }

        protected internal virtual void ConfigureOnPokemon() =>
            Builder.HasMany(type => type.OnPokemon)
                .WithOne(slot => slot.Type)
                .HasForeignKey(slot => slot.TypeId);

        protected internal virtual void ConfigureMoves() =>
            Builder.HasMany(type => type.Moves)
                .WithOne(move => move.Type)
                .HasForeignKey(move => move.TypeId);
    }
}