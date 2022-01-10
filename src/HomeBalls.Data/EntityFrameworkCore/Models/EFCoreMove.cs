namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreMove :
        EFCoreNamedRecord<UInt16>,
        IHomeBallsMove
    {
        public virtual Byte? DamageCategoryId { get; init; }

        public virtual EFCoreMoveDamageCategory? DamageCategory { get; init; }

        public virtual Byte? TypeId { get; init; }

        public virtual EFCoreType? Type { get; init; }
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreMoveConfiguration :
        EFCoreRecordConfiguration<EFCoreMove>
    {
        public EFCoreMoveConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureDamageCategory();
            ConfigureType();
        }

        protected internal virtual void ConfigureDamageCategory() =>
            ConfigureLogged(() => Builder
                .HasOne(move => move.DamageCategory)
                .WithMany(category => category.Moves)
                .HasForeignKey(move => move.DamageCategoryId));

        protected internal virtual void ConfigureType() =>
            ConfigureLogged(() => Builder
                .HasOne(move => move.Type)
                .WithMany(type => type.Moves)
                .HasForeignKey(move => move.TypeId));
    }
}