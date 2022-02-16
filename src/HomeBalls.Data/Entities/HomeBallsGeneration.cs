namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsGeneration :
        HomeBalls.Entities.HomeBallsGeneration,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsGeneration>
    {
        #nullable disable
        public HomeBallsGeneration() : base()
        {
            base.Names = new List<HomeBallsString> { };
            GameVersions = new List<HomeBallsGameVersion> { };
        }
        #nullable enable

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsGeneration);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual IEnumerable<HomeBallsGameVersion> GameVersions { get; init; }

        public virtual HomeBalls.Entities.HomeBallsGeneration ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsGeneration>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsGenerationConfiguration :
        HomeBallsEntityConfiguration<HomeBallsGeneration>
    {
        public HomeBallsGenerationConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureGameVersions();
        }

        protected internal virtual void ConfigureGameVersions() =>
            Builder.HasMany(generation => generation.GameVersions)
                .WithOne(version => version.Generation)
                .HasForeignKey(version => version.GenerationId);
    }
}