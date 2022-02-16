namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsStat :
        HomeBalls.Entities.HomeBallsStat,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsStat>
    {
        public HomeBallsStat()
        {
            base.Names = new List<HomeBallsString> { };
            DecreasedOn = new List<HomeBallsNature> { };
            IncreasedOn = new List<HomeBallsNature> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsStat);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual IEnumerable<HomeBallsNature> DecreasedOn { get; init; }

        public virtual IEnumerable<HomeBallsNature> IncreasedOn { get; init; }

        public virtual HomeBalls.Entities.HomeBallsStat ToBaseType() =>
            this.Adapt<HomeBalls.Entities.HomeBallsStat>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsStatConfiguration :
        HomeBallsEntityConfiguration<HomeBallsStat>
    {
        public HomeBallsStatConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureDecreasedOn();
            ConfigureIncreasedOn();
        }

        protected internal virtual void ConfigureDecreasedOn() =>
            Builder.HasMany(stat => stat.DecreasedOn)
                .WithOne(nature => nature.DecreasedStat)
                .HasForeignKey(nature => nature.DecreasedStatId);

        protected internal virtual void ConfigureIncreasedOn() =>
            Builder.HasMany(stat => stat.IncreasedOn)
                .WithOne(nature => nature.IncreasedStat)
                .HasForeignKey(nature => nature.IncreasedStatId);
    }
}
