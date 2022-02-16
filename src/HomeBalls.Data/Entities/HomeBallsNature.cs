#nullable disable

namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsNature :
        HomeBalls.Entities.HomeBallsNature,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsNature>
    {
        public HomeBallsNature()  =>
            base.Names = new List<HomeBallsString> { };

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsNature);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual HomeBallsStat DecreasedStat { get; init; }

        public virtual HomeBallsStat IncreasedStat { get; init; }

        public virtual HomeBalls.Entities.HomeBallsNature ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsNature>();
    }
}

#nullable enable

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsNatureConfiguration :
        HomeBallsEntityConfiguration<HomeBallsNature>
    {
        public HomeBallsNatureConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureDecreasesStat();
            ConfigureIncreasesStat();
        }

        protected internal virtual void ConfigureDecreasesStat() =>
            Builder.HasOne(nature => nature.DecreasedStat)
                .WithMany(stat => stat.DecreasedOn)
                .HasForeignKey(nature => nature.DecreasedStatId);

        protected internal virtual void ConfigureIncreasesStat() =>
            Builder.HasOne(nature => nature.IncreasedStat)
                .WithMany(stat => stat.IncreasedOn)
                .HasForeignKey(nature => nature.IncreasedStatId);
    }
}