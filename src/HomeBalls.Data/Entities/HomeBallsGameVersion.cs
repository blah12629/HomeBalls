#nullable disable

namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsGameVersion :
        HomeBalls.Entities.HomeBallsGameVersion,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsGameVersion>
    {
        public HomeBallsGameVersion() : base()
        {
            base.Names = new List<HomeBallsString> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsGameVersion);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual HomeBallsGeneration Generation { get; init; }

        public virtual HomeBalls.Entities.HomeBallsGameVersion ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsGameVersion>();
    }
}

#nullable enable

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsGameVersionConfiguration :
        HomeBallsEntityConfiguration<HomeBallsGameVersion>
    {
        public HomeBallsGameVersionConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureGenerations();
        }

        protected internal virtual void ConfigureGenerations() =>
            Builder.HasOne(version => version.Generation)
                .WithMany(generation => generation.GameVersions)
                .HasForeignKey(version => version.GenerationId);
    }
}