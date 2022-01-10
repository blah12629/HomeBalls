namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreMoveDamageCategory :
        EFCoreNamedRecord<Byte>,
        IHomeBallsMoveDamageCategory
    {
        public virtual IEnumerable<EFCoreMove> Moves { get; init; } =
            new List<EFCoreMove> { };
    }

    public class EFCoreMoveDamageCategoryConfiguration :
        EFCoreRecordConfiguration<EFCoreMoveDamageCategory>
    {
        public EFCoreMoveDamageCategoryConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureMoves();
        }

        protected internal virtual void ConfigureMoves() =>
            ConfigureLogged(() => Builder
                .HasMany(category => category.Moves)
                .WithOne(move => move.DamageCategory)
                .HasForeignKey(move => move.DamageCategoryId));
    }
}