namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsMove :
        HomeBalls.Entities.HomeBallsMove,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsMove>
    {
        public HomeBallsMove() =>
            base.Names = new List<HomeBallsString> { };

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsMove);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual HomeBallsMoveDamageCategory? DamageCategory { get; init; }

        public virtual HomeBallsType? Type { get; init; }

        public virtual HomeBalls.Entities.HomeBallsMove ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsMove>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsMoveConfiguration :
        HomeBallsEntityConfiguration<HomeBallsMove>
    {
        public HomeBallsMoveConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureDamageCategory();
            ConfigureType();
        }

        protected internal virtual void ConfigureDamageCategory() =>
            Builder.HasOne(move => move.DamageCategory)
                .WithMany(category => category.Moves)
                .HasForeignKey(move => move.DamageCategoryId);

        protected internal virtual void ConfigureType() =>
            Builder.HasOne(move => move.Type)
                .WithMany(type => type.Moves)
                .HasForeignKey(move => move.TypeId);
    }
}