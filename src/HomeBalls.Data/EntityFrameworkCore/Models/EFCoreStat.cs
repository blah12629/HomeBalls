namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreStat :
        EFCoreNamedRecord<Byte>,
        IHomeBallsStat
    {
        public virtual IEnumerable<EFCoreNature> DecreasedOn { get; init; } =
            new List<EFCoreNature> { };

        public virtual IEnumerable<EFCoreNature> IncreasedOn { get; init; } =
            new List<EFCoreNature> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreStatConfiguration :
        EFCoreRecordConfiguration<EFCoreStat>
    {
        public EFCoreStatConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureDecreasedOn();
            ConfigureIncreasedOn();
        }

        protected internal virtual void ConfigureDecreasedOn() =>
            ConfigureLogged(() => Builder
                .HasMany(stat => stat.DecreasedOn)
                .WithOne(nature => nature.DecreasedStat)
                .HasForeignKey(nature => nature.DecreasedStatId));

        protected internal virtual void ConfigureIncreasedOn() =>
            ConfigureLogged(() => Builder
                .HasMany(stat => stat.IncreasedOn)
                .WithOne(nature => nature.IncreasedStat)
                .HasForeignKey(nature => nature.IncreasedStatId));
    }
}