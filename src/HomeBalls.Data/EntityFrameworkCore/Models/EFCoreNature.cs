namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreNature :
        EFCoreNamedRecord<Byte>,
        IHomeBallsNature
    {
        #nullable disable
        public EFCoreNature() { }
        #nullable enable

        public virtual Byte DecreasedStatId { get; init; }

        public virtual EFCoreStat DecreasedStat { get; init; }

        public virtual Byte IncreasedStatId { get; init; }

        public virtual EFCoreStat IncreasedStat { get; init; }
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreNatureConfiguration :
        EFCoreRecordConfiguration<EFCoreNature>
    {
        public EFCoreNatureConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureDecreasedStat();
            ConfigureIncreasedStat();
        }

        protected internal virtual void ConfigureDecreasedStat() =>
            ConfigureLogged(() => Builder
                .HasOne(nature => nature.DecreasedStat)
                .WithMany(stat => stat.DecreasedOn)
                .HasForeignKey(nature => nature.DecreasedStatId));

        protected internal virtual void ConfigureIncreasedStat() => 
            ConfigureLogged(() => Builder
                .HasOne(nature => nature.IncreasedStat)
                .WithMany(stat => stat.IncreasedOn)
                .HasForeignKey(nature => nature.IncreasedStatId));
    }
}