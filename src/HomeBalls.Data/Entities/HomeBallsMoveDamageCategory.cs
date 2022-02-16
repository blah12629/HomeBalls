#nullable disable

namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsMoveDamageCategory :
        HomeBalls.Entities.HomeBallsMoveDamageCategory,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsMoveDamageCategory>
    {
        public HomeBallsMoveDamageCategory()
        {
            base.Names = new List<HomeBallsString> { };
            Moves = new List<HomeBallsMove> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsMoveDamageCategory);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual IEnumerable<HomeBallsMove> Moves { get; init; }

        public virtual HomeBalls.Entities.HomeBallsMoveDamageCategory ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsMoveDamageCategory>();
    }
}

#nullable enable

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsMoveDamageCategoryConfiguration :   
        HomeBallsEntityConfiguration<HomeBallsMoveDamageCategory>
    {
        public HomeBallsMoveDamageCategoryConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureMoves();
        }

        protected internal virtual void ConfigureMoves() =>
            Builder.HasMany(category => category.Moves)
                .WithOne(move => move.DamageCategory)
                .HasForeignKey(move => move.DamageCategoryId);
    }
}