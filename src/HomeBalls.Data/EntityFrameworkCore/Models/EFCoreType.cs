namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreType :
        EFCoreNamedRecord<Byte>,
        IHomeBallsType
    {
        public virtual IEnumerable<EFCorePokemonTypeSlot> OnForms { get; init; } =
            new List<EFCorePokemonTypeSlot> { };

        public virtual IEnumerable<EFCoreMove> Moves { get; init; } =
            new List<EFCoreMove> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreTypeConfiguration :
        EFCoreRecordConfiguration<EFCoreType>
    {
        public EFCoreTypeConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureKey()
        {
            base.ConfigureKey();
            ConfigureOnForms();
            ConfigureMoves();
        }

        protected internal virtual void ConfigureOnForms() =>
            ConfigureLogged(() => Builder
                .HasMany(type => type.OnForms)
                .WithOne(form => form.Type)
                .HasForeignKey(form => form.TypeId));

        protected internal virtual void ConfigureMoves() =>
            ConfigureLogged(() => Builder
                .HasMany(type => type.Moves)
                .WithOne(move => move.Type)
                .HasForeignKey(move => move.TypeId));
    }
}